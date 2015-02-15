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
using DAO;

namespace DAO.Funcionario
{
    public class FuncionarioDAO : IFuncionario
    {
        public bool Salvar(FuncionarioModel funcionario)
        {
            try
            {
                var sql = @"INSERT INTO Funcionario VALUES(@Nome, @Cpf, @Rg, @DataNascimento,@CarteiraTrabalho,@Serie, @Id_Endereco, @Id_TipoFuncionario, @Id_Contato,@DataAdmissao)";
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
                comando.Parameters.AddWithValue("@DataAdmissao",funcionario.DataAdmissao);

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
                var stb = new StringBuilder();
                stb.Append("SELECT * FROM FUNCIONARIO ");
                stb.Append("WHERE NOME LIKE @Nome ");
                if (funcionario.Id != 0)
                    stb.Append("AND Id_Funcionario = @Codigo");

                var comando = new SqlCommand(stb.ToString(), Connection.GetConnection());
                comando.Parameters.AddWithValue("@Nome", "%" + funcionario.Nome + "%");
                comando.Parameters.AddWithValue("@Codigo", funcionario.Id);
                
                return Connection.getDataTable(comando);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa", "FuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
