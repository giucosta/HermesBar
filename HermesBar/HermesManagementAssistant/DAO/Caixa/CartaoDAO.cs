using DAO.Utils;
using MODEL.Caixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Caixa
{
    public class CartaoDAO
    {
        public bool Salvar(CartaoModel cartao)
        {
            try
            {
                AccessObject<CartaoModel> AO = new AccessObject<CartaoModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("NumeroCartao", cartao.NumeroCartao);
                AO.InsertParameter("Cliente", cartao.Cliente.Id);
                AO.InsertParameter("Data", cartao.Data);
                AO.InsertParameter("ValorTotal", cartao.ValorTotal);
                AO.InsertParameter("FormaPagamento", cartao.FormaPagamento);
                AO.InsertParameter("HoraEntrada", cartao.HoraEntrada);
                AO.InsertParameter("HoraSaida", cartao.HoraSaida);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
