using System;
using Xunit;
using ZombieSurvival;
using AutoFixture;

namespace ZombieSurvial.Tests
{
    public class SurvivorTests
    {
        private readonly Fixture fixture;

        public SurvivorTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public void SurviorHasName()
        {
            var survivor = new Survivor("Bob");

            var result = survivor.Name;

            Assert.Equal("Bob", result);
        }

        [Fact]
        public void SurviorBeginsWithZeroWounds()
        {
            var survivor = new Survivor(fixture.Create<string>());
            
            var result = survivor.Wounds;

            Assert.Equal(0, result);
        }

        [Fact]
        public void SurviorIsAliveWithOneWound()
        {
            var survivor = new Survivor(fixture.Create<string>());
            survivor.Maim(1);
            
            var result = survivor.Alive;

            Assert.Equal(true, result);
        }

        [Fact]
        public void SurviorIsDeadWithTwoWounds()
        {
            var survivor = new Survivor(fixture.Create<string>());
            survivor.Maim(2);
            
            var result = survivor.Alive;

            Assert.Equal(false, result);
        }

        [Fact]
        public void SurviorIsDeadWithThreeWounds()
        {
            var survivor = new Survivor(fixture.Create<string>());
            survivor.Maim(3);
            
            var result = survivor.Alive;

            Assert.Equal(false, result);
        }

        [Fact]
        public void SurviorStartsWithThreeActions()
        {
            var survivor = new Survivor(fixture.Create<string>());

            var result = survivor.Actions;

            Assert.Equal(3, result);
        }
    }
}
