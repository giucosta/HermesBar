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
using MODEL.Atracoes;

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
        private EstiloAtracoesBLL _estiloAtracoesBLL = null;
        public EstiloAtracoesBLL EstiloAtracoesBLL
        {
            get
            {
                if (_estiloAtracoesBLL == null)
                    _estiloAtracoesBLL = new EstiloAtracoesBLL();
                return _estiloAtracoesBLL;
            }
        }
        public List<AtracoesModel> Pesquisa(AtracoesModel atracoes)
        {
            var atracoesList = AtracoesDAO.Pesquisa(atracoes).DataTableToList<AtracoesModel>();
            foreach (var x in atracoesList)
            {
                x.Contato = ContatoBLL.RecuperaContatoId(RecuperaIdContato(x));
                x.Estilo = EstiloAtracoesBLL.RecuperaEstiloId(RecuperaIdAtracoes(x));
            }
            return atracoesList;
        }
        public List<AtracoesModel> Pesquisa()
        {
            return AtracoesDAO.RetornaTodasAtracoes().DataTableToList<AtracoesModel>();
        }
        public List<EstiloAtracoesModel> RecuperaEstilos()
        {
            return EstiloAtracoesBLL.RetornaEstilos();
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
            if (AtracoesDAO.Excluir(atracoes))
            {
                if (ContatoBLL.Excluir(atracoes.Contato))
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
            AccessObject<AtracoesModel> AO = new AccessObject<AtracoesModel>();
            AO.GetTransaction();
            if (ContatoBLL.Editar(atracoes.Contato))
            {
                if (AtracoesDAO.Editar(atracoes))
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
        public int RecuperaIdContato(AtracoesModel atracao)
        {
            return (int)AtracoesDAO.RecuperaIdContato(atracao).Rows[0]["Id_Contato"];
        }
        public int RecuperaIdAtracoes(AtracoesModel atracao)
        {
            return (int)AtracoesDAO.RecuperaIdEstilo(atracao).Rows[0]["Id_EstiloAtracoes"];
        }
    }
}
