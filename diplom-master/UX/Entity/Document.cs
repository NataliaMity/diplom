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
    
    public partial class Document
    {
        public int DocumentID { get; set; }
        public byte[] DocumentSource { get; set; }
        public string ProjectID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
    
        public virtual Project Project { get; set; }
    }
}
