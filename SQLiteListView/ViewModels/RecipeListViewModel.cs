using SQLiteListView.Models;
using SQLiteListView.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SQLiteListView.ViewModels
{
    public class RecipeListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public INavigation Navigation { get; internal set; }
        public ICommand NewAddPage { get; protected set; }

        private Recipe _recipe { get; set; }

        public Recipe Recipe
        {
            get { return _recipe; }
            set
            {
                if (_recipe != null)
                    _recipe = value;
                OnPropertyChanged();
            }
        }


        public RecipeListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.Recipes = new ObservableCollection<Recipe>();
            NewAddPage = new Command(async () => await CreateNewAddPage());
            Init();
        }


        // Gets all recipes from the database and adds them to the observable collection
        private void Init()
        {
            var enumarator = App.RecipeDbcontroller.GetRecipe();
            if (enumarator == null)
            {
                App.RecipeDbcontroller.SaveRecipe(new Recipe { RecipeName = "Moussaka", Serves = 6, PrepTime = "30", CookTime = "2 Hours", MealType = "Dinner" });

                enumarator = App.RecipeDbcontroller.GetRecipe();
            }
            foreach(Recipe r in enumarator)
            {
                //cleans database of all empty records
                if (r.RecipeName == null || r.CookTime == null)
                {
                    App.RecipeDbcontroller.DeleteRecipe(r.RecipeID);
                }
                else
                {
                    Recipes.Add(r);
                }                
            }
        }

        //navigates to a new add page
        public async Task CreateNewAddPage()
        {
            await Navigation.PushAsync(new AddPage());
        }


        // Get's details of selected recipe and creates a new recipe page.
        private Recipe _selectedRecipe { get; set; }
        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                if (_selectedRecipe != value)
                {
                    _selectedRecipe = value;
                    Navigation.PushAsync(new RecipePage(SelectedRecipe));
                }
            }
        }

        private ICommand _deleteCommand { get; set; }

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(() =>
        {

        }));
 

       private ICommand _searchCommand;

        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command<string>((text) =>
        {
            if (text.Length >= 1)
            {
                Recipes.Clear();
                Init();
                var suggestions = Recipes.Where(c => c.RecipeName.ToLower().StartsWith(text.ToLower())).ToList();
                Recipes.Clear();
                foreach (var recipe in suggestions)
                    Recipes.Add(recipe);

            }
            else
            {
                Recipes.Clear();
                Init();
            }

        }));

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}

