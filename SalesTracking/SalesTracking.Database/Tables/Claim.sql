CREATE TABLE [dbo].[Claim]
(
	[Id] INT NOT NULL PRIMARY KEY , 
    [ClaimName] NVARCHAR(50) NOT NULL, 
    [ModuleId] INT NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT getdate(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL
    CONSTRAINT FK_ClaimModuleId FOREIGN KEY  (ModuleId) REFERENCES [dbo].Module(Id)
)
