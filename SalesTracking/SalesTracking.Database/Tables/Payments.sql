CREATE TABLE [dbo].[Payments]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [InvoiceNo] NVARCHAR(50) NULL, 
    [PaymentTypeId] INT NOT NULL, 
    [ChequeNo] NUMERIC NULL, 
    [ChequeDate] DATETIME2 NULL, 
    [Amount] DECIMAL(18,2) NOT NULL DEFAULT 0,
    [SalesId] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL 
    CONSTRAINT FK_PaymentsSalesId FOREIGN KEY (SalesId)
    REFERENCES Sales(Id),
    CONSTRAINT FK_PaymentsPaymentTypeId FOREIGN KEY (PaymentTypeId)
    REFERENCES PaymentType(Id)
)
