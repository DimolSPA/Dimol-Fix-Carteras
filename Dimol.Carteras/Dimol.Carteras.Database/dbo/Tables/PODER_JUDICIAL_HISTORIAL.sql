﻿CREATE TABLE [dbo].[PODER_JUDICIAL_HISTORIAL] (
    [ID_CAUSA]         INT           NOT NULL,
    [ID_CUADERNO]      INT           NOT NULL,
    [FOLIO]            INT           NULL,
    [RUTA_DOCUMENTO]   VARCHAR (500) NULL,
    [ETAPA]            VARCHAR (200) NULL,
    [TRAMITE]          VARCHAR (200) NULL,
    [DESC_TRAMITE]     VARCHAR (200) NOT NULL,
    [FECHA_TRAMITE]    DATETIME      NOT NULL,
    [FOJA]             INT           NOT NULL,
    [FECHA_HISTORIAL]  DATETIME      NULL,
    [RUTA_DIMOL]       VARCHAR (500) NULL,
    [FECHA_RUTA_DIMOL] DATETIME      NULL
);


GO
CREATE NONCLUSTERED INDEX [IDX_ID_CAUSA]
    ON [dbo].[PODER_JUDICIAL_HISTORIAL]([ID_CAUSA] ASC);
