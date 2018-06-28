﻿CREATE TABLE [dbo].[CLIENTES_ESTADOS_HISTORIAL] (
    [PCLID]          INT      NOT NULL,
    [ESTID]          INT      NOT NULL,
    [ACCION]         INT      NOT NULL,
    [USRID_REGISTRO] INT      NOT NULL,
    [FEC_REGISTRO]   DATETIME CONSTRAINT [DF_CLIENTES_ESTADOS_HISTORIAL_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL
);
