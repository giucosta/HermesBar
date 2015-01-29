using DAO.Estabelecimento;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace BLL.Estabelecimento
{
    public class EstabelecimentoBLL
    {
        private EstabelecimentoDAO _estabelecimentoDAO = null;
        public EstabelecimentoDAO EstabelecimentoDAO
        {
            get
            {
                if (_estabelecimentoDAO == null)
                    _estabelecimentoDAO = new EstabelecimentoDAO();
                return _estabelecimentoDAO;
            }
        }

        public bool Salvar(EstabelecimentoModel estabelecimento)
        {
            if(Validacoes.ValidaCNPJ(estabelecimento.Cnpj))
                return EstabelecimentoDAO.Salvar(estabelecimento);
            return false;
        }
    }
}
