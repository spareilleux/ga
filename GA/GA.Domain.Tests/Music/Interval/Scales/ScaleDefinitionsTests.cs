using GA.Domain.Music.Scales;
using GA.Domain.Music.Scales.Modes;
using NUnit.Framework;

namespace GA.Domain.Tests.Music.Interval.Scales
{
    [TestFixture]
    public class ScaleDefinitionsTests
    {
        [Test]
        public void TestMethod1()
        {
            var dorian = ScaleDefinition.Major[MajorScaleMode.Dorian];
        }
    }
}
