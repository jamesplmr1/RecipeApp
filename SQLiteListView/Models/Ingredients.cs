using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SQLiteListView.Models
{
    public class Ingredients : INotifyPropertyChanged
    {
        // primary key
        [PrimaryKey, AutoIncrement]
        public int IngredientsId { get; set; }

        // other attributes
        private string _ingredientsName { get; set; }
        private double _ammount { get; set; }
        private string _units { get; set; }

        public string IngredientsName
        {
            get { return _ingredientsName; }
            set
            {
                _ingredientsName = value;
                OnPropertyChanged();
            }
        }
        public double Ammount
        {
            get { return _ammount; }
            set
            {
                _ammount = value;
                OnPropertyChanged();
            }
        }


        public string Units
        {
            get { return _units; }
            set
            {
                _units = value;
                OnPropertyChanged();
            }
        }
        public int RecipeID
        {
            get { return _recipeID; }
            set
            {
                _recipeID = value;
                OnPropertyChanged();
            }
        }

        // specify the foreign key
        [ForeignKey(typeof(Recipe))]
        public int _recipeID { get; set; }

        // many to one relationship with recipe
        [ManyToOne]
        public Recipe Recipe { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
