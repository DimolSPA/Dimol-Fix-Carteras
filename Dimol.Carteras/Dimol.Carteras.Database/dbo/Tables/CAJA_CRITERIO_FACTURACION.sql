﻿CREATE TABLE [dbo].[CAJA_CRITERIO_FACTURACION] (
    [CRITERIO_ID]             INT          NOT NULL,
    [CODEMP]                  INT          NOT NULL,
    [PCLID]                   NUMERIC (15) NOT NULL,
    [DESCRIPCION]             VARCHAR (30) NOT NULL,
    [FACTURADO_NOCORRESPONDE] VARCHAR (1)  CONSTRAINT [DF_CAJA_CRITERIO_FACTURACION_FACTURADO_NOCORRESPONDE] DEFAULT ('S') NOT NULL,
    [REQUIERE_APRUEBA]        VARCHAR (1)  CONSTRAINT [DF_CAJA_CRITERIO_FACTURACION_REQUIERE_APRUEBA] DEFAULT ('N') NOT NULL,
    [CRITERIO_APLICA]         VARCHAR (1)  CONSTRAINT [DF_CAJA_CRITERIO_FACTURACION_CRITERIO_APLICA] DEFAULT ('N') NOT NULL,
    [CRITERIO_APLICA_SIMBOLO] VARCHAR (2)  NULL,
    [CRITERIO_APLICA_VALOR]   NUMERIC (3)  NULL,
    [IMPUTABLE]               VARCHAR (1)  CONSTRAINT [DF_CAJA_CRITERIO_FACTURACION_IMPUTABLE] DEFAULT ('N') NOT NULL,
    [CONDICION_ID]            INT          NULL,
    [PARAREMESA]              VARCHAR (1)  CONSTRAINT [DF_CAJA_CRITERIO_FACTURACION_PARAREMESA] DEFAULT ('N') NOT NULL,
    CONSTRAINT [PK_CAJA_CRITERIO_FACTURACION] PRIMARY KEY CLUSTERED ([CRITERIO_ID] ASC),
    CONSTRAINT [CKC_CAJA_CRITERIO_FACTURACION_CRITERIO_APLICA] CHECK ([CRITERIO_APLICA]='N' OR [CRITERIO_APLICA]='S'),
    CONSTRAINT [CKC_CAJA_CRITERIO_FACTURACION_FACTURADO_NOCORRESPONDE] CHECK ([FACTURADO_NOCORRESPONDE]='N' OR [FACTURADO_NOCORRESPONDE]='S'),
    CONSTRAINT [CKC_CAJA_CRITERIO_FACTURACION_IMPUTABLE] CHECK ([IMPUTABLE]='N' OR [IMPUTABLE]='S'),
    CONSTRAINT [CKC_CAJA_CRITERIO_FACTURACION_PARAREMESA] CHECK ([PARAREMESA]='N' OR [PARAREMESA]='S'),
    CONSTRAINT [CKC_CAJA_CRITERIO_FACTURACION_REQUIERE_APRUEBA] CHECK ([REQUIERE_APRUEBA]='N' OR [REQUIERE_APRUEBA]='S'),
    CONSTRAINT [FK_CAJA_CRITERIO_FACTURACION_CAJA_CONDICION_FACTURACION] FOREIGN KEY ([CONDICION_ID]) REFERENCES [dbo].[CAJA_CONDICION_FACTURACION] ([CONDICION_ID])
);
