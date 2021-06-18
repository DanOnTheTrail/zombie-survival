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

        [Fact]
        public void SurvirorStartsAtLevelBlue()
        {
            var sut = new Survivor(fixture.Create<string>());

            var result = sut.Level;

            Assert.Equal(Level.Blue,  result);
        }
        [Fact]
        public void foo()
        {
            var sut = new Survivor(fixture.Create<string>());

            var experience = sut.Experience;
            var result = sut.Kill();

            Assert.Equal(experience + 1, result);
        }

    }
}
