using Xunit;
using ZombieSurvival;
using AutoFixture;


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
            sut.FoundSurvivor(new Survivor(fixture.Create<string>()));

            var result = sut.SurvivorCount;

            Assert.Equal(1, result);
        }

        [Fact]
        public void ThereCanOnlyBeSurvivorWithTheSameName(){
            var sut = new Game();
            var surviror = fixture.Create<string>();
            sut.FoundSurvivor(new Survivor(surviror));
            sut.FoundSurvivor(new Survivor(surviror));

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
            var survivor = new Survivor(fixture.Create<string>());
            sut.FoundSurvivor(survivor);
            
            survivor.Maim(2);

            var result = sut.Running;
            Assert.False(result);
        }

        [Fact]
        public void GameRemainsRunningIfThereIsOneSurvivor(){
            var sut = new Game();
            var survivor1 = new Survivor(fixture.Create<string>());
            var survivor2 = new Survivor(fixture.Create<string>());
            sut.FoundSurvivor(survivor1);
            sut.FoundSurvivor(survivor2);

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
            var survivor1 = new Survivor(fixture.Create<string>());
            sut.FoundSurvivor(survivor1);
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
            var survivor1 = new Survivor(fixture.Create<string>());
            var survivor2 = new Survivor(fixture.Create<string>());
            var survivor3 = new Survivor(fixture.Create<string>());

            sut.FoundSurvivor(survivor1);
            sut.FoundSurvivor(survivor2);
            sut.FoundSurvivor(survivor3);

            for (int i = 0; i < 10; i++)
            {
                survivor1.Kill();
            }

            for (int i = 0; i < 45; i++)
            {
                survivor3.Kill();
            }

            survivor3.Maim(10);

            var result = sut.Level;
            Assert.Equal(Level.Yellow, result);
        }
    }

}
