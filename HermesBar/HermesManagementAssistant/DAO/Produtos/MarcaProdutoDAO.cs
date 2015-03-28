using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Utils;
using DAO.Connections;
using UTIL;
using System.Data;

namespace DAO.Produtos
{
    public class MarcaProdutoDAO
    {
        public bool Salvar(MarcaModel marca)
        {
            try
            {
                AccessObject<MarcaModel> AO = new AccessObject<MarcaModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("Marca", marca.Marca);
                AO.InsertParameter("Id_Fornecedor", marca.Fornecedor.Id);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","MarcaProdutoDAO",e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public DataTable RetornaMarcas()
        {
            try
            {
                AccessObject<MarcaModel> AO = new AccessObject<MarcaModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaMarcas", "MarcaProdutoDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public DataTable RecuperaMarcaId(int id)
        {
            try
            {
                AccessObject<MarcaModel> AO = new AccessObject<MarcaModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_Marca", ConstantesDAO.EQUAL, id);
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
