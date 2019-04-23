CREATE TABLE [dbo].[PoliticalEntitySucceeding] (
    [Id]                          INT IDENTITY (1, 1) NOT NULL,
    [PoliticalEntityId]           INT NOT NULL,
    [SucceedingPoliticalEntityId] INT NOT NULL,
    [Year] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PoliticalEntitySucceeding_PoliticalEntityId_To_PoliticalEntity] FOREIGN KEY ([PoliticalEntityId]) REFERENCES [dbo].[PoliticalEntity] ([Id]),
    CONSTRAINT [FK_PoliticalEntitySucceeding_SucceedingPoliticalEntityId_To_PoliticalEntity] FOREIGN KEY ([SucceedingPoliticalEntityId]) REFERENCES [dbo].[PoliticalEntity] ([Id])
);

