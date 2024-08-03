BEGIN	/**** Module ********/

print 'Inserting data for Module'

INSERT INTO [dbo].[Module] ([Id], [ModuleName],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  1, N'Role Module', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Module] where Id= 1)

INSERT INTO [dbo].[Module] ([Id], [ModuleName],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  2, N'User Module', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Module] where Id= 2)

INSERT INTO [dbo].[Module] ([Id], [ModuleName],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  3, N'Customer Module', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Module] where Id= 3)

INSERT INTO [dbo].[Module] ([Id], [ModuleName],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  4, N'Product Module', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Module] where Id= 4)

INSERT INTO [dbo].[Module] ([Id], [ModuleName],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  5, N'Purchase Module', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Module] where Id= 5)

INSERT INTO [dbo].[Module] ([Id], [ModuleName],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  6, N'Sales Module', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Module] where Id= 6)

print 'End inserting data for Module'

END

BEGIN	/**** Claim ********/

print 'Inserting data for Claim'

DECLARE @ModuleId INT

SET @ModuleId  = 1 -- Role

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  1, N'Role Landing', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 1)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  2, N'Role View', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 2)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  3, N'Role Add', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 3)


SET @ModuleId  = 2 -- User

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  4, N'User Landing', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 4)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  5, N'User View', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 5)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  6, N'User Add', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 6)


SET @ModuleId  = 3 -- Customer

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  7, N'Customer Landing', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 7)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  8, N'Customer View', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 8)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  9, N'Customer Add', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 9)


SET @ModuleId  = 4 -- Product

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  10, N'Product Landing', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 10)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  11, N'Product View', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 11)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  12, N'Product Add', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 12)



SET @ModuleId  = 5 -- Purchase

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  13, N'Purchase Landing', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 13)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  14, N'Purchase View', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 14)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  15, N'Purchase Add', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 15)


SET @ModuleId  = 6 -- Sales

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  16, N'Sales Landing', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 16)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  17, N'Sales View', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 17)

INSERT INTO [dbo].[Claim]([Id],[ClaimName],[ModuleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  18, N'Sales Add', @ModuleId , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Claim] where Id= 18)

print 'End inserting data for Claim'

END