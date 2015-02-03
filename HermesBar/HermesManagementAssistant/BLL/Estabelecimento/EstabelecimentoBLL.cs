using DAO.Comum;
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
        private EnderecoDAO _enderecoDAO = null;
        public EnderecoDAO EnderecoDAO
        {
            get
            {
                if (_enderecoDAO == null)
                    _enderecoDAO = new EnderecoDAO();
                return _enderecoDAO;
            }
        }
        private ContatoDAO _contatoDAO = null;
        public ContatoDAO ContatoDAO
        {
            get
            {
                if (_contatoDAO == null)
                    _contatoDAO = new ContatoDAO();
                return _contatoDAO;
            }
        }
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
        #endregion
        public bool Salvar(EstabelecimentoModel estabelecimento)
        {
            if (Validacoes.ValidaCNPJ(estabelecimento.Cnpj))
            {
                estabelecimento.Endereco = EnderecoDAO.Salvar(estabelecimento.Endereco);
                estabelecimento.Contato = ContatoDAO.Salvar(estabelecimento.Contato);
                estabelecimento.ConfigEstabelecimento = ConfigEstabelecimentoDAO.Salvar(estabelecimento.ConfigEstabelecimento);

                if(ConfigEstabelecimentoEstabelecimentoDAO.Salvar(estabelecimento))
                    return EstabelecimentoDAO.Salvar(estabelecimento);
            }
            return false;
        }
    }
}
