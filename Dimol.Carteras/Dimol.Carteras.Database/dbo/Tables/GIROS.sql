CREATE TABLE [dbo].[GIROS] (
    [GIR_CODEMP] INT            NOT NULL,
    [GIR_GIRID]  INT            NOT NULL,
    [GIR_NOMBRE] VARCHAR (1200) NOT NULL,
    CONSTRAINT [PK_GIROS] PRIMARY KEY NONCLUSTERED ([GIR_CODEMP] ASC, [GIR_GIRID] ASC),
    CONSTRAINT [FK_GIROS_EMPRESA_G_EMPRESA] FOREIGN KEY ([GIR_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[GIROS]([GIR_CODEMP] ASC, [GIR_GIRID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[GIROS]([GIR_CODEMP] ASC, [GIR_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara los distintos giros tanto para clientes como para proveedores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GIROS';

