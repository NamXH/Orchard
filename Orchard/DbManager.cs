using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Orchard
{
    public static class DbManager
    {
        static SQLite.SQLiteConnection _db;


        static DbManager()
        {
            _db = new SQLiteConnection("database.dat");
            CreateDummyData();
        }

        static void CreateTables()
        {
            _db.CreateTable<Sprayer>();
            _db.CreateTable<Operator>();
            _db.CreateTable<OrchardBlock>();
        }

        static void DropTables()
        {
            _db.DropTable<Sprayer>();
            _db.DropTable<Operator>();
            _db.DropTable<OrchardBlock>();
        }

        public static IList<T> GetTable<T>() where T : new()
        {
            var ret = _db.Table<T>().ToList();
            return ret;
        }

        static void CreateDummyData()
        {
            DropTables();
            CreateTables();

            var oList = new List<Operator>()
            {
                new Operator() { Name = "aaa" },
                new Operator() { Name = "bbb" },
                new Operator() { Name = "ccc" }
            };

            _db.InsertAll(oList);
        }
    }
}

