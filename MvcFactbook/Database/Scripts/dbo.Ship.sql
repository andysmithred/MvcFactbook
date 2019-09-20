CREATE TABLE [dbo].[Ship] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [Launched]  DATETIME       NULL,
    [Complete]  BIT            DEFAULT ((0)) NOT NULL,
    [DockyardId] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ship_To_Dockyard] FOREIGN KEY ([DockyardId]) REFERENCES [dbo].[Dockyard] ([Id])
);
