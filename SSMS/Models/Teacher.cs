//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teacher
    {
        public int TeacherId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> MaterialId { get; set; }
        public string Gender { get; set; }
        public string FullNameInArabic { get; set; }
        public string FullNameInEnglish { get; set; }
        public Nullable<int> ClassId { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Material Material { get; set; }
        public virtual User User { get; set; }
    }
}
