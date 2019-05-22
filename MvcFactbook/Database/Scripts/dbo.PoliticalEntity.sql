CREATE TABLE [dbo].[PoliticalEntity] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [ShortName]             NVARCHAR (50)  NOT NULL,
    [Name]                  NVARCHAR (MAX) NOT NULL,
    [FullName]              NVARCHAR (MAX) NULL,
    [Code]                  NVARCHAR (3)   NOT NULL,
    [StartDate]             DATETIME       NULL,
    [EndDate]               DATETIME       NULL,
    [Exists]                BIT            DEFAULT ((0)) NOT NULL,
    [HasEmblem]             BIT            DEFAULT ((0)) NOT NULL,
    [PoliticalEntityTypeId] INT            NOT NULL,
    [Complete] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PoliticalEntity_To_PoliticalEntityType] FOREIGN KEY ([PoliticalEntityTypeId]) REFERENCES [dbo].[PoliticalEntityType] ([Id])
);

