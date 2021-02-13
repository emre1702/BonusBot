using BonusBot.AudioModule.LavaLink.Models;
using System;
using System.IO;

namespace BonusBot.AudioModule.LavaLink.Helpers
{
    internal class TrackHelper
    {
        public LavaLinkTrack DecodeTrack(string trackString)
        {
            const int trackInfoVersioned = 1;
            var raw = Convert.FromBase64String(trackString);

            var decoded = new LavaLinkTrack
            {
                Hash = trackString
            };

            using var ms = new MemoryStream(raw);
            using var jb = new JavaBinaryHelper(ms);

            var messageHeader = jb.ReadInt32();
            var messageFlags = (int)((messageHeader & 0xC0000000L) >> 30);
            var version = (messageFlags & trackInfoVersioned) != 0 ? jb.ReadSByte() & 0xFF : 1;

            decoded.Info.Title = jb.ReadJavaUtf8();
            decoded.Info.Author = jb.ReadJavaUtf8();
            decoded.Info.TrackLength = jb.ReadInt64();
            decoded.Info.Id = jb.ReadJavaUtf8();
            decoded.Info.IsStream = jb.ReadBoolean();

            var uri = jb.ReadNullableString();
            decoded.Info.Uri = uri != null && version >= 2 ? new Uri(uri) : null;

            return decoded;
        }
    }
}