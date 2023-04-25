CREATE PROCEDURE [dbo].[spUser_InsertMany]
	@newUsers CreateUser readonly
AS
BEGIN
	INSERT INTO dbo.[User] (FirstName, LastName)
	SELECT [FirstName], [LastName]
	FROM @newUsers;
end
