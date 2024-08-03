CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(MAX) NOT NULL, 
    [PhoneNumber] NVARCHAR(14) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL
)
