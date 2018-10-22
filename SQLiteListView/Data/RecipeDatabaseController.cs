using SQLite;
using SQLiteListView.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SQLiteListView.Data
{
    public class RecipeDatabaseController
    {
        private static object locker = new object();
        public SQLiteConnection database;

        public RecipeDatabaseController()
        {
            this.database = DependencyService.Get<ISQLite>().GetConnection();
            this.database.CreateTable<Recipe>();
            this.database.CreateTable<Ingredients>();
            this.database.CreateTable<Equipment>();
            this.database.CreateTable<Method>();

        }

        //Recipe CRUD
        public IEnumerable<Recipe> GetRecipe()
        {
            lock (locker)
            {
                if (database.Table<Recipe>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<Recipe>();
                }
            }
        }

        public int SaveRecipe(Recipe recipe)
        {
            lock (locker)
            {
                if (recipe.RecipeID != 0)
                {
                    this.database.Update(recipe);
                    return recipe.RecipeID;
                }
                else
                {
                    return this.database.Insert(recipe);
                }
            }
        }

        public int DeleteRecipe(int Id)
        {
            lock (locker)
            {
                return this.database.Delete<Recipe>(Id);
            }
        }

        // Equipment CRUD 
        public IEnumerable<Equipment> GetEquipment()
        {
            lock (locker)
            {
                if (database.Table<Equipment>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<Equipment>();
                }
            }
        }
        public IEnumerable<Equipment> GetRecipeEquipment(int RecipeID)
        {
            var equipment = GetEquipment();
            lock (locker)
            {
                return equipment.Where(m => m.RecipeId == RecipeID);
            }
        }
        public int SaveEquipment(Equipment equipment)
        {
            lock (locker)
            {
                if (equipment.EquipmentId != 0)
                {
                    this.database.Update(equipment);
                    return equipment.EquipmentId;
                }
                else
                {
                    return this.database.Insert(equipment);
                }
            }
        }

        public int DeleteEquipment(int Id)
        {
            lock (locker)
            {
                return this.database.Delete<Equipment>(Id);
            }
        }

        // Ingredients CRUD 

        public IEnumerable<Ingredients> GetIngredients()
        {
            lock (locker)
            {
                if (database.Table<Ingredients>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<Ingredients>();
                }
            }
        }
        public IEnumerable<Ingredients> GetRecipeIngredients(int RecipeID)
        {
            int recipeID = RecipeID;
            var ingredients = GetIngredients();
            lock (locker)
            {
                return ingredients.Where(m => m.RecipeID == RecipeID);
            }
        }
        public int SaveIngredients(Ingredients ingredients)
        {
            lock (locker)
            {
                if (ingredients.IngredientsId != 0)
                {
                    this.database.Update(ingredients);
                    return ingredients.IngredientsId;
                }
                else
                {
                    return this.database.Insert(ingredients);
                }
            }
        }

        public int DeleteIngredients(int Id)
        {
            lock (locker)
            {
                return this.database.Delete<Ingredients>(Id);
            }
        }

        //Method CRUD
        public IEnumerable<Method> GetMethod()
        {
            lock (locker)
            {
                if (database.Table<Method>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<Method>();
                }
            }
        }

        public IEnumerable<Method> GetRecipeMethod(int RecipeID)
        {
            var Method = GetMethod();
            lock (locker)
            {
                return Method.Where(m => m.RecipeId == RecipeID);
            }
        }

        public int SaveMethod(Method method)
        {
            lock (locker)
            {
                if (method.MethodID != 0)
                {
                    this.database.Update(method);
                    return method.MethodID;
                }
                else
                {
                    return this.database.Insert(method);
                }
            }
        }

        public int DeleteMethod(int Id)
        {
            lock (locker)
            {
                return this.database.Delete<Method>(Id);
            }
        }
    }
}
