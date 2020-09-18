using System.Collections.Generic;

namespace Data
{
    public partial class CtMarca
    {
        public CtMarca()
        {
            CtProducto = new HashSet<CtProducto>();
        }

        public long PKIdMarca { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<CtProducto> CtProducto { get; set; }
    }
}