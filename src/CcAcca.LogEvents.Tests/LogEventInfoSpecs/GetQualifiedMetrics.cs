using System.Collections.Generic;
using NUnit.Framework;

namespace CcAcca.LogEvents.Tests.LogEventInfoSpecs
{
    [TestFixture]
    public class GetQualifiedMetrics
    {
        [Test]
        public void None()
        {
            var evt = new LogEventInfo("Test");
            Assert.That(evt.GetQualifiedMetrics(), Is.Null);
        }

        [Test]
        public void One_no_Prefix()
        {
            var evt = new LogEventInfo("Test")
            {
                Metrics =
                {
                    ["M1"] = 1
                }
            };
            var expected = new Dictionary<string, double> {["M1"] = 1};
            Assert.That(evt.GetQualifiedMetrics(), Is.EquivalentTo(expected));
        }

        [Test]
        public void One_dynamic_no_Prefix()
        {
            var evt = new LogEventInfo("Test")
            {
                DynamicMetrics =
                {
                    ["M1"] = DynamicMetric.From(() => 1)
                }
            };
            var expected = new Dictionary<string, double> {["M1"] = 1};
            Assert.That(evt.GetQualifiedMetrics(), Is.EquivalentTo(expected));
        }

        [Test]
        public void One_dynamic_has_Prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                DynamicMetrics =
                {
                    ["M1"] = DynamicMetric.From(() => 1)
                }
            };
            var expected = new Dictionary<string, double> {["App.M1"] = 1};
            Assert.That(evt.GetQualifiedMetrics(), Is.EquivalentTo(expected));
        }

        [Test]
        public void One_has_Prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Metrics =
                {
                    ["M1"] = 1
                }
            };
            var expected = new Dictionary<string, double> {["App.M1"] = 1};
            Assert.That(evt.GetQualifiedMetrics(), Is.EquivalentTo(expected));
        }

        [Test]
        public void Event_and_metric_has_same_Prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Metrics =
                {
                    ["App.M1"] = 1
                }
            };
            var expected = new Dictionary<string, double> {["App.M1"] = 1};
            Assert.That(evt.GetQualifiedMetrics(), Is.EquivalentTo(expected));
        }

        [Test]
        public void Event_and_metric_has_different_Prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Metrics =
                {
                    ["Metric.M1"] = 1
                }
            };
            var expected = new Dictionary<string, double> {["App.Metric.M1"] = 1};
            Assert.That(evt.GetQualifiedMetrics(), Is.EquivalentTo(expected));
        }

        [Test]
        public void Many_has_Prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Metrics =
                {
                    ["M1"] = 1,
                    ["M2"] = 2
                }
            };
            var expected = new Dictionary<string, double>
            {
                ["App.M1"] = 1,
                ["App.M2"] = 2
            };
            Assert.That(evt.GetQualifiedMetrics(), Is.EquivalentTo(expected));
        }

        [Test]
        public void Many_meric_and_dynamic_metric()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Metrics =
                {
                    ["M1"] = 1,
                    ["M2"] = 2
                },
                DynamicMetrics =
                {
                    ["D1"] = DynamicMetric.From(() => 1),
                    ["D2"] = DynamicMetric.From(() => 2)
                }
            };
            var expected = new Dictionary<string, double>
            {
                ["App.M1"] = 1,
                ["App.M2"] = 2,
                ["App.D1"] = 1,
                ["App.D2"] = 2
            };
            Assert.That(evt.GetQualifiedMetrics(), Is.EquivalentTo(expected));
        }
    }
}