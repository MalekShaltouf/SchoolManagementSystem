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
    
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.Marks = new HashSet<Mark>();
        }
    
        public int StudentID { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Gender { get; set; }
        public string FullNameInArabic { get; set; }
        public string FullNameInEnglish { get; set; }
        public Nullable<int> Age { get; set; }
    
        public virtual Class Class { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mark> Marks { get; set; }
        public virtual User User { get; set; }
    }
}
