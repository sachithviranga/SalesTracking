CREATE TABLE [dbo].[ProductPrice]
(
	[Id] INT IDENTITY(1,1)  NOT NULL PRIMARY KEY, 
    [ProductId] INT NOT NULL, 
    [SellPrice] DECIMAL(18, 2) NOT NULL, 
    [UnitPrice] DECIMAL(18, 2) NOT NULL, 
    [StartData] DATETIME2 NOT NULL, 
    [EndDate] DATETIME2 NULL, 
    [IsCurrent] BIT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT getdate(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL, 
    CONSTRAINT FK_ProductPrice FOREIGN KEY (ProductId)
    REFERENCES Product(Id)
)
