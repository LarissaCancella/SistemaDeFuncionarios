using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeFuncionarios
{
    public class DepartamentosRepository
    {
        private Model model;

        public DepartamentosRepository()
        {
            model = new Model();
        }

        public void InsertDepartamento(Departamento departamento)
        {
            model.Departamento.Add(departamento);
            model.SaveChanges();
        }

        public Departamento FindByNome(string nome)
        {
            return model.Departamento.Where(x => x.Nome == nome).FirstOrDefault();
        }

        public void Delete(Departamento departamentoToDelete)
        {
            Departamento departamento = model.Departamento.Where(x => x.Id == departamentoToDelete.Id).FirstOrDefault();
            if(departamento == null)
            {
                MessageBox.Show("Departamento não encontrado.");
            } else if(departamento.idGerente != 0)
            {
                MessageBox.Show("O departamento não pôde ser excluido pois existe um gerente associado.");
            } else
            {
                model.Departamento.Remove(departamento);
                model.SaveChanges();
            }
            
        }

        public void Update(Departamento newDepartamento)
        {
            Departamento departamento = model.Departamento.Where(x => x.Id == newDepartamento.Id).FirstOrDefault();
            departamento.Nome = newDepartamento.Nome;
            departamento.idGerente = newDepartamento.idGerente;
            model.SaveChanges();
        }

        public void DeleteGerentes(List<Departamento> departamentos)
        {
            foreach (var departamento in departamentos)
            {
                departamento.idGerente = 0;
            }        
            model.SaveChanges();
        }

        public IQueryable<Departamento> GetDepartamentos()
        {
            return model.Set<Departamento>();
        }

        public List<Departamento> GetDepartamentosList()
        {
            return model.Departamento.ToList();
        }
    }
}
