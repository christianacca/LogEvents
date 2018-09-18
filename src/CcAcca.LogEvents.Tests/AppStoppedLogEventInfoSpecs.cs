using System;
using NUnit.Framework;

namespace CcAcca.LogEvents.Tests
{
    [TestFixture]
    public class AppStoppedLogEventInfoSpecs
    {
        [Test]
        public void SignalTime_supplied_in_Ctor()
        {
            var signalTime = DateTime.Now.AddMilliseconds(-200);

            var evt = new AppStoppedLogEventInfo(signalTime);

            Assert.That(evt.ShutdownDuration.TotalMilliseconds, Is.EqualTo(200d).Within(3));
        }

        [Test]
        public void No_SignalTime_supplied_in_Ctor()
        {
            var evt = new AppStoppedLogEventInfo();

            Assert.That(evt.ShutdownDuration, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void AppStarted_supplied_in_Ctor()
        {
            var appStarted = DateTime.Now.AddMinutes(-200);

            var evt = new AppStoppedLogEventInfo(appStarted: appStarted);

            Assert.That(evt.TotalRuntime.TotalMinutes, Is.EqualTo(200d).Within(0.01));
        }

        [Test]
        public void No_AppStarted_supplied_in_Ctor()
        {
            var evt = new AppStoppedLogEventInfo();

            Assert.That(evt.TotalRuntime, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void Should_use_prefix()
        {
            var evt = new AppStoppedLogEventInfo();

            Assert.That(evt.Prefix, Is.EqualTo(LogPrefixes.AppEvent));
        }
    }
}