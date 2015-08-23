using ENTITY.Commom;
using ENTITY.Establishment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Establishment
{
    public class EstablishmentDAO : Connection.Connection
    {
        public DataSet Get(HMA_EST est)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EST.GET);
                InserParameter("ID_EST", SqlDbType.Int, est._ID);

                return GetResultAsDataSet();
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
        public DataTable Insert(HMA_EST est, HMA_END end, HMA_CON con)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EST.INSERT);
                InserParameter("USR", SqlDbType.Int, est._USR);
                InserParameter("ATV", SqlDbType.Int, est._ATV);
                InserParameter("FAN", SqlDbType.VarChar, est.FAN);
                InserParameter("RAZ", SqlDbType.VarChar, est.RAZ);
                InserParameter("CNPJ", SqlDbType.VarChar, est.CNPJ);
                InserParameter("INSEST", SqlDbType.VarChar, est.INSEST);
                InserParameter("INSMUN", SqlDbType.VarChar, est.INSMUN);
                InserParameter("QUANT_MESA", SqlDbType.Int, est.QUANT_MESA);
                InserParameter("QUANT_CLI", SqlDbType.Int, est.QUANT_CLI);

                InserParameter("RUA", SqlDbType.VarChar, end.RUA);
                InserParameter("NUM", SqlDbType.VarChar, end.NUM);
                InserParameter("COMP", SqlDbType.VarChar, end.COMP);
                InserParameter("BAI", SqlDbType.VarChar, end.BAI);
                InserParameter("CEP", SqlDbType.VarChar, end.CEP);
                InserParameter("CID", SqlDbType.VarChar, end.CID);
                InserParameter("UF", SqlDbType.Int, end.UF);

                InserParameter("NOM", SqlDbType.VarChar, con.NOM);
                InserParameter("TEL", SqlDbType.VarChar, con.TEL);
                InserParameter("CEL", SqlDbType.VarChar, con.CEL);
                InserParameter("EMA", SqlDbType.VarChar, con.EMA);

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
        public DataTable Update(HMA_EST est, HMA_END end, HMA_CON con)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EST.UPDATE);
                InserParameter("ID", SqlDbType.Int, est._ID);
                InserParameter("USR", SqlDbType.Int, est._USR);
                InserParameter("ATV", SqlDbType.Int, est._ATV);
                InserParameter("FAN", SqlDbType.VarChar, est.FAN);
                InserParameter("RAZ", SqlDbType.VarChar, est.RAZ);
                InserParameter("CNPJ", SqlDbType.VarChar, est.CNPJ);
                InserParameter("INSEST", SqlDbType.VarChar, est.INSEST);
                InserParameter("INSMUN", SqlDbType.VarChar, est.INSMUN);
                InserParameter("QUANT_MESA", SqlDbType.Int, est.QUANT_MESA);
                InserParameter("QUANT_CLI", SqlDbType.Int, est.QUANT_CLI);

                InserParameter("RUA", SqlDbType.VarChar, end.RUA);
                InserParameter("NUM", SqlDbType.VarChar, end.NUM);
                InserParameter("COMP", SqlDbType.VarChar, end.COMP);
                InserParameter("BAI", SqlDbType.VarChar, end.BAI);
                InserParameter("CEP", SqlDbType.VarChar, end.CEP);
                InserParameter("CID", SqlDbType.VarChar, end.CID);
                InserParameter("UF", SqlDbType.Int, end.UF);

                InserParameter("NOM", SqlDbType.VarChar, con.NOM);
                InserParameter("TEL", SqlDbType.VarChar, con.TEL);
                InserParameter("CEL", SqlDbType.VarChar, con.CEL);
                InserParameter("EMA", SqlDbType.VarChar, con.EMA);

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
        public DataTable Inative(HMA_EST est)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EST.INACTIVE);
                InserParameter("ID", SqlDbType.Int, est._ID);
                InserParameter("USR", SqlDbType.Int, est._USR);

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
        public DataTable Active(HMA_EST est)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_EST.ACTIVE);
                InserParameter("ID", SqlDbType.Int, est._ID);
                InserParameter("USR", SqlDbType.Int, est._USR);

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
