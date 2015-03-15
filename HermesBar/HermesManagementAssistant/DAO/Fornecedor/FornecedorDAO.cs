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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@RazaoSocial", fornecedor.RazaoSocial);
                Connection.AddParameter("@Cnpj", fornecedor.Cnpj);
                Connection.AddParameter("@InscricaoEstadual", fornecedor.InscricaoEstadual);
                Connection.AddParameter("@Contato", fornecedor.Contato.Id);
                Connection.AddParameter("@Endereco", fornecedor.Endereco.Id);
                return Connection.ExecutarComando();
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
    }
}
