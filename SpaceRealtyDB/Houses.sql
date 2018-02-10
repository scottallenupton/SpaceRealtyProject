CREATE TABLE [dbo].[Houses]
(
	[MLS] INT NOT NULL PRIMARY KEY, 
    [Street1] NVARCHAR(50) NULL, 
    [Street2] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [State] NVARCHAR(50) NULL, 
    [ZipCode] INT NULL, 
    [Neighborhood] NVARCHAR(50) NULL, 
    [SalesPrice] MONEY NULL, 
    [DateListed] DATE NULL, 
    [Bedrooms] INT NULL, 
    [Bathrooms] DECIMAL NULL, 
    [GarageSize] INT NULL, 
    [SquareFeet] INT NULL, 
    [LotSize] INT NULL, 
    [Description] NVARCHAR(MAX) NULL
)
