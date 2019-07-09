using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ControleEstoque.Models;

namespace ControleEstoque.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ControleEstoque.Models.Fornecedor> Fornecedor { get; set; }
        public DbSet<ControleEstoque.Models.Produto> Produto { get; set; }
        public DbSet<ControleEstoque.Models.Cliente> Cliente { get; set; }
        public DbSet<ControleEstoque.Models.Movimentacao> Movimentacao { get; set; }
    }
}
