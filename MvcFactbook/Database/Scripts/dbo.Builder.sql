CREATE TABLE [dbo].[Builder] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [Founded]         INT            NULL,
    [Defunct]         INT            NULL,
    [Complete]        BIT            DEFAULT ((0)) NOT NULL,
    [ShortName]       NVARCHAR (50)  NULL,
    [AlternativeNames] NVARCHAR (MAX) NULL,
    [Location] NVARCHAR(MAX) NULL, 
    [Parent] NVARCHAR(MAX) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

