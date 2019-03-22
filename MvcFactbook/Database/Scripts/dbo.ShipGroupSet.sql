CREATE TABLE [dbo].[ShipGroupSet] (
    [Id]			INT      IDENTITY (1, 1) NOT NULL,
    [ShipServiceId] INT      NOT NULL,
    [ShipGroupId]	INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ShipGroupSet_To_ShipService] FOREIGN KEY ([ShipServiceId]) REFERENCES [dbo].[ShipService] ([Id]),
    CONSTRAINT [FK_ShipGroupSet_To_ShipGroup] FOREIGN KEY ([ShipGroupId]) REFERENCES [dbo].[ShipGroup] ([Id])
);
