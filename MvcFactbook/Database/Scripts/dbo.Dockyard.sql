CREATE TABLE [dbo].[Dockyard] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (MAX) NOT NULL,
    [Complete]         BIT     DEFAULT ((0)) NOT NULL,
    [ShortName]        NVARCHAR (50)  NULL,
    [AlternativeNames] NVARCHAR (MAX) NULL,
    [Location]         NVARCHAR (MAX) NULL,
    [Latitude]         FLOAT (53)     NULL,
    [Longitude]        FLOAT (53)     NULL,
    [Zoom]             INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

