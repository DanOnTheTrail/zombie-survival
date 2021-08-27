using Xunit;
using ZombieSurvival;
using AutoFixture;

namespace ZombieSurvial.Tests
{
public class SkillTreeTests
    {
        private readonly Fixture fixture;

        public SkillTreeTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public void Foo()
        {
            var sut = new SkillTree();

            var result = sut.PotentialSkills(Level.Yellow);

            Assert.Contains(SkillsConstants.PlusOneAction, result);
        }
    }
}