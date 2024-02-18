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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Persons]') AND type in (N'U'))
DROP TABLE [dbo].[Persons]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Skills]') AND type in (N'U'))
DROP TABLE [dbo].[Skills]
GO

CREATE TABLE Persons
(
    PersonID int,
    LastName varchar(255),
    FirstName varchar(255),
    Class int
);
GO

INSERT INTO Persons
    (PersonID, LastName, FirstName, Class)
VALUES
    (1, 'Trout', 'Fisher', 3),
    (2, 'Lemming', 'Ed', 2),
    (3, 'Plod', 'Flying', 1)
GO

CREATE TABLE Skills
(
    Class int,
    Details varchar(255)
);
GO

INSERT INTO Skills
    (Class, Details)
VALUES
    (1, 'Flying Monkey'),
    (2, 'Daggers'),
    (3, 'Backflip')
GO

Select * from Persons
Select * from Skills