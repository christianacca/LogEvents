using System;
using NUnit.Framework;

namespace CcAcca.LogEvents.Tests.LogEventInfoSpecs
{
    [TestFixture]
    public class Metrics
    {
        [Test]
        public void Can_add_using_object_initializer_syntax()
        {
            var evt = new LogEventInfo("Test")
            {
                Metrics =
                {
                    ["M1"] = 1
                }
            };

            Assert.That(evt.Metrics["M1"], Is.EqualTo(1d));
        }

        [Test]
        public void Should_throw_when_set_whitespace_key()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.Metrics[""] = 1;
            });
        }

        [Test]
        public void Should_throw_when_add_whitespace_key()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.Metrics.Add("", 1);
            });
        }
    }
}