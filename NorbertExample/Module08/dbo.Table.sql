CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Action] NVARCHAR(10) NULL, 
    [Pathname] NVARCHAR(255) NULL, 
    [OldPathName] NVARCHAR(255) NULL, 
    [TimeAffected] DATETIME NULL
)
