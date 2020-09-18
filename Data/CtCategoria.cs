using System.Collections.Generic;

namespace Data
{
    public partial class CtCategoria
    {
        public CtCategoria()
        {
            CtProducto = new HashSet<CtProducto>();
        }

        public long PKIdCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<CtProducto> CtProducto { get; set; }
    }
}
