CREATE PROCEDURE [dbo].[spUser_Get]
	@Id UNIQUEIDENTIFIER
AS
begin
	select Id, FirstName, LastName
	from dbo.[User]
	where Id = @Id;
end