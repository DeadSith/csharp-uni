namespace Lab6
{
    public class Human : IHasName
    {
        public string Name { get; set; }

        public string ParentalName { get; set; }

        public string GetName() => Name;

        public override string ToString() => $"{GetType()}: {Name} {ParentalName}";
    }
}