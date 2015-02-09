using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Atracoes;
using System.Data;
using MODEL;
using BLL.Comum;

namespace BLL.Atracoes
{
    public class AtracoesBLL
    {
        private AtracoesDAO _atracoesDAO = null;
        public AtracoesDAO DAO
        {
            get
            {
                if (_atracoesDAO == null)
                    _atracoesDAO = new AtracoesDAO();
                return _atracoesDAO;
            }
        }
        private ContatoBLL _contatoBLL = null;
        public ContatoBLL ContatcBLL
        {
            get
            {
                if (_contatoBLL == null)
                    _contatoBLL = new ContatoBLL();
                return _contatoBLL;
            }
        }

        public DataTable Pesquisa(AtracoesModel atracoes)
        {
            return DAO.Pesquisa(atracoes);
        }
        public List<String> RecuperaEstilos()
        {
            return DAO.RecuperaEstilos();
        }

        public bool Salvar(AtracoesModel atracoes)
        {
            atracoes.Contato = ContatcBLL.Salvar(atracoes.Contato);
            return DAO.Salvar(atracoes);
        }
    }
}
