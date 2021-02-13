using BonusBot.AudioModule.Extensions;
using BonusBot.AudioModule.LavaLink.Payloads;
using BonusBot.Helper.Events;
using Discord;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.LavaLink.Helpers
{
    internal class SocketHelper : IAsyncDisposable
    {
        public AsyncEvent<SocketHelper>? OnClosed;
        public AsyncEvent<string>? OnMessage;

        private bool _isUseable;
        private TimeSpan _interval;
        private int _reconnectAttempts;
        private ClientWebSocket? _clientWebSocket;
        private readonly Encoding _encoding;
        private readonly Configuration _config;
        private CancellationTokenSource _cancellationTokenSource = new();
        private readonly Func<LogMessage, Task>? _log;

        public SocketHelper(Configuration configuration, Func<LogMessage, Task>? log)
        {
            _log = log;
            _config = configuration;
            _encoding = new UTF8Encoding(false);
            ServicePointManager.ServerCertificateValidationCallback += (_, __, ___, ____) => true;
        }

        public async Task ConnectAsync()
        {
            _clientWebSocket = new ClientWebSocket();
            _clientWebSocket.Options.SetRequestHeader("User-Id", $"{_config.UserId}");
            _clientWebSocket.Options.SetRequestHeader("Num-Shards", $"{_config.Shards}");
            _clientWebSocket.Options.SetRequestHeader("Authorization", _config.Password);
            var url = new Uri($"ws://{_config.Host}:{_config.Port}");

            if (_reconnectAttempts == _config.ReconnectAttempts)
                return;

            try
            {
                _log?.WriteLog(LogSeverity.Info, $"Connecting to {url}.");
                await _clientWebSocket.ConnectAsync(url, CancellationToken.None).ContinueWith(VerifyConnectionAsync);
            }
            catch { }
        }

        public Task SendPayload(BasePayload payload)
        {
            if (!_isUseable || _clientWebSocket is null)
                return Task.CompletedTask;

            var serialize = JsonSerializer.Serialize<object>(payload);
            _log?.WriteLog(LogSeverity.Debug, serialize);
            var seg = new ArraySegment<byte>(_encoding.GetBytes(serialize));
            return _clientWebSocket.SendAsync(seg, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async ValueTask DisposeAsync()
        {
            _isUseable = false;

            if (_clientWebSocket is { })
                await _clientWebSocket
                    .CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed called.", CancellationToken.None)
                    .ConfigureAwait(false);

            try { _cancellationTokenSource?.Cancel(); }
            catch { }

            _clientWebSocket?.Dispose();
            _clientWebSocket = null;
        }

        private async Task VerifyConnectionAsync(Task task)
        {
            if (task.IsCanceled || task.IsFaulted || task.Exception is { })
            {
                _isUseable = false;
                await RetryConnectionAsync().ConfigureAwait(false);
            }
            else
            {
                _log?.WriteLog(LogSeverity.Info, "WebSocket connection established!");
                _isUseable = true;
                _reconnectAttempts = 0;
                _cancellationTokenSource = new CancellationTokenSource();
                await ReceiveAsync(_cancellationTokenSource.Token).ConfigureAwait(false);
            }
        }

        private async Task RetryConnectionAsync()
        {
            try { _cancellationTokenSource?.Cancel(); }
            catch { }

            if (_reconnectAttempts > _config.ReconnectAttempts && _config.ReconnectAttempts != -1)
                return;

            if (_isUseable)
                return;

            _reconnectAttempts++;
            _interval += _config.ReconnectInterval;
            _log?.WriteLog(LogSeverity.Warning,
                _reconnectAttempts == _config.ReconnectAttempts ?
                $"This was the last attempt at re-establishing websocket connection." :
                $"Attempt #{_reconnectAttempts}. Next retry in {_interval.TotalSeconds} seconds.");

            await Task.Delay(_interval).ContinueWith(_ => ConnectAsync()).ConfigureAwait(false);
        }

        private async Task ReceiveAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    byte[] bytes;
                    using var stream = new MemoryStream();
                    var buffer = new byte[_config.BufferSize];
                    var segment = new ArraySegment<byte>(buffer);
                    while (_clientWebSocket?.State == WebSocketState.Open)
                    {
                        var result = await _clientWebSocket.ReceiveAsync(segment, cancellationToken)
                            .ConfigureAwait(false);
                        if (result.MessageType == WebSocketMessageType.Close)
                            if (result.CloseStatus == WebSocketCloseStatus.EndpointUnavailable)
                            {
                                _isUseable = false;
                                await RetryConnectionAsync().ConfigureAwait(false);
                                break;
                            }

                        await stream.WriteAsync(buffer.AsMemory(0, result.Count), cancellationToken).ConfigureAwait(false);
                        if (result.EndOfMessage)
                            break;
                    }

                    bytes = stream.ToArray();

                    if (bytes.Length <= 0)
                        continue;

                    var parse = _encoding.GetString(bytes).Trim('\0');
                    await (OnMessage?.InvokeAsync(parse) ?? Task.CompletedTask);
                }
            }
            catch (Exception ex) when (ex.HResult == -2147467259)
            {
                _isUseable = false;
                await (OnClosed?.InvokeAsync(this) ?? Task.CompletedTask);
                await RetryConnectionAsync().ConfigureAwait(false);
            }
        }
    }
}