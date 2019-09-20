CREATE TABLE [dbo].[Dockyard]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Complete] NCHAR(10) NOT NULL DEFAULT 0, 
    [ShortName] NVARCHAR(50) NULL, 
    [AlternativeNames] NVARCHAR(MAX) NULL, 
    [Location] NVARCHAR(MAX) NULL, 
    [Latitude] FLOAT NULL, 
    [Longitude] FLOAT NULL, 
    [Zoom] INT NULL
)
