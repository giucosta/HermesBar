using HermesBarWCF.DataContracts;
using HermesBarWCF.IContract;
using MODEL.Address;
using MODEL.Client;
using MODEL.Employee;
using MODEL.Establishment;
using MODEL.Event;
using MODEL.PDV.Client;
using MODEL.PDV.PayBox;
using MODEL.Product;
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
        private Produtos _produto = null;
        public Produtos Produtos
        {
            get
            {
                if (_produto == null)
                    _produto = new Produtos();
                return _produto;
            }
        }

        public bool Insert(ProdutoModel produto, UsuarioModel usuario)
        {
            return Produtos.Insert(produto, usuario);
        }
        public List<ProdutoModel> Get()
        {
            return Produtos.Get();
        }
        public ProdutoModel GetId(int id, int ativo)
        {
            return Produtos.GetId(id, ativo);
        }
        public bool Active(ProdutoModel produto, UsuarioModel usuario)
        {
            return Produtos.Active(produto, usuario);
        }
        public bool Inactive(ProdutoModel produto, UsuarioModel usuario)
        {
            return Produtos.Inactive(produto, usuario);
        }
        public int GetNextCode()
        {
            return Produtos.GetNextCode();
        }
        public bool Update(ProdutoModel produto, UsuarioModel usuario)
        {
            return Produtos.Update(produto, usuario);
        }
    }
    public class LoginService : ILogin
    {
        private Login _login = null;
        private Login Login
        {
            get
            {
                if (_login == null)
                    _login = new Login();
                return _login;
            }
        }

        public UsuarioModel EfetuarLogin(UsuarioModel usuario)
        {
            return Login.EfetuarLogin(usuario);
        }
    }
    public class EstabelecimentoService : IEstablishment
    {
        private Establishment _estabelecimento = null;
        private Establishment Estabelecimento
        {
            get
            {
                if (_estabelecimento == null)
                    _estabelecimento = new Establishment();
                return _estabelecimento;
            }
        }
        public List<EstablishmentModel> Get(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return Estabelecimento.Get(estabelecimento, usuario);
        }
        public bool Insert(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return Estabelecimento.Insert(estabelecimento, usuario);
        }
        public bool Update(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return Estabelecimento.Update(estabelecimento, usuario);
        }
        public bool Inactive(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return Estabelecimento.Inactive(estabelecimento, usuario);
        }
        public bool Active(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return Estabelecimento.Active(estabelecimento, usuario);
        }
    }
    public class EnderecoService : IAddress
    {
        private Address _address = null;
        private Address Address
        {
            get
            {
                if (_address == null)
                    _address = new Address();
                return _address;
            }
        }

        public EnderecoModel GetStates(EnderecoModel endereco)
        {
            return Address.GetStates(endereco);
        }
    }
    public class ClienteService : IClient
    {
        private Client _client = null;
        private Client Client
        {
            get
            {
                if (_client == null)
                    _client = new Client();
                return _client;
            }
        }

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
    public class EventoService : IEvent
    {
        private Event _event = null;
        private Event Event
        {
            get
            {
                if (_event == null)
                    _event = new Event();
                return _event;
            }
        }

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
    public class FuncionarioService : IEmployee
    {
        private Employee _employee = null;
        private Employee Employee
        {
            get
            {
                if (_employee == null)
                    _employee = new Employee();
                return _employee;
            }
        }

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
    public class CaixaService : IPayBox
    {
        private PayBox _payBox = null;
        private PayBox PayBox
        {
            get
            {
                if (_payBox == null)
                    _payBox = new PayBox();
                return _payBox;
            }
        }
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
    public class PdvClienteService : IPdvClient
    {
        private PdvClient _pdv = null;
        private PdvClient PdvClient
        {
            get
            {
                if (_pdv == null)
                    _pdv = new PdvClient();
                return _pdv;
            }
        }
        public bool Insert(PdvClientModel client, UsuarioModel user)
        {
            return PdvClient.Insert(client, user);
        }
        public string GetCar(PdvClientModel client, UsuarioModel user)
        {
            return PdvClient.GetCar(client, user);
        }
        public bool Pedido(string cartaoCliente, string codigoAtendente, string nomeProduto, string quantidade, UsuarioModel user, int idCaixa)
        {
            return PdvClient.Pedido(cartaoCliente, codigoAtendente, nomeProduto, quantidade, user, idCaixa);
        }
        public List<PdvFechamentoClientModel> Fechamento(PdvClientModel client)
        {
            return PdvClient.Fechamento(client);
        }
    }
}
