CREATE TABLE [dbo].[DockyardHistory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DockyardId] INT NOT NULL, 
    [ShipbuilderId] INT NOT NULL, 
    [Start] INT NULL, 
    [End] INT NULL,
	CONSTRAINT [FK_DockyardHistory_DockyardId_To_Dockyard] FOREIGN KEY ([DockyardId]) REFERENCES [dbo].[Dockyard] ([Id]),
    CONSTRAINT [FK_DockyardHistory_ShipbuilderId_To_Shipbuilder] FOREIGN KEY ([ShipbuilderId]) REFERENCES [dbo].[Shipbuilder] ([Id])
)
