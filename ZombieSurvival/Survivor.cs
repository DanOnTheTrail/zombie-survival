using System.Collections.Generic;

namespace ZombieSurvival
{
    public class Survivor
    {
        public bool Alive { get => Wounds < 2; }
        public string Name { get; private set; }
        public int Wounds { get; private set; }
        public int Actions { get; } = 3;
        private IList<string> ItemsInHand;

        public Survivor(string name)
        {
            Name = name;
            ItemsInHand = new List<string>();
        }

        public void Maim(int wounds)
        {
            Wounds += wounds;
        }

        public void Hold(string item)
        {
            if (ItemsInHand.Count < 2)
            {
                ItemsInHand.Add(item);
            }
        }

        public IList<string> GetItemsInHand()
        {
            return ItemsInHand;
        }
    }
}
