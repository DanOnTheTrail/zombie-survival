using System;
using System.Collections.Generic;
using System.Linq;

namespace ZombieSurvival
{

    public class Game
    {
        public ISet<Survivor> Survivors { get; }
        public Level Level 
        {
            get 
            {
                if (Survivors.Count == 0) {
                    return Level.Blue;
                } else {
                    return Survivors.OrderBy(s => s.Experience).Last(s => s.Alive).Level;
                }
            }
        }

        public bool Running
        {
            get => Survivors.Count == 0 || Survivors.Any(s => s.Alive);
        }

        public int SurvivorCount { get => Survivors.Count; }
        public Stack<History> History { get; set; }

        public Game()
        {
            Survivors = new HashSet<Survivor>();
            History = new Stack<History>();

            History.Push(new History { Name = "Game Begin", Time = DateTime.Now });
        }

        public void FoundSurvivor(string name)
        {
            var survivor = new Survivor(name, this);
            Survivors.Add(survivor);
            History.Push(new History { Name = HistoryConstants.SurvivorFound, Time = DateTime.Now });
        }
    }
}