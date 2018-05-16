using System;
using SQLite;
namespace XamMedicEvent
{
    public class EventsNoId : Attribute
    {
        [PrimaryKey, AutoIncrement]  //These are attributes that define the property below it
        //public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Hours { get; set; }
        public int Strength { get; set; }
        public int Length { get; set; }
        public string Food { get; set; }

    }


}