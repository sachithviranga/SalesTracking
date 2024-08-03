CREATE TABLE [dbo].[StockPurchaseDetails]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ProductId] INT NOT NULL,
    [Qty] INT NOT NULL, 
    [UnitPrice] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    [SellPrice] DECIMAL(18, 2) NULL, 
    [StockPurchaseId] INT NOT NULL,

    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT getdate(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL , 
    [UpdateBy] VARCHAR(128) NULL
    CONSTRAINT FK_PurchaseId FOREIGN KEY (StockPurchaseId)
    REFERENCES StockPurchase(Id),
    [Amount] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    CONSTRAINT FK_PurchaseProduct FOREIGN KEY (ProductId)
    REFERENCES Product(Id)
)
