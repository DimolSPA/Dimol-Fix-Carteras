﻿CREATE TABLE [dbo].[TIPOS_DOCUMENTOS_DEUDORES_IDIOMAS] (
    [TDI_CODEMP] INT           NOT NULL,
    [TDI_TDDID]  INT           NOT NULL,
    [TDI_IDID]   INT           NOT NULL,
    [TDI_NOMBRE] VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_TIPOS_DOCUMENTOS_DEUDORES_I] PRIMARY KEY CLUSTERED ([TDI_CODEMP] ASC, [TDI_TDDID] ASC, [TDI_IDID] ASC),
    CONSTRAINT [FK_TIPOS_DO_IDIOMAS_T_IDIOMAS] FOREIGN KEY ([TDI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID]),
    CONSTRAINT [FK_TIPOS_DO_TIPDD_IDI_TIPOS_DO] FOREIGN KEY ([TDI_CODEMP], [TDI_TDDID]) REFERENCES [dbo].[TIPOS_DOCUMENTOS_DEUDORES] ([TDD_CODEMP], [TDD_TDDID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_DOCUMENTOS_DEUDORES_IDIOMAS]([TDI_CODEMP] ASC, [TDI_TDDID] ASC, [TDI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos idiomas para cada tipo de documentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_DOCUMENTOS_DEUDORES_IDIOMAS';

