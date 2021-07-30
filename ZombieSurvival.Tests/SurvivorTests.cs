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
            var sut = SutFactory("Bob");

            var result = sut.Name;

            Assert.Equal("Bob", result);
        }

        [Fact]
        public void SurviorBeginsWithZeroWounds()
        {
            var sut = SutFactory();
            
            var result = sut.Wounds;

            Assert.Equal(0, result);
        }

        [Fact]
        public void SurviorIsAliveWithOneWound()
        {
            var sut = SutFactory();
            sut.Maim(1);
            
            var result = sut.Alive;

            Assert.True(result);
        }

        [Fact]
        public void SurviorIsDeadWithTwoWounds()
        {
            var sut = SutFactory();
            sut.Maim(2);
            
            var result = sut.Alive;

            Assert.False(result);
        }

        [Fact]
        public void SurviorIsDeadWithThreeWounds()
        {
            var sut = SutFactory();
            sut.Maim(3);
            
            var result = sut.Alive;

            Assert.False(result);
        }

        [Fact]
        public void SurviorStartsWithThreeActions()
        {
            var sut = SutFactory();

            var result = sut.Actions;

            Assert.Equal(3, result);
        }

        [Fact]
        public void SurvivorCanHoldItemInHand()
        {
            var sut = SutFactory();
            sut.Hold("hammer");

            var result = sut.GetItemsInHand();

            Assert.Contains("hammer", result);
        }

        [Fact]
        public void SurvivorCanHoldTwoItemsInHand()
        {
            var sut = SutFactory();
            sut.Hold("hammer");
            sut.Hold("Frying Pan");

            var result = sut.GetItemsInHand();

            Assert.Contains("hammer", result);
            Assert.Contains("Frying Pan", result);
        }
        
        [Fact]
        public void SurvivorCannotHoldMoreThanTwoItemsInHand()
        {
            var sut = SutFactory();
            sut.Hold("hammer");
            sut.Hold("Frying Pan");
            sut.Hold("Can O Beans");

            var result = sut.GetItemsInHand();

            Assert.Equal(2, result.Count);
            Assert.Contains("hammer", result);
            Assert.Contains("Frying Pan", result);
        }

        [Fact]
        public void SurvivorCanHoldUpToThreeItemsInReserve()
        {
            var sut = SutFactory();
            var reserveItems = fixture.CreateMany<string>(count: 4);
            
            reserveItems.ToList().ForEach(item => { sut.Stash(item); });

            var result = sut.GetItemsInReserve();
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void SurvivorWithAWoundCanOnlyHoldFourItems(){
            var sut = SutFactory();
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
            var sut = SutFactory();
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
            var sut = SutFactory();
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
            var sut = SutFactory();

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
            var sut = SutFactory();

            for (int i = 0; i < xp; i++)
            {
                sut.Kill();
            }

            Assert.Equal(level, sut.Level);
            Assert.Equal(xp, sut.Experience);
        }

        public Survivor SutFactory(string name = null, Game game = null){
            if (string.IsNullOrEmpty(name)) {
                name = fixture.Create<string>();
            }

            if(game == null) {
                game = new Game();
            }

            return new Survivor(name, game);
        }
    }
}
