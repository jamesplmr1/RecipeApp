using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SQLiteListView.Models
{
    public class Method
    {
        [PrimaryKey, AutoIncrement]
        public int MethodID { get; set; }

        // specify the foreign key
        [ForeignKey(typeof(Recipe))]
        public int RecipeId { get; set; }

        // many to one relationship with recipe
        [ManyToOne]
        public Recipe Recipe { get; set; }

        // Other Attributes
        public int MethodNumber { get; set; }

        public string MethodName { get; set; }

    }
}