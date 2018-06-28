﻿CREATE TABLE [dbo].[VISITA_TERRENO_SOLICITUD_ESTATUS] (
    [SOLICITUD_ID]   INT      NOT NULL,
    [ID_ESTATUS]     INT      NOT NULL,
    [USRID_CREACION] INT      NOT NULL,
    [FEC_CREACION]   DATETIME CONSTRAINT [DF_FEC_CREACION_VTSOLICITUD_ESTATUS] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_VISITA_TERRENO_SOLICITUD_ESTATUS] PRIMARY KEY CLUSTERED ([SOLICITUD_ID] ASC, [ID_ESTATUS] ASC),
    CONSTRAINT [FK_ESTATUS] FOREIGN KEY ([ID_ESTATUS]) REFERENCES [dbo].[VISITA_TERRENO_TIPO_ESTATUS_ESTADO] ([ID_ESTATUS]),
    CONSTRAINT [FK_VISITA_TERRENO_SOLICITUD_VTSE] FOREIGN KEY ([SOLICITUD_ID]) REFERENCES [dbo].[VISITA_TERRENO_SOLICITUD] ([SOLICITUD_ID])
);
