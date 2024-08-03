BEGIN	/**** CustomerType ********/

print 'Inserting data for CustomerType'

INSERT INTO [dbo].[CustomerType] ([Id], [Name],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  1, N'Cash', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[CustomerType] where Id= 1)

INSERT INTO [dbo].[CustomerType] ([Id], [Name],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  2, N'Credit', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[CustomerType] where Id= 2)

print 'End inserting data for CustomerType'

END

BEGIN	/**** PaymentType ********/

print 'Inserting data for PaymentType'

INSERT INTO [dbo].[PaymentType] ([Id], [Name],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  1, N'Cash', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[PaymentType] where Id= 1)

INSERT INTO [dbo].[PaymentType] ([Id], [Name],[IsActive], [CreateBy],[CreateDate], [UpdateDate],[UpdateBy])
Select  2, N'Cheque', 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[PaymentType] where Id= 2)

print 'End inserting data for PaymentType'

END