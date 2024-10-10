using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi_C_.ORM;

public partial class WebApiVitorContext : DbContext
{
    public WebApiVitorContext()
    {
    }

    public WebApiVitorContext(DbContextOptions<WebApiVitorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCliente> TbClientes { get; set; }

    public virtual DbSet<TbEndereco> TbEnderecos { get; set; }

    public virtual DbSet<TbProduto> TbProdutos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    public virtual DbSet<TbVenda> TbVendas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB205_15\\SQLEXPRESS;Database=WebApiVitor;User Id=dvj;Password=admin1234;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.ToTable("TB_CLIENTE");

            entity.Property(e => e.DocIdentificacao).HasColumnName("Doc_identificacao");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbEndereco>(entity =>
        {
            entity.ToTable("TB_ENDERECO");

            entity.Property(e => e.Cep)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Cidade)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkCliente).HasColumnName("FK_Cliente");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.N).HasColumnName("N°");
            entity.Property(e => e.Preferencia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PReferencia");

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.TbEnderecos)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_ENDERECO_TB_CLIENTE");
        });

        modelBuilder.Entity<TbProduto>(entity =>
        {
            entity.ToTable("TB_PRODUTO");

            entity.Property(e => e.Nfiscal).HasColumnName("NFiscal");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Preco)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.Senha)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbVenda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TB_VENDA");

            entity.ToTable("TB_VENDAS");

            entity.Property(e => e.FkCliente).HasColumnName("FK_Cliente");
            entity.Property(e => e.FkProduto).HasColumnName("FK_Produto");
            entity.Property(e => e.Valor)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDA_TB_CLIENTE");

            entity.HasOne(d => d.FkProdutoNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.FkProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDA_TB_PRODUTO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
