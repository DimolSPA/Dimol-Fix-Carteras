﻿CREATE TABLE [dbo].[VISITA_TERRENO_SOLICITUD_GESTOR] (
    [SOLICITUD_ID]   INT           NOT NULL,
    [GESID]          INT           NOT NULL,
    [GESTOR]         VARCHAR (100) NOT NULL,
    [TELEFONO_IMEI]  VARCHAR (100) NOT NULL,
    [TELEFONO_NUM]   VARCHAR (100) NOT NULL,
    [USRID_REGISTRO] INT           NOT NULL,
    [FEC_REGISTRO]   DATETIME      CONSTRAINT [DF_VISITA_SOLICITUD_GESTOR_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_VISITA_TERRENO_SOLICITUD_GESTOR] PRIMARY KEY CLUSTERED ([SOLICITUD_ID] ASC, [GESID] ASC, [TELEFONO_IMEI] ASC),
    CONSTRAINT [FK_GESTOR_VISITA_TERRENO_SOLICITUD] FOREIGN KEY ([SOLICITUD_ID]) REFERENCES [dbo].[VISITA_TERRENO_SOLICITUD] ([SOLICITUD_ID])
);

