--EXEC SP_Carrega_Valores_Teste
--DROP PROCEDURE SP_Carrega_Valores_Teste
CREATE PROCEDURE SP_Carrega_Valores_Teste -- INSERIR DADOS PARA TESTAR A APLICACAO
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
INSERT INTO Atracoes VALUES('Atração 1','Rock n Roll',1,'100,00','01:00:00');
INSERT INTO Atracoes VALUES('Atração 2','Rockabilly',1,	'100,00','01:00:00');
INSERT INTO Atracoes VALUES('Atração 3','Country Music',1,'100,00','01:30:00');
INSERT INTO Atracoes VALUES('Atração 4','Pagode',1,'0,00','00:03:00');
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
INSERT INTO Produto VALUES('0001','011231414114',		'Coca-Cola lata 350ML',		'Coca lata',		1,'Coca-Cola',	'Lata',			1,120,100,150,'1,50','3,00','');
INSERT INTO Produto VALUES('0002','011123123231414114',	'Skol lata 350ML',			'Skol lata',		3,'Skol',		'Lata',			1,120,100,150,'1,80','4,00','');
INSERT INTO Produto VALUES('0003','3123231414114',		'Skol Garrafa 600ML',		'Skol Garrafa',		2,'Skol',		'Garrafa',		1,120,100,150,'1,80','4,00','');
--
INSERT INTO Estabelecimento VALUES('Hermes Bar e Restaurante','Hermes Bar','96.541.733/0001-02','ISENTO',1,1);
--
INSERT INTO ConfigEstabelecimento_Estabelecimento VALUES(1,1);
