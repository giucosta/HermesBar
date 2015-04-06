using DAO.Utils;
using MODEL.Atracoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace DAO.Atracoes
{
    public class EstiloAtracoesDAO
    {
        public bool Salvar(EstiloAtracoesModel estilo)
        {
            try
            {
                AccessObject<EstiloAtracoesModel> AO = new AccessObject<EstiloAtracoesModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("Estilo", estilo.Estilo);
                return AO.ExecuteCommand();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable RetornaTipos()
        {
            try
            {
                AccessObject<EstiloAtracoesModel> AO = new AccessObject<EstiloAtracoesModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                return AO.GetDataTable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public EstiloAtracoesModel RecuperaEstiloPorId(int id)
        {
            try
            {
                AccessObject<EstiloAtracoesModel> AO = new AccessObject<EstiloAtracoesModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_EstiloAtracoes", ConstantesDAO.EQUAL, id);
                return CarregaEstilo(AO.GetDataTable());
            }
            catch (Exception)
            {
                throw;
            }
        }
        private EstiloAtracoesModel CarregaEstilo(DataTable data)
        {
            if (data.Rows.Count != 0)
            {
                var estilo = new EstiloAtracoesModel()
                {
                    Id = (int)data.Rows[0]["Id_EstiloAtracoes"],
                    Estilo = data.Rows[0]["Estilo"].ToString()
                };
                return estilo;
            }
            return null;
        }
    }
}
