CREATE TABLE [dbo].[Sales]
(
	[Id] INT IDENTITY(1,1) NOT NULL , 
    [CustomerId] INT NOT NULL, 
    [TransactionDate] DATETIME2 NOT NULL DEFAULT getdate(), 
    [InvoiceNo] NVARCHAR(50) NOT NULL, 
    [IsApproved] BIT NOT NULL DEFAULT 0, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL ,
    [ApprovedDate] DATETIME2 NULL, 
    [ApprovedBy] VARCHAR(128) NULL
    CONSTRAINT PK_SalesId PRIMARY KEY (Id),
    [TotalAmout] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    [TotalPayment] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    CONSTRAINT FK_SalesCustomerId FOREIGN KEY (CustomerId)
    REFERENCES Customer(Id)
)
