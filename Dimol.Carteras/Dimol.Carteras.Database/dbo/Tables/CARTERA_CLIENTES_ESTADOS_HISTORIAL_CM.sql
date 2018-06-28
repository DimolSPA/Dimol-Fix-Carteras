﻿CREATE TABLE [dbo].[CARTERA_CLIENTES_ESTADOS_HISTORIAL_CM] (
    [CEH_CODEMP]     INT             NOT NULL,
    [CEH_PCLID]      NUMERIC (15)    NOT NULL,
    [CEH_CTCID]      NUMERIC (15)    NOT NULL,
    [CEH_CCBID]      INT             NOT NULL,
    [CEH_FECHA]      DATETIME        NOT NULL,
    [CEH_ESTID]      SMALLINT        NOT NULL,
    [CEH_SUCID]      INT             NOT NULL,
    [CEH_GESID]      INT             NULL,
    [CEH_IPRED]      VARCHAR (30)    NOT NULL,
    [CEH_IPMAQUINA]  VARCHAR (30)    NOT NULL,
    [CEH_COMENTARIO] TEXT            NULL,
    [CEH_MONTO]      DECIMAL (15, 2) NULL,
    [CEH_SALDO]      DECIMAL (15, 2) NULL,
    [CEH_USRID]      INT             NOT NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES_ESTADOS_HI_CM] PRIMARY KEY NONCLUSTERED ([CEH_CODEMP] ASC, [CEH_PCLID] ASC, [CEH_CTCID] ASC, [CEH_CCBID] ASC, [CEH_FECHA] ASC, [CEH_ESTID] ASC)
);

