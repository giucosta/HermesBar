using BLL.Comum;
using DAO.Comum;
using DAO.Estabelecimento;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BLL.Estabelecimento
{
    public class EstabelecimentoBLL
    {
        #region AccessMethod
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
        private EnderecoBLL _enderecoBLL = null;
        public EnderecoBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new EnderecoBLL();
                return _enderecoBLL;
            }
        }
        private ContatoDAO _contatoDAO = null;
        private ContatoBLL _contatoBLL = null;
        public ContatoBLL ContatoBLL
        {
            get
            {
                if (_contatoBLL == null)
                    _contatoBLL = new ContatoBLL();
                return _contatoBLL;
            }
        }
        private ConfigEstabelecimentoBLL _configEstabelecimentoBLL = null;
        public ConfigEstabelecimentoBLL ConfigEstabelecimentoBLL
        {
            get
            {
                if (_configEstabelecimentoBLL == null)
                    _configEstabelecimentoBLL = new ConfigEstabelecimentoBLL();
                return _configEstabelecimentoBLL;
            }
        }
        private ConfigEstabelecimentoEstabelecimentoBLL _configEstabelecimentoEstabelecimentoBLL = null;
        public ConfigEstabelecimentoEstabelecimentoBLL ConfigEstabelecimentoEstabelecimentoBLL
        {
            get
            {
                if (_configEstabelecimentoEstabelecimentoBLL == null)
                    _configEstabelecimentoEstabelecimentoBLL = new ConfigEstabelecimentoEstabelecimentoBLL();
                return _configEstabelecimentoEstabelecimentoBLL;
            }
        }
        #endregion
        public bool Salvar(EstabelecimentoModel estabelecimento)
        {
            if (Validacoes.ValidaCNPJ(estabelecimento.Cnpj))
            {
                estabelecimento.Endereco = EnderecoBLL.Salvar(estabelecimento.Endereco);
                estabelecimento.Contato = ContatoBLL.Salvar(estabelecimento.Contato);
                estabelecimento.ConfigEstabelecimento = ConfigEstabelecimentoBLL.Salvar(estabelecimento.ConfigEstabelecimento);

                var estabelecimentoSalvo = EstabelecimentoDAO.Salvar(estabelecimento);
                if (estabelecimentoSalvo != null)
                {
                    estabelecimentoSalvo.ConfigEstabelecimento = new ConfigEstabelecimentoModel();
                    estabelecimentoSalvo.ConfigEstabelecimento = estabelecimentoSalvo.ConfigEstabelecimento;
                    return ConfigEstabelecimentoEstabelecimentoBLL.Salvar(estabelecimentoSalvo);
                }
            }
            return false;
        }
    }
}
