//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDoCuoi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DANHMUCSP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DANHMUCSP()
        {
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        public int MaDM { get; set; }
        public string TenDM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
