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
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@Marca", marca.Marca);
                Connection.AddParameter("@Id_Fornecedor", marca.Fornecedor.Id);

                return Connection.ExecutarComando();
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
                AO.CreateSelectWithSimpleParameter("Marca");
                Connection.GetCommand(AO.ReturnQuery());

                return Connection.getDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("ReotnaMarcas", "MarcaProdutoDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }
    }
}
