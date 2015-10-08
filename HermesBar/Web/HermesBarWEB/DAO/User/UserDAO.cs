﻿using ENTITY.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.User
{
    public class UserDAO : Connection.Connection
    {
        public DataTable Get(HMA_USR user)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_USR.GET);
                InserParameter("ID", SqlDbType.Int, user._ID);

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
        public DataTable Insert(HMA_USR user)
        {
            try
            {
                OpenConnection();
                CreateDataAdapter(SQL.SP_USR.INSERT);
                InserParameter("USR", SqlDbType.Int, user._ID);
                InserParameter("ATV", SqlDbType.Int, user._ATV);
                InserParameter("NOM", SqlDbType.VarChar, user.NOM);
                InserParameter("EMA", SqlDbType.VarChar, user.EMA);
                InserParameter("ID_PER", SqlDbType.Int, user.PER_ID);

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
