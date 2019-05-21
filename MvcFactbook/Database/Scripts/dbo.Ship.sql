CREATE TABLE [dbo].[Ship] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [Launched]  DATETIME       NULL,
    [BuilderId] INT            NULL,
    [Complete] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ship_To_Builder] FOREIGN KEY ([BuilderId]) REFERENCES [dbo].[Builder] ([Id])
);

