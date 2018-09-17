using NUnit.Framework;

namespace CcAcca.LogEvents.Tests
{
    [TestFixture]
    public class ToString
    {
        [Test]
        public void EventName_Only()
        {
            var evt = new LogEventInfo("Test");
            Assert.That(evt.ToString(), Is.EqualTo("Event: Test"));
        }

        [Test]
        public void EventName_with_prefix()
        {
            var evt = new LogEventInfo("Test", "App");
            Assert.That(evt.ToString(), Is.EqualTo("Event: App.Test"));
        }

        [Test]
        public void Properties_one()
        {
            var evt = new LogEventInfo("Test")
            {
                Properties =
                {
                    ["Prop1"] = "Value 1"
                }
            };
            Assert.That(evt.ToString(), Is.EqualTo("Event: Test |Prop1: Value 1"));
        }

        [Test]
        public void Properties_one_with_prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Properties =
                {
                    ["Prop1"] = "Value 1"
                }
            };
            Assert.That(evt.ToString(), Is.EqualTo("Event: App.Test |App.Prop1: Value 1"));
        }

        [Test]
        public void Properties_multiple()
        {
            var evt = new LogEventInfo("Test")
            {
                Properties =
                {
                    ["Prop1"] = "Value 1",
                    ["Prop2"] = "Value 2"
                }
            };
            Assert.That(evt.ToString(), Is.EqualTo("Event: Test |Prop1: Value 1 |Prop2: Value 2"));
        }

        [Test]
        public void Metrics_one()
        {
            var evt = new LogEventInfo("Test")
            {
                Metrics =
                {
                    ["M1"] = 5
                }
            };
            Assert.That(evt.ToString(), Is.EqualTo("Event: Test |M1: 5"));
        }

        [Test]
        public void Metrics_one_with_prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Metrics =
                {
                    ["M1"] = 5
                }
            };
            Assert.That(evt.ToString(), Is.EqualTo("Event: App.Test |App.M1: 5"));
        }

        [Test]
        public void Metrics_multiple()
        {
            var evt = new LogEventInfo("Test")
            {
                Metrics =
                {
                    ["M1"] = 5,
                    ["M2"] = 1.589
                }
            };
            Assert.That(evt.ToString(), Is.EqualTo("Event: Test |M1: 5 |M2: 1.589"));
        }

        [Test]
        public void Properties_and_Metrics_multiple()
        {
            var evt = new LogEventInfo("Test")
            {
                Metrics =
                {
                    ["M1"] = 5,
                    ["M2"] = 1.589
                },
                Properties =
                {
                    ["Prop1"] = "Value 1",
                    ["Prop2"] = "Value 2"
                }
            };
            Assert.That(evt.ToString(), Is.EqualTo("Event: Test |Prop1: Value 1 |Prop2: Value 2 |M1: 5 |M2: 1.589"));
        }

        [Test]
        public void Properties_and_Metrics_multiple_with_prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Metrics =
                {
                    ["M1"] = 5,
                    ["M2"] = 1.589
                },
                Properties =
                {
                    ["Prop1"] = "Value 1",
                    ["Prop2"] = "Value 2"
                }
            };
            Assert.That(evt.ToString(), Is.EqualTo("Event: App.Test |App.Prop1: Value 1 |App.Prop2: Value 2 |App.M1: 5 |App.M2: 1.589"));
        }
    }
}