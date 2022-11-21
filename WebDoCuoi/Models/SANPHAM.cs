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
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CTHOADONs = new HashSet<CTHOADON>();
            this.HINHANHs = new HashSet<HINHANH>();
        }
    
        public int MaSP { get; set; }
        public int MaDM { get; set; }
        public int MaNTK { get; set; }
        public string TenSP { get; set; }
        public string MauSac { get; set; }
        public string ChatLieu { get; set; }
        public string Mota { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public Nullable<int> SLton { get; set; }
        public string AnhSP { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHOADON> CTHOADONs { get; set; }
        public virtual DANHMUCSP DANHMUCSP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANH> HINHANHs { get; set; }
        public virtual NTK NTK { get; set; }
    }
}
