using System;
using System.Collections.Generic;
using System.Linq;

namespace ZombieSurvival
{
    public class Survivor
    {
        public Game Game { get; }
        public bool Alive { get => Wounds < 2; }
        public string Name { get; private set; }
        public int Experience { get; private set; } = 0;
        public int Wounds { get; private set; }
        public int Actions { get; } = 3;
        private IList<string> ItemsInHand;
        private IList<string> ItemsInReserve; 
        private int MaxReserveItemsBase;
        public Level Level 
        { 
            get => Experience switch
                {
                    (>= 0) and (<= 5) => Level.Blue,
                    (>= 6) and (<= 17) => Level.Yellow,
                    (>= 18) and (<= 41) => Level.Orange,
                    (>= 42) => Level.Red,
                    (<0) => throw new ArgumentOutOfRangeException(),
                };
        }

        private int MaxReserveItems => MaxReserveItemsBase - Wounds;

        public Survivor(string name, Game game)
        {
            Name = name;
            ItemsInHand = new List<string>();
            ItemsInReserve = new List<string>();
            MaxReserveItemsBase = 3;
            Game = game;
        }

        public void Maim(int wounds)
        {
            Wounds += wounds;

            if ((ItemsInHand.Count + ItemsInReserve.Count) == 5) 
            {
                ItemsInReserve.Remove(ItemsInReserve.Last());
            }
        }

        public void Hold(string item)
        {
            if (ItemsInHand.Count < 2)
            {
                if (Game != null)
                {
                    Game.History.Push(new History() { Name = "Survivor picked up an item", Time = DateTime.Now });
                }

                ItemsInHand.Add(item);
            }
        }

        public IList<string> GetItemsInHand()
        {
            return ItemsInHand;
        }

        public IList<string> GetItemsInReserve()
        {
            return ItemsInReserve;
        }

        public void Stash(string item)
        {
            if (ItemsInReserve.Count < MaxReserveItems)
            {
                ItemsInReserve.Add(item);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Survivor))
            {
                return false;
            }
            return (this.Name == ((Survivor)obj).Name);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public void Kill()
        {
            Experience++;
        }
    }
}
