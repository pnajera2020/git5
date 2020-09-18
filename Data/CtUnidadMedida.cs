using System.Collections.Generic;

namespace Data
{
    public partial class CtUnidadMedida
    {
        public CtUnidadMedida()
        {
            CtProducto = new HashSet<CtProducto>();
        }

        public long PKIdUnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<CtProducto> CtProducto { get; set; }
    }
}