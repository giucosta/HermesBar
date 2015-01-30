using DAO.Estabelecimento;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Estabelecimento
{
    public class ConfigEstabelecimentoBLL
    {
        private ConfigEstabelecimentoDAO _configEstabelecimentoDAO = null;
        public ConfigEstabelecimentoDAO ConfigEstabelecimentoDAO
        {
            get
            {
                if (_configEstabelecimentoDAO == null)
                    _configEstabelecimentoDAO = new ConfigEstabelecimentoDAO();
                return _configEstabelecimentoDAO;
            }
        }

        public ConfigEstabelecimentoModel Salvar(ConfigEstabelecimentoModel configEstabelecimento)
        {
            return ConfigEstabelecimentoDAO.Salvar(configEstabelecimento);
        }
    }
}
