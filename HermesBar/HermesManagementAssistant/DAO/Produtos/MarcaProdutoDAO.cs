using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Utils;
using DAO.Connections;
using UTILS;
using System.Data;

namespace DAO.Produtos
{
    public class MarcaProdutoDAO
    {
        public bool Salvar(MarcaProdutoModel marca)
        {
            try
            {
                AccessObject<MarcaProdutoModel> AO = new AccessObject<MarcaProdutoModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("Marca", marca.Marca);
                AO.InsertParameter("Id_Fornecedor", marca.Fornecedor.Id);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","MarcaProdutoDAO",e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return false;
            }
        }
        public DataTable RetornaMarcas()
        {
            try
            {
                AccessObject<MarcaProdutoModel> AO = new AccessObject<MarcaProdutoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("ReotnaMarcas", "MarcaProdutoDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }
    }
}
