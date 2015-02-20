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
                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());
                comando.Parameters.AddWithValue("@RazaoSocial", fornecedor.RazaoSocial);
                comando.Parameters.AddWithValue("@Cnpj", fornecedor.Cnpj);
                comando.Parameters.AddWithValue("@InscricaoEstadual", fornecedor.InscricaoEstadual);
                comando.Parameters.AddWithValue("@Contato", fornecedor.Contato.Id);
                comando.Parameters.AddWithValue("@Endereco", fornecedor.Endereco.Id);
                Connection.ExecutarComando(comando);
                return true;
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
                if (fornecedor.Id != 0)
                {
                    AO.InsertParameter(ConstantesDAO.WHERE, "Id_Fornecedor", ConstantesDAO.EQUAL, "@Id");
                    AO.InsertParameter(ConstantesDAO.OR, "RazaoSocial", ConstantesDAO.LIKE, "@RazaoSocial");
                }
                AO.InsertParameter(ConstantesDAO.WHERE, "RazaoSocial", ConstantesDAO.LIKE, "@RazaoSocial");
                
                var comando = new SqlCommand(AO.ReturnQuery(),Connection.GetConnection());
                comando.Parameters.AddWithValue("@Id",fornecedor.Id);
                comando.Parameters.AddWithValue("@RazaoSocial", fornecedor.RazaoSocial);

                return Connection.getDataTable(comando);
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisar", "FornecedorDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
