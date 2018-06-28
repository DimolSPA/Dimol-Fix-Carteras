﻿CREATE TABLE [dbo].[CAJA_CRITERIO_SIMBOLO] (
    [SIMBOLO_ID]  INT           NOT NULL,
    [CODEMP]      INT           NOT NULL,
    [DESCRIPCION] VARCHAR (100) NULL,
    [PARAREMESA]  VARCHAR (1)   CONSTRAINT [DF_CAJA_CRITERIO_SIMBOLO_PARAREMESA] DEFAULT ('S') NOT NULL,
    CONSTRAINT [PK_CAJA_CRITERIO_SIMBOLO] PRIMARY KEY CLUSTERED ([SIMBOLO_ID] ASC),
    CONSTRAINT [CKC_CAJA_CRITERIO_SIMBOLO_PARAREMESA] CHECK ([PARAREMESA]='N' OR [PARAREMESA]='S')
);

