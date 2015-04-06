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
using UTIL;
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
                AO.GetCommand();
                AO.InsertParameter("Nome", funcionario.Nome);
                AO.InsertParameter("Cpf", funcionario.Cpf);
                AO.InsertParameter("Rg", funcionario.Rg);
                AO.InsertParameter("DataNascimento", funcionario.DataNascimento);
                AO.InsertParameter("CarteiraTrabalho", funcionario.CarteiraTrabalho);
                AO.InsertParameter("Serie", funcionario.Serie);
                AO.InsertParameter("DataAdmissao", funcionario.DataAdmissao);
                AO.InsertParameter("Endereco", funcionario.Endereco.Id);
                AO.InsertParameter("Tipo", funcionario.Tipo.Id);
                AO.InsertParameter("Contato", funcionario.Contato.Id);
                
                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "FuncionarioDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public bool Excluir(FuncionarioModel funcionario)
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.DeleteFromId();
                AO.GetCommand();
                AO.InsertParameter("Id_Funcionario",funcionario.Id);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir", "FuncionarioDAO", e.Message, Constantes.ATipoMetodo.DELETE);
                throw e;
            }
        }
        public bool Editar(FuncionarioModel funcionario)
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateSpecificQuery(@"UPDATE Funcionario SET 
                                                    Nome = @Nome,
                                                    Cpf = @Cpf,
                                                    Rg = @Rg,
                                                    DataNascimento = @DataNascimento,
                                                    CarteiraTrabalho = @CarteiraTrabalho,
                                                    Serie = @Serie,
                                                    DataAdmissao = @DataAdmissao");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Funcionario", ConstantesDAO.EQUAL, funcionario.Id);
                AO.InsertParameter("Nome", funcionario.Nome);
                AO.InsertParameter("Cpf", funcionario.Cpf);
                AO.InsertParameter("Rg", funcionario.Rg);
                AO.InsertParameter("DataNascimento", funcionario.DataNascimento);
                AO.InsertParameter("CarteiraTrabalho", funcionario.CarteiraTrabalho);
                AO.InsertParameter("Serie", funcionario.Serie);
                AO.InsertParameter("DataAdmissao", funcionario.DataAdmissao);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Editar","FuncionarioDAO",e.Message,Constantes.ATipoMetodo.UPDATE);
                throw e;
            }
        }
        public DataTable Pesquisa(FuncionarioModel func)
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                if (func.Id != 0)
                {
                    AO.InsertParameter(ConstantesDAO.WHERE, "Id_Funcionario", ConstantesDAO.EQUAL, func.Id);
                    AO.InsertParameter(ConstantesDAO.AND, "Nome", ConstantesDAO.LIKE, func.Nome);
                }else
                    AO.InsertParameter(ConstantesDAO.WHERE, "Nome", ConstantesDAO.LIKE, func.Nome);
                
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa", "FuncionarioDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public DataTable PesquisaPorId(int id)
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE,"Id_Funcionario",ConstantesDAO.EQUAL, id);
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("PesquisaPorId","FuncionarioDAO",e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public int RetornaIdEndereco(FuncionarioModel funcionario)
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateSelectWithSimpleParameter("Id_Endereco");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Funcionario",ConstantesDAO.EQUAL,funcionario.Id);
                return (int)AO.GetDataTable().Rows[0]["Id_Endereco"];
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaIdEndereco", "FuncionarioDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public int RetornaIdContato(FuncionarioModel funcionario)
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateSelectWithSimpleParameter("Id_Contato");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Funcionario", ConstantesDAO.EQUAL, funcionario.Id);
                return (int)AO.GetDataTable().Rows[0]["Id_Contato"];
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaIdEndereco", "FuncionarioDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public int RetornaIdTipo(FuncionarioModel funcionario)
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateSelectWithSimpleParameter("Id_TipoFuncionario");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Funcionario", ConstantesDAO.EQUAL, funcionario.Id);
                return (int)AO.GetDataTable().Rows[0]["Id_TipoFuncionario"];
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaIdTipo", "FuncionarioDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public DataTable PesquisaFuncionarioCpf(string cpf)
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Cpf", ConstantesDAO.EQUAL, cpf);
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("PesqusiaFuncionarioCpf", "FuncionarioDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }

        public List<FuncionarioModel> GeraRelatorio()
        {
            AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
            AO.CreateSelectAll();
            AO.GetCommand();
            return AO.GetDataTable().DataTableToList<FuncionarioModel>();
        }
    }
}
