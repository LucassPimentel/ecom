USE eauth;
GO

-- Criar Login
CREATE LOGIN ad WITH PASSWORD = 'P@lmeira$';

-- Criar Usuário para o login
CREATE USER ad FOR LOGIN ad;

-- Adicionar o usuário à role de leitura e escrita
EXEC sp_addrolemember 'db_datareader', 'ad'; -- leitura
EXEC sp_addrolemember 'db_datawriter', 'ad'; -- escrita