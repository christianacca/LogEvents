using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CcAcca.LogEvents.Tests.LogEventInfoSpecs
{
    [TestFixture]
    public class DynamicMetrics
    {
        [Test]
        public void Can_add_using_object_initializer_syntax()
        {
            var evt = new LogEventInfo("Test")
            {
                DynamicMetrics =
                {
                    ["M1"] = DynamicMetric.From(() => 1d)
                }
            };

            Assert.That(evt.DynamicMetrics["M1"].Value, Is.EqualTo(1d));
        }

        [Test]
        public void Should_throw_when_set_whitespace_key()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.DynamicMetrics[""] = DynamicMetric.From(() => 1d);
            });
        }

        [Test]
        public void Should_throw_when_add_key_value_pair_with_whitespace_key()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.DynamicMetrics.Add(new KeyValuePair<string, DynamicMetric>("", DynamicMetric.From(() => 1d)));
            });
        }

        [Test]
        public void Should_throw_when_add_whitespace_key()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.DynamicMetrics.Add("", DynamicMetric.From(() => 1d));
            });
        }
    }
}