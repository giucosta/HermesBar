using DAO.Commom;
using ENTITY.Commom;
using HELPER;
using MODEL.Address;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BLL.Commom
{
    public class AddressBLL
    {
        private AddressDAO AddressDAO = Singleton<AddressDAO>.Instance();
        
        public HMA_END ConvertModelToEntity(EnderecoModel model, UsuarioModel usuario)
        {
            try
            {
                var entity = Singleton<HMA_END>.Instance();

                entity._ID = model.Id;
                entity._USR = usuario.Id;
                entity.BAI = model.Bairro;
                entity.CEP = model.Cep;
                entity.CID = model.Cidade;
                entity.COMP = model.Complemento;
                entity.NUM = model.Numero;
                entity.RUA = model.Rua;
                entity.UF = Convert.ToInt32(model.UfSelected);

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EnderecoModel ConvertEntityToModel(HMA_END end)
        {
            try
            {
                var model = Singleton<EnderecoModel>.Instance();
                model.Bairro = end.BAI;
                model.Cep = end.CEP;
                model.Cidade = end.CID;
                model.Complemento = end.COMP;
                model.Id = end._ID;
                model.Numero = end.NUM;
                model.Rua = end.RUA;
                model.UfSelected = end.UF.ToString();

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public EnderecoModel GetStates(EnderecoModel model)
        {
            try
            {
                var result = AddressDAO.GetStates();

                model.UfList = new List<SelectListItem>();
                for (int i = 0; i < result.Rows.Count; i++)
                    model.UfList.Add(new SelectListItem() { Text = result.Rows[i]["SIG"].ToString(), Value = result.Rows[i]["_ID"].ToString() });

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
