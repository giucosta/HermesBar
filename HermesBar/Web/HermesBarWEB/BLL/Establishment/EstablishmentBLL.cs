using DAO.Establishment;
using ENTITY.Establishment;
using MODEL.Establishment;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using ENTITY.Commom;
using BLL.Commom;

namespace BLL.Establishment
{
    public class EstablishmentBLL
    {
        #region Singleton
        private EstablishmentDAO _estDAO = null;
        private EstablishmentDAO EstablishmentDAO
        {
            get
            {
                if (_estDAO == null)
                    _estDAO = new EstablishmentDAO();
                return _estDAO;
            }
        }
        private EnderecoBLL _enderecoBLL = null;
        private EnderecoBLL EnderecoBLL
        {
            get
            {
                if (_enderecoBLL == null)
                    _enderecoBLL = new EnderecoBLL();
                return _enderecoBLL;
            }
        }
        private ContatoBLL _contatoBLL = null;
        private ContatoBLL ContatoBLL
        {
            get
            {
                if (_contatoBLL == null)
                    _contatoBLL = new ContatoBLL();
                return _contatoBLL;
            }
        }
        #endregion
        public List<EstablishmentModel> Get(EstablishmentModel est, UsuarioModel user)
        {
            try
            {
                var result = EstablishmentDAO.Get(ConvertModelToEntity(est, user));
                var establisment = result.Tables[0].DataTableToList<HMA_EST>();
                var address = result.Tables[1].DataTableToList<HMA_END>();
                var contact = result.Tables[2].DataTableToList<HMA_CON>();

                var list = new List<EstablishmentModel>();
                for (int i = 0; i < establisment.Count; i++)
                    list.Add(ConvertEntityToModel(establisment[i], address[i], contact[i]));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private HMA_EST ConvertModelToEntity(EstablishmentModel est, UsuarioModel user)
        {
            try
            {
                var entity = new HMA_EST();
                entity._ID = est.Id;
                entity._ATV = Convert.ToInt32(est.StatusSelected);
                entity._USR = user.Id;
                entity.CNPJ = est.Cnpj;
                entity.FAN = est.NomeFantasia;
                entity.INSEST = est.InscricaoEstadual;
                entity.INSMUN = est.InscricaoMunicipal;
                entity.QUANT_CLI = est.QuantidadeMaximoPessoas;
                entity.QUANT_MESA = est.QuantidadeMesas;
                entity.RAZ = est.RazaoSocial;

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private EstablishmentModel ConvertEntityToModel(HMA_EST est, HMA_END end, HMA_CON con)
        {
            try
            {
                var model = new EstablishmentModel();
                model.Cnpj = est.CNPJ;
                model.Id = est._ID;
                model.InscricaoEstadual = est.INSEST;
                model.InscricaoMunicipal = est.INSMUN;
                model.NomeFantasia = est.FAN;
                model.QuantidadeMaximoPessoas = est.QUANT_CLI;
                model.QuantidadeMesas = est.QUANT_MESA;
                model.RazaoSocial = est.RAZ;
                model.StatusSelected = est._ATV.ToString();

                model.Endereco = EnderecoBLL.ConvertEntityToModel(end);
                model.Contato = ContatoBLL.ConvertEntityToModel(con);

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
