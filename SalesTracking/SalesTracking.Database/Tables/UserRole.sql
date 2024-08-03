CREATE TABLE [dbo].[UserRole]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserId] INT NOT NULL, 
    [RoleId] INT NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [CreateBy] VARCHAR(128) NOT NULL, 
    [UpdateDate] DATETIME2 NULL, 
    [UpdateBy] VARCHAR(128) NULL
    CONSTRAINT FK_UserRoleId FOREIGN KEY  (RoleId) REFERENCES [dbo].[Role](Id),
    CONSTRAINT FK_UserUserId FOREIGN KEY  (UserId) REFERENCES [dbo].[User](Id)
)
