using SQLite;

namespace SQLiteListView.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}