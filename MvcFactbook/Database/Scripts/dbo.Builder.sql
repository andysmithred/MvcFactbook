CREATE TABLE [dbo].[Builder] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [Founded] INT            NULL,
    [Defunct] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

