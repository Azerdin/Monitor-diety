
namespace ProjektNutrition.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System;
    using System.Runtime.Serialization;

    public class DataPoint
    {
        public DataPoint(string label, double y)
        {
            this.Y = y;
            this.Label = label;
            
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}