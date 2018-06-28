﻿CREATE TABLE [dbo].[DEUDORES_TELEFONOS_SITREL] (
    [CODEMP]      INT          NOT NULL,
    [CTCID]       NUMERIC (15) NOT NULL,
    [NUMERO]      VARCHAR (20) NOT NULL,
    [TIPO]        VARCHAR (20) NOT NULL,
    [ANEXO]       VARCHAR (10) NULL,
    [CODIGO_AREA] VARCHAR (20) NULL,
    [FECHA]       DATETIME     NOT NULL,
    [ORIGEN]      CHAR (1)     CONSTRAINT [DF__DEUDORES___ORIGE__39794BAC] DEFAULT ('C') NULL,
    [ENVIADO]     CHAR (1)     CONSTRAINT [DF__DEUDORES___ENVIA__3A6D6FE5] DEFAULT ('N') NULL,
    CONSTRAINT [PK_DEUDORES_TELEFONOS_SITREL] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [CTCID] ASC, [NUMERO] ASC),
    CONSTRAINT [FK_DEUDORES_TELEFONO_SITREL] FOREIGN KEY ([CODEMP], [CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID])
);

