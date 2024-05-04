namespace Food_Waste_API
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public bool IsComplete { get; set; }
        public bool IsFish { get; set; }
        public bool IsMeat { get; set; }
        public bool IsVeg { get; set; }
    }
}
