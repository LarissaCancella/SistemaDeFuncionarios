namespace SistemaDeFuncionarios
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Funcionario")]
    public partial class Funcionario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; }

        [Column(TypeName = "money")]
        public decimal Salario { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataContratacao { get; set; }

        public int IdDepartamento { get; set; }

        public virtual Departamento Departamento { get; set; }
    }
}
