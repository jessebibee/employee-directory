using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Models
{
    //public class Location
    //{
    //    public Location(string city, string state)
    //    {
    //        City = city;
    //        State = state;
    //    }

    //    public string City { get; set; }

    //    public string State { get; set; }

    //    public static Location Austin = new Location("Austin", "TX");
        
    //    public static Location Dallas = new Location("Austin", "TX");
        
    //    public static Location Houston = new Location("Houston", "TX");
    //}

    public enum Location : byte
    {
        Austin = 1,
        Dallas = 2,
        Houston = 3
    }
}