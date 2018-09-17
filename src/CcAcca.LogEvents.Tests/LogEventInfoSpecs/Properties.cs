using System;
using NUnit.Framework;

namespace CcAcca.LogEvents.Tests.LogEventInfoSpecs
{
    [TestFixture]
    public class Properties
    {
        [Test]
        public void Can_add_using_object_initializer_syntax()
        {
            var evt = new LogEventInfo("Test")
            {
                Properties =
                {
                    ["Prop1"] = "Value1"
                }
            };

            Assert.That(evt.Properties["Prop1"], Is.EqualTo("Value1"));
        }

        [Test]
        public void Should_throw_when_add_null_value()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.Properties.Add("Prop1", null);
            });
        }

        [Test]
        public void Should_throw_when_add_whitespace_value()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.Properties.Add("Prop1", "");
            });
        }

        [Test]
        public void Should_throw_when_set_null_value()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.Properties["Prop1"] = null;
            });
        }

        [Test]
        public void Should_throw_when_set_whitespace_value()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.Properties["Prop1"] = "";
            });
        }

        [Test]
        public void Should_throw_when_set_whitespace_key()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.Properties[""] = "Value1";
            });
        }

        [Test]
        public void Should_throw_when_add_whitespace_key()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var evt = new LogEventInfo("Test");
                evt.Properties.Add("", "Value1");
            });
        }
    }
}