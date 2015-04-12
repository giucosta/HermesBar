using DAO.Caixa;
using MODEL.Caixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace BLL.Caixa
{
    public class CaixaAbertoBLL
    {
        private CaixaAbertoDAO _caixaAbertoDAO = null;
        public CaixaAbertoDAO CaixaAbertoDAO
        {
            get
            {
                if (_caixaAbertoDAO == null)
                    _caixaAbertoDAO = new CaixaAbertoDAO();
                return _caixaAbertoDAO;
            }
        }

        public List<CaixaModel> RetornaMovimentacoes()
        {
            return CaixaAbertoDAO.RetornaMovimentacoes().DataTableToList<CaixaModel>();
        }
        public bool AbrirCaixa(CaixaModel caixa)
        {
            caixa.DataAbertura = DateTime.Now;
            return CaixaAbertoDAO.AbrirCaixa(caixa);
        }
    }
}
