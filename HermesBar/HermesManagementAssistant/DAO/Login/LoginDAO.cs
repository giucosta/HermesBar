﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Connections;
using System.Data.SqlClient;
using System.Data;
using MODEL;

namespace DAO.Login
{
    public class LoginDAO
    {
        /// <summary>
        /// Recebe login como parametro, efetua a decriptacao da senha do banco e compara com a digitada pelo usuario
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool EfetuaLogin(LoginModel login)
        {
            try
            {
                string sql = "SELECT * FROM Login WHERE Login = @Login";
                
                var comando = new SqlCommand(sql, Connection.getConnection());
                comando.Parameters.Add(new SqlParameter("@Login", login.Login));

                if (login.Senha.Equals(Encript.EncriptMd5.Descriptografar(Connection.getDataTable(comando).Rows[0]["Senha"].ToString())))
                    return true;

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
