﻿CREATE TABLE [dbo].[CARTOLA_MOVIMIENTOS_TIPO_ESTATUS] (
    [ESTATUS_ID]  INT          NOT NULL,
    [DESCRIPCION] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_CARTOLA_MOVIMIENTOS_TIPO_ESTATUS] PRIMARY KEY CLUSTERED ([ESTATUS_ID] ASC)
);

