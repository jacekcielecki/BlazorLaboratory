CREATE TABLE [dbo].[UserGroupUser]
(
	[Id]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL ,
    [UserGroupId]  UNIQUEIDENTIFIER NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserGroupUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserGroupUser_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] (Id),
    CONSTRAINT [FK_UserGroupUser_UserGroup] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[UserGroup] (Id)
)
