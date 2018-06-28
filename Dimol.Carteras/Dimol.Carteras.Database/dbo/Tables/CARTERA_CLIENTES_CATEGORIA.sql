﻿CREATE TABLE [dbo].[CARTERA_CLIENTES_CATEGORIA] (
    [CCC_CODEMP]    INT          NOT NULL,
    [CCC_CTCID]     NUMERIC (15) NOT NULL,
    [CCC_CATEGORIA] VARCHAR (2)  NULL,
    [CCC_FECHA]     DATETIME     NOT NULL,
    [CCC_USRID]     INT          NOT NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES_CATEGORIA] PRIMARY KEY NONCLUSTERED ([CCC_CODEMP] ASC, [CCC_CTCID] ASC),
    CONSTRAINT [FK_CARTERA_CATEGORIA_DEUDORES] FOREIGN KEY ([CCC_CODEMP], [CCC_CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID]),
    CONSTRAINT [FK_CARTERA_CATEGORIA_USUARIOS] FOREIGN KEY ([CCC_CODEMP], [CCC_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);

