CREATE TABLE [dbo].[RoleClaim]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [RoleId] INT NOT NULL, 
    [ClaimId] INT NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL
    CONSTRAINT FK_RoleId FOREIGN KEY  (RoleId) REFERENCES [dbo].Role(Id),
    CONSTRAINT FK_ClaimId FOREIGN KEY  (ClaimId) REFERENCES [dbo].Claim(Id),
)
