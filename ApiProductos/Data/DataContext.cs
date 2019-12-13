using ApiProductos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProductos.Data

{
    public class DataContext : DbContext
    {
        public DbSet<CategoriasModel> Categorias { get; set; }
        public DbSet<ProductosModel> Productos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = ServerName; Initial Catalog = DataBaseName; User Id = UserName; Password = Password");
        }

    }
}
