CREATE TABLE [dbo].[ShipGroup] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
	[Icon]		  NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
