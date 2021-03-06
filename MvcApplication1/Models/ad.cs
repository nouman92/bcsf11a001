//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class ad
    {
      
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Ad Titles")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string title { get; set; }
        [Required(ErrorMessage = "Please Enter Ad Description")]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 30)]
        public string description { get; set; }
        public string image { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public bool activity { get; set; }
        public string user { get; set; }
        public Nullable<int> catagory { get; set; }
    
        public virtual user user1 { get; set; }
        public virtual catagory catagory1 { get; set; }
    }
}
