using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SistemaDeFuncionarios
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model2")
        {
        }

        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>()
                .HasMany(e => e.Funcionario)
                .WithRequired(e => e.Departamento)
                .HasForeignKey(e => e.IdDepartamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Cpf)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.Salario)
                .HasPrecision(19, 4);
        }
    }
}
