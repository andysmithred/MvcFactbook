CREATE TABLE [dbo].[PoliticalEntityDockyard]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PoliticalEntityId] INT NOT NULL, 
    [DockyardId] INT NOT NULL,
	CONSTRAINT [FK_PolitcalEntityDockyard_To_PoliticalEntity] FOREIGN KEY ([PoliticalEntityId]) REFERENCES [dbo].[PoliticalEntity] ([Id]),
    CONSTRAINT [FK_PolitcalEntityDockyard_To_Dockyard] FOREIGN KEY ([DockyardId]) REFERENCES [dbo].[Dockyard] ([Id])
)
