using Xunit;
using ZombieSurvival;
using AutoFixture;
using System;
using System.Linq;

namespace ZombieSurvial.Tests
{
    public class GameTests {
        private readonly Fixture fixture;

        public GameTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public void GameStartsWithNoSurvivors(){
            var sut = new Game();

            var result = sut.SurvivorCount;

            Assert.Equal(0, result);
        }

        [Fact]
        public void SurvivorsCanBeFoundInGame(){
            var sut = new Game();
            sut.FoundSurvivor(fixture.Create<string>());

            var result = sut.SurvivorCount;

            Assert.Equal(1, result);
        }

        [Fact]
        public void ThereCanOnlyBeSurvivorWithTheSameName(){
            var sut = new Game();
            sut.FoundSurvivor("foo");
            sut.FoundSurvivor("foo");

            var result = sut.SurvivorCount;

            Assert.Equal(1, result);
        }

        [Fact]
        public void NewGameIsRunningByDefault(){
            var sut = new Game();

            var result = sut.Running;

            Assert.True(result);
        }

        [Fact]
        public void NewGameIsNotRunningWhenAllSurvivorsDie(){
            var sut = new Game();
            sut.FoundSurvivor(fixture.Create<string>());
            var survivor = sut.Survivors.First();
            
            survivor.Maim(2);

            var result = sut.Running;
            Assert.False(result);
        }

        [Fact]
        public void GameRemainsRunningIfThereIsOneSurvivor(){
            var sut = new Game();
            sut.FoundSurvivor(fixture.Create<string>());
            sut.FoundSurvivor(fixture.Create<string>());
            var survivor1 = sut.Survivors.First();
            var survivor2 = sut.Survivors.Last();

            survivor1.Maim(2);

            var result = sut.Running;
            Assert.True(result);
        }

        [Fact]
        public void GameStartsAtLevelBlue(){
            var sut = new Game();

            var result = sut.Level;

            Assert.Equal(Level.Blue, result);
        }

        [Fact]
        public void GameLevelIsBasedOnSoleSurvivorLevel(){
            var sut = new Game();
            sut.FoundSurvivor(fixture.Create<string>());
            var survivor1 = sut.Survivors.First();
            for (int i = 0; i < 6; i++)
            {
                survivor1.Kill();
            }

            var result = sut.Level;

            Assert.Equal(Level.Yellow, result);
        }

        [Fact]
        public void GameLevelIsBasedOnHighestLevelPlayer(){
            var sut = new Game();

            sut.FoundSurvivor(fixture.Create<string>());
            sut.FoundSurvivor(fixture.Create<string>());

            var survivor1 = sut.Survivors.First();
            var survivor2 = sut.Survivors.Last();

            for (int i = 0; i < 10; i++)
            {
                survivor1.Kill();
            }

            for (int i = 0; i < 45; i++)
            {
                survivor2.Kill();
            }

            survivor2.Maim(10);

            var result = sut.Level;
            Assert.Equal(Level.Yellow, result);
        }

        [Fact]
        public void GameHistoryLogsGameStartTime() 
        {
            var sut = new Game();
            
            var result = sut.History.FirstOrDefault();

            Assert.Equal("Game Begin", result.Name);
            Assert.IsType<DateTime>(result.Time);
        }

        [Fact]
        public void GameHistoryLogsWhenPlayerGetsEquipment(){
            var sut = new Game();
            sut.FoundSurvivor(fixture.Create<string>());

            sut.Survivors.First().Hold("bar");

            var result = sut.History.Pop();
            Assert.Equal("Survivor picked up an item", result.Name);
        }
    }

}
