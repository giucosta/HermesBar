using ENTITY.Commom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UTIL
{
    public class AddressParameters : Connection.Connection
    {
        public void InsertAddressParameters(ref HMA_END end)
        {
            try
            {
                InserParameter("RUA", SqlDbType.VarChar, end.RUA);
                InserParameter("NUM", SqlDbType.VarChar, end.NUM);
                InserParameter("BAI", SqlDbType.VarChar, end.BAI);
                InserParameter("COMP", SqlDbType.VarChar, end.COMP);
                InserParameter("CEP", SqlDbType.VarChar, end.CEP);
                InserParameter("CID", SqlDbType.VarChar, end.CID);
                InserParameter("UF", SqlDbType.Int, end.UF);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class ContactParameters : Connection.Connection
    {
        public void InsertContactParameters(ref HMA_CON con)
        {
            InserParameter("NOM", SqlDbType.VarChar, con.NOM);
            InserParameter("TEL", SqlDbType.VarChar, con.TEL);
            InserParameter("CEL", SqlDbType.VarChar, con.CEL);
            InserParameter("EMA", SqlDbType.VarChar, con.EMA);
        }
    }
}
