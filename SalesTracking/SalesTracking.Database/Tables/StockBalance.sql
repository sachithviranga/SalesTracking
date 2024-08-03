CREATE TABLE [dbo].[StockBalance]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY ,
	[ReferenceType] INT NOT NULL,
	[ReferenceId] INT NOT NULL ,
	[ReferenceLineId] INT NOT NULL,
	[BatchId] NVARCHAR(100) NOT NULL,
	[ProductId] INT NOT NULL, 
	[Qty] INT NOT NULL,
	[UnitPrice] DECIMAL(18, 2) NULL, 
    [SellPrice] DECIMAL(18, 2) NULL, 
	[TransactionDate] DATETIME2 NOT NULL DEFAULT getdate(), 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [CreateBy] VARCHAR(128) NOT NULL,
	CONSTRAINT FK_StockBalanceProductId FOREIGN KEY (ProductId)
    REFERENCES Product(Id)
)
