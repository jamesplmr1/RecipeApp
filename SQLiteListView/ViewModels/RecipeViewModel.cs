using SQLiteListView.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SQLiteListView.ViewModels
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        public List<PickerNo> PickersList { get; set; }
        public ObservableCollection<Equipment> EquipmentList { get; set; }
        public ObservableCollection<Ingredients> IngredientsList { get; set; }
        public ObservableCollection<Method> MethodList { get; set; }
        public ObservableCollection<Ingredients> UpdatedIngredientsList { get; set; }
        public Recipe Recipe { get; set; }

        public RecipeViewModel(Recipe Recipe)
        {
            this.Recipe = Recipe;
            this.EquipmentList = new ObservableCollection<Equipment>();
            this.IngredientsList = new ObservableCollection<Ingredients>();
            this.MethodList = new ObservableCollection<Method>();
            InitEquipment();
            InitIngredients();
            InitMethods();
            PickersList = GetPickers();
            CheckVideo();
        }


        private bool _videoIsVisible { get; set; }

        public bool VideoIsVisible
        {
            get { return _videoIsVisible; }
            set
            {
                _videoIsVisible = value;
                OnPropertyChanged();
            }
        }

        private void CheckVideo()
        {
            if (Recipe.RecipeName != "Scrambled Eggs")
            {
                VideoIsVisible = false;
            }
            else
            {
                VideoIsVisible = true;
            }
        }

        private void InitEquipment()
        {
            var enumarable = App.RecipeDbcontroller.GetRecipeEquipment(Recipe.RecipeID);
            foreach (Equipment e in enumarable)
            {
                EquipmentList.Add(e);
            }
        }

        private void InitIngredients()
        {
           var enumarable = App.RecipeDbcontroller.GetRecipeIngredients(Recipe.RecipeID);
            foreach(Ingredients i in enumarable)
            {
                IngredientsList.Add(i);
            }
            
        }


        // Methods Initiator
        private void InitMethods()
        {
            var enumarable = App.RecipeDbcontroller.GetRecipeMethod(Recipe.RecipeID);
            foreach (Method m in enumarable)
            {
                MethodList.Add(m);
            }

        }

        public List<PickerNo> GetPickers()
        {
            var pickers = new List<PickerNo>()
            {
                new PickerNo{NumberOfPeople = 1},
                new PickerNo{NumberOfPeople = 2},
                new PickerNo{NumberOfPeople = 3},
                new PickerNo{NumberOfPeople = 4},
                new PickerNo{NumberOfPeople = 5},
                new PickerNo{NumberOfPeople = 6},
                new PickerNo{NumberOfPeople = 7},
                new PickerNo{NumberOfPeople = 8},
                new PickerNo{NumberOfPeople = 9},
                new PickerNo{NumberOfPeople = 10},

            };
            return pickers;
        }

        private PickerNo _selectedPicker { get; set; }

        public PickerNo SelectedPicker
        {
            get { return _selectedPicker; }
            set
            {
                _selectedPicker = value;
                IngredientsCalculator();
            }
        }

        void IngredientsCalculator()
        {
            IngredientsList.Clear();
            InitIngredients();
            foreach (Ingredients i in IngredientsList)
            {
                i.Ammount = (i.Ammount / Recipe.Serves) * SelectedPicker.NumberOfPeople;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
