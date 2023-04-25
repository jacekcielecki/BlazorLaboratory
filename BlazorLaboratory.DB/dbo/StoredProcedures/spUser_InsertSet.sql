CREATE PROCEDURE [dbo].[spUser_InsertSet]
	@people CreateUser readonly
AS
BEGIN
	INSERT INTO dbo.Person(FirstName, LastName)
	SELECT [FirstName], [LastName]
	FROM @people;
end
