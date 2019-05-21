CREATE TABLE [dbo].[ShipService] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Penant]        NVARCHAR (10)  NULL,
    [Name]          NVARCHAR (MAX) NOT NULL,
    [ShipId]        INT            NOT NULL,
    [ShipClassId]   INT            NOT NULL,
    [ShipSubTypeId] INT            NOT NULL,
    [StartService]  DATETIME       NULL,
    [EndService]    DATETIME       NULL,
    [BranchId]      INT            NULL,
    [Fate]          NVARCHAR (MAX) NULL,
    [Active]        BIT            DEFAULT ((0)) NOT NULL,
    [Complete] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ShipService_To_Ship] FOREIGN KEY ([ShipId]) REFERENCES [dbo].[Ship] ([Id]),
    CONSTRAINT [FK_ShipService_To_ShipClass] FOREIGN KEY ([ShipClassId]) REFERENCES [dbo].[ShipClass] ([Id]),
    CONSTRAINT [FK_ShipService_To_ShipSubType] FOREIGN KEY ([ShipSubTypeId]) REFERENCES [dbo].[ShipSubType] ([Id]),
    CONSTRAINT [FK_ShipService_To_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([Id])
);

