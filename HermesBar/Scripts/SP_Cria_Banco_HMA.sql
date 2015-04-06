--use master;
--EXEC SP_Cria_Banco_HMA;
-- DROP PROCEDURE SP_Cria_Banco_HMA;
CREATE PROCEDURE SP_Cria_Banco_HMA
AS
	IF EXISTS(SELECT * FROM sys.databases WHERE name = 'HermesBar')
	BEGIN
		DROP DATABASE HermesBar;
		CREATE DATABASE HermesBar;
		PRINT('Banco criado com sucesso!');
	END
	ELSE
	BEGIN
		CREATE DATABASE HermesBar;
		PRINT('Banco criado com sucesso!');
	END