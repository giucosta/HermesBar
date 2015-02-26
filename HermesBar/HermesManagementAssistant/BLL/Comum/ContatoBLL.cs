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
                    return DAO.Salvar(contato);
            }  
            return null;
        }
        public int RecuperaProximoId()
        {
            return DAO.RecuperaProximoId();
        }
    }
}
