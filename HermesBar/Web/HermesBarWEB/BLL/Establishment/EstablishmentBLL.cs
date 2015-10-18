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
using HELPER;

namespace BLL.Establishment
{
    public class EstablishmentBLL
    {
        #region Singleton
        private EstablishmentDAO EstablishmentDAO = Singleton<EstablishmentDAO>.Instance();
        private AddressBLL AddressBLL = Singleton<AddressBLL>.Instance();
        private ContactBLL ContactBLL = Singleton<ContactBLL>.Instance();
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
        public bool Insert(EstablishmentModel est, UsuarioModel user)
        {
            try
            {
                ProccessForInsert(ref est);
                var establishment = ConvertModelToEntity(est, user);
                var address = AddressBLL.ConvertModelToEntity(est.Endereco, user);
                var contact = ContactBLL.ConvertModelToEntity(est.Contato, user);

                return EstablishmentDAO.Insert(establishment, address, contact).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(EstablishmentModel est, UsuarioModel user)
        {
            try
            {
                ProccessForInsert(ref est);
                var establishment = ConvertModelToEntity(est, user);
                var address = AddressBLL.ConvertModelToEntity(est.Endereco, user);
                var contact = ContactBLL.ConvertModelToEntity(est.Contato, user);

                return EstablishmentDAO.Update(establishment, address, contact).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Inactive(EstablishmentModel est, UsuarioModel user)
        {
            try
            {
                return EstablishmentDAO.Inactive(ConvertModelToEntity(est, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Active(EstablishmentModel est, UsuarioModel user)
        {
            try
            {
                return EstablishmentDAO.Active(ConvertModelToEntity(est, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private void ProccessForInsert(ref EstablishmentModel est)
        {
            try
            {
                est.Cnpj = est.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                if (!string.IsNullOrEmpty(est.Contato.Telefone))
                    est.Contato.Telefone = est.Contato.Telefone.Replace("(", "").Replace(")", "").Replace("-", "");
                else
                    est.Contato.Telefone = "";
                if (!string.IsNullOrEmpty(est.Contato.Celular))
                    est.Contato.Celular = est.Contato.Celular.Replace("(", "").Replace(")", "").Replace("-", "");
                else
                    est.Contato.Celular = "";

                est.Endereco.Cep = est.Endereco.Cep.Replace("-", "");

                if (string.IsNullOrWhiteSpace(est.InscricaoEstadual))
                    est.InscricaoEstadual = "ISENTO";
                if (string.IsNullOrWhiteSpace(est.InscricaoMunicipal))
                    est.InscricaoMunicipal = "ISENTO";
            }
            catch (Exception)
            {
                throw;
            }
        }
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
                entity.MTR = Convert.ToInt32(est.MatrizSelected);

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
                model.MatrizSelected = est.MTR.ToString();

                model.Endereco = AddressBLL.ConvertEntityToModel(end);
                model.Contato = ContactBLL.ConvertEntityToModel(con);

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
