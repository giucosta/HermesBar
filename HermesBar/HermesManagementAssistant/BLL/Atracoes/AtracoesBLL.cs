using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Atracoes;
using System.Data;
using MODEL;
using BLL.Comum;
using UTILS;
using DAO.Utils;

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
            return AtracoesDAO.Pesquisa(atracoes).DataTableToList<AtracoesModel>();
        }
        public List<AtracoesModel> ListarAtracoes()
        {
            return AtracoesDAO.RetornaTodasAtracoes().DataTableToList<AtracoesModel>();
        }
        public List<String> RecuperaEstilos()
        {
            return AtracoesDAO.RecuperaEstilos().DataTableToListString("Estilo");
        }
        public bool Salvar(AtracoesModel atracoes)
        {
            AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
            AO.GetTransaction();
            var contato = ContatoBLL.Salvar(atracoes.Contato);
            if (contato != null)
            {
                atracoes.Contato = contato;
                if (AtracoesDAO.Salvar(atracoes))
                {
                    AO.Commit();
                    return true;
                }
                else
                    AO.Rollback();
            }
            else
                AO.Rollback();
            return false;
        }
        public bool Excluir(AtracoesModel atracoes)
        {
            AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
            AO.GetTransaction();
            if (ContatoBLL.Excluir(atracoes.Contato)){
                if (AtracoesDAO.Excluir(atracoes))
                {
                    AO.Commit();
                    return true;
                }
            }
            else
                AO.Rollback();
            return false;
        }
        public bool Editar(AtracoesModel atracoes)
        {
            return AtracoesDAO.Editar(atracoes);
        }
        public int RecuperaIdContato(AtracoesModel atracao)
        {
            return (int)AtracoesDAO.RecuperaIdContato(atracao).Rows[0]["Id_Contato"];
        }
    }
}
