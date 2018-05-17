using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
namespace XamMedicEvent
{
    public class DataManager
    {
        // public static SQLiteConnection db;
        public string databasePath;
        public string databaseName;

        SQLiteConnection db;

        public DataManager()
        {//Set the DB connection string
            databaseName = "EventsDB.db";
            databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);// Documents folder

            db = new SQLiteConnection(databasePath);

            // Initializes a new instance of the Database. if the database doesn't exist, it will create the database and all the tables.
            db.CreateTable<Events>();
        }

        //https://docs.microsoft.com/en-us/xamarin/android/data-cloud/data-access/using-sqlite-orm
        public IEnumerable<Events> GetItems()
        {
            if (db.Table<Events>().Count() > 0)
            {
                return db.Query<Events>("select * from Events");
            }
            else
            {
                //making some fake items to stop the system from crashing when the DB doesn't connect
                List<Events> fakeitem = new List<Events>();
                //make a single item
                Events item = new Events();
                item = FakeEntry();
                fakeitem.AddRange(new[] { item }); //add it to the fake item list
                return fakeitem;
            }
        }

        public string CountEntries()
        {
            int count = db.Query<Events>("select * from Events").Count;
            if (count > 0)
            {
                return count.ToString();
            }
            else
            {
                return " no entries ";

            }

        }

        public Events FakeEntry()
        {
            Events item = new Events();
            item.Id = 1;
            item.Date = DateTime.Now.Date;
            item.Time = DateTime.Now.Date;
            item.Hours = 2;
            item.Strength = 2;
            item.Length = 2;
            item.Food = "Fake DB entry";
            return item;

        }


        public void AddItem(Events NewEvent)
        {//make sure ID = null
            try
            {
                //  var addThis = new Event() { Title = title, Details = details };
                db.Insert(NewEvent);
            }
            catch (Exception e)
            {
                Console.WriteLine("Add Error:" + e.Message);
            }
        }

        //public static void EditItem(Event EditEvent)
        //{
        //    try
        //    {
        //        // http://stackoverflow.com/questions/14007891/how-are-sqlite-records-updated 
        //        //  var EditThis = new Event() { Title = title, Details = details, Id = listid };
        //        db.Update(EditEvent);
        //        //or this
        //        //db.Execute("UPDATE tblToDoList Set Title = ?, Details =, WHERE ID = ?", title, details, listid);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Update Error:" + e.Message);
        //    }
        //}

        public void DeleteItem(int listid)
        {
            // https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_3_using_sqlite_orm/ 
            try
            {
                db.Delete<Events>(listid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Error:" + ex.Message);
            }
        }

        //public void CopyTheDB()
        //{
        //    // Check if your DB has already been extracted. If the file does not exist move it. 
        //    //WARNING!!!!!!!!!!! If your DB changes from the first time you install it, ie: you change the tables, and you get errors then comment out the if wrapper so that it is FORCED TO UPDATE. 
        //    //Otherwise you spend hours staring at code that should run OK but the db, that you can’t see inside of on your phone, is diffferent from the db you are coding to.   
        //    //if (!File.Exists(DataManager.databasePath))
        //    //{
        //    Android.Content.Res.AssetManager Assets = Android.App.Application.Context.Assets;
        //    using (BinaryReader br = new BinaryReader(Assets.Open(databaseName)))
        //    {
        //        using (BinaryWriter bw = new BinaryWriter(new FileStream(databasePath, FileMode.Create)))
        //        {
        //            byte[] buffer = new byte[2048];
        //            int len = 0;
        //            while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
        //            {
        //                bw.Write(buffer, 0, len);
        //            }
        //        }
        //    }
        //    db = new SQLiteConnection(databasePath);
        //}
    }
}