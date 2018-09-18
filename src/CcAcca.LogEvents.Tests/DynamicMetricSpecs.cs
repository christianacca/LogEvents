using System;
using NUnit.Framework;

namespace CcAcca.LogEvents.Tests
{
    [TestFixture]
    public class DynamicMetricSpecs
    {
        [Test]
        public void Can_create_with_lambda()
        {
            DateTime startTime = DateTime.Now.AddMilliseconds(-100);
            var metric = DynamicMetric.From(() => (DateTime.Now - startTime).TotalMilliseconds);
            Assert.That(metric.Value, Is.EqualTo(100d).Within(5));
        }

        [Test]
        public void Can_create_from_subclass()
        {
            DateTime startTime = DateTime.Now.AddMilliseconds(-100);
            var metric = new DurationMetric(startTime);
            Assert.That(metric.Value, Is.EqualTo(100d).Within(5));
        }

        private class DurationMetric : DynamicMetric
        {
            public DurationMetric(DateTime? startTime = null)
            {
                StartTime = startTime ?? DateTime.Now;
            }

            private DateTime StartTime { get; }

            public override double Value => (DateTime.Now - StartTime).TotalMilliseconds;
        }
    }
}