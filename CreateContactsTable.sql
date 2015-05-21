GO
USE [ContactsDB];

GO
CREATE TABLE [dbo].[Contacts] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50)  NULL,
    [LastName]  VARCHAR (50)  NULL,
    [Address]   VARCHAR (MAX) NULL,
    [ZipCode]   INT           NULL,
    [City]      VARCHAR (50)  NULL,
    [Telephone] VARCHAR (20)  NULL,
    [Email]     VARCHAR (MAX) NULL,
    [BirthDate] DATETIME      NULL,
    CONSTRAINT [pk_ContactPErsonId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

