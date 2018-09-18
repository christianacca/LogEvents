using System;
using System.Threading;
using NUnit.Framework;

namespace CcAcca.LogEvents.Tests
{
    [TestFixture]
    public class AppHeartbeatLogEventInfoSpecs
    {
        [Test]
        public void Uptime_should_return_elapsed_time_since_event_created()
        {
            var beat = new AppHeartbeatLogEventInfo();
            Assert.That(beat.Uptime, Is.EqualTo(TimeSpan.Zero).Within(1).Milliseconds);
            Thread.Sleep(1000);

            // note: we're rounding to minutes hence the large tolorance level
            Assert.That(beat.Uptime, Is.EqualTo(1000.Milliseconds()).Within(200).Milliseconds);
        }

        [Test]
        public void Uptime_should_be_treated_as_metric()
        {
            var beat = new AppHeartbeatLogEventInfo();
            Assert.That(beat.HasMetric, Is.True);
        }

        [Test]
        public void Uptime_metric_should_be_in_minutes()
        {
            var beat = new AppHeartbeatLogEventInfo();
            Thread.Sleep(1000);
            Assert.That(beat.DynamicMetrics["UptimeMinutes"].Value, Is.EqualTo(0d).Within(0.02));
        }
    }
}