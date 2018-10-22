using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQLiteListView.Models;
using SQLiteListView.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SQLiteListView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipePage : ContentPage
	{

        public RecipePage(Recipe Recipe)
        {
            InitializeComponent();
            BindingContext = new RecipeViewModel(Recipe);                
        }

    }
}