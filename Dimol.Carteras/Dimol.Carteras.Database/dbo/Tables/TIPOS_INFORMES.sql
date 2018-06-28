CREATE TABLE [dbo].[TIPOS_INFORMES] (
    [TIF_CODEMP] INT          NOT NULL,
    [TIF_TIFID]  INT          NOT NULL,
    [TIF_NOMBRE] VARCHAR (80) NOT NULL,
    CONSTRAINT [PK_TIPOS_INFORMES] PRIMARY KEY CLUSTERED ([TIF_CODEMP] ASC, [TIF_TIFID] ASC),
    CONSTRAINT [FK_TIPOS_IN_EMPRESA_T_EMPRESA] FOREIGN KEY ([TIF_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_INFORMES]([TIF_CODEMP] ASC, [TIF_TIFID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TIPOS_INFORMES]([TIF_CODEMP] ASC, [TIF_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos tipos de informes o documentos que se almacenaran dentro de algun directorio de la base de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_INFORMES';

