namespace BonusBot.TDSConnectorServerModule.Services
{
    public class SupportRequestService : SupportRequest.SupportRequestBase
    {
        /*public override async Task<SupportRequestCreateReply> Create(SupportRequestCreateRequest request, ServerCallContext context)
        {
            try
            {
                var client = await _socketClientHandler.ClientSource.Task;

                var guild = client.GetGuild(request.GuildId);
                if (guild is null)
                    return new SupportRequestCreateReply
                    {
                        ErrorMessage = $"The guild with Id {request.GuildId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
                        CreatedChannelId = 0,
                        ErrorType = string.Empty
                    };

                var user = guild.GetUser(request.UserId);
                if (user is null)
                    return new SupportRequestCreateReply
                    {
                        ErrorMessage = $"The user with Id {request.UserId} does not exist.",
                        ErrorStackTrace = Environment.StackTrace,
                        CreatedChannelId = 0,
                        ErrorType = string.Empty
                    };

                await Main.ServiceProvider.GetRequiredService<SupportRequestHandler>()
                    .CreateRequest(guild, user, request.AuthorName, request.Title, request.Text, (SupportType)request.SupportType, request.AtLeastAdminLevel, false);

                return new SupportRequestCreateReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    CreatedChannelId = 0,
                    ErrorType = string.Empty
                };
            }
            catch (Exception ex)
            {
                var baseEx = ex.GetBaseException();
                return new SupportRequestCreateReply
                {
                    ErrorMessage = baseEx.Message,
                    ErrorStackTrace = ex.StackTrace ?? Environment.StackTrace,
                    CreatedChannelId = 0,
                    ErrorType = ex.GetType().Name + "|" + baseEx.GetType().Name
                };
            }
        }

        public override async Task<SupportRequestReply> Answer(SupportRequestAnswerRequest request, ServerCallContext context)
        {
            try
            {
                var client = await _socketClientHandler.ClientSource.Task;

                var guild = client.GetGuild(request.GuildId);
                if (guild is null)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                var guildEntity = Main.ServiceProvider.GetRequiredService<DatabaseHandler>()
                    .Get<GuildEntity>(guild.Id);
                if (guildEntity is null)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                var categoryId = guildEntity.SupportRequestCategoryId;
                if (categoryId == 0)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };
                var supportRequestCategory = guild.GetCategoryChannel(categoryId);
                if (supportRequestCategory is null)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                var channel = supportRequestCategory.Channels
                    .OfType<SocketTextChannel>()
                    .FirstOrDefault(c => c.Name.EndsWith("_" + request.SupportRequestId));
                if (channel is null)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                var supportRequestHandler = Main.ServiceProvider.GetRequiredService<SupportRequestHandler>();
                if (channel.Name.StartsWith("closed-"))
                    await supportRequestHandler.ToggleClosedRequest(channel, null, request.AuthorName, false, false);

                request.Text = $"Answer from {request.AuthorName}:\n" + request.Text;

                await supportRequestHandler.AnswerRequest(channel, null, request.AuthorName, request.Text, false);

                return new SupportRequestReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    ErrorType = string.Empty
                };
            }
            catch (Exception ex)
            {
                var baseEx = ex.GetBaseException();
                return new SupportRequestReply
                {
                    ErrorMessage = baseEx.Message,
                    ErrorStackTrace = ex.StackTrace ?? Environment.StackTrace,
                    ErrorType = ex.GetType().Name + "|" + baseEx.GetType().Name
                };
            }
        }

        public override async Task<SupportRequestReply> ToggleClosed(SupportRequestToggleClosedRequest request, ServerCallContext context)
        {
            try
            {
                var client = await _socketClientHandler.ClientSource.Task;

                var guild = client.GetGuild(request.GuildId);
                if (guild is null)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                var guildEntity = Main.ServiceProvider.GetRequiredService<DatabaseHandler>()
                    .Get<GuildEntity>(guild.Id);
                if (guildEntity is null)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                var categoryId = guildEntity.SupportRequestCategoryId;
                if (categoryId == 0)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };
                var supportRequestCategory = guild.GetCategoryChannel(categoryId);
                if (supportRequestCategory is null)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                var channel = supportRequestCategory.Channels
                    .OfType<SocketTextChannel>()
                    .FirstOrDefault(c => c.Name.EndsWith("_" + request.SupportRequestId));
                if (channel is null)
                    return new SupportRequestReply
                    {
                        ErrorMessage = string.Empty,
                        ErrorStackTrace = string.Empty,
                        ErrorType = string.Empty
                    };

                await Main.ServiceProvider.GetRequiredService<SupportRequestHandler>()
                    .ToggleClosedRequest(channel, null, request.RequesterName, request.Closed, false);

                return new SupportRequestReply
                {
                    ErrorMessage = string.Empty,
                    ErrorStackTrace = string.Empty,
                    ErrorType = string.Empty
                };
            }
            catch (Exception ex)
            {
                var baseEx = ex.GetBaseException();
                return new SupportRequestReply
                {
                    ErrorMessage = baseEx.Message,
                    ErrorStackTrace = ex.StackTrace ?? Environment.StackTrace,
                    ErrorType = ex.GetType().Name + "|" + baseEx.GetType().Name
                };
            }
        }

        private string GetUniversalDateTimeString(DateTimeOffset dateTime)
        {
            var enUsCulture = CultureInfo.CreateSpecificCulture("en-US");
            return dateTime.ToString("f", enUsCulture) + " +00:00";
        }*/
    }
}