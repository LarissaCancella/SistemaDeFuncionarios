using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeFuncionarios
{
    public class FuncionariosRepository
    {
        private Model model;

        private DepartamentosRepository departamentosRepository;

        public FuncionariosRepository()
        {
            model = new Model();
            departamentosRepository = new DepartamentosRepository();
        }

        public void InsertFuncionario(Funcionario funcionario)
        {
            model.Funcionario.Add(funcionario);
            model.SaveChanges();
        }

        public Funcionario FindByCPF(string cpf)
        {
            return model.Funcionario.Where(x => x.Cpf == cpf).First();
        }

        public void Update(Funcionario newFuncionario)
        {
            Funcionario funcionario = model.Funcionario.Where(x => x.Id == newFuncionario.Id).First();
            funcionario.Nome = newFuncionario.Nome;
            funcionario.Cpf = newFuncionario.Cpf;
            funcionario.Salario = newFuncionario.Salario;
            funcionario.DataContratacao = newFuncionario.DataContratacao;
            funcionario.Departamento = newFuncionario.Departamento;
            model.SaveChanges();
        }

        public void DeleteByCPF(string cpf)
        {
            Funcionario funcionario = model.Funcionario.Where(x => x.Cpf == cpf).First();
            List<Departamento> departamentos = model.Departamento.Where(x => x.idGerente == funcionario.Id).ToList();
            if(departamentos.Count > 0)
            {
                departamentosRepository.DeleteGerentes(departamentos);
            }
            model.Funcionario.Remove(funcionario);
            model.SaveChanges();
        }

        public IQueryable<Funcionario> GetFuncionarios()
        {
            return model.Set<Funcionario>();
        }

        public List<Funcionario> GetListFuncionarios()
        {
            return model.Funcionario.ToList();
        }

    }
}
