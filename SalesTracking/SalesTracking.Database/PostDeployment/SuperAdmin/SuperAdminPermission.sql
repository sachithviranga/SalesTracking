BEGIN TRANSACTION [Tran1]

BEGIN TRY

print 'Inserting data for Super Admin'

INSERT INTO [dbo].[Role]([RoleName],[IsSystem],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  N'Super Admin', 1 , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[Role] where [RoleName]= 'Super Admin')

DECLARE @RoleID INT

SELECT TOP 1 @RoleID = Id FROM [dbo].[Role] where [RoleName]= 'Super Admin' AND [IsSystem] = 1

INSERT INTO [dbo].[RoleClaim]([RoleId],[ClaimId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
SELECT @RoleID , CL.Id , 1, N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
FROM [dbo].Claim CL
WHERE NOT EXISTS (select 1 from  [dbo].[RoleClaim] where [RoleId] = @RoleID AND [ClaimId] = CL.Id AND CL.IsActive = 1)


INSERT INTO [dbo].[User] ([FirstName],[LastName],[Email],[Password],[PhoneNumber],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  N'Super' , N'Admin' , N'superadmin@salestracking.com', N'$2a$11$9IubQvmtGlAzbuf3Z/Rq.O.EFY5CjCVVhtKQ4m6uZzOxZfwxzsDIy','1234567', 1 , N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[User] where [Email]= 'superadmin@salestracking.com')
--password -12345

DECLARE @UserID INT

select TOP 1 @UserID = Id from  [dbo].[User] where [Email]= 'superadmin@salestracking.com'

INSERT INTO [dbo].[UserRole]([UserId],[RoleId],[IsActive],[CreateBy],[CreateDate],[UpdateDate],[UpdateBy])
Select  @UserID , @RoleID, 1 , N'System', CAST(GETDATE() AS DateTime2), NULL, NULL
where not exists(select 1 from  [dbo].[UserRole] where [UserId] = @UserID AND [RoleId]= @RoleID )

print 'End inserting data for Super Admin'




COMMIT TRANSACTION [Tran1]

END TRY
BEGIN CATCH
  ROLLBACK TRANSACTION [Tran1]
END CATCH  

GO