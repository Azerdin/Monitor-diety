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
    
    public partial class Post
    {
        public int id { get; set; }
        public string Users_id { get; set; }
        public string topic { get; set; }
        public string content { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
