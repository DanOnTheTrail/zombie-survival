namespace ZombieSurvival
{
    public class Survivor
    {
        public bool Alive { get => Wounds < 2; }
        public string Name { get; private set; }
        public int Wounds { get; private set; }
        public int Actions { get; } = 3;

        public Survivor(string name)
        {
            Name = name;
        }

        public void Maim(int wounds)
        {
            Wounds += wounds;
        }
    }
}
