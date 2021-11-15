using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSMS.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        [Compare("Password", ErrorMessage = "The Confirm Password not math with Password")]
        public string ConfirmPassword { get; set; }
    }
    public class UserMetaData
    {
        
    }
}