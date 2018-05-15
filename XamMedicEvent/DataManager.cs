﻿using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
namespace XamMedicEvent
{
    public static class DataManager
    {
        public static SQLiteConnection db;
        public static string databasePath;
        public static string databaseName;

        static DataManager()
        {//Set the DB connection string
            databaseName = "EventsDB.db";
            databasePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), databaseName);
            db = new SQLiteConnection(databasePath);
        }

        //https://docs.microsoft.com/en-us/xamarin/android/data-cloud/data-access/using-sqlite-orm
        public static IEnumerable<Event> GetData()
        {
            if (db.Table<Event>().Count() > 0)
            {
                return db.Query<Event>("select * from Events");
            }
            else
            {
              //making some fake items to stop the system from crashing when the DB doesn't connect
                List<Event> fakeitem = new List<Event>();
                //make a single item
                Event item = new Event();
                // item.Id = 1;
                item.Date = DateTime.Now.Date;
                item.Time = DateTime.Now.Date;
                item.Hours = 2;
                item.Strength = 2;
                item.Length = 2;
                item.Food = "Fake DB entry";
                fakeitem.AddRange(new[] { item }); //add it to the fake item list
                return fakeitem;
            }
        }



        public static void AddItem(Event NewEvent)
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

        public static void DeleteItem(int listid)
        {
            // https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_3_using_sqlite_orm/ 
            try
            {
                db.Delete<Event>(listid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Error:" + ex.Message);
            }
        }

        public static void CopyTheDB()
        {
            // Check if your DB has already been extracted. If the file does not exist move it. 
            //WARNING!!!!!!!!!!! If your DB changes from the first time you install it, ie: you change the tables, and you get errors then comment out the if wrapper so that it is FORCED TO UPDATE. 
            //Otherwise you spend hours staring at code that should run OK but the db, that you can’t see inside of on your phone, is diffferent from the db you are coding to.   
            if (!File.Exists(DataManager.databasePath))
            {
                Android.Content.Res.AssetManager Assets = Android.App.Application.Context.Assets;
                using (BinaryReader br = new BinaryReader(Assets.Open(DataManager.databaseName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(DataManager.databasePath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }
    }
}