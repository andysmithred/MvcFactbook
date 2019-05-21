CREATE TABLE [dbo].[Branch] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [ArmedForceId] INT           DEFAULT ((1)) NOT NULL,
    [BranchTypeId] INT           NOT NULL,
    [HasEmblem]    BIT           NOT NULL,
    [Complete] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Branch_To_BranchType] FOREIGN KEY ([BranchTypeId]) REFERENCES [dbo].[BranchType] ([Id]),
    CONSTRAINT [FK_Branch_To_ArmedForce] FOREIGN KEY ([ArmedForceId]) REFERENCES [dbo].[ArmedForce] ([Id])
);

