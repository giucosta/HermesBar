using DAO.Banco;
using MODEL.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace BLL.Banco
{
    public class CentroCustoBLL
    {
        private CentroCustoDAO _centroCustoDAO = null;
        public CentroCustoDAO CentroCustoDAO
        {
            get
            {
                if (_centroCustoDAO == null)
                    _centroCustoDAO = new CentroCustoDAO();
                return _centroCustoDAO;
            }
        }

        public bool Salvar(CentroCustoModel centroCusto)
        {
            try
            {
                if (!VerificaCodigoExistente(centroCusto))
                    return CentroCustoDAO.Salvar(centroCusto);
                else
                    UTIL.Session.MensagemErro = "Código já existente";
                return false;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public List<CentroCustoModel> GetAllCentroCusto(CentroCustoModel centroCusto)
        {
            try
            {
                return CentroCustoDAO.PesquisaCentroCusto(centroCusto).DataTableToList<CentroCustoModel>();
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        public bool Editar(CentroCustoModel centroCusto)
        {
            try
            {
                return CentroCustoDAO.Editar(centroCusto);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }

        }
        public bool VerificaCodigoExistente(CentroCustoModel centroCusto)
        {
            try
            {
                if (CentroCustoDAO.VerificaCodigoExistente(centroCusto).Rows.Count > 0)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return true;
            }
        }
        public bool Excluir(CentroCustoModel centroCusto)
        {
            try
            {
                return CentroCustoDAO.Excluir(centroCusto);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }

    }
}
