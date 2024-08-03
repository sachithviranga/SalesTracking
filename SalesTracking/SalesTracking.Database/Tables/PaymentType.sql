CREATE TABLE [dbo].[PaymentType]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(100) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL, 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL
)
