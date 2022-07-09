
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/04/2022 17:52:44
-- Generated from EDMX file: C:\Users\Tyler Rodríguez\source\repos\ProxiBusNicWeb\ProxiBusNicWeb\Models\ProxiBusNicEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ProxiBusNicBD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ParadaSugerencia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sugerencias] DROP CONSTRAINT [FK_ParadaSugerencia];
GO
IF OBJECT_ID(N'[dbo].[FK_ParadaBusParada]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BusParadas] DROP CONSTRAINT [FK_ParadaBusParada];
GO
IF OBJECT_ID(N'[dbo].[FK_BusBusParada]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BusParadas] DROP CONSTRAINT [FK_BusBusParada];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Paradas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Paradas];
GO
IF OBJECT_ID(N'[dbo].[Sugerencias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sugerencias];
GO
IF OBJECT_ID(N'[dbo].[BusParadas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BusParadas];
GO
IF OBJECT_ID(N'[dbo].[Buses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Buses];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Paradas'
CREATE TABLE [dbo].[Paradas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descripcion] nvarchar(100)  NOT NULL,
    [Alias] nvarchar(100)  NULL,
    [FotoParada] varbinary(max)  NULL,
    [Estado] bit  NOT NULL,
    [Longitud] nvarchar(max)  NULL,
    [Latitud] nvarchar(max)  NULL,
    [FechaCreacion] datetime  NOT NULL,
    [UsuarioCreacion] nvarchar(100)  NOT NULL,
    [FechaModificacion] datetime  NOT NULL,
    [UsuarioModificacion] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Sugerencias'
CREATE TABLE [dbo].[Sugerencias] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DescripcionSugerencia] nvarchar(100)  NOT NULL,
    [UsuarioCreacion] nvarchar(100)  NOT NULL,
    [FechaCreacion] datetime  NOT NULL,
    [ParadaId] int  NOT NULL
);
GO

-- Creating table 'BusParadas'
CREATE TABLE [dbo].[BusParadas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ParadaId] int  NOT NULL,
    [BusId] int  NOT NULL
);
GO

-- Creating table 'Buses'
CREATE TABLE [dbo].[Buses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NumeroRuta] nvarchar(10)  NOT NULL,
    [Estado] bit  NOT NULL,
    [FotoBus] varbinary(max)  NULL,
    [FechaCreacion] datetime  NOT NULL,
    [UsuarioCreacion] nvarchar(100)  NOT NULL,
    [FechaModificacion] datetime  NOT NULL,
    [UsuarioModificacion] nvarchar(100)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Paradas'
ALTER TABLE [dbo].[Paradas]
ADD CONSTRAINT [PK_Paradas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sugerencias'
ALTER TABLE [dbo].[Sugerencias]
ADD CONSTRAINT [PK_Sugerencias]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BusParadas'
ALTER TABLE [dbo].[BusParadas]
ADD CONSTRAINT [PK_BusParadas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Buses'
ALTER TABLE [dbo].[Buses]
ADD CONSTRAINT [PK_Buses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ParadaId] in table 'Sugerencias'
ALTER TABLE [dbo].[Sugerencias]
ADD CONSTRAINT [FK_ParadaSugerencia]
    FOREIGN KEY ([ParadaId])
    REFERENCES [dbo].[Paradas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ParadaSugerencia'
CREATE INDEX [IX_FK_ParadaSugerencia]
ON [dbo].[Sugerencias]
    ([ParadaId]);
GO

-- Creating foreign key on [ParadaId] in table 'BusParadas'
ALTER TABLE [dbo].[BusParadas]
ADD CONSTRAINT [FK_ParadaBusParada]
    FOREIGN KEY ([ParadaId])
    REFERENCES [dbo].[Paradas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ParadaBusParada'
CREATE INDEX [IX_FK_ParadaBusParada]
ON [dbo].[BusParadas]
    ([ParadaId]);
GO

-- Creating foreign key on [BusId] in table 'BusParadas'
ALTER TABLE [dbo].[BusParadas]
ADD CONSTRAINT [FK_BusBusParada]
    FOREIGN KEY ([BusId])
    REFERENCES [dbo].[Buses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BusBusParada'
CREATE INDEX [IX_FK_BusBusParada]
ON [dbo].[BusParadas]
    ([BusId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------