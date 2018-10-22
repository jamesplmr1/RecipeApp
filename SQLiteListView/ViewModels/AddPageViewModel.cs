using SQLiteListView.Models;
using SQLiteListView.ViewModels.Commands;
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
    public class AddPageViewModel : INotifyPropertyChanged
    {      
        public AddPageViewModel()
        {
            this.EquipmentList = new ObservableCollection<Equipment>();
            this.IngredientsList = new ObservableCollection<Ingredients>();
            this.MethodList = new ObservableCollection<Method>();
            Equipment = new Equipment();
            Ingredient = new Ingredients();
            Method = new Method();
            GetRecipeID();
            
        }


        private void GetRecipeID()
        {
            Recipe Recipe = new Recipe()
            {
                Equipment = new ObservableCollection<Equipment>(),
                Ingredients = new ObservableCollection<Ingredients>(),
                Method = new ObservableCollection<Method>(),
            };

            App.RecipeDbcontroller.SaveRecipe(Recipe);
            this.Recipe = Recipe;
            Method.MethodNumber = 1;
            
        }


        #region Equipment
        private ObservableCollection<Equipment> _equipmentList { get; set; }
        public ObservableCollection<Equipment> EquipmentList
        {
            get { return _equipmentList; }
            set
            {
                if (_equipmentList != value)
                    _equipmentList = value;
                OnPropertyChanged();
            }
        }

        private Equipment _equipment { get; set; }
        public Equipment Equipment
        {
            get { return _equipment; }
            set
            {
                _equipment = value;
                OnPropertyChanged();
            }
        }
        private string _equipmentMessage { get; set; }

        public string EquipmentMessage
        {
            get { return _equipmentMessage; }
            set
            {
                _equipmentMessage = value;
                OnPropertyChanged();
            }
        }
        private ICommand _saveEquipmentCommand;

        public ICommand SaveEquipmentCommand => _saveEquipmentCommand ?? (_saveEquipmentCommand = new Command(() =>
        {
            Equipment equipment = new Equipment();
            App.RecipeDbcontroller.SaveEquipment(equipment);

            if (Equipment.EquipmentName != null )
            {
                equipment.EquipmentName = Equipment.EquipmentName;
                equipment.Recipe = Recipe;
                equipment.RecipeId = Recipe.RecipeID;
                App.RecipeDbcontroller.SaveEquipment(equipment);
                Recipe.Equipment.Add(equipment);
                EquipmentList.Add(equipment);
            }
            else
            {
                EquipmentMessage = "Please add at least one item of Equipment";
                App.RecipeDbcontroller.DeleteEquipment(Equipment.EquipmentId);
                Recipe.Equipment.Remove(equipment);

            }
        }));

        #endregion

        #region Ingredients
        // Aquire Ingredients from the user and store a new ingredient on a button click

        public ObservableCollection<Ingredients> IngredientsList = new ObservableCollection<Ingredients>();

        private Ingredients _ingredient { get; set; }
        public Ingredients Ingredient
        {
            get { return _ingredient; }
            set
            {
                _ingredient = value;
            }
        }
        
        private string _ingredientMessage { get; set; }

        public string IngredientMessage
        {
            get { return _ingredientMessage; }
            set
            {
                _ingredientMessage = value;
                OnPropertyChanged();
            }
        }
        private string _ammountEntry { get; set; }

        public string AmmountEntry
        {
            get { return _ammountEntry; }
            set
            {
                AmmountEntry = value;
            }
        }

        private ICommand _saveIngredientCommand;

        public ICommand SaveIngredientCommand => _saveIngredientCommand ?? (_saveIngredientCommand = new Command(() =>
        {
            Ingredients ingredient = new Ingredients();
            App.RecipeDbcontroller.SaveIngredients(ingredient);
            try
            {
                if (Ingredient.IngredientsName != null && Ingredient.Ammount != 0)
                {
                    ingredient.IngredientsName = Ingredient.IngredientsName;
                    ingredient.Ammount = Ingredient.Ammount;
                    ingredient.Units = Ingredient.Units;
                    ingredient.Recipe = Recipe;
                    ingredient.RecipeID = Recipe.RecipeID;
                    App.RecipeDbcontroller.SaveIngredients(ingredient);
                    IngredientsList.Add(ingredient);
                    Recipe.Ingredients.Add(ingredient);
                    IngredientMessage = "Your ingredient has been added";
                }
                else if (Ingredient.Ammount != 0)
                {
                    IngredientMessage = "please type in a number";
                    App.RecipeDbcontroller.DeleteIngredients(ingredient.IngredientsId);
                }
                else
                {
                    IngredientMessage = "please add at least one ingredient";
                    App.RecipeDbcontroller.DeleteIngredients(ingredient.IngredientsId);
                }
            }
            catch(Exception ex) 
            {
                IngredientMessage = ex.Message; 
            }
           
        }));

        #endregion

        #region Method
        private ObservableCollection<Method> _methodList { get; set; }
        public ObservableCollection<Method> MethodList
        {
            get { return _methodList; }
            set
            {
                if (_methodList != value)
                    _methodList = value;
                OnPropertyChanged();
            }
        }

        private string _methodMessage { get; set; }

        public string MethodMessage
        {
            get { return _methodMessage; }
            set
            {
                _methodMessage = value;
                OnPropertyChanged();
            }
        }

        private Method _method { get; set; }
        public Method Method
        {
            get { return _method; }
            set
            {
                _method = value;
                OnPropertyChanged();
            }
        }

        private ICommand _saveMethodCommand;

        public ICommand SaveMethodCommand => _saveMethodCommand ?? (_saveMethodCommand = new Command(() =>
        {
            Method method = new Method();
            App.RecipeDbcontroller.SaveMethod(method);

            if (Method.MethodName != null)
            {
                method.MethodNumber = Method.MethodNumber;
                method.MethodName = Method.MethodName;
                method.Recipe = Recipe;
                method.RecipeId = Recipe.RecipeID;
                App.RecipeDbcontroller.SaveMethod(method);
                MethodList.Add(method);
                Recipe.Method.Add(method);
                MethodMessage = "Your Method has been added";
                Method.MethodNumber++;
                Method.MethodName = "";
            }
            else
                MethodMessage = "please write out at least one step on how to make it";
        }));

        #endregion

        #region AddRecipe

        private Recipe _recipe { get; set; }
        public Recipe Recipe
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                OnPropertyChanged();
            }
        }

        private string _recipeMessage { get; set; }
        public string RecipeMessage
        {
            get { return _recipeMessage; }
            set
            {
                _recipeMessage = value;
                OnPropertyChanged();
            }
        }

        private ICommand _saveRecipeCommand;

        public ICommand SaveRecipeCommand => _saveRecipeCommand ?? (_saveRecipeCommand = new Command(() =>
        {
            Recipe recipe = Recipe;
            
            if (Recipe.RecipeName != null && Recipe.Serves != 0 && Recipe.MealType != null)
            {
                Recipe.Ingredients = IngredientsList;
                Recipe.Equipment = EquipmentList;
                Recipe.Method = MethodList;
                App.RecipeDbcontroller.SaveRecipe(Recipe);
                RecipeMessage = "Recipe Added";
            }
            else
            {
                if (Recipe.RecipeName == null)
                    RecipeMessage = "Please Give the Recipe a name";
                if (Recipe.Serves == 0)
                    RecipeMessage += "\r\n" + " " + "please add the number of people it serves";
                if (Recipe.MealType == null)
                    RecipeMessage += "\r\n" + " " + "please tell type in a meal type, is it Breakfast, Lunch, Dinner or a Snack ";
            }
        }));

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

