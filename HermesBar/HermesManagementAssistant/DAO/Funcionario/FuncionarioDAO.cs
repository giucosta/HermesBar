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
using DAO.Utils;

namespace DAO.Funcionario
{
    public class FuncionarioDAO : IFuncionario
    {
        public bool Salvar(FuncionarioModel funcionario)
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateDataInsert();
                //var sql = @"INSERT INTO Funcionario VALUES(@Nome, @Cpf, @Rg, @DataNascimento,@CarteiraTrabalho,@Serie, @Id_Endereco, @Id_TipoFuncionario, @Id_Contato,@DataAdmissao)";
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Nome", funcionario.Nome);
                Connection.AddParameter("@Cpf", funcionario.Cpf);
                Connection.AddParameter("@Rg", funcionario.Rg);
                Connection.AddParameter("@DataNascimento", funcionario.DataNascimento);
                Connection.AddParameter("@CarteiraTrabalho", funcionario.CarteiraTrabalho);
                Connection.AddParameter("@Serie", funcionario.Serie);
                Connection.AddParameter("@Id_Endereco", funcionario.Endereco.Id);
                Connection.AddParameter("@Id_TipoFuncionario", funcionario.Tipo.Id);
                Connection.AddParameter("@Id_Contato", funcionario.Contato.Id);
                Connection.AddParameter("@DataAdmissao", funcionario.DataAdmissao);
                
                return Connection.ExecutarComando();
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
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.DeleteFromId();
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Id_Funcionario", funcionario.Id);
                
                return Connection.ExecutarComando();
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
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateSelectAll();
                AO.InsertParameter(ConstantesDAO.WHERE, "Nome", ConstantesDAO.LIKE, "@Nome");
                if (funcionario.Id != 0)
                    AO.InsertParameter(ConstantesDAO.AND, "Id_Funcionario", ConstantesDAO.EQUAL, "@Codigo");

                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Nome", "%" + funcionario.Nome + "%");
                Connection.AddParameter("@Codigo", funcionario.Id);
                
                return Connection.getDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa", "FuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
