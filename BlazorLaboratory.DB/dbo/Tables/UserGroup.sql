CREATE TABLE [dbo].[UserGroup]
(
	[Id]			UNIQUEIDENTIFIER	CONSTRAINT [DF_UserGroup_Id] DEFAULT (newid()),
	[Name]			NVARCHAR (100)		NOT NULL,
	[Description]	NVARCHAR (300)		NOT NULL,
	[DateCreated]	DATETIME			DEFAULT (getutcdate()) NOT NULL, 
    [CreatorId]		UNIQUEIDENTIFIER	NOT NULL,
	[ModeratorId]	UNIQUEIDENTIFIER	NOT NULL,
	CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UserGroup_Creator]	FOREIGN KEY (CreatorId) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT [FK_UserGroup_Moderator]	FOREIGN KEY ([ModeratorId]) REFERENCES [dbo].[User] ([Id])
)
