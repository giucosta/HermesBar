using DAO.Atracoes;
using MODEL.Atracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace BLL.Atracoes
{
    public class EstiloAtracoesBLL
    {
        private EstiloAtracoesDAO _estiloAtracoesDAO = null;
        public EstiloAtracoesDAO EstiloAtracoesDAO
        {
            get
            {
                if (_estiloAtracoesDAO == null)
                    _estiloAtracoesDAO = new EstiloAtracoesDAO();
                return _estiloAtracoesDAO;
            }
        }

        public bool Salvar(EstiloAtracoesModel estilo)
        {
            if (!string.IsNullOrEmpty(estilo.Estilo))
                return EstiloAtracoesDAO.Salvar(estilo);
            return false;
        }
        public List<EstiloAtracoesModel> RetornaEstilos()
        {
            return EstiloAtracoesDAO.RetornaTipos().DataTableToList<EstiloAtracoesModel>();
        }
        public EstiloAtracoesModel RecuperaEstiloId(int id)
        {
            return EstiloAtracoesDAO.RecuperaEstiloPorId(id);
        }
    }
}
