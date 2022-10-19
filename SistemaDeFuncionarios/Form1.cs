using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SistemaDeFuncionarios
{
    public partial class Form1 : Form
    {

        private FuncionariosRepository funcionariosRepository;
        private DepartamentosRepository departamentosRepository;
        private int idFunc;
        private int idDep;
        private bool DepCellWasSelected = false;
        private bool FuncCellWasSelected = false;
        public Form1()
        {
            funcionariosRepository = new FuncionariosRepository();
            departamentosRepository = new DepartamentosRepository();
            InitializeComponent();
            comboBoxDep.ResetText();
            comboBoxFunc.ResetText();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'empresaDataSet.Departamento'. Você pode movê-la ou removê-la conforme necessário.
            this.departamentoTableAdapter.Fill(this.empresaDataSet.Departamento);
            // TODO: esta linha de código carrega dados na tabela 'empresaDataSet.Funcionario'. Você pode movê-la ou removê-la conforme necessário.
            this.funcionarioTableAdapter.Fill(this.empresaDataSet.Funcionario);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                Funcionario funcionario = BuildFuncionario();
                Funcionario funcionarioByCpf = funcionariosRepository.FindByCPF(funcionario.Cpf);
                if(funcionarioByCpf != null)
                {
                    MessageBox.Show("Já existe um funcionario com o mesmo CPF cadastrado, por favor insira um diferente.");
                    // ClearFieldsFunc();
                }
                else
                {
                    funcionariosRepository.InsertFuncionario(funcionario);
                    dataGridViewFunc.DataSource = funcionariosRepository.GetFuncionarios().ToList();
                    ClearFieldsFunc();
                    RefreshComboBoxFunc();
                }
                
            }
            

        }

        public void RefreshComboBoxFunc()
        {
            List<Funcionario> list = funcionariosRepository.GetListFuncionarios();
            comboBoxFunc.DataSource = list;
            comboBoxFunc.DisplayMember = "Nome";
            comboBoxFunc.ValueMember = "Id";
        }

        public void RefreshComboBoxDep()
        {
            List<Departamento> list = departamentosRepository.GetDepartamentosList();
            comboBoxDep.DataSource = list;
            comboBoxDep.DisplayMember = "Nome";
            comboBoxDep.ValueMember = "Id";
        }

        private bool ValidateFields()
        {
            if(string.IsNullOrEmpty(textNomeFunc.Text) || 
                string.IsNullOrEmpty(maskedTextCpf.Text) || 
                string.IsNullOrEmpty(maskedTextSalario.Text) || 
                maskedTextSalario.MaskCompleted == false ||
                string.IsNullOrEmpty(datePickerContratacao.Value.ToString()) ||
                comboBoxDep.SelectedValue == null) 
            {
                // mostrar dialog
                MessageBox.Show("Todos os campos são obrigatorios, por favor preencha-os antes de cadastrar um novo Funcionario");
                Console.WriteLine("Todos os campos são obrigatorios, por favor preencha-os antes de cadastrar um novo Funcionario");
                return false;
            } else if(textNomeFunc.Text.Any(char.IsDigit))
            {
                // mostrar dialog
                MessageBox.Show("O campo Nome não pode possuir dígitos, tente novamente");
                Console.WriteLine("O campo Nome não pode possuir dígitos, tente novamente");
                return false;
            } else if(datePickerContratacao.Value.Year < 2000 || datePickerContratacao.Value > DateTime.Today)
            {
                MessageBox.Show("A data de contratação deve ser posterior ao ano 2000 e inferior a data atual");
                return false;
            } else if(maskedTextCpf.Text.Length < 11)
            {
                MessageBox.Show("O campo cpf deve possuir 11 digitos.");
                return false;
            } else
            {
                return true;
            }
        }

        private void ClearFieldsFunc()
        {
            textNomeFunc.Clear();
            maskedTextCpf.Clear();
            maskedTextSalario.Clear();
            datePickerContratacao.ResetText();
        }

        private void ClearFieldsDep()
        {
            textNomeDep.Clear();
        }

        public Funcionario BuildFuncionario()
        {
            Funcionario funcionario = new Funcionario
            {
                Nome = textNomeFunc.Text,
                Cpf = maskedTextCpf.Text,
                Salario = Decimal.Parse(maskedTextSalario.Text),
                DataContratacao = datePickerContratacao.Value,
                IdDepartamento = Int16.Parse(comboBoxDep.SelectedValue.ToString())
            };
            return funcionario;
        }

        public Departamento BuildDepartamento()
        {
            Departamento departamento = new Departamento();
            departamento.Nome = textNomeDep.Text;
            if (comboBoxFunc.SelectedValue != null)
            {
                departamento.idGerente = Int16.Parse(comboBoxFunc.SelectedValue.ToString());
            }
            return departamento;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void splitter3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textNomeDep.Text))
            {
                // mostra dialog
                MessageBox.Show("Insira um nome existente para consultar o departamento.");
                Console.WriteLine("Insira um nome existente para consultar o departamento.");
            } else if (textNomeDep.Text.Any(char.IsDigit))
            {
                // mostrar dialog
                MessageBox.Show("O campo Nome não pode possuir dígitos, tente novamente"); 
            } else
            {
                Departamento departamento = departamentosRepository.FindByNome(textNomeDep.Text);
                if (departamento == null)
                {
                    MessageBox.Show("Departamento não encontrado.");
                } else
                {
                    List<Departamento> listDepartamentosByNome = new List<Departamento>
                {
                    departamento
                };

                    dataGridViewDep.DataSource = listDepartamentosByNome;
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textNomeDep.Text))
            {
                // mostra dialog
                MessageBox.Show("Insira um nome para o departamento.");
                Console.WriteLine("Insira um nome para o departamento.");
            }
            else if (textNomeDep.Text.Any(char.IsDigit))
            {
                // mostrar dialog
                MessageBox.Show("O campo Nome não pode possuir dígitos, tente novamente");
            }
            else
            {
                Departamento departamento = BuildDepartamento();
                departamentosRepository.InsertDepartamento(departamento);
                dataGridViewDep.DataSource = departamentosRepository.GetDepartamentos().ToList();
                ClearFieldsDep();
                RefreshComboBoxDep();
            }
        }

        private void datePickerContratacao_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewDep_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewDep_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDep.Rows[e.RowIndex];
                idDep = Int16.Parse(row.Cells[0].Value.ToString());
                textNomeDep.Text = row.Cells[1].Value.ToString();
                comboBoxFunc.SelectedValue = row.Cells[2].Value.ToString();
                DepCellWasSelected = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(maskedTextCpf.Text))
            {
                // mostra dialog
                MessageBox.Show("Insira um cpf existente para consultar o Funcionario");
                Console.WriteLine("Insira um cpf existente para consultar o Funcionario");
            } else
            {
                Funcionario funcionario = funcionariosRepository.FindByCPF(maskedTextCpf.Text);
                List<Funcionario> listFuncionariosByCpf = new List<Funcionario>
                {
                    funcionario
                };

                dataGridViewFunc.DataSource = listFuncionariosByCpf;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridViewFunc.DataSource = funcionariosRepository.GetFuncionarios().ToList();
            ClearFieldsFunc();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridViewDep.DataSource = departamentosRepository.GetDepartamentos().ToList();
            ClearFieldsDep();
        }

        private void dataGridViewFunc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridViewFunc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewFunc.Rows[e.RowIndex];
                idFunc = Int16.Parse(row.Cells[0].Value.ToString());
                textNomeFunc.Text = row.Cells[1].Value.ToString();
                maskedTextCpf.Text = row.Cells[2].Value.ToString();
                maskedTextSalario.Text = row.Cells[3].Value.ToString();
                datePickerContratacao.Value = (DateTime)row.Cells[4].Value;
                comboBoxDep.SelectedValue = row.Cells[5].Value.ToString();
                FuncCellWasSelected = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(ValidateFields())
            {
                if(FuncCellWasSelected)
                {
                    Funcionario funcionario = BuildFuncionario();
                    funcionario.Id = idFunc;
                    funcionariosRepository.Update(funcionario);
                    dataGridViewFunc.DataSource = funcionariosRepository.GetFuncionarios().ToList();
                    ClearFieldsFunc();
                    RefreshComboBoxFunc();
                } else
                {
                    MessageBox.Show("Selecione um Funcionario para editar.");
                }
                FuncCellWasSelected = false;
                
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // so pode deletar departamento que nao esteja relacionado a nenhum gerente
            if (DepCellWasSelected)
            {
                Departamento departamento = BuildDepartamento();
                departamento.Id = idDep;
                departamentosRepository.Delete(departamento);
                dataGridViewDep.DataSource = departamentosRepository.GetDepartamentos().ToList();
                ClearFieldsDep();
                RefreshComboBoxDep();
            } else
            {
                MessageBox.Show("Selecione um departamento para excluir.");
            }
            DepCellWasSelected = false;
                
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(maskedTextCpf.Text))
            {
                // mostra dialog
                MessageBox.Show("Insira um cpf para excluir o Funcionario");
                Console.WriteLine("Insira um cpf para excluir o Funcionario");
            } else if(FuncCellWasSelected)
            {
                funcionariosRepository.DeleteByCPF(maskedTextCpf.Text);
                dataGridViewFunc.DataSource = funcionariosRepository.GetFuncionarios().ToList();
                dataGridViewDep.DataSource = departamentosRepository.GetDepartamentos().ToList();
                ClearFieldsFunc();
                RefreshComboBoxFunc();
            } else
            {
                MessageBox.Show("Selecione um Funcionario para excluir.");
            }
            FuncCellWasSelected = false;
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textNomeDep.Text))
            {
                MessageBox.Show("Insira um nome para o departamento");
            }
            else if (textNomeDep.Text.Any(char.IsDigit))
            {
                // mostrar dialog
                MessageBox.Show("O campo Nome não pode possuir dígitos, tente novamente");
            }
            else
            {
                if (DepCellWasSelected)
                {
                    Departamento departamento = BuildDepartamento();
                    departamento.Id = idDep;
                    departamentosRepository.Update(departamento);
                    dataGridViewDep.DataSource = departamentosRepository.GetDepartamentos().ToList();
                    ClearFieldsDep();
                    RefreshComboBoxDep();
                }
                else
                {
                    MessageBox.Show("Selecione um departamento para editar");
                }
                DepCellWasSelected = false;

            }
            
        }

        private void maskedTextSalario_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void comboBoxFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
