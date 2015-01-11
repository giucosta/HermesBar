using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Atracoes;
using System.Data;
using MODEL;

namespace BLL.Atracoes
{
    public class AtracoesBLL
    {
        private AtracoesDAO _atracoesDAO = null;
        public AtracoesDAO DAO
        {
            get
            {
                if (_atracoesDAO == null)
                    _atracoesDAO = new AtracoesDAO();
                return _atracoesDAO;
            }
        }

        public DataTable Pesquisa(AtracoesModel atracoes)
        {
            return DAO.Pesquisa(atracoes);
        }
    }
}
