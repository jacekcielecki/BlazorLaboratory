﻿CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[FirstName] NVARCHAR(50) NOT NULL, 
	[LastName] NVARCHAR(50) NOT NULL,
	[ContactDetailsId] INT
)
