using SQLiteListView.Models;
using SQLiteListView.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SQLiteListView.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipeListView : ContentPage
	{
        private ObservableCollection<Recipe> recipe = new ObservableCollection<Recipe>();
        
        public RecipeListView ()
		{
			InitializeComponent();
            BindingContext = new RecipeListViewModel(Navigation);

        }
    }
}