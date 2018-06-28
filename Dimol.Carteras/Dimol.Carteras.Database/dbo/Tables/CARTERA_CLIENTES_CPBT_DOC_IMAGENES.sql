﻿CREATE TABLE [dbo].[CARTERA_CLIENTES_CPBT_DOC_IMAGENES] (
    [CDI_CODEMP]       INT            NOT NULL,
    [CDI_PCLID]        NUMERIC (15)   NOT NULL,
    [CDI_CTCID]        NUMERIC (15)   NOT NULL,
    [CDI_CCBID]        INT            NOT NULL,
    [CDI_CDID]         INT            NOT NULL,
    [CDI_IMAGEN]       IMAGE          NULL,
    [CDI_TPCID]        INT            NOT NULL,
    [CDI_RUTA_ARCHIVO] VARCHAR (1000) NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES_CPBT_DOC_I] PRIMARY KEY NONCLUSTERED ([CDI_CODEMP] ASC, [CDI_PCLID] ASC, [CDI_CTCID] ASC, [CDI_CCBID] ASC, [CDI_CDID] ASC),
    CONSTRAINT [FK_CARTERA__CARTCPBTD_CARTERA_] FOREIGN KEY ([CDI_CODEMP], [CDI_PCLID], [CDI_CTCID], [CDI_CCBID]) REFERENCES [dbo].[CARTERA_CLIENTES_CPBT_DOC] ([CCB_CODEMP], [CCB_PCLID], [CCB_CTCID], [CCB_CCBID]),
    CONSTRAINT [FK_CARTERA__TIPIMG_CP_TIPOS_IM] FOREIGN KEY ([CDI_CODEMP], [CDI_TPCID]) REFERENCES [dbo].[TIPOS_IMAGENES_CPBTDOC] ([TPC_CODEMP], [TPC_TPCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CARTERA_CLIENTES_CPBT_DOC_IMAGENES]([CDI_CTCID] ASC, [CDI_PCLID] ASC, [CDI_CODEMP] ASC, [CDI_CDID] ASC, [CDI_CCBID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las distintas imagenes para cada tipo de documentos o comprobantes de la cartera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC_IMAGENES';
