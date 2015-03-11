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
                AO.GetCommand();
                AO.InsertParameter("Nome",funcionario.Nome);
                AO.InsertParameter("Cpf",funcionario.Cpf);
                AO.InsertParameter("Rg",funcionario.Rg);
                AO.InsertParameter("DataNascimento",funcionario.DataNascimento);
                AO.InsertParameter("CarteiraTrabalho",funcionario.CarteiraTrabalho);
                AO.InsertParameter("Serie",funcionario.Serie);
                AO.InsertParameter("Endereco",funcionario.Endereco.Id);
                AO.InsertParameter("Tipo",funcionario.Tipo.Id);
                AO.InsertParameter("Contato",funcionario.Contato.Id);
                AO.InsertParameter("DataAdmissao",funcionario.DataAdmissao);

                return AO.ExecuteCommand();
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
                AO.GetCommand();
                AO.InsertParameter("Id_Funcionario",funcionario.Id);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Excluir", "FuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.DELETE);
                return false;
            }
        }
        public DataTable Pesquisa()
        {
            try
            {
                AccessObject<FuncionarioModel> AO = new AccessObject<FuncionarioModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisa", "FuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
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
                Log.Log.GravarLog("PesquisaPorId","FuncionarioDAO",e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
