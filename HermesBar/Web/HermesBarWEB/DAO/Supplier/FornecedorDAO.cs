using ENTITY.Commom;
using ENTITY.Supplier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Supplier
{
    public class FornecedorDAO : Connection.Connection
    {   
        public DataTable Insert(HMA_FOR forn, HMA_END end, HMA_CON con)
        {
            try
            {
                OpenConnection();
                var data = new DataTable();
                CreateDataAdapter("[dbo].[HMA_FOR_INS]");
                InserParameter("ATV", SqlDbType.Int, forn._ATV);
                InserParameter("USR", SqlDbType.Int, forn._USR);
                InserParameter("RAZ", SqlDbType.VarChar, forn.RAZ);
                InserParameter("FAN", SqlDbType.VarChar, forn.FAN);
                InserParameter("CNPJ", SqlDbType.VarChar, forn.CNPJ);
                InserParameter("INSMUN", SqlDbType.VarChar, forn.INSMUN);
                InserParameter("INSEST", SqlDbType.VarChar, forn.INSEST);

                InserParameter("RUA", SqlDbType.VarChar, end.RUA);
                InserParameter("NUM", SqlDbType.VarChar, end.NUM);
                InserParameter("BAI", SqlDbType.VarChar, end.BAI);
                InserParameter("COMP", SqlDbType.VarChar, end.COMP);
                InserParameter("CEP", SqlDbType.VarChar, end.CEP);
                InserParameter("CID", SqlDbType.VarChar, end.CID);
                InserParameter("UF", SqlDbType.Int, end.UF);

                InserParameter("NOM", SqlDbType.VarChar, con.NOM);
                InserParameter("TEL", SqlDbType.VarChar, con.TEL);
                InserParameter("CEL", SqlDbType.VarChar, con.CEL);
                InserParameter("EMA", SqlDbType.VarChar, con.EMA);
                
                return GetResult(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataSet Get(HMA_FOR forn)
        {
            try
            {
                OpenConnection();
                var data = new DataSet();
                CreateDataAdapter("[dbo].[SP_HMA_FOR_GET]");
                InserParameter("FOR_ID", SqlDbType.Int, forn._ID);

                return GetResultAsDataSet(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataTable Update(HMA_FOR forn, HMA_END end, HMA_CON con)
        {
            try
            {
                OpenConnection();
                var data = new DataTable();

                CreateDataAdapter("[dbo].[SP_HMA_FOR_UPD]");
                InserParameter("ID_FOR",SqlDbType.Int, forn._ID);
                InserParameter("ATV", SqlDbType.Int, forn._ATV);
                InserParameter("USR", SqlDbType.Int, forn._USR);
                InserParameter("RAZ", SqlDbType.VarChar, forn.RAZ);
                InserParameter("FAN", SqlDbType.VarChar, forn.FAN);
                InserParameter("CNPJ", SqlDbType.VarChar, forn.CNPJ);
                InserParameter("INSMUN", SqlDbType.VarChar, forn.INSMUN);
                InserParameter("INSEST", SqlDbType.VarChar, forn.INSEST);

                InserParameter("RUA", SqlDbType.VarChar, end.RUA);
                InserParameter("NUM", SqlDbType.VarChar, end.NUM);
                InserParameter("BAI", SqlDbType.VarChar, end.BAI);
                InserParameter("COMP", SqlDbType.VarChar, end.COMP);
                InserParameter("CEP", SqlDbType.VarChar, end.CEP);
                InserParameter("CID", SqlDbType.VarChar, end.CID);
                InserParameter("UF", SqlDbType.Int, end.UF);

                InserParameter("NOM", SqlDbType.VarChar, con.NOM);
                InserParameter("TEL", SqlDbType.VarChar, con.TEL);
                InserParameter("CEL", SqlDbType.VarChar, con.CEL);
                InserParameter("EMA", SqlDbType.VarChar, con.EMA);

                return GetResult(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
