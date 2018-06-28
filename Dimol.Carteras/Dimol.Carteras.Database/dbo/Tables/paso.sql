CREATE TABLE [dbo].[paso] (
    [rut]     VARCHAR (20)    NOT NULL,
    [numero]  NUMERIC (15)    NOT NULL,
    [cpbt]    VARCHAR (30)    NOT NULL,
    [estado]  NUMERIC (5)     NULL,
    [fecha]   DATETIME        NULL,
    [saldo]   NUMERIC (20, 3) NULL,
    [estado2] VARCHAR (1)     NULL,
    [mail]    VARCHAR (150)   NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [index_pri1]
    ON [dbo].[paso]([rut] ASC, [numero] ASC);

