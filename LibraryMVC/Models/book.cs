//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class book
    {
        public int id { get; set; }
        public string name { get; set; }
        public string genre { get; set; }
        public string language { get; set; }
        public string publisherName { get; set; }
        public string authorName { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public Nullable<System.DateTime> issuedFrom { get; set; }
        public Nullable<System.DateTime> issuedTo { get; set; }
        public string borrowedBy { get; set; }
    }
}
