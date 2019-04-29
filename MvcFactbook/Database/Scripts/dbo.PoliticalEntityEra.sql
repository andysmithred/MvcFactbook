CREATE TABLE [dbo].[PoliticalEntityEra] (
    [Id]                INT            NOT NULL IDENTITY,
    [PoliticalEntityId] INT            NOT NULL,
    [StartDate]         DATETIME       NULL,
    [EndDate]           DATETIME       NULL,
    [Description]       NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolitcalEntityEra_To_PoliticalEntity] FOREIGN KEY ([PoliticalEntityId]) REFERENCES [dbo].[PoliticalEntity] ([Id])
);

