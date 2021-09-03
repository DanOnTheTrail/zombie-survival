using Xunit;
using ZombieSurvival;
using AutoFixture;
using System.Linq;

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
        public void YellowSurvivorsHavePlusOneActionSkill()
        {
            var sut = new SkillTree();

            var result = sut.UnlockedSkills(Level.Yellow);

            Assert.Contains(SkillsConstants.PlusOneAction, result);
        }

        [Theory]
        [InlineData(0, Level.Blue)]
        [InlineData(1, Level.Yellow)]
        [InlineData(2, Level.Orange)]
        [InlineData(3, Level.Red)]
        public void foo(int expectedUnlockedSkillsCount, Level level)
        {
            var sut = new SkillTree();

            var result = sut.UnlockedSkills(level).ToList();

            Assert.Equal(expectedUnlockedSkillsCount, result.Count);
        }
    }
}