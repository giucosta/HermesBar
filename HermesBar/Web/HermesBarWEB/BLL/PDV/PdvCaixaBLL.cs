using DAO.PDV;
using ENTITY.PDV;
using MODEL.PDV.Caixa;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
namespace BLL.PDV
{
    public class PdvCaixaBLL
    {
        #region Singleton
        private Pdv_CaixaDAO _caixaDAO = null;
        private Pdv_CaixaDAO CaixaDAO
        {
            get
            {
                if (_caixaDAO == null)
                    _caixaDAO = new Pdv_CaixaDAO();
                return _caixaDAO;
            }
        }
        #endregion

        public bool Open(CaixaModel caixa, UsuarioModel usuario)
        {
            try
            {
                return CaixaDAO.Open(ConvertModelToEntity(caixa, usuario)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private HMA_PDV_CAI ConvertModelToEntity(CaixaModel caixa, UsuarioModel usuario)
        {
            try
            {
                var entity = new HMA_PDV_CAI();
                entity._ATV = Convert.ToInt32(caixa.StatusSelected);
                entity._ID = caixa.Id;
                entity._USR = usuario.Id;
                entity.DT_ABER = caixa.DataAbertura;
                entity.DT_FEC = caixa.DataFechamento;
                entity.VLR_FIN = caixa.ValorFechamento;
                entity.VLR_INI = caixa.ValorAbertura;
                
                return entity;

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
