using System;
using System.Collections.Generic;
using System.Linq;

namespace ZombieSurvival
{

    public class Game
    {
        private ISet<Survivor> survivors;
        public Level Level 
        {
            get 
            {
                if (survivors.Count == 0) {
                    return Level.Blue;
                } else {
                    return survivors.OrderBy(s => s.Experience).Last(s => s.Alive).Level;
                }
            }
        }

        public bool Running
        {
            get => survivors.Count == 0 || survivors.Any(s => s.Alive);
        }

        public int SurvivorCount { get => survivors.Count; }
        public Stack<History> History { get; set; }

        public Game()
        {
            survivors = new HashSet<Survivor>();
            History = new Stack<History>();

            History.Push(new History { Name = "Game Begin", Time = DateTime.Now });
        }

        public void FoundSurvivor(Survivor survivor)
        {                
            survivors.Add(survivor);
        }
    }
}