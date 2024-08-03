CREATE TABLE [dbo].[StockPurchasePayment]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [StockPurchaseId] INT NOT NULL, 
    [PaymentTypeId] INT NOT NULL, 
    [ChequeNo] NUMERIC NULL, 
    [ChequeDate] DATETIME2 NULL, 
    [Amount] DECIMAL(18,2) NOT NULL DEFAULT 0,
    [Note] NVARCHAR(500) NULL,
    [IsApproved] BIT NOT NULL DEFAULT 0, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL ,
    [ApprovedDate] DATETIME2 NULL, 
    [ApprovedBy] VARCHAR(128) NULL
    CONSTRAINT FK_StockPurchasePaymentStockPurchaseId FOREIGN KEY (StockPurchaseId)
    REFERENCES StockPurchase(Id),
    CONSTRAINT FK_StockPurchasePaymentPaymentTypeId FOREIGN KEY (PaymentTypeId)
    REFERENCES PaymentType(Id)
)
