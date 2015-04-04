using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorBancoHMA
{
    public class Connection
    {
        private SqlConnection conn = null;
        private SqlCommand command = null;
        private static string servidor = string.Empty;
        
        public Connection()
        {
            
        }
        public string CriarBanco(string nomeServidor)
        {
            servidor = nomeServidor;
            DataSet ds = new DataSet();
            try
            {
                StoredProcedureCriarBanco(nomeServidor);
                conn = new SqlConnection(@"Data Source=" + nomeServidor + @"; Integrated Security=True");
                SqlCommand cmd = new SqlCommand("SP_Cria_Banco_HMA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return "Banco criado com sucesso!";
            }
            catch (Exception ex)
            {
                return ex.InnerException.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
        public string ResetarBanco()
        {
            DataSet ds = new DataSet();
            try
            {
                StoredProcedureResetarBanco();
                conn = new SqlConnection(@"Data Source= " + servidor + @"; Database=HermesBar; Integrated Security=True");
                SqlCommand cmd = new SqlCommand("SP_Resetar_Banco_HMA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return "Banco resetado com sucesso!";
            }
            catch (Exception ex)
            {
                return ex.InnerException.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
        public string CriarBancoTeste()
        {
            DataSet ds = new DataSet();
            try
            {
                StoredProcedureCriarBancoTeste();
                conn = new SqlConnection(@"Data Source= " + servidor + @"; Database=HermesBar; Integrated Security=True");
                SqlCommand cmd = new SqlCommand("SP_Carrega_Valores_Teste", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return "Banco para teste criado com sucesso!";
            }
            catch (Exception ex)
            {
                return ex.InnerException.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
        public string CriarTabelas()
        {
            DataSet ds = new DataSet();
            try
            {
                StoredProcedureCriarTabelas();
                conn = new SqlConnection(@"Data Source= " + servidor + @"; Database=HermesBar; Integrated Security=True");
                SqlCommand cmd = new SqlCommand("SP_Gerencia_Tabelas_HMA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return "Tabelas criadas com sucesso!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
        private void StoredProcedureCriarBanco(string nomeServidor)
        {
            StringBuilder sbSP = new StringBuilder();
            sbSP.AppendLine("CREATE PROCEDURE SP_Cria_Banco_HMA AS IF EXISTS(SELECT * FROM sys.databases WHERE name = 'HermesBar') BEGIN DROP DATABASE HermesBar; CREATE DATABASE HermesBar; PRINT('Banco criado com sucesso!'); END ELSE BEGIN CREATE DATABASE HermesBar; PRINT('Banco criado com sucesso!'); END");
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=" + nomeServidor + @"; Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        private void StoredProcedureResetarBanco()
        {
            StringBuilder sbSP = new StringBuilder();
            sbSP.AppendLine(@"
                                CREATE PROCEDURE SP_Resetar_Banco_HMA
                                AS
	                                EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
	                                EXEC sp_MSForEachTable '
	                                IF OBJECTPROPERTY(object_id(''?''), ''TableHasForeignRef'') = 1
	                                DELETE FROM ?
	                                else
	                                TRUNCATE TABLE ?
	                                '
	                                EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source= " + servidor + @"; Database=HermesBar; Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        private void StoredProcedureCriarBancoTeste()
        {
            ResetarBanco();
            StringBuilder sbSP = new StringBuilder();
            sbSP.AppendLine(@"
                             CREATE PROCEDURE SP_Carrega_Valores_Teste
    AS
    EXEC SP_Resetar_Banco_HMA
    --
    INSERT INTO Login	VALUES('Administrador','uB4ejaoYjrs=',NULL);
    INSERT INTO Perfil	VALUES ('Administrador');
    INSERT INTO Usuario VALUES(1,1,'Administrador','A','giulianocosta@outlook.com');
    --
    INSERT INTO Contato VALUES('Giuliano Henrique Costa','(41)3226-5336','(41)9147-6537','giulianocosta@outlook.com',NULL);
    INSERT INTO Contato VALUES('Franciele Antqueves','(41)3333-3333','(41)9999-9999','emailteste1@emailteste1.com','www.google.com.br');
    INSERT INTO Contato VALUES('Anna Paula Queiroz','(41)4444-4444','(41)8888-8888','emailteste2@emailteste2.com','www.uol.com.br');
    INSERT INTO Contato VALUES('Phillip da Silva','(41)5555-5555','(41)7777-7777','emailteste3@emailteste3.com','www.up.com.br');
    --
	INSERT INTO EstiloAtracoes VALUES('Rock n Roll');
	INSERT INTO EstiloAtracoes VALUES('Rockabilly');
	INSERT INTO EstiloAtracoes VALUES('Country Music');
	INSERT INTO EstiloAtracoes VALUES('Southern Rock');
	--
    INSERT INTO Atracoes VALUES('Atração 1',1,1,'100,00','01:00:00');
    INSERT INTO Atracoes VALUES('Atração 2',2,1,'100,00','01:00:00');
    INSERT INTO Atracoes VALUES('Atração 3',3,1,'100,00','01:30:00');
    INSERT INTO Atracoes VALUES('Atração 4',4,1,'0,00','00:03:00');
    --
    INSERT INTO TipoFuncionario VALUES('Gerente');
    INSERT INTO TipoFuncionario VALUES('Garçom');
    INSERT INTO TipoFuncionario VALUES('Segurança');
    INSERT INTO TipoFuncionario VALUES('Cozinheiro(a)');
    --
    INSERT INTO Endereco VALUES('Rua Jornalista Paulino de Almeida',	'71',NULL,'Cajuru','82970-030','Curitiba','PR','Cobrança');
    INSERT INTO Endereco VALUES('Rua Anne Frank','10000','APTO 1201','Hauer','80000-000','Curitiba','PR','Entrega');
    INSERT INTO Endereco VALUES('Rua Marechal Floriano Peixoto','10',NULL,'Centro','82900-000','Curitiba','PR','Filial');
    INSERT INTO Endereco VALUES('Av Estados Unidos','245','SALA 201','Bacacheri','82900-000','Curitiba','PR','Filial');
    --
    INSERT INTO Funcionario VALUES('Funcionário Um Pereira','247.431.806-27','10.000.000-0','28-03-1989','0001','0020',1,1,1,'13-03-2015');
    INSERT INTO Funcionario VALUES('Funcionário Dois Pereira','631.080.853-28','10.000.000-0','28-03-1970','0001','0020',1,1,1,'13-02-2011');
    INSERT INTO Funcionario VALUES('Funcionário Três Pereira','271.389.587-14','10.000.000-0','28-03-1970','0001','0020',1,1,1,'13-02-2011');
    INSERT INTO Funcionario VALUES('Funcionário Quatro Pereira','558.383.283-55','10.000.000-0','28-03-1970','0001','0020',1,1,1,'13-02-2011');
    --
    INSERT INTO ConfigEstabelecimento VALUES('S','C','40','10');
    --
    INSERT INTO Fornecedor VALUES('Fornecedor Um Ltda','82.668.555/0001-70','Isento',2,2);
    INSERT INTO Fornecedor VALUES('Fornecedor Dois Ltda','75.867.577/0001-02','Isento',3,3);
    INSERT INTO Fornecedor VALUES('Fornecedor Três Ltda','24.520.499/0001-26','Isento',1,1);
    INSERT INTO Fornecedor VALUES('Fornecedor Quatro Ltda','75.767.708/0001-71','Isento',4,4);
    --
    INSERT INTO TipoProduto VALUES('Refrigerante 300ML','Refrigerantes com no máximo 300ML');
    INSERT INTO TipoProduto VALUES('Cerveja 600ML','Cervejas de garrafa de 600ML');
    INSERT INTO TipoProduto VALUES('Cerveja 350ML','Cervejas de lata com 350ML');
    INSERT INTO TipoProduto VALUES('Agua 200ML','Garrafas Pet de água com 200 ML');
    --
	INSERT INTO Marca VALUES('Heineken', 1);
	INSERT INTO Marca VALUES('Coca-Cola', 2);
	INSERT INTO Marca VALUES('Pepsi', 2);
	INSERT INTO Marca VALUES('Ouro Fino', 2);
	--
    INSERT INTO Produto VALUES('0001','011231414114','Coca-Cola lata 350ML','Coca lata',1,1,'Lata',1,100,80,150,'1,50','3,00','');
    INSERT INTO Produto VALUES('0002','011123123231414114','Skol lata 350ML','Skol lata',3,2,'Lata',			1,100,80,150,'1,80','4,00','');
    INSERT INTO Produto VALUES('0003','3123231414114','Skol Garrafa 600ML','Skol Garrafa',	2,2,'Garrafa',		1,100,80,150,'1,80','4,00','');
    --
	INSERT INTO Estoque VALUES(1,100,150,80);
	INSERT INTO Estoque VALUES(2,100,150,80);
	INSERT INTO Estoque VALUES(3,100,150,80);
	--
    INSERT INTO Estabelecimento VALUES('Hermes Bar e Restaurante','Hermes Bar','96.541.733/0001-02','ISENTO',1,1,1);");
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source= " + servidor + @"; Database=HermesBar; Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        private void StoredProcedureCriarTabelas()
        {
            StringBuilder sbSP = new StringBuilder();
            sbSP.AppendLine(@"CREATE PROCEDURE SP_Gerencia_Tabelas_HMA
        AS
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Login')
        BEGIN
	        EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
			DROP TABLE Endereco;
	        DROP TABLE Contato;
	        DROP TABLE Usuario;
	        DROP TABLE Login;
	        DROP TABLE Perfil;
	        DROP TABLE Atracoes;
	        DROP TABLE Funcionario;
	        DROP TABLE Estabelecimento;
			DROP TABLE ConfigEstabelecimento;
	        DROP TABLE TipoFuncionario;
			DROP TABLE Fornecedor;
	        DROP TABLE Produto;
	        DROP TABLE TipoProduto;
		    DROP TABLE EstiloAtracoes;
			DROP TABLE Marca;
            DROP TABLE Estoque;
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Perfil')
        BEGIN
	        DROP TABLE Perfil;
	        CREATE TABLE Perfil( Perfil varchar(100) not null, Id_Perfil INT IDENTITY(1,1) PRIMARY KEY);
        END
        ELSE 
        BEGIN
	        CREATE TABLE Perfil( Perfil varchar(100) not null, Id_Perfil INT IDENTITY(1,1) PRIMARY KEY);
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Login')
        BEGIN
	        DROP TABLE Login;
	        CREATE TABLE Login(Id_Login INT IDENTITY(1,1) PRIMARY KEY, Login varchar(20) not null, Senha varchar(100) not null, DataUltimoLogin DATETIME);
        END
        ELSE 
        BEGIN
	        CREATE TABLE Login(Id_Login INT IDENTITY(1,1) PRIMARY KEY, Login varchar(20) not null, Senha varchar(100) not null, DataUltimoLogin DATETIME);
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Usuario')
        BEGIN
	        DROP TABLE Usuario;
	        CREATE TABLE Usuario(Id_Usuario INT IDENTITY(1,1) PRIMARY KEY,Id_Login INT,Id_Perfil INT,Nome varchar(100) not null,Status varchar(1) not null, Email VARCHAR(100) NULL, FOREIGN KEY(Id_Login)REFERENCES Login(Id_Login),FOREIGN KEY(Id_Perfil)REFERENCES Perfil(Id_Perfil));
        END
        ELSE 
        BEGIN
	        CREATE TABLE Usuario(Id_Usuario INT IDENTITY(1,1) PRIMARY KEY,Id_Login INT,Id_Perfil INT,Nome varchar(100) not null,Status varchar(1) not null, Email VARCHAR(100) NULL, FOREIGN KEY(Id_Login)REFERENCES Login(Id_Login),FOREIGN KEY(Id_Perfil)REFERENCES Perfil(Id_Perfil));
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Contato')
        BEGIN
	        DROP TABLE Contato;
	        CREATE TABLE Contato(Id_Contato INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Telefone VARCHAR(15),Celular VARCHAR(15),Email VARCHAR(100),Site VARCHAR(100));
        END
        ELSE 
        BEGIN
	        CREATE TABLE Contato(Id_Contato INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Telefone VARCHAR(15),Celular VARCHAR(15),Email VARCHAR(100),Site VARCHAR(100));
        END
        --
	    IF EXISTS(SELECT name FROM sysobjects WHERE name = 'EstiloAtracoes')
	    BEGIN
		    DROP TABLE EstiloAtracoes;
		    CREATE TABLE EstiloAtracoes(Id_EstiloAtracoes INT IDENTITY(1,1) PRIMARY KEY, Estilo VARCHAR(100) NOT NULL);
	    END
	    ELSE
	    BEGIN
		    CREATE TABLE EstiloAtracoes(Id_EstiloAtracoes INT IDENTITY(1,1) PRIMARY KEY, Estilo VARCHAR (100) NOT NULL);
	    END
	    --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Atracoes')
        BEGIN
	        DROP TABLE Atracoes;
	        CREATE TABLE Atracoes(Id_Atracoes INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Id_EstiloAtracoes INT ,Id_Contato INT, Ultimo_Valor_Cobrado VARCHAR(10), Tempo_Show VARCHAR(10),FOREIGN KEY(Id_EstiloAtracoes) REFERENCES EstiloAtracoes(Id_EstiloAtracoes), FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato));
        END
        ELSE 
        BEGIN
	        CREATE TABLE Atracoes(Id_Atracoes INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Id_EstiloAtracoes INT ,Id_Contato INT, Ultimo_Valor_Cobrado VARCHAR(10), Tempo_Show VARCHAR(10),FOREIGN KEY(Id_EstiloAtracoes) REFERENCES EstiloAtracoes(Id_EstiloAtracoes), FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato));
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'TipoFuncionario')
        BEGIN
	        DROP TABLE TipoFuncionario;
	        CREATE TABLE TipoFuncionario(Id_TipoFuncionario INT IDENTITY(1,1) PRIMARY KEY,Tipo VARCHAR(100));
        END
        ELSE 
        BEGIN
	        CREATE TABLE TipoFuncionario(Id_TipoFuncionario INT IDENTITY(1,1) PRIMARY KEY,Tipo VARCHAR(100));
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Endereco')
        BEGIN
	        DROP TABLE Endereco;
	        CREATE TABLE Endereco(Id_Endereco INT IDENTITY(1,1) PRIMARY KEY,Rua VARCHAR(100),Numero VARCHAR(10),Complemento VARCHAR(100) NULL,Bairro VARCHAR(100),Cep VARCHAR(10),Cidade VARCHAR(100),Estado VARCHAR(2),TipoEndereco VARCHAR(40));
        END
        ELSE 
        BEGIN
	        CREATE TABLE Endereco(Id_Endereco INT IDENTITY(1,1) PRIMARY KEY,Rua VARCHAR(100),Numero VARCHAR(10),Complemento VARCHAR(100),Bairro VARCHAR(100),Cep VARCHAR(10),Cidade VARCHAR(100),Estado VARCHAR(2),TipoEndereco VARCHAR(40));
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Funcionario')
        BEGIN
	        DROP TABLE Funcionario;
	        CREATE TABLE Funcionario(Id_Funcionario INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Cpf VARCHAR(15),Rg VARCHAR(15),DataNascimento DATETIME,CarteiraTrabalho VARCHAR(20),Serie VARCHAR(10),Id_Endereco INT,Id_TipoFuncionario INT,Id_Contato INT,DataAdmissao DATE,FOREIGN KEY(Id_Contato) REFERENCES Contato(Id_Contato),FOREIGN KEY(Id_Endereco) REFERENCES Endereco(Id_Endereco),FOREIGN KEY(Id_TipoFuncionario) REFERENCES TipoFuncionario(Id_TipoFuncionario));
        END
        ELSE 
        BEGIN
	        CREATE TABLE Funcionario(Id_Funcionario INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Cpf VARCHAR(15),Rg VARCHAR(15),DataNascimento DATETIME,CarteiraTrabalho VARCHAR(20),Serie VARCHAR(10),Id_Endereco INT,Id_TipoFuncionario INT,Id_Contato INT,DataAdmissao DATE,FOREIGN KEY(Id_Contato) REFERENCES Contato(Id_Contato),FOREIGN KEY(Id_Endereco) REFERENCES Endereco(Id_Endereco),FOREIGN KEY(Id_TipoFuncionario) REFERENCES TipoFuncionario(Id_TipoFuncionario));
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'ConfigEstabelecimento')
        BEGIN
	        DROP TABLE ConfigEstabelecimento;
	        CREATE TABLE ConfigEstabelecimento(Id_ConfigEstabelecimento int IDENTITY(1,1) PRIMARY KEY,AgruparItensQuantidade varchar(1),TipoSistema varchar(1),QuantidadeMesa varchar(2),TaxaServico varchar(2));
        END
        ELSE 
        BEGIN
	        CREATE TABLE ConfigEstabelecimento(Id_ConfigEstabelecimento int IDENTITY(1,1) PRIMARY KEY,AgruparItensQuantidade varchar(1),TipoSistema varchar(1),QuantidadeMesa varchar(2),TaxaServico varchar(2));
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Estabelecimento')
        BEGIN
	        DROP TABLE Estabelecimento;
	        CREATE TABLE Estabelecimento(Id_Estabelecimento int IDENTITY(1,1) PRIMARY KEY,RazaoSocial VARCHAR(100),NomeFantasia VARCHAR(100),Cnpj VARCHAR(30),InscricaoEstadual VARCHAR(30),Id_Endereco int,Id_Contato INT, Id_ConfigEstabelecimento INT ,FOREIGN KEY(Id_Endereco)REFERENCES Endereco(Id_Endereco),FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato), FOREIGN KEY(Id_ConfigEstabelecimento)REFERENCES ConfigEstabelecimento(Id_ConfigEstabelecimento));
        END
        ELSE 
        BEGIN
	        CREATE TABLE Estabelecimento(Id_Estabelecimento int IDENTITY(1,1) PRIMARY KEY,RazaoSocial VARCHAR(100),NomeFantasia VARCHAR(100),Cnpj VARCHAR(30),InscricaoEstadual VARCHAR(30),Id_Endereco int,Id_Contato INT, Id_ConfigEstabelecimento INT ,FOREIGN KEY(Id_Endereco)REFERENCES Endereco(Id_Endereco),FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato), FOREIGN KEY(Id_ConfigEstabelecimento)REFERENCES ConfigEstabelecimento(Id_ConfigEstabelecimento));
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'TipoProduto')
        BEGIN
	        DROP TABLE TipoProduto;
	        CREATE TABLE TipoProduto(Id_TipoProduto INT IDENTITY(1,1) PRIMARY KEY,Tipo VARCHAR(100),Descricao VARCHAR(200));
        END
        ELSE 
        BEGIN
	        CREATE TABLE TipoProduto(Id_TipoProduto INT IDENTITY(1,1) PRIMARY KEY,Tipo VARCHAR(100),Descricao VARCHAR(200));
        END
        --
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Fornecedor')
        BEGIN
	        DROP TABLE Fornecedor;
	        CREATE TABLE Fornecedor(Id_Fornecedor INT IDENTITY(1,1) PRIMARY KEY,RazaoSocial VARCHAR(100) NOT NULL,Cpj VARCHAR(20),InscricaoEstadual VARCHAR(20),Id_Contato INT,Id_Endereco INT,FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato),FOREIGN KEY(Id_Endereco)REFERENCES Endereco(Id_Endereco));
        END
        ELSE 
        BEGIN
	        CREATE TABLE Fornecedor(Id_Fornecedor INT IDENTITY(1,1) PRIMARY KEY,RazaoSocial VARCHAR(100) NOT NULL,Cpj VARCHAR(20),InscricaoEstadual VARCHAR(20),Id_Contato INT,Id_Endereco INT,FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato),FOREIGN KEY(Id_Endereco)REFERENCES Endereco(Id_Endereco));
        END
        --
		IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Marca')
		BEGIN
			DROP TABLE Marca;
			CREATE TABLE Marca(Id_Marca INT IDENTITY(1,1) PRIMARY KEY, Marca VARCHAR(100), Id_Fornecedor INT, FOREIGN KEY(Id_Fornecedor) REFERENCES Fornecedor(Id_Fornecedor));
		END
		ELSE
		BEGIN
			CREATE TABLE Marca(Id_Marca INT IDENTITY(1,1) PRIMARY KEY, Marca VARCHAR(100), Id_Fornecedor INT, FOREIGN KEY(Id_Fornecedor) REFERENCES Fornecedor(Id_Fornecedor));
		END
		--
        IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Produto')
        BEGIN
	        DROP TABLE Produto;
	        CREATE TABLE Produto(Id_Produto INT IDENTITY(1,1) PRIMARY KEY,CodigoOriginal VARCHAR(20),CodigoBarras VARCHAR(50),Nome VARCHAR(100),NomeReduzido VARCHAR(100),Id_TipoProduto INT,Id_Marca INT ,Unidade VARCHAR(10),Id_Fornecedor INT,QuantidadeEstoque INT,EstoqueMinimo INT,EstoqueIdeal INT,ValorCusto VARCHAR(10),ValorVenda VARCHAR(10),Observacao VARCHAR(200),FOREIGN KEY(Id_TipoProduto)REFERENCES TipoProduto(Id_TipoProduto),FOREIGN KEY(Id_Fornecedor)REFERENCES Fornecedor(Id_Fornecedor), FOREIGN KEY(Id_Marca) REFERENCES Marca(Id_Marca));
        END
        ELSE 
        BEGIN
	        CREATE TABLE Produto(Id_Produto INT IDENTITY(1,1) PRIMARY KEY,CodigoOriginal VARCHAR(20),CodigoBarras VARCHAR(50),Nome VARCHAR(100),NomeReduzido VARCHAR(100),Id_TipoProduto INT,Id_Marca INT ,Unidade VARCHAR(10),Id_Fornecedor INT,QuantidadeEstoque INT,EstoqueMinimo INT,EstoqueIdeal INT,ValorCusto VARCHAR(10),ValorVenda VARCHAR(10),Observacao VARCHAR(200),FOREIGN KEY(Id_TipoProduto)REFERENCES TipoProduto(Id_TipoProduto),FOREIGN KEY(Id_Fornecedor)REFERENCES Fornecedor(Id_Fornecedor), FOREIGN KEY(Id_Marca) REFERENCES Marca(Id_Marca));
        END
		--
		IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Estoque')
		BEGIN
			DROP TABLE Estoque;
			CREATE TABLE Estoque(Id_Estoque INT IDENTITY(1,1) PRIMARY KEY,Id_Produto INT,QuantidadeEstoque INT,QuantidadeIdeal INT,QuantidadeMinima INT,FOREIGN KEY(Id_Produto)REFERENCES Produto(Id_Produto));
		END
		ELSE
		BEGIN
			CREATE TABLE Estoque(Id_Estoque INT IDENTITY(1,1) PRIMARY KEY,Id_Produto INT,QuantidadeEstoque INT,QuantidadeIdeal INT,QuantidadeMinima INT,FOREIGN KEY(Id_Produto)REFERENCES Produto(Id_Produto));
		END");
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source= " + servidor + @"; Database=HermesBar; Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
