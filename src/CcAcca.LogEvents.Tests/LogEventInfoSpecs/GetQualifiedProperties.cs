using System.Collections.Generic;
using NUnit.Framework;

namespace CcAcca.LogEvents.Tests.LogEventInfoSpecs
{
    [TestFixture]
    public class GetQualifiedProperties
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
                Properties =
                {
                    ["Prop1"] = "Value1"
                }
            };
            var expected = new Dictionary<string, string> {["Prop1"] = "Value1"};
            Assert.That(evt.GetQualifiedProperties(), Is.EquivalentTo(expected));
        }

        [Test]
        public void One_has_Prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Properties =
                {
                    ["Prop1"] = "Value1"
                }
            };
            var expected = new Dictionary<string, string> {{"App.Prop1", "Value1"}};
            Assert.That(evt.GetQualifiedProperties(), Is.EquivalentTo(expected));
        }

        [Test]
        public void Event_and_property_has_same_Prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Properties =
                {
                    ["App.Prop1"] = "Value1"
                }
            };
            var expected = new Dictionary<string, string> { ["App.Prop1"] = "Value1" };
            Assert.That(evt.GetQualifiedProperties(), Is.EquivalentTo(expected));
        }

        [Test]
        public void Event_and_property_has_different_Prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Properties =
                {
                    ["Props.Prop1"] = "Value1"
                }
            };
            var expected = new Dictionary<string, string> { ["App.Props.Prop1"] = "Value1" };
            Assert.That(evt.GetQualifiedProperties(), Is.EquivalentTo(expected));
        }

        [Test]
        public void Many_has_Prefix()
        {
            var evt = new LogEventInfo("Test", "App")
            {
                Properties =
                {
                    ["Prop1"] = "Value1",
                    ["Prop2"] = "Value2"
                }
            };
            var expected = new Dictionary<string, string>
            {
                ["App.Prop1"] = "Value1",
                ["App.Prop2"] = "Value2"
            };
            Assert.That(evt.GetQualifiedProperties(), Is.EquivalentTo(expected));
        }
    }
}