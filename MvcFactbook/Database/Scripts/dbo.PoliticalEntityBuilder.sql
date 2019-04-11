CREATE TABLE [dbo].[PoliticalEntityBuilder]
(
	[Id]                INT      IDENTITY (1, 1) NOT NULL,
    [PoliticalEntityId] INT      NOT NULL,
    [BuilderId]            INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolitcalEntityBuilder_To_PoliticalEntity] FOREIGN KEY ([PoliticalEntityId]) REFERENCES [dbo].[PoliticalEntity] ([Id]),
    CONSTRAINT [FK_PolitcalEntityBuilder_To_Builder] FOREIGN KEY ([BuilderId]) REFERENCES [dbo].[Builder] ([Id])
)