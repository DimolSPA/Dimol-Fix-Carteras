CREATE TABLE [dbo].[EMAIL_TEMPLATE] (
    [Id_Template]   INT            NOT NULL,
    [Id_Mail]       INT            NOT NULL,
    [Header]        VARCHAR (2000) NULL,
    [Asunto]        VARCHAR (2000) NULL,
    [Body]          VARCHAR (8000) NULL,
    [UsrCrea]       VARCHAR (200)  NULL,
    [FechaCrea]     DATETIME       NULL,
    [UsrModifica]   VARCHAR (200)  NULL,
    [FechaModifica] DATETIME       NULL,
    CONSTRAINT [PK_EMAIL_TEMPLATE] PRIMARY KEY CLUSTERED ([Id_Template] ASC),
    CONSTRAINT [FK_EMAIL_EMAIL_TEMPLATE] FOREIGN KEY ([Id_Mail]) REFERENCES [dbo].[EMAIL] ([Id_Email])
);

