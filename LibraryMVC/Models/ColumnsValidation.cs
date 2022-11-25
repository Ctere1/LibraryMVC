using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    [MetadataType(typeof(bookMetadata))]
    public partial class book
    {
        public class bookMetadata
        {
            [DisplayName("ID")]
            public int id { get; set; }
            [DisplayName("Name")]
            public string name { get; set; }
            [DisplayName("Genre")]
            public string genre { get; set; }
            [DisplayName("Language")]
            public string language { get; set; }
            [DisplayName("Publisher Name")]
            public string publisherName { get; set; }
            [DisplayName("Author Name")]
            public string authorName { get; set; }
            [DisplayName("Description")]
            public string description { get; set; }
            [DisplayName("Is Active?")]
            public bool isActive { get; set; }
            [DisplayName("Issued From")]
            public Nullable<System.DateTime> issuedFrom { get; set; }
            [DisplayName("Issued To")]
            public Nullable<System.DateTime> issuedTo { get; set; }
            [DisplayName("Borrowed By")]
            public string borrowedBy { get; set; }
        }


    }

    [MetadataType(typeof(userMetadata))]
    public partial class user
    {
        public class userMetadata
        {
            [DisplayName("ID")]
            public int id { get; set; }
            [DisplayName("Name")]
            public string name { get; set; }
            [DisplayName("Email")]
            public string email { get; set; }
            [DisplayName("Password")]
            public string password { get; set; }
        }
    }
}