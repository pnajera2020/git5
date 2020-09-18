using System.Collections.Generic;

namespace Data
{
    public partial class CtProveedor
    {
        public CtProveedor()
        {
            CtProducto = new HashSet<CtProducto>();
        }

        public long PKIdProveedor { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<CtProducto> CtProducto { get; set; }
    }
}