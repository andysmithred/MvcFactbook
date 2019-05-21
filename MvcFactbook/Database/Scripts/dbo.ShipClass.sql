CREATE TABLE [dbo].[ShipClass] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX) NOT NULL,
    [SubClassName] NVARCHAR (MAX) NULL,
    [Displacement] INT            NULL,
    [Length]       FLOAT (53)     NULL,
    [Beam]         FLOAT (53)     NULL,
    [Propulsion]   INT            NULL,
    [Speed]        FLOAT (53)     NULL,
    [Crew]         INT            NULL,
    [Year]         INT            NULL,
    [Complete] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

