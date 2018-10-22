using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteListView.Models
{
    public class Equipment
    {
        [PrimaryKey, AutoIncrement]
        public int EquipmentId { get; set; }

        [ForeignKey(typeof(Recipe))]
        public int RecipeId { get; set; }

        // many to one relationship with recipe
        [ManyToOne]
        public Recipe Recipe { get; set; }

        // Other Attributes
        public string EquipmentName { get; set; }

    }
}