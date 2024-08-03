CREATE TABLE [dbo].[Customer]
(
	[Id] INT IDENTITY(1,1)  NOT NULL PRIMARY KEY, 
    [Name] NCHAR(100) NOT NULL, 
    [Address] NCHAR(100) NOT NULL, 
    [PhoneNo] NUMERIC(15) NOT NULL, 
    [CustomerTypeId] INT NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT getdate(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL
    CONSTRAINT FK_CustomerTypeId FOREIGN KEY  (CustomerTypeId) REFERENCES [dbo].CustomerType(Id),
)
