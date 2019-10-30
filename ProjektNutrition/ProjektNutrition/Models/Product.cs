//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektNutrition.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Product_FoodDay = new HashSet<Product_FoodDay>();
        }
    
        public int id { get; set; }
        public string Users_id { get; set; }
        public string name { get; set; }
        public double caloric { get; set; }
        public double protein { get; set; }
        public double carb { get; set; }
        public double fat { get; set; }
        public Nullable<double> weight { get; set; }
        public Nullable<double> count { get; set; }
        public int Category_id { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_FoodDay> Product_FoodDay { get; set; }
    }
}