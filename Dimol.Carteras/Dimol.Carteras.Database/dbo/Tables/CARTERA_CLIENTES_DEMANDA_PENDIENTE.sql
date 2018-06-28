﻿CREATE TABLE [dbo].[CARTERA_CLIENTES_DEMANDA_PENDIENTE] (
    [CDP_CODEMP] INT          NOT NULL,
    [CDP_PCLID]  NUMERIC (15) NOT NULL,
    [CDP_CTCID]  NUMERIC (15) NOT NULL,
    [CDP_CCBID]  INT          NOT NULL,
    [CDP_FECHA]  DATETIME     NOT NULL,
    [CDP_USRID]  INT          NOT NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES_DEMANDA_PENDIENTE] PRIMARY KEY NONCLUSTERED ([CDP_CODEMP] ASC, [CDP_PCLID] ASC, [CDP_CTCID] ASC, [CDP_CCBID] ASC),
    CONSTRAINT [FK_CARTERA_DEMANDA_PENDIENTE_CARTERA] FOREIGN KEY ([CDP_CODEMP], [CDP_PCLID], [CDP_CTCID], [CDP_CCBID]) REFERENCES [dbo].[CARTERA_CLIENTES_CPBT_DOC] ([CCB_CODEMP], [CCB_PCLID], [CCB_CTCID], [CCB_CCBID]),
    CONSTRAINT [FK_CARTERA_DEMANDA_PENDIENTE_USUARIOS] FOREIGN KEY ([CDP_CODEMP], [CDP_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);

