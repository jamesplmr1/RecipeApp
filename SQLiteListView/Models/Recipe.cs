using SQLite;
using SQLiteListView.FormsVideoLibrary;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace SQLiteListView.Models
{
    public class Recipe : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int RecipeID { get; set; }
        private string recipeName;
        private string videoURL;
        private string imageURL;
        private string prepTime;
        private string cookTime;
        private int serves;
        private string mealType;

        public string RecipeName
        {
            get { return recipeName; }
            set
            {
                recipeName = value;
                OnPropertyChange("recipeName");
            }
        }
        public string VideoURL
        {
            get { return videoURL; }
            set
            {
                videoURL = value;
                OnPropertyChange("videoURL");
            }
        }

        public string ImageURL
        {
            get { return imageURL; }
            set
            {
                imageURL = value;
                OnPropertyChange("ImageURL");
            }
        }
        public string PrepTime
        {
            get { return prepTime; }
            set
            {
                prepTime = value;
                OnPropertyChange("prepTime");
            }
        }
        public string CookTime
        {
            get { return cookTime; }
            set
            {
                cookTime = value;
                OnPropertyChange("cookTime");
            }
        }
        public int Serves
        {
            get { return serves; }
            set
            {
                serves = value;
                OnPropertyChange("serves");
            }
        }
        public string MealType
        {
            get { return mealType; }
            set
            {
                mealType = value;
                OnPropertyChange("mealType");
            }
        }


        // one to many relationship with equipment
        [OneToMany (CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Equipment> Equipment { get; set; }
        
        // one to many relationship with ingredients 
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Ingredients> Ingredients { get; set; }

        // one to many relationship with equipment
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Method> Method { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
