using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Banco;
using UTIL;
using DAO.Utils;
using System.Data;

namespace DAO.Banco
{
    public class CentroCustoDAO
    {
        public bool Salvar(CentroCustoModel centroCusto)
        {
            try
            {
                AccessObject<CentroCustoModel> AO = new AccessObject<CentroCustoModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("Codigo", centroCusto.Codigo);
                AO.InsertParameter("Nome",centroCusto.Nome);
                AO.InsertParameter("PermiteLancamento", centroCusto.PermiteLancamento);
                AO.InsertParameter("Status", centroCusto.Status);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "CentroCustoDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }

        public DataTable GetAllCentroCusto(CentroCustoModel centroCusto)
        {
            try
            {
                AccessObject<CentroCustoModel> AO = new AccessObject<CentroCustoModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertComparisionAttribute();
                if(!string.IsNullOrEmpty(centroCusto.Nome))
                    AO.InsertParameter(ConstantesDAO.AND, "Nome", ConstantesDAO.LIKE, centroCusto.Nome);
                if (!string.IsNullOrEmpty(centroCusto.Codigo))
                    AO.InsertParameter(ConstantesDAO.AND, "Codigo", ConstantesDAO.EQUAL, centroCusto.Codigo);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("GetAllCentroCusto", "CentroCustoDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public bool Editar(CentroCustoModel centroCusto)
        {
            try
            {
                AccessObject<CentroCustoModel> AO = new AccessObject<CentroCustoModel>();
                AO.CreateSpecificQuery("UPDATE CentroCusto SET Nome = @Nome, Codigo = @Codigo, Status = @Status, @PermiteLancamento = @PermiteLancamento");
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_CentroCusto", ConstantesDAO.EQUAL, centroCusto.Id);
                AO.InsertParameter("Codigo", centroCusto.Codigo);
                AO.InsertParameter("Nome", centroCusto.Nome);
                AO.InsertParameter("Status", centroCusto.Status);
                AO.InsertParameter("PermiteLancamento", centroCusto.PermiteLancamento);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Editar", "CentroCustoDAO", e.Message, Constantes.ATipoMetodo.UPDATE);
                throw e;
            }
        }
    }
}
