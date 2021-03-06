﻿CREATE TABLE [dbo].[TESORERIA_CUENTAS_BANCARIAS_CLIENTES_PAGOS] (
    [CUENTA_ID]      INT          NOT NULL,
    [PCLID]          NUMERIC (15) NOT NULL,
    [USRID_REGISTRO] INT          NOT NULL,
    [FEC_REGISTRO]   DATETIME     CONSTRAINT [DF_TESORERIA_CUENTAS_BANCARIAS_CLIENTES_PAGOS_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TESORERIA_CUENTAS_BANCARIAS_CLIENTES_PAGOS] PRIMARY KEY CLUSTERED ([CUENTA_ID] ASC, [PCLID] ASC),
    CONSTRAINT [FK_TESORERIA_CUENTAS_BANCARIAS_CLIENTES_PAGOS] FOREIGN KEY ([CUENTA_ID]) REFERENCES [dbo].[TESORERIA_CUENTAS_BANCARIAS] ([CUENTA_ID])
);

