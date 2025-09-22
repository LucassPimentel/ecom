USE eauth;
GO

-- Criar Login
CREATE LOGIN ad WITH PASSWORD = 'P@lmeira$';

-- Criar Usuário para o login
CREATE USER ad FOR LOGIN ad;

-- Adicionar o usuário à role de leitura e escrita
EXEC sp_addrolemember 'db_datareader', 'ad'; -- leitura
EXEC sp_addrolemember 'db_datawriter', 'ad'; -- escrita

-- Inserir Roles iniciais
INSERT INTO eauth..Roles (Name, Description)
VALUES ('User', 'Role de usuário padrão'),
('Admin', 'Role Admin')

-- Inserir foreign key na tabela Users
ALTER TABLE Users
ADD IdRole INT NULL DEFAULT 1
CONSTRAINT FK_Users_Roles FOREIGN KEY (IdRole)
REFERENCES Roles