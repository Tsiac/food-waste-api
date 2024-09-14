using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Food_Waste_API
{
    public class Menu
    {
        public Guid id { get; set; }
        public string dateString { get; set; }
        public List<Dish> dishes { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public bool isComplete { get; set; }
        public bool isFish { get; set; }
        public bool isMeat { get; set; }
        public bool isVeg { get; set; }
        public int maxAttendees { get; set; }
        public List<string> attendees { get; set; } = [];

        public List<Comment> comments { get; set; } = [];

    }

    public class Dish
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public List<Ingredient> ingredients { get; set; }
    }

    public class Ingredient
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string quantity { get; set; }
        public string counter { get; set; }
    }
    public class Ingredient2
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string counter { get; set; }
    }

    public class Comment
    {
        public int id { get; set; }
        public string name { get; set; }
        public string comment { get; set; }
        public string dateCreated { get; set; }
    }


    public class Person
    {
        public string id { get; set; }
        public List<Ingredient2> storeCupboard { get; set; }
    }

}
