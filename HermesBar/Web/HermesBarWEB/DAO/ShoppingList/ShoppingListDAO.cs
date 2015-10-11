using ENTITY.ShoppingList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ShoppingList
{
    public class ShoppingListDAO : Connection.Connection
    {
        public DataTable Insert(HMA_LIS_COM shoppingList)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_LIS_COM.INSERT);
                InserParameter("USR", SqlDbType.Int, shoppingList._USR);
                InserParameter("ID_PROD", SqlDbType.Int, shoppingList._ID_PROD);
                InserParameter("QUANT", SqlDbType.Int, shoppingList.QUANT);
                InserParameter("ID_LIST", SqlDbType.Int, shoppingList._ID_LIS);

                return GetResult();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataTable GetNextId()
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_LIS_COM.GET_NEXT_ID);

                return GetResult();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataTable Get()
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_LIS_COM.GET);

                return GetResult();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
