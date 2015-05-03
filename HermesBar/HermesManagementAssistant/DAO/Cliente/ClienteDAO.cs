using MODEL.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Utils;
using UTIL;
using System.Data;

namespace DAO.Cliente
{
    public class ClienteDAO
    {
        public bool Salvar(ClienteModel cliente)
        {
            try
            {
                AccessObject<ClienteModel> AO = new AccessObject<ClienteModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("Nome", cliente.Nome);
                AO.InsertParameter("Rg", cliente.RG);
                AO.InsertParameter("Telefone", cliente.Telefone);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "ClienteDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public DataTable RecuperaUltimoCadastro()
        {
            try
            {
                AccessObject<ClienteModel> AO = new AccessObject<ClienteModel>();
                AO.CreateSpecificQuery("SELECT TOP 1 * FROM Cliente ORDER BY Id_Cliente DESC");
                AO.GetCommand();

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable Pesquisar(ClienteModel cliente)
        {
            try
            {
                AccessObject<ClienteModel> AO = new AccessObject<ClienteModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertComparisionAttribute();
                if (cliente.Id != 0)
                    AO.InsertParameter(ConstantesDAO.AND, "Id_Cliente", ConstantesDAO.EQUAL, cliente.Id);
                else if (!string.IsNullOrEmpty(cliente.RG))
                    AO.InsertParameter(ConstantesDAO.AND, "RG", ConstantesDAO.EQUAL, cliente.RG);

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Pesquisar", "ClienteDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
    }
}
