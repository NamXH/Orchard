using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Orchard.Android;

[assembly: Dependency(typeof(DbManager))]
namespace Orchard.Android
{
    public class DbManager : ISQLite
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "database.dat";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}

