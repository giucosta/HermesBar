using DAO.Comum;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

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
            if (Validacoes.ValidarEmail(contato.Email))
            {
                if(!string.IsNullOrWhiteSpace(contato.Nome))
                    return DAO.Salvar(VerifyNullValues(contato));
            }  
            return null;
        }

        private ContatoModel VerifyNullValues(ContatoModel contato)
        {
            if (string.IsNullOrEmpty(contato.Celular))
                contato.Celular = "";
            if (string.IsNullOrEmpty(contato.Email))
                contato.Email = "";
            if (string.IsNullOrEmpty(contato.Nome))
                contato.Nome = "";
            if (string.IsNullOrEmpty(contato.Site))
                contato.Site = "";
            if (string.IsNullOrEmpty(contato.Telefone))
                contato.Site = "";
            return contato;
        }
        public int RecuperaProximoId()
        {
            return DAO.RecuperaProximoId();
        }
    }
}
