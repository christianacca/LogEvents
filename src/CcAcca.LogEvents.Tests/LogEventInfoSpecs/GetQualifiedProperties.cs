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
        public void Properties_one_no_Prefix()
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
        public void Properties_one_has_Prefix()
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
        public void Properties_many_has_Prefix()
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