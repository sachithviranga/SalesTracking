CREATE TABLE [dbo].[Product]
(
	[Id] INT IDENTITY(1,1)  NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT getdate(), 
    [Createby] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [Updateby] VARCHAR(128) NULL 
)
