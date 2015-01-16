using DAO.Abstract;
using DAO.Connections;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Funcionario
{
    public class FuncionarioDAO : IFuncionario
    {
        public bool Salvar(FuncionarioModel funcionario)
        {
            try
            {
                var sql = @"INSERT INTO Funcionario VALUES(@Nome, @Cpf, @Rg, @DataNascimento,@CarteiraTrabalho,@Serie, @Id_Endereco, @Id_TipoFuncionario, @Id_Contato)";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome", funcionario.Nome);
                comando.Parameters.AddWithValue("@Cpf", funcionario.Cpf);
                comando.Parameters.AddWithValue("@Rg", funcionario.Rg);
                comando.Parameters.AddWithValue("@DataNascimento", funcionario.DataNascimento);
                comando.Parameters.AddWithValue("@CarteiraTrabalho", funcionario.CarteiraTrabalho);
                comando.Parameters.AddWithValue("@Serie", funcionario.Serie);
                comando.Parameters.AddWithValue("@Id_Endereco", funcionario.Endereco.Id);
                comando.Parameters.AddWithValue("@Id_TipoFuncionario", funcionario.Tipo.Id);
                comando.Parameters.AddWithValue("@Id_Contato", funcionario.Contato.Id);

                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "FuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
        public bool Excluir(FuncionarioModel funcionario)
        {
            try
            {
                var sql = @"DELETE Funcionario WHERE Id_Funcionario = @IdFuncionario";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@IdFuncionario", funcionario.Id);
                
                Connection.ExecutarComando(comando);
                return true;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir", "FuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
        public DataTable Pesquisa(FuncionarioModel funcionario)
        {
            try
            {

                var sql = "SELECT * FROM Funcionario WHERE Nome LIKE @Nome OR Id_Funcionario = @Codigo";
                var comando = new SqlCommand(sql, Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome", "%" + funcionario.Nome + "%");
                comando.Parameters.AddWithValue("@Codigo", funcionario.Id);

                var dataTable = Connection.getDataTable(comando);
                var func = CriaTabelaFuncionario();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    func.Rows.Add(
                        dataTable.Rows[i]["Id_funcionario"],
                        dataTable.Rows[i]["Nome"],
                        dataTable.Rows[i]["Cpf"],
                        dataTable.Rows[i]["Rg"],
                        String.Format("{0:dd/MM/yyyy}", dataTable.Rows[i]["DataAdmissao"])
                    );
                }
                return func; 
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa", "FuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }

        private DataTable CriaTabelaFuncionario()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Código",typeof(int));
            dataTable.Columns.Add("Nome", typeof(string));
            dataTable.Columns.Add("Cpf", typeof(string));
            dataTable.Columns.Add("Rg", typeof(string));
            dataTable.Columns.Add("Data de Admissao", typeof(string));

            return dataTable;
        }
    }
}
