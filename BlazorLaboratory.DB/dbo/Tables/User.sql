CREATE TABLE [dbo].[User]
(
	[Id] UNIQUEIDENTIFIER CONSTRAINT [DF_User_Id] DEFAULT (newid()) NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL, 
	[LastName] NVARCHAR(50) NOT NULL,
	[ContactDetailsId] INT	CONSTRAINT [FK_ContactDetails]	FOREIGN KEY ([ContactDetailsId]) REFERENCES [dbo].[ContactDetails] ([Id]),
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
)
