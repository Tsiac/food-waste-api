namespace Food_Waste_API
{
    public class Test
    {

        public class Rootobject
        {
            public int id { get; set; }
            public string username { get; set; }
            public string name { get; set; }
            public bool isComplete { get; set; }
            public bool isVeg { get; set; }
            public bool isMeat { get; set; }
            public bool isFish { get; set; }
            public Dish[] dishes { get; set; }
            public string dateString { get; set; }
        }

        public class Dish
        {
            public string id { get; set; }
            public string name { get; set; }
            public Ingredient[] ingredients { get; set; }
        }

        public class Ingredient
        {
            public string id { get; set; }
            public string name { get; set; }
            public string quantity { get; set; }
            public string counter { get; set; }
        }

    }
}
