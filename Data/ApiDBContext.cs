using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options) { }

        //Creación de tablas
        public DbSet<CtCategoria> CtCategoria { get; set; }
        public DbSet<CtMarca> CtMarca { get; set; }
        public DbSet<CtProducto> CtProducto { get; set; }
        public DbSet<CtProveedor> CtProveedor { get; set; }
        public DbSet<CtUnidadMedida> CtUnidadMedida { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<CtCategoria>(entity =>
            {
                entity.HasKey(e => e.PKIdCategoria);
                entity.ToTable("cat_categoria");
                entity.Property(e => e.PKIdCategoria).HasColumnName("id_categoria");
                entity.Property(e => e.Descripcion).HasColumnName("ds_descripcion");
                entity.Property(e => e.Activo).HasColumnName("b_Activo");

                //entity.HasData(new CtCategoria { PKIdCategoria = 1, Descripcion = "Alimentos", Activo = true });
                //entity.HasData(new CtCategoria { PKIdCategoria = 2, Descripcion = "Ropa", Activo = true });
                //entity.HasData(new CtCategoria { PKIdCategoria = 3, Descripcion = "Juguetes", Activo = true });
                //entity.HasData(new CtCategoria { PKIdCategoria = 4, Descripcion = "Oficina", Activo = true });
                //entity.HasData(new CtCategoria { PKIdCategoria = 5, Descripcion = "Salud", Activo = true });
            });

            modelBuilder.Entity<CtMarca>(entity =>
            {
                entity.HasKey(e => e.PKIdMarca);
                entity.ToTable("cat_marca");
                entity.Property(e => e.PKIdMarca).HasColumnName("id_marca");
                entity.Property(e => e.Descripcion).HasColumnName("ds_descripcion");
                entity.Property(e => e.Activo).HasColumnName("b_activo");
                //entity.HasData(new CtMarca { PKIdMarca = 1, Descripcion = "MARCA 1", Activo = true });
                //entity.HasData(new CtMarca { PKIdMarca = 2, Descripcion = "MARCA 2", Activo = true });
                //entity.HasData(new CtMarca { PKIdMarca = 3, Descripcion = "MARCA 3", Activo = true });
                //entity.HasData(new CtMarca { PKIdMarca = 4, Descripcion = "MARCA 4", Activo = true });
                //entity.HasData(new CtMarca { PKIdMarca = 5, Descripcion = "MARCA 5", Activo = true });
            });

            modelBuilder.Entity<CtProveedor>(entity =>
            {
                entity.HasKey(e => e.PKIdProveedor);
                entity.ToTable("cat_proveedor");
                entity.Property(e => e.PKIdProveedor).HasColumnName("id_proveedor");
                entity.Property(e => e.Descripcion).HasColumnName("ds_descripcion");
                entity.Property(e => e.Activo).HasColumnName("b_activo");
                //entity.HasData(new CtProveedor { PKIdProveedor = 1, Descripcion = "PROVEEDOR 1", Activo = true });
                //entity.HasData(new CtProveedor { PKIdProveedor = 2, Descripcion = "PROVEEDOR 2", Activo = true });
                //entity.HasData(new CtProveedor { PKIdProveedor = 3, Descripcion = "PROVEEDOR 3", Activo = true });
                //entity.HasData(new CtProveedor { PKIdProveedor = 4, Descripcion = "PROVEEDOR 4", Activo = true });
                //entity.HasData(new CtProveedor { PKIdProveedor = 5, Descripcion = "PROVEEDOR 5", Activo = true });
            });

            modelBuilder.Entity<CtUnidadMedida>(entity =>
            {
                entity.HasKey(e => e.PKIdUnidadMedida);
                entity.ToTable("cat_unidad_medida");
                entity.Property(e => e.PKIdUnidadMedida).HasColumnName("id_unidad_medida");
                entity.Property(e => e.Descripcion).HasColumnName("ds_descripcion");
                entity.Property(e => e.Activo).HasColumnName("b_activo");
                //entity.HasData(new CtUnidadMedida { PKIdUnidadMedida = 1, Descripcion = "UNIDAD_MEDIDA 1", Activo = true });
                //entity.HasData(new CtUnidadMedida { PKIdUnidadMedida = 2, Descripcion = "UNIDAD_MEDIDA 2", Activo = true });
                //entity.HasData(new CtUnidadMedida { PKIdUnidadMedida = 3, Descripcion = "UNIDAD_MEDIDA 3", Activo = true });
                //entity.HasData(new CtUnidadMedida { PKIdUnidadMedida = 4, Descripcion = "UNIDAD_MEDIDA 4", Activo = true });
                //entity.HasData(new CtUnidadMedida { PKIdUnidadMedida = 5, Descripcion = "UNIDAD_MEDIDA 5", Activo = true });
            });

            modelBuilder.Entity<CtProducto>(entity =>
            {
                entity.HasKey(e => e.PKIdProducto);
                entity.ToTable("tb_producto");
                entity.Property(e => e.PKIdProducto).HasColumnName("id_producto");
                entity.Property(e => e.SKU).HasColumnName("ds_sku");
                entity.Property(e => e.Descripcion).HasColumnName("ds_descripcion");
                entity.Property(e => e.PrecioVenta).HasColumnName("f_precio_venta");
                entity.Property(e => e.Costo).HasColumnName("f_costo");
                entity.Property(e => e.Imagen).HasColumnName("ds_imagen");
                entity.Property(e => e.FKIdProveedor).HasColumnName("id_proveedor");
                entity.Property(e => e.FKIdCategoria).HasColumnName("id_categoria");
                entity.Property(e => e.FKIdMarca).HasColumnName("id_marca");
                entity.Property(e => e.FKIdUnidadMedida).HasColumnName("id_unidad_medida");
                entity.Property(e => e.Activo).HasColumnName("b_activo");

                entity.HasOne(d => d.FKIdCategoriaNavigation)
                    .WithMany(p => p.CtProducto)
                    .HasForeignKey(d => d.FKIdCategoria)
                    .HasConstraintName("fk_cat_categoria_id_categoria");

                entity.HasOne(d => d.FKIdMarcaNavigation)
                    .WithMany(p => p.CtProducto)
                    .HasForeignKey(d => d.FKIdMarca)
                    .HasConstraintName("fk_cat_marca_id_marca");

                entity.HasOne(d => d.FKIdProveedorNavigation)
                    .WithMany(p => p.CtProducto)
                    .HasForeignKey(d => d.FKIdProveedor)
                    .HasConstraintName("fk_cat_proveedor_id_proveedor");

                entity.HasOne(d => d.FKIdUnidadMedidaNavigation)
                    .WithMany(p => p.CtProducto)
                    .HasForeignKey(d => d.FKIdUnidadMedida)
                    .HasConstraintName("fk_cat_unidad_medida_id_unidad_medida");
            });
        }
    }
}

