using DAO.PDV;
using ENTITY.PDV;
using MODEL.PDV.PayBox;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
namespace BLL.PDV
{
    public class PdvPayBoxBLL
    {
        #region Singleton
        private Pdv_PayBoxDAO _payBoxDAO = null;
        private Pdv_PayBoxDAO PayBoxDAO
        {
            get
            {
                if (_payBoxDAO == null)
                    _payBoxDAO = new Pdv_PayBoxDAO();
                return _payBoxDAO;
            }
        }
        #endregion

        public bool Open(PayBoxModel payBox, UsuarioModel user)
        {
            try
            {
                return PayBoxDAO.Open(ConvertModelToEntity(payBox, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Close(PayBoxModel payBox, UsuarioModel user)
        {
            try
            {
                return PayBoxDAO.Close(ConvertModelToEntity(payBox, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PayBoxModel VerifyPayBox()
        {
            try
            {
                var result = PayBoxDAO.VerifyPayBox();
                if (result.Rows.Count != 0)
                {
                    var model = ConvertEntityToModel(result.DataTableToList<HMA_PDV_CAI>().FirstOrDefault());
                    model.Aberto = true;
                    return model;
                }
                var model = new PayBoxModel();
                model.Aberto = false;

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region Private Methods
        private HMA_PDV_CAI ConvertModelToEntity(PayBoxModel payBox, UsuarioModel user)
        {
            try
            {
                var entity = new HMA_PDV_CAI();
                entity._ATV = Convert.ToInt32(payBox.StatusSelected);
                entity._ID = payBox.Id;
                entity._USR = user.Id;
                entity.DT_ABER = payBox.DataAbertura;
                entity.DT_FEC = payBox.DataFechamento;
                entity.VLR_FIN = payBox.ValorFechamento;
                entity.VLR_INI = payBox.ValorAbertura;
                
                return entity;

            }
            catch (Exception)
            {
                throw;
            }
        }
        private PayBoxModel ConvertEntityToModel(HMA_PDV_CAI payBox)
        {
            try
            {
                var model = new PayBoxModel();
                model.DataAbertura = payBox.DT_ABER;
                model.DataFechamento = payBox.DT_FEC;
                model.Id = payBox._ID;
                model.StatusSelected = payBox._ATV.ToString();
                model.ValorAbertura = payBox.VLR_INI;
                model.ValorFechamento = payBox.VLR_FIN;

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
