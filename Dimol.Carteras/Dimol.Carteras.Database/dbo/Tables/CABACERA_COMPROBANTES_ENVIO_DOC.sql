﻿CREATE TABLE [dbo].[CABACERA_COMPROBANTES_ENVIO_DOC] (
    [CBV_CODEMP] INT          NOT NULL,
    [CBV_SUCID]  INT          NOT NULL,
    [CBV_TPCID]  INT          NOT NULL,
    [CBV_NUMERO] NUMERIC (15) NOT NULL,
    [CBV_PCLID]  INT          NOT NULL,
    [CBV_CTCID]  INT          NOT NULL,
    [CBV_TDEID]  INT          NOT NULL,
    CONSTRAINT [PK_CABACERA_COMPROBANTES_ENVIO] PRIMARY KEY CLUSTERED ([CBV_CODEMP] ASC, [CBV_SUCID] ASC, [CBV_TPCID] ASC, [CBV_NUMERO] ASC, [CBV_PCLID] ASC, [CBV_CTCID] ASC, [CBV_TDEID] ASC),
    CONSTRAINT [FK_CABACERA_CABCPBT_E_CABACERA] FOREIGN KEY ([CBV_CODEMP], [CBV_SUCID], [CBV_TPCID], [CBV_NUMERO]) REFERENCES [dbo].[CABACERA_COMPROBANTES] ([CBC_CODEMP], [CBC_SUCID], [CBC_TPCID], [CBC_NUMERO]),
    CONSTRAINT [FK_CABACERA_TIPENVDOC_TIPOS_DO] FOREIGN KEY ([CBV_CODEMP], [CBV_TDEID]) REFERENCES [dbo].[TIPOS_DOCUMENTOS_ENVIOS] ([TDE_CODEMP], [TDE_TDEID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CABACERA_COMPROBANTES_ENVIO_DOC]([CBV_CODEMP] ASC, [CBV_SUCID] ASC, [CBV_TPCID] ASC, [CBV_NUMERO] ASC, [CBV_PCLID] ASC, [CBV_CTCID] ASC, [CBV_TDEID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos envios de documentos, que se realizan tanto para clientes o deudores...
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CABACERA_COMPROBANTES_ENVIO_DOC';

