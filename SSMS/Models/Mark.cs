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
    
    public partial class Mark
    {
        public int MarkId { get; set; }
        public Nullable<int> StudentId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> MaterialId { get; set; }
        public Nullable<int> Mark1 { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Material Material { get; set; }
        public virtual Student Student { get; set; }
    }
}
