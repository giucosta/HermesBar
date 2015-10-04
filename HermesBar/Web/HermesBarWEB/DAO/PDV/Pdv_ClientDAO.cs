using ENTITY.PDV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PDV
{
    public class Pdv_ClientDAO : Connection.Connection
    {
        public DataTable Insert(HMA_PDV_CLI cli)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CLI.INSERT);
                InserParameter("USR", SqlDbType.Int, cli._USR);
                InserParameter("ID_CLI", SqlDbType.Int, cli._ID_CLI);
                InserParameter("ID_CAI", SqlDbType.Int, cli._ID_CAI);
                InserParameter("NUM_CAR", SqlDbType.Int, cli.NUM_CAR);

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
        public DataTable GetCar(HMA_PDV_CLI cli)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CLI.GET_CAR);
                InserParameter("NUMEROCARTAO", SqlDbType.Int, cli.NUM_CAR);

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
        public DataTable Close(HMA_PDV_CLI cli)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CLI.FECHAR);
                InserParameter("NUM_CAR", SqlDbType.Int, cli.NUM_CAR);
                InserParameter("NUM_CAI", SqlDbType.Int, cli._ID_CAI);

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
        public DataTable CloseCommand(HMA_PDV_CLI cli)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_PDV_CLI.FECHAR_COMANDA);
                InserParameter("NUM_CAR", SqlDbType.Int, cli.NUM_CAR);
                InserParameter("USR", SqlDbType.Int, cli._USR);
                InserParameter("ID_CAI", SqlDbType.Int, cli._ID_CAI);
                InserParameter("VLR_REC", SqlDbType.Decimal, cli.VLR_REC);
                InserParameter("VLR_TOT", SqlDbType.Decimal, cli.CONS_TOT);
                InserParameter("TRC", SqlDbType.Decimal, cli.TRC);
                InserParameter("FRM_PAG", SqlDbType.VarChar, cli.FRM_PAG);

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
