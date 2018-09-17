using System;
using NUnit.Framework;

namespace CcAcca.LogEvents.Tests
{
    [TestFixture]
    public class AppStartedLogEventInfoSpecs
    {
        [Test]
        public void StartupTime_supplied_in_Ctor()
        {
            var started = DateTime.Now.AddMilliseconds(-200);

            var evt = new AppStartedLogEventInfo(started);

            Assert.That(evt.Duration.TotalMilliseconds, Is.EqualTo(200d));
        }

        [Test]
        public void No_StartupTime_supplied_in_Ctor()
        {
            var evt = new AppStartedLogEventInfo();

            Assert.That(evt.Duration, Is.EqualTo(TimeSpan.Zero));
        }


        [Test]
        public void Should_use_prefix()
        {
            var evt = new AppStartedLogEventInfo();

            Assert.That(evt.Prefix, Is.EqualTo(LogPrefixes.AppEvent));
        }


        [Test]
        public void Should_use_prefix_in_prop_names()
        {
            var evt = new AppStartedLogEventInfo
            {
                Properties =
                {
                    { "AppDomainId", "1" }
                }
            };

            Assert.That(evt.ToString(), Contains.Substring("App.AppDomainId: 1"));
        }


    }
}