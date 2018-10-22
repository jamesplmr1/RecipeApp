using SQLiteListView.Data;
using SQLiteListView.ViewModels;
using SQLiteListView.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace SQLiteListView
{
	public partial class App : Application
	{
        private static RecipeDatabaseController recipeDbcontroller;

        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new RecipeListView());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static RecipeDatabaseController RecipeDbcontroller
        {
            get
            {
                if(recipeDbcontroller == null)
                {
                    recipeDbcontroller = new RecipeDatabaseController();
                }
                return recipeDbcontroller;
            }
        }

    }
}
