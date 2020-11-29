using BLL.Models;
using DAL.Repositories.EntityFramework.Configurations;
using DAL.Repositories.EntityFramework.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.EntityFramework
{
    public class WebContext : IdentityDbContext<User>
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<ItemVenda> ItemsVenda { get; set; }

        public DbSet<Venda> Vendas { get; set; }

        public DbSet<TipoUsuario> TipoUsuarios { get; set; }

        public DbSet<EnderecoEntrega> EnderecoEntrega { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<PedidoSituacao> PedidoSituacoes { get; set; }

        public DbSet<Estoque> Estoques { get; set; }

        public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }

        public DbSet<MarcaCartaoCredito> MarcasCartaoCredito { get; set; }

        public DbSet<UsuarioClassificacaoProduto> UsuarioClassificacaoProduto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
