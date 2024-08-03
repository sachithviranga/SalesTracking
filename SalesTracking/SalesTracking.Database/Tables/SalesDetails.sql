CREATE TABLE [dbo].[SalesDetails]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ProductId] INT NOT NULL, 
    [SalesId] INT NOT NULL ,
    [StockBalanceId]  INT NOT NULL, 
    [Qty] INT NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL
    CONSTRAINT FK_SalesDetailsProductId FOREIGN KEY (ProductId)
    REFERENCES Product(Id),
    [PriceId] INT NOT NULL DEFAULT 0, 
    [SellPrice] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    [Amount] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    CONSTRAINT FK_SalesDetailsSalesId FOREIGN KEY (SalesId)
    REFERENCES Sales(Id),
    CONSTRAINT FK_SalesPriceId FOREIGN KEY (PriceId)
    REFERENCES ProductPrice(Id)
)
