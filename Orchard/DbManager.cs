using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Orchard
{
    public static class DbManager
    {
        static object _locker = new object();

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
            lock (_locker)
            {
                var ret = _db.Table<T>().ToList();
                return ret;
            }
        }

        public static void AddItem<T>(T item) where T : new()
        {
            lock (_locker)
            {
                _db.Insert(item);
            }
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

            var sList = new List<Sprayer>()
            {
                new Sprayer() { Name = "1" },
                new Sprayer() { Name = "2" },
                new Sprayer() { Name = "3" },
            };
            _db.InsertAll(sList);

            var obList = new List<OrchardBlock>()
            {
                new OrchardBlock() { Name = "b1" },
                new OrchardBlock() { Name = "b2" },
                new OrchardBlock() { Name = "b3" },
            };
            _db.InsertAll(obList);
        }
    }
}

