//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetoBudget.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class naturezaGasto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public naturezaGasto()
        {
            this.itensOrcamentarios = new HashSet<itensOrcamentarios>();
        }
    
        public int idNaturezaGasto { get; set; }
        public string nomeNatureza { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<itensOrcamentarios> itensOrcamentarios { get; set; }
    }
}
