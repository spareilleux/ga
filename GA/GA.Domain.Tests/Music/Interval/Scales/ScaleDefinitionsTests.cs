using GA.Domain.Music.Intervals.Scales;
using GA.Domain.Music.Intervals.Scales.Modes;
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
