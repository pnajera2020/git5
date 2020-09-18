using System.Collections.Generic;

namespace Data
{
    public partial class CtProducto
    {
        public long PKIdProducto { get; set; }
        public string SKU { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Costo { get; set; }
        public string Imagen { get; set; }
        public long FKIdProveedor { get; set; }
        public long FKIdCategoria { get; set; }
        public long FKIdMarca { get; set; }
        public long FKIdUnidadMedida { get; set; }
        public bool Activo { get; set; }
        public virtual CtCategoria FKIdCategoriaNavigation { get; set; }
        public virtual CtMarca FKIdMarcaNavigation { get; set; }
        public virtual CtProveedor FKIdProveedorNavigation { get; set; }
        public virtual CtUnidadMedida FKIdUnidadMedidaNavigation { get; set; }
    }
}
