use master

DROP DATABASE Shinobi

IF NOT EXISTS (SELECT *
FROM sys.databases
WHERE name = 'shinobi')
BEGIN
    CREATE DATABASE shinobi;
END;
GO

use shinobi
    GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ninja]') AND type in (N'U'))
DROP TABLE [dbo].[Ninja]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Skill]') AND type in (N'U'))
DROP TABLE [dbo].[Skill]
GO

CREATE TABLE Ninja
(
    ID int PRIMARY KEY  IDENTITY(1, 1),
    LastName varchar(255),
    FirstName varchar(255),
    Level int
);
GO

INSERT INTO Ninja
    (LastName, FirstName, Level)
VALUES
    ('Trout', 'Fisher', 3),
    ('Lemming', 'Ed', 2),
    ('Plod', 'Flying', 1)
GO

CREATE TABLE Skill
(
    Level int PRIMARY KEY  IDENTITY(1, 1),
    Details varchar(255)
);
GO

INSERT INTO Skill
    (Details)
VALUES
    ('Flying Monkey'),
    ('Daggers'),
    ('Backflip')
GO

Select * from Ninja
Select * from Skill