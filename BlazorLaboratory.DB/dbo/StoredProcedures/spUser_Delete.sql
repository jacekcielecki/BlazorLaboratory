CREATE PROCEDURE [dbo].[spUser_Delete]
	@Id UNIQUEIDENTIFIER
AS
begin
	delete
	from dbo.[User]
	where Id = @Id;
end