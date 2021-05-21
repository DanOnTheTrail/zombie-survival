using Xunit;
using ZombieSurvival;
using AutoFixture;
using System.Linq;

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

            Assert.True(result);
        }

        [Fact]
        public void SurviorIsDeadWithTwoWounds()
        {
            var survivor = new Survivor(fixture.Create<string>());
            survivor.Maim(2);
            
            var result = survivor.Alive;

            Assert.False(result);
        }

        [Fact]
        public void SurviorIsDeadWithThreeWounds()
        {
            var survivor = new Survivor(fixture.Create<string>());
            survivor.Maim(3);
            
            var result = survivor.Alive;

            Assert.False(result);
        }

        [Fact]
        public void SurviorStartsWithThreeActions()
        {
            var survivor = new Survivor(fixture.Create<string>());

            var result = survivor.Actions;

            Assert.Equal(3, result);
        }

        [Fact]
        public void SurvivorCanHoldItemInHand()
        {
            var survivor = new Survivor(fixture.Create<string>());
            survivor.Hold("hammer");

            var result = survivor.GetItemsInHand();

            Assert.Contains("hammer", result);
        }

        [Fact]
        public void SurvivorCanHoldTwoItemsInHand()
        {
            var survivor = new Survivor(fixture.Create<string>());
            survivor.Hold("hammer");
            survivor.Hold("Frying Pan");

            var result = survivor.GetItemsInHand();

            Assert.Contains("hammer", result);
            Assert.Contains("Frying Pan", result);
        }
        
        [Fact]
        public void SurvivorCannotHoldMoreThanTwoItemsInHand()
        {
            var survivor = new Survivor(fixture.Create<string>());
            survivor.Hold("hammer");
            survivor.Hold("Frying Pan");
            survivor.Hold("Can O Beans");

            var result = survivor.GetItemsInHand();

            Assert.Equal(2, result.Count);
            Assert.Contains("hammer", result);
            Assert.Contains("Frying Pan", result);
        }
    }
}
