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

        [Fact]
        public void SurvivorCanHoldUpToThreeItemsInReserve()
        {
            var sut = new Survivor(fixture.Create<string>());
            var reserveItems = fixture.CreateMany<string>(count: 4);
            
            reserveItems.ToList().ForEach(item => { sut.Stash(item); });

            var result = sut.GetItemsInReserve();
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void SurvivorWithAWoundCanOnlyHoldFourItems(){
            var sut = new Survivor(fixture.Create<string>());
            var handItems = fixture.CreateMany<string>(count: 2);
            handItems.ToList().ForEach(item => { sut.Hold(item); });
            var reserveItems = fixture.CreateMany<string>(count: 3);
            reserveItems.ToList().ForEach(item => { sut.Stash(item); });
            
            sut.Maim(1);

            var result = sut.GetItemsInHand().Count + sut.GetItemsInReserve().Count;
            Assert.Equal(4, result);
        }

        [Fact]
        public void SurvivorWithAWoundCannotPickUpAFifthItem()
        {
            var sut = new Survivor(fixture.Create<string>());
            var handItems = fixture.CreateMany<string>(count: 2);
            handItems.ToList().ForEach(item => { sut.Hold(item); });
            var reserveItems = fixture.CreateMany<string>(count: 3);
            reserveItems.ToList().ForEach(item => { sut.Stash(item); });
            sut.Maim(1);

            sut.Stash(fixture.Create<string>());

            var result = sut.GetItemsInHand().Count + sut.GetItemsInReserve().Count;
            Assert.Equal(4, result);
        }

        [Fact]
        public void WoundedSurvivorCanHoldOneLessItem()
        {
            var sut = new Survivor(fixture.Create<string>());
            var handItems = fixture.CreateMany<string>(count: 2);
            handItems.ToList().ForEach(item => { sut.Hold(item); });
            sut.Stash(fixture.Create<string>()); 
            sut.Maim(1);

            sut.Stash(fixture.Create<string>());

            var result = sut.GetItemsInHand().Count + sut.GetItemsInReserve().Count;
            Assert.Equal(4, result);
        }

        [Fact]
        public void SurvivorStartsWithZeroExperience()
        {
            var sut = new Survivor(fixture.Create<string>());

            var result = sut.Experience;

            Assert.Equal(0,  result);
        }

        [Theory]
        [InlineData(0, Level.Blue)]
        [InlineData(5, Level.Blue)]
        [InlineData(6, Level.Yellow)]
        [InlineData(17, Level.Yellow)]
        [InlineData(18, Level.Orange)]
        [InlineData(41, Level.Orange)]
        [InlineData(42, Level.Red)]
        public void SurvivorGainsALevelWithExperience(int xp, Level level)
        {
            var sut = new Survivor(fixture.Create<string>());

            for (int i = 0; i < xp; i++)
            {
                sut.Kill();
            }

            Assert.Equal(level, sut.Level);
            Assert.Equal(xp, sut.Experience);
        }
    }
}
