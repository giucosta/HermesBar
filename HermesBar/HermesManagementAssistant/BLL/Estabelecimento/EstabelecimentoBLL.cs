using BLL.Comum;
using DAO.Comum;
using DAO.Estabelecimento;
using DAO.Utils;
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
                AccessObject<EstabelecimentoModel> AO = new AccessObject<EstabelecimentoModel>();
                AO.GetTransaction();

                var endereco = EnderecoBLL.Salvar(estabelecimento.Endereco);
                if (endereco != null)
                    estabelecimento.Endereco = endereco;
                else
                {
                    AO.Rollback();
                    return false;
                }

                var contato = ContatoBLL.Salvar(estabelecimento.Contato);
                if (contato != null)
                    estabelecimento.Contato = contato;
                else
                {
                    AO.Rollback();
                    return false;
                }

                var config = ConfigEstabelecimentoBLL.Salvar(estabelecimento.ConfigEsstabelecimento);
                if (config != null)
                    estabelecimento.ConfigEstabelecimento = config;
                else
                {
                    AO.Rollback();
                    return false;
                }

                if (EstabelecimentoDAO.Salvar(estabelecimento) != null)
                {
                    AO.Commit();
                    return true;
                }
                else
                    AO.Rollback();
            }
            return false;
        }
    }
}
