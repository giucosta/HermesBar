using MODEL.Fornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Utils;
using DAO.Connections;
using System.Data.SqlClient;
using System.Data;
using UTILS;
using DAO.Comum;

namespace DAO.Fornecedor
{
    public class FornecedorDAO
    {
        public bool Salvar(FornecedorModel fornecedor)
        {
            try
            {
                AccessObject<FornecedorModel> AO = new AccessObject<FornecedorModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("RazaoSocial", fornecedor.RazaoSocial);
                AO.InsertParameter("Cpj", fornecedor.Cpj);
                AO.InsertParameter("InscricaoEstadual", fornecedor.InscricaoEstadual);
                AO.InsertParameter("Contato", fornecedor.Contato.Id);
                AO.InsertParameter("Endereco", fornecedor.Endereco.Id);
                
                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","FornecedorDAO",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
        public DataTable Pesquisar(FornecedorModel fornecedor)
        {
            try
            {
                AccessObject<FornecedorModel> AO = new AccessObject<FornecedorModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                if (fornecedor.Id != 0)
                {
                    AO.InsertParameter(ConstantesDAO.WHERE, "Id_Fornecedor", ConstantesDAO.EQUAL, fornecedor.Id);
                    AO.InsertParameter(ConstantesDAO.AND, "RazaoSocial", ConstantesDAO.LIKE, fornecedor.RazaoSocial);
                }
                else
                    AO.InsertParameter(ConstantesDAO.WHERE, "RazaoSocial", ConstantesDAO.LIKE, fornecedor.RazaoSocial);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisar", "FornecedorDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        public bool Editar(FornecedorModel fornecedor)
        {
            try
            {
                AccessObject<FornecedorModel> AO = new AccessObject<FornecedorModel>();
                AO.CreateSpecificQuery("UPDATE Fornecedor SET RazaoSocial = @RazaoSocial, Cpj = @Cpj, InscricaoEstadual = @InscricaoEstadual");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Fornecedor", ConstantesDAO.EQUAL, fornecedor.Id);
                AO.InsertParameter("RazaoSocial", fornecedor.RazaoSocial);
                AO.InsertParameter("Cpj", fornecedor.Cpj);
                AO.InsertParameter("InscricaoEstadual", fornecedor.InscricaoEstadual);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Editar", "FornecedorDAO", e.Message, Constantes.ATipoMetodo.UPDATE);
                return false;
            }
        }
        public bool Excluir(FornecedorModel fornecedor)
        {
            try
            {
                AccessObject<FornecedorModel> AO = new AccessObject<FornecedorModel>();
                AO.DeleteFromId();
                AO.GetCommand();
                AO.InsertParameter("Id_Fornecedor",fornecedor.Id);
                return AO.ExecuteCommand();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int RetornaIdContato(FornecedorModel fornecedor)
        {
            try
            {
                AccessObject<FornecedorModel> AO = new AccessObject<FornecedorModel>();
                AO.CreateSpecificQuery("SELECT Id_Contato as Id FROM Fornecedor");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Fornecedor", ConstantesDAO.EQUAL, fornecedor.Id);
                return (int)AO.GetDataTable().Rows[0]["Id"];
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int RetornaIdEndereco(FornecedorModel fornecedor)
        {
            try
            {
                AccessObject<FornecedorModel> AO = new AccessObject<FornecedorModel>();
                AO.CreateSpecificQuery("SELECT Id_Endereco AS Id FROM FOrnecedor");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Fornecedor", ConstantesDAO.EQUAL, fornecedor.Id);
                return (int)AO.GetDataTable().Rows[0]["Id"];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
