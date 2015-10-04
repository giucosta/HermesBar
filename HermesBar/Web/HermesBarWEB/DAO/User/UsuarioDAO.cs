﻿using ENTITY.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.User
{
    public class UsuarioDAO : Connection.Connection
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
    }
}
