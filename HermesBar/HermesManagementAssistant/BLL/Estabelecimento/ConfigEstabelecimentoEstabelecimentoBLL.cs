using DAO.Estabelecimento;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Estabelecimento
{
    public class ConfigEstabelecimentoEstabelecimentoBLL
    {
        private ConfigEstabelecimentoEstabelecimentoDAO _configEstabelecimentoEstabelecimentoDAO = null;
        public ConfigEstabelecimentoEstabelecimentoDAO ConfigEstabelecimentoEstabelecimentoDAO
        {
            get
            {
                if (_configEstabelecimentoEstabelecimentoDAO == null)
                    _configEstabelecimentoEstabelecimentoDAO = new ConfigEstabelecimentoEstabelecimentoDAO();
                return _configEstabelecimentoEstabelecimentoDAO;
            }
        }

        public bool Salvar(EstabelecimentoModel estabelecimento)
        {
            return ConfigEstabelecimentoEstabelecimentoDAO.Salvar(estabelecimento);
        }
    }
}
