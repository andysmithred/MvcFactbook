CREATE TABLE [dbo].[Flag] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Code]        NVARCHAR (6)   NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [StartDate]   DATETIME       NULL,
    [EndDate]     DATETIME       NULL,
    [Active]      BIT            NOT NULL,
    [Complete] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

