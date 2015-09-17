using ENTITY.PDV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PDV
{
    public class Pdv_PayBoxDAO : Connection.Connection
    {
        public DataTable Open(HMA_PDV_CAI caixa)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CAI.OPEN);
                InserParameter("USR", SqlDbType.Int, caixa._USR);
                InserParameter("ATV",SqlDbType.Int, caixa._ATV);
                InserParameter("DT_ABER",SqlDbType.DateTime, caixa.DT_ABER);
                InserParameter("VLR_INI",SqlDbType.Decimal, caixa.VLR_INI);

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
        public DataTable Close(HMA_PDV_CAI caixa)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CAI.CLOSE);
                InserParameter("ID", SqlDbType.Int, caixa._ID);
                InserParameter("USR", SqlDbType.Int, caixa._USR);
                InserParameter("VLR_FIN", SqlDbType.Decimal, caixa.VLR_FIN);

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
        public DataTable Reinforcement(HMA_PDV_CAI caixa)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CAI.REINFORCEMENT);
                InserParameter("USR", SqlDbType.Int, caixa._USR);
                InserParameter("ID", SqlDbType.Int, caixa._ID);
                InserParameter("VLR_REF", SqlDbType.Decimal, caixa.VLR_REF);
                InserParameter("DSC", SqlDbType.VarChar, caixa.DESCR);

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
        public DataTable Depletion(HMA_PDV_CAI caixa)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CAI.DEPLETION);
                InserParameter("USR", SqlDbType.Int, caixa._USR);
                InserParameter("ID", SqlDbType.Int, caixa._ID);
                InserParameter("VLR", SqlDbType.Decimal, caixa.VLR_SAN);
                InserParameter("DSC", SqlDbType.VarChar, caixa.DESCR);

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
        public DataTable VerifyPayBox()
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CAI.VER_PAYBOX);

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
