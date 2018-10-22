using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteListView.Data;
using SQLiteListView.Droid.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace SQLiteListView.Droid.Data
{
    class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var slqiteFileName = "ItemDataBase";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentPath, slqiteFileName);

            var con = new SQLite.SQLiteConnection(path);
            return con;
        }
    }
}