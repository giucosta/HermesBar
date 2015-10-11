using HELPER;
using HermesBarWCF.DataContracts;
using HermesBarWCF.IContract;
using MODEL.Address;
using MODEL.Client;
using MODEL.Commom;
using MODEL.Employee;
using MODEL.Establishment;
using MODEL.Event;
using MODEL.PDV.Client;
using MODEL.PDV.PayBox;
using MODEL.Product;
using MODEL.Supplier;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HermesBarWCF
{
    public class ProductService : IProduct
    {
        public Product Product = Singleton<Product>.Instance();

        public bool Insert(ProdutoModel product, UsuarioModel user)
        {
            return Product.Insert(product, user);
        }
        public List<ProdutoModel> Get()
        {
            return Product.Get();
        }
        public ProdutoModel GetId(int id, int ativo)
        {
            return Product.GetId(id, ativo);
        }
        public bool Active(ProdutoModel product, UsuarioModel user)
        {
            return Product.Active(product, user);
        }
        public bool Inactive(ProdutoModel product, UsuarioModel user)
        {
            return Product.Inactive(product, user);
        }
        public int GetNextCode()
        {
            return Product.GetNextCode();
        }
        public bool Update(ProdutoModel product, UsuarioModel user)
        {
            return Product.Update(product, user);
        }
    }
    public class LoginService : ILogin
    {
        private Login Login = Singleton<Login>.Instance();

        public UsuarioModel EfetuarLogin(UsuarioModel user)
        {
            return Login.EfetuarLogin(user);
        }
    }
    public class EstablishmentService : IEstablishment
    {
        private Establishment Establishment = Singleton<Establishment>.Instance();
        public List<EstablishmentModel> Get(EstablishmentModel establishment, UsuarioModel user)
        {
            return Establishment.Get(establishment, user);
        }
        public bool Insert(EstablishmentModel establishment, UsuarioModel user)
        {
            return Establishment.Insert(establishment, user);
        }
        public bool Update(EstablishmentModel establishment, UsuarioModel user)
        {
            return Establishment.Update(establishment, user);
        }
        public bool Inactive(EstablishmentModel establishment, UsuarioModel user)
        {
            return Establishment.Inactive(establishment, user);
        }
        public bool Active(EstablishmentModel establishment, UsuarioModel user)
        {
            return Establishment.Active(establishment, user);
        }
    }
    public class AddressService : IAddress
    {
        private Address Address = Singleton<Address>.Instance();

        public EnderecoModel GetStates(EnderecoModel address)
        {
            return Address.GetStates(address);
        }
    }
    public class ClientService : IClient
    {
        private Client Client = Singleton<Client>.Instance();

        public List<ClientModel> Get(ClientModel client, UsuarioModel user)
        {
            return Client.Get(client, user);
        }
        public bool Insert(ClientModel client, UsuarioModel user, bool cadastroRapido)
        {
            return Client.Insert(client, user, cadastroRapido);
        }
        public bool Inactive(ClientModel client, UsuarioModel user)
        {
            return Client.Inactive(client, user);
        }
        public bool Active(ClientModel client, UsuarioModel user)
        {
            return Client.Active(client, user);
        }
        public bool Update(ClientModel client, UsuarioModel user)
        {
            return Client.Update(client, user);
        }
    }
    public class EventService : IEvent
    {
        private Event Event = Singleton<Event>.Instance();

        public List<EventModel> Get(EventModel evento, UsuarioModel user)
        {
            return Event.Get(evento, user);
        }
        public bool Insert(EventModel evento, UsuarioModel user)
        {
            return Event.Insert(evento, user);
        }
        public bool Update(EventModel evento, UsuarioModel user)
        {
            return Event.Update(evento, user);
        }
    }
    public class EmployeeService : IEmployee
    {
        private Employee Employee = Singleton<Employee>.Instance();

        public List<EmployeeModel> Get(EmployeeModel model, UsuarioModel user)
        {
            return Employee.Get(model, user);
        }
        public bool Insert(EmployeeModel model, UsuarioModel user)
        {
            return Employee.Insert(model, user);
        }
        public List<TypeEmployeeModel> GetTypes()
        {
            return Employee.GetTypes();
        }
        public List<PlaceEmployeeModel> GetPlaces()
        {
            return Employee.GetPlaces();
        }
        public bool Update(EmployeeModel model, UsuarioModel user)
        {
            return Employee.Update(model, user);
        }
        public bool Active(EmployeeModel model, UsuarioModel user)
        {
            return Employee.Active(model, user);
        }
        public bool Inactive(EmployeeModel model, UsuarioModel user)
        {
            return Employee.Inactive(model, user);
        }
    }
    public class PayBoxService : IPayBox
    {
        private PayBox PayBox = Singleton<PayBox>.Instance();
        public bool Open(PayBoxModel payBox, UsuarioModel user)
        {
            return PayBox.Open(payBox, user);
        }
        public PayBoxModel VerifyPayBox()
        {
            return PayBox.VerifyPayBox();
        }
        public bool Close(PayBoxModel payBox, UsuarioModel user)
        {
            return PayBox.Close(payBox, user);
        }
        public bool Reinforcement(PayBoxModel payBox, UsuarioModel user)
        {
            return PayBox.Reinforcement(payBox, user);
        }
        public bool Depletion(PayBoxModel payBox, UsuarioModel user)
        {
            return PayBox.Depletion(payBox, user);
        }
    }
    public class PdvClientService : IPdvClient
    {
        private PdvClient PdvClient = Singleton<PdvClient>.Instance();
        
        public int Insert(PdvClientModel client, UsuarioModel user)
        {
            return PdvClient.Insert(client, user);
        }
        public string GetCard(PdvClientModel client, UsuarioModel user)
        {
            return PdvClient.GetCard(client, user);
        }
        public bool Order(string cartaoCliente, string codigoAtendente, string nomeProduto, string quantidade, UsuarioModel user, int idCaixa)
        {
            return PdvClient.Order(cartaoCliente, codigoAtendente, nomeProduto, quantidade, user, idCaixa);
        }
        public List<PdvFechamentoClientModel> Close(PdvClientModel client)
        {
            return PdvClient.Close(client);
        }
        public bool CloseCommands(PdvClientModel client, UsuarioModel user)
        {
            return PdvClient.CloseCommands(client, user);
        }
    }
    public class UserService : IUser
    {
        private User User = Singleton<User>.Instance();
        public List<UsuarioModel> Get(UsuarioModel user)
        {
            return User.Get(user);
        }
        public bool Insert(UsuarioModel user)
        {
            return User.Insert(user);
        }
        public bool Update(UsuarioModel user)
        {
            return User.Update(user);
        }
        public bool Active(UsuarioModel user)
        {
            return User.Active(user);
        }
        public bool Inactive(UsuarioModel user)
        {
            return User.Inactive(user);
        }
    }
    public class ProfileService : IProfile
    {
        private Profile Profile = Singleton<Profile>.Instance();

        public List<PerfilModel> Get()
        {
            return Profile.Get();
        }
    }
    public class SupplierService : ISupplier
    {
        private Supplier Supplier = Singleton<Supplier>.Instance();
        public bool Insert(FornecedorModel supplier, UsuarioModel user)
        {
            try
            {
                return Supplier.Insert(supplier, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<FornecedorModel> Get(FornecedorModel model, UsuarioModel user)
        {
            try
            {
                return Supplier.Get(model, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(FornecedorModel supplier, UsuarioModel user)
        {
            return Supplier.Update(supplier, user);
        }
    }
    public class ProductTypeService : IProductType
    {
        private ProductType ProductType = Singleton<ProductType>.Instance();
        public List<TipoModel> Get()
        {
            try
            {
                return ProductType.Get();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TipoModel GetId(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return ProductType.GetId(tipo, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return ProductType.Insert(tipo, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(TipoModel tipo, UsuarioModel user)
        {
            try
            {
                return ProductType.Update(tipo, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class ProductUnitySizeService : IProductUnitySize
    {
        private ProductUnitySize ProductUnitySize = Singleton<ProductUnitySize>.Instance();
        public List<UnidadeMedidaModel> Get()
        {
            try
            {
                return ProductUnitySize.Get();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(UnidadeMedidaModel model, UsuarioModel user)
        {
            try
            {
                return ProductUnitySize.Insert(model, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class EmailService : IEmail
    {
        private Email Email = Singleton<Email>.Instance();
        public LayoutModel Get()
        {
            return Email.Get();
        }
    }
}
