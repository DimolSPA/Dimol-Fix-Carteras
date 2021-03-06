﻿CREATE TABLE [dbo].[TESORERIA_CRITERIO_IMPUTACION_ORDEN] (
    [CODEMP]         INT          NOT NULL,
    [PCLID]          NUMERIC (15) NOT NULL,
    [CAPITAL]        NUMERIC (1)  NOT NULL,
    [INTERES]        NUMERIC (1)  NOT NULL,
    [HONORARIO]      NUMERIC (1)  NOT NULL,
    [USRID_REGISTRO] INT          NOT NULL,
    [FEC_REGISTRO]   DATETIME     CONSTRAINT [DF_CRITERIO_IMPUTACION_ORDEN_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TESORERIA_CRITERIO_IMPUTACION_ORDEN] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [PCLID] ASC)
);

