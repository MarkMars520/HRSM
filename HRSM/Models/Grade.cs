//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRSM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Grade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grade()
        {
            this.Student = new HashSet<Student>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string MProfession { get; set; }
        public int Lecturer { get; set; }
        public int Counsellor { get; set; }
    
        public virtual Teacher Teacher { get; set; }
        public virtual Teacher Teacher1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Student { get; set; }
    }
}