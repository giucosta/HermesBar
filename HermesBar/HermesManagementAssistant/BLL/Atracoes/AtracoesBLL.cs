using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Atracoes;
using System.Data;
using MODEL;
using BLL.Comum;
using DAO;

namespace BLL.Atracoes
{
    public class AtracoesBLL
    {
        private AtracoesDAO _atracoesDAO = null;
        public AtracoesDAO AtracoesDAO
        {
            get
            {
                if (_atracoesDAO == null)
                    _atracoesDAO = new AtracoesDAO();
                return _atracoesDAO;
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

        public List<AtracoesModel> Pesquisa(AtracoesModel atracoes)
        {
            return Conversores.DataTableToList<AtracoesModel>(AtracoesDAO.Pesquisa(atracoes));
        }
        public List<String> RecuperaEstilos()
        {
            var atributos = new List<String>();
            atributos.Add("Estilo");

            return AtracoesDAO.RecuperaEstilos().DataTableToListString(atributos);
        }

        public bool Salvar(AtracoesModel atracoes)
        {
            atracoes.Contato = ContatoBLL.Salvar(atracoes.Contato);
            if (atracoes.Contato != null)
                return AtracoesDAO.Salvar(atracoes);
            else
                return false;
        }
    }
}
