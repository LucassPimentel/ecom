USE ecom;
GO

CREATE TABLE dbo.Categories(
Id INT PRIMARY KEY IDENTITY,
Title VARCHAR(100),
CreatedAt DATETIME NULL,
UpdatedAt DATETIME NULL,
);
GO

CREATE TABLE dbo.Products(
Id INT PRIMARY KEY IDENTITY,
CategoryId INT,
Name VARCHAR(100) NOT NULL,
Description VARCHAR(500) NULL,
Price DECIMAL(10,2) NOT NULL,
Stock INT NOT NULL,
ImageUrl VARCHAR(300) NULL,
CreatedAt DATETIME NULL,
UpdatedAt DATETIME NULL,
FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);
GO