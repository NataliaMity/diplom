//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MityaginaNP.UX.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskProject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskProject()
        {
            this.Documents = new HashSet<Document>();
            this.Notifications = new HashSet<Notification>();
        }
    
        public int TaskID { get; set; }
        public string TaskText { get; set; }
        public int DepartmentID { get; set; }
        public string ProjectID { get; set; }
        public string UserLogin { get; set; }
        public System.DateTime TaskDeadLine { get; set; }
        public System.DateTime TaskStart { get; set; }
        public int StatusId { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
        public virtual TaskStatu TaskStatu { get; set; }
    }
}
