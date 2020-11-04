CREATE TABLE Customer(
CustomerId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
CustomerName nvarchar (50) NOT NULL 
);
GO
CREATE TABLE [Status](
StatusId int PRIMARY KEY NOT NULL,
StatusDescription nvarchar(50) NOT NULL
);
CREATE TABLE Matter(
Id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
CustomerId int FOREIGN KEY REFERENCES Customer(CustomerId) NOT NULL,
MatterCatagory nvarchar(30) NOT NULL,
StatusId int FOREIGN KEY REFERENCES [Status](StatusId) NOT NULL,
MatterDescription nvarchar(max) NOT NULL,
[Time] datetime2 NOT NULL
);