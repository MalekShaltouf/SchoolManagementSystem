using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSMS.Models
{
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student {

    }
    public class StudentMetaData
    {
        //[Remote("IsUserInOneClass","Admin",ErrorMessage = "Cann't Add Student in Multiple Class")]
        //public Nullable<int> UserId { get; set; }
    }
}