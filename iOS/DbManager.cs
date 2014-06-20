using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Orchard.iOS;

[assembly: Dependency(typeof(DbManager))]
namespace Orchard.iOS
{
    public class DbManager : ISQLite
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "database.dat";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }

    }
}

