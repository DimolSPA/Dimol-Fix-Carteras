﻿CREATE TABLE [dbo].[VISITA_TERRENO_SOLICITUD] (
    [CODEMP]         INT             NOT NULL,
    [SOLICITUD_ID]   INT             NOT NULL,
    [CTCID]          INT             NOT NULL,
    [DIRECCION]      VARCHAR (800)   NULL,
    [IDREGION]       INT             NOT NULL,
    [IDCIUDAD]       INT             NOT NULL,
    [IDCOMUNA]       INT             NOT NULL,
    [COMUNA]         VARCHAR (50)    NOT NULL,
    [DEUDA]          DECIMAL (15, 2) NOT NULL,
    [VISITADA]       CHAR (1)        CONSTRAINT [DF_VISITADA_VTSOLICITUD] DEFAULT ('N') NOT NULL,
    [ID_ESTATUS]     INT             NOT NULL,
    [FEC_CREACION]   DATETIME        CONSTRAINT [DF_FEC_CREACION_VTSOLICITUD] DEFAULT (getdate()) NOT NULL,
    [USRID_CREACION] INT             NOT NULL,
    [LATITUD]        NUMERIC (12, 9) CONSTRAINT [DF_LATITUD] DEFAULT ((0)) NOT NULL,
    [LONGITUD]       NUMERIC (12, 9) CONSTRAINT [DF_LONGITUD] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_VISITA_TERRENO_SOLICITUD] PRIMARY KEY CLUSTERED ([SOLICITUD_ID] ASC),
    CONSTRAINT [FK_ESTATUS_TERRENO_SOLICITUD] FOREIGN KEY ([ID_ESTATUS]) REFERENCES [dbo].[VISITA_TERRENO_TIPO_ESTATUS_ESTADO] ([ID_ESTATUS])
);

