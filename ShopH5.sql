CREATE DATABASE SHOPTHUCHANH
GO
USE SHOPTHUCHANH
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Username VARCHAR(20) NOT NULL UNIQUE,
    _Password VARCHAR(20) NOT NULL,
    Role INT DEFAULT 1
);

drop table users

GO
-- Tạo bảng Category
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
);

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    Price DECIMAL(18, 2) NOT NULL,
	Img VARCHAR (MAX),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME NOT NULL,
    TotalPrice DECIMAL(18, 2) NOT NULL
);

CREATE TABLE OrderDetails (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT,
    ProductId INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- INSERT CATEGORIES
GO
SET IDENTITY_INSERT Categories ON;
GO
INSERT INTO Categories(Id, Name)
VALUES
(1, N'Strawberry'),
(2, N'Berry'),
(3, N'Lemon')
GO
SET IDENTITY_INSERT Categories OFF;
SELECT * FROM Categories


--INSERT PRODUCT
select * from Categories
select * from Products
GO
INSERT INTO Products (Name, Price, Img, CategoryId, Description)
VALUES
(N'StrawberryL', 85, 'https://themewagon.github.io/fruitkha/assets/img/products/product-img-1.jpg', 1, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dicta sint dignissimos, rem commodi cum voluptatem quae reprehenderit repudiandae ea tempora incidunt ipsa, quisquam animi perferendis eos eum modi! Tempora, earum.'),
(N'StrawberryM', 90, 'https://themewagon.github.io/fruitkha/assets/img/products/product-img-6.jpg', 1, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dicta sint dignissimos, rem commodi cum voluptatem quae reprehenderit repudiandae ea tempora incidunt ipsa, quisquam animi perferendis eos eum modi! Tempora, earum.')

GO
INSERT INTO Products (Name, Price, Img, CategoryId, Description)
VALUES
(N'BerryL', 79, 'https://themewagon.github.io/fruitkha/assets/img/products/product-img-2.jpg', 2, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dicta sint dignissimos, rem commodi cum voluptatem quae reprehenderit repudiandae ea tempora incidunt ipsa, quisquam animi perferendis eos eum modi! Tempora, earum.'),
(N'BerryM', 99, 'https://themewagon.github.io/fruitkha/assets/img/products/product-img-5.jpg', 2, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dicta sint dignissimos, rem commodi cum voluptatem quae reprehenderit repudiandae ea tempora incidunt ipsa, quisquam animi perferendis eos eum modi! Tempora, earum.')

GO
INSERT INTO Products (Name, Price, Img, CategoryId, Description)
VALUES
(N'Lemon', 109, 'https://themewagon.github.io/fruitkha/assets/img/products/product-img-3.jpg', 3, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dicta sint dignissimos, rem commodi cum voluptatem quae reprehenderit repudiandae ea tempora incidunt ipsa, quisquam animi perferendis eos eum modi! Tempora, earum.'),
(N'LemonM', 119, 'https://themewagon.github.io/fruitkha/assets/img/products/product-img-4.jpg', 3, 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dicta sint dignissimos, rem commodi cum voluptatem quae reprehenderit repudiandae ea tempora incidunt ipsa, quisquam animi perferendis eos eum modi! Tempora, earum.')

--Select * from Products

INSERT INTO Users(Name, Username, _Password)
VALUES ('John', 'a', '1');
INSERT INTO Users(Name, Username, _Password)
VALUES ('Hena', 'b', '1');

INSERT INTO Users(Name, Username, _Password,Role) 
VALUES ('admin', 'c', '1','0');

select * from Users
