﻿CREATE TABLE [dbo].[VISITA_TERRENO_DETALLE_TELEFONOS] (
    [ID_VISITA_DETALLE] INT          NOT NULL,
    [ID_VISITA]         INT          NOT NULL,
    [NUMERO]            NUMERIC (12) NOT NULL,
    CONSTRAINT [PK_VISITA_TERRENO_DETALLE_TELEFONOS] PRIMARY KEY CLUSTERED ([ID_VISITA_DETALLE] ASC, [ID_VISITA] ASC, [NUMERO] ASC),
    CONSTRAINT [FK_VISITA_TERRENO_DETALLE_TELEFONOS_VTD] FOREIGN KEY ([ID_VISITA_DETALLE], [ID_VISITA]) REFERENCES [dbo].[VISITA_TERRENO_DETALLE] ([ID_VISITA_DETALLE], [ID_VISITA])
);

