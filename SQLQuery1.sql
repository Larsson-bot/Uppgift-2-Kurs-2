CREATE TABLE Errands(
Id int IDENTITY (1,1) PRIMARY KEY NOT NULL,
CustomerName Nvarchar(30) NOT NULL,
ErrandCatagory nvarchar(30) NOT NULL,
ErrandStatus nvarchar(30) NOT NULL FOREIGN KEY REFERENCES [Status] (ErrandStatus),
ErrandDescription nvarchar(max) NOT NULL,
ErrandTime datetime2 NOT NULL
);
GO
