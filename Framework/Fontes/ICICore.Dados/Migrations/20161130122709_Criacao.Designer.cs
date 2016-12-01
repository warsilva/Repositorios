using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ICICore.Dados.Contextos;

namespace ICICore.Dados.Migrations
{
    [DbContext(typeof(ContextoPrincipal))]
    [Migration("20161130122709_Criacao")]
    partial class Criacao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ICICore.Entidades.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Perfil");
                });

            modelBuilder.Entity("ICICore.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataAdmissao");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(200)")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int?>("PerfilId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("PerfilId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("ICICore.Entidades.Usuario", b =>
                {
                    b.HasOne("ICICore.Entidades.Perfil", "Perfil")
                        .WithMany()
                        .HasForeignKey("PerfilId");
                });
        }
    }
}
