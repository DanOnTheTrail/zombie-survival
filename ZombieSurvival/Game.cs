using System.Collections.Generic;
using System.Linq;

namespace ZombieSurvival
{

    public class Game
    {
        private ISet<Survivor> survivors;
        public bool Running
        {
            get => survivors.Count == 0 || survivors.Any(s => s.Alive);
        }

        public int SurvivorCount { get => survivors.Count; }

        public Game()
        {
            survivors = new HashSet<Survivor>();
        }

        public void FoundSurvivor(Survivor survivor)
        {                
            survivors.Add(survivor);
        }
    }
}