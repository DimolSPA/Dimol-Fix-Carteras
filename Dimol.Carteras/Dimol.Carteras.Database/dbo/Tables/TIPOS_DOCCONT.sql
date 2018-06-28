CREATE TABLE [dbo].[TIPOS_DOCCONT] (
    [TDC_CODEMP] INT           NOT NULL,
    [TDC_TDCID]  INT           NOT NULL,
    [TDC_NOMBRE] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TIPOS_DOCCONT] PRIMARY KEY NONCLUSTERED ([TDC_CODEMP] ASC, [TDC_TDCID] ASC),
    CONSTRAINT [FK_TIPOS_DO_EMP_TIPDO_EMPRESA] FOREIGN KEY ([TDC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_DOCCONT]([TDC_CODEMP] ASC, [TDC_TDCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos tipos de documentos que almacenara los contratos, anexos, etc...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_DOCCONT';

