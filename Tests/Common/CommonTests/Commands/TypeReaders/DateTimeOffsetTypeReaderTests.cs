using BonusBot.Common;
using BonusBot.Common.Commands.TypeReaders;
using BonusBot.Common.Interfaces.Commands;
using BonusBot.Common.Interfaces.Guilds;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Reflection;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace CommonTests.Commands.TypeReaders
{
    public class DateTimeOffsetTypeReaderTests
    {
        private ICustomCommandContext _contextMock;

        [SetUp]
        public void SetUp()
        {
            _contextMock = Substitute.For<ICustomCommandContext>();
        }

        [Test]
        public async Task Convert_UTC_Without_TimeZone_Info_Without_Guild()
        {
            var input = "24.04.2021 15:34";
            var typeReader = new DateTimeOffsetTypeReader();
            var expected = new DateTimeOffset(2021, 4, 24, 15, 34, 0, TimeSpan.Zero);

            var result = await typeReader.ReadAsync(_contextMock, input, null);

            Assert.IsNull(result.ErrorReason);
            Assert.AreEqual(expected, result.BestMatch);
        }

        [Test]
        public async Task Convert_UTC_With_UTC_TimeZone_Info_Without_Guild()
        {
            var input = "24.04.2021 15:34 +0";
            var typeReader = new DateTimeOffsetTypeReader();
            var expected = new DateTimeOffset(2021, 4, 24, 15, 34, 0, TimeSpan.Zero);

            var result = await typeReader.ReadAsync(_contextMock, input, null);

            Assert.IsNull(result.ErrorReason);
            Assert.AreEqual(expected, result.BestMatch);
        }

        [Test]
        public async Task Convert_UTC_With_CET_TimeZone_Info_Without_Guild()
        {
            var input = "24.04.2021 15:34 +2";
            var typeReader = new DateTimeOffsetTypeReader();
            var expected = new DateTimeOffset(2021, 4, 24, 15, 34, 0, TimeSpan.FromHours(2));

            var result = await typeReader.ReadAsync(_contextMock, input, null);

            Assert.IsNull(result.ErrorReason);
            Assert.AreEqual(expected, result.BestMatch);
        }

        [Test]
        public async Task Convert_UTC_With_NotLocal_TimeZone_Info_Without_Guild()
        {
            var input = "24.04.2021 15:34 +5";
            var typeReader = new DateTimeOffsetTypeReader();
            var expected = new DateTimeOffset(2021, 4, 24, 15, 34, 0, TimeSpan.FromHours(5));

            var result = await typeReader.ReadAsync(_contextMock, input, null);

            Assert.IsNull(result.ErrorReason);
            Assert.AreEqual(expected, result.BestMatch);
        }

        [Test]
        public async Task Convert_CET_Without_TimeZone_Info()
        {
            var input = "24.04.2021 15:34";
            var typeReader = new DateTimeOffsetTypeReader();
            SetBonusGuildMock("CET");
            var timeZone = TZConvert.GetTimeZoneInfo("CET");
            var expected = new DateTimeOffset(2021, 4, 24, 15, 34, 0, timeZone.GetUtcOffset(DateTime.Now));

            var result = await typeReader.ReadAsync(_contextMock, input, null);

            Assert.IsNull(result.ErrorReason);
            Assert.AreEqual(expected, result.BestMatch);
        }

        [Test]
        public async Task Convert_CET_With_UTC_TimeZone_Info()
        {
            var input = "24.04.2021 15:34 +0";
            var typeReader = new DateTimeOffsetTypeReader();
            SetBonusGuildMock("CET");
            var timeZone = TZConvert.GetTimeZoneInfo("CET");
            var expected = new DateTimeOffset(2021, 4, 24, 15 + DateTimeOffset.Now.Offset.Hours, 34, 0, timeZone.GetUtcOffset(DateTime.Now));

            var result = await typeReader.ReadAsync(_contextMock, input, null);

            Assert.IsNull(result.ErrorReason);
            Assert.AreEqual(expected, result.BestMatch);
        }

        [Test]
        public async Task Convert_CET_With_CET_TimeZone_Info()
        {
            var input = "24.04.2021 15:34 +2";
            var typeReader = new DateTimeOffsetTypeReader();
            SetBonusGuildMock("CET");
            var timeZone = TZConvert.GetTimeZoneInfo("CET");
            var expected = new DateTimeOffset(2021, 4, 24, 15, 34, 0, timeZone.GetUtcOffset(DateTime.Now));

            var result = await typeReader.ReadAsync(_contextMock, input, null);

            Assert.IsNull(result.ErrorReason);
            Assert.AreEqual(expected, result.BestMatch);
        }

        [Test]
        public async Task Convert_CET_With_NotLocal_TimeZone_Info()
        {
            var input = "24.04.2021 15:34 -7";
            var typeReader = new DateTimeOffsetTypeReader();
            SetBonusGuildMock("America/Whitehorse");
            var timeZone = TZConvert.GetTimeZoneInfo("America/Whitehorse");
            var expected = new DateTimeOffset(2021, 4, 24, 15, 34, 0, timeZone.GetUtcOffset(DateTime.Now));

            var result = await typeReader.ReadAsync(_contextMock, input, null);

            Assert.IsNull(result.ErrorReason);
            Assert.AreEqual(expected, result.BestMatch);
        }

        private void SetBonusGuildMock(string timeZone)
        {
            var bonusGuildMock = Substitute.For<IBonusGuild>();
            var settings = Substitute.For<IGuildSettingsHandler>();
#pragma warning disable CA2012 // Use ValueTasks correctly
            settings.Get<string>(Arg.Any<string>(), CommonSettings.TimeZone).ReturnsForAnyArgs(ValueTask.FromResult(timeZone));
            settings.Get<string>(Arg.Any<Assembly>(), CommonSettings.TimeZone).ReturnsForAnyArgs(ValueTask.FromResult(timeZone));
#pragma warning restore CA2012 // Use ValueTasks correctly
            bonusGuildMock.Settings.ReturnsForAnyArgs(settings);
            _contextMock.BonusGuild.ReturnsForAnyArgs(bonusGuildMock);
        }
    }
}
