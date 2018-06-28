﻿CREATE TABLE [dbo].[CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_CRITERIO] (
    [HISTORIAL_CRITERIO_ID] INT             NOT NULL,
    [DOCUMENTO_ID]          INT             NOT NULL,
    [ID_CRITERIO]           INT             NOT NULL,
    [MONTO_FACTURAR]        DECIMAL (15, 2) NOT NULL,
    [USRID_CREACION]        INT             NOT NULL,
    [FEC_CREACION]          DATETIME        CONSTRAINT [DF_CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_CRITERIO_FEC_CREACION] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_CRITERIO] PRIMARY KEY CLUSTERED ([HISTORIAL_CRITERIO_ID] ASC)
);

