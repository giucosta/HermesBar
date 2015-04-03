using DAO.Comum;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace BLL.Comum
{
    public class ContatoBLL
    {
        private ContatoDAO _contatoDAO = null;
        public ContatoDAO DAO
        {
            get
            {
                if (_contatoDAO == null)
                    _contatoDAO = new ContatoDAO();
                return _contatoDAO;
            }
        }
        public ContatoModel Salvar(ContatoModel contato)
        {
            try
            {
                if (Validacoes.ValidarEmail(contato.Email))
                    return DAO.Salvar(VerifyNullValues(contato));
                return null;
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        public bool Excluir(ContatoModel contato)
        {
            if (contato.Id != 0)
                return DAO.Excluir(contato);
            return false;
        }
        private ContatoModel VerifyNullValues(ContatoModel contato)
        {
            if (string.IsNullOrEmpty(contato.Celular))
                contato.Celular = "";
            if (string.IsNullOrEmpty(contato.Site))
                contato.Site = "";
            if (string.IsNullOrEmpty(contato.Telefone))
                contato.Telefone = "";
            return contato;
        }
        public int RecuperaProximoId()
        {
            return DAO.RecuperaProximoId();
        }
        public ContatoModel RecuperaContatoId(int id)
        {
            if(id != 0)
                return DAO.PesquisaContatoId(id);
            return null;
        }
        public bool Editar(ContatoModel contato)
        {
            if (contato.Id != 0)
                return DAO.Editar(contato);
            return false;
        }
    }
}
