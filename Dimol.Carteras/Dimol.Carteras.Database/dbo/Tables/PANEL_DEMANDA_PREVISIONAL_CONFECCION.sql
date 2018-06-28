﻿CREATE TABLE [dbo].[PANEL_DEMANDA_PREVISIONAL_CONFECCION] (
    [CODEMP]               INT      NOT NULL,
    [ID_BORRADOR]          INT      NOT NULL,
    [ID_VERSION]           INT      NOT NULL,
    [ID_PANEL_PREVISIONAL] INT      NOT NULL,
    [CORRELATIVO]          INT      IDENTITY (1, 1) NOT NULL,
    [HTML]                 TEXT     NOT NULL,
    [FECHA_CREACION]       DATETIME NOT NULL,
    [USER_CREACION]        INT      NOT NULL,
    CONSTRAINT [PK_PANEL_DEMANDA_PREVISIONAL_CONFECCION_1] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [ID_BORRADOR] ASC, [ID_VERSION] ASC, [CORRELATIVO] ASC),
    CONSTRAINT [FK_PANEL_DEMANDA_PREVISIONAL_CONFECCION_PANEL_DEMANDA_PREVISIONAL] FOREIGN KEY ([ID_PANEL_PREVISIONAL]) REFERENCES [dbo].[PANEL_DEMANDA_PREVISIONAL] ([PANEL_ID]),
    CONSTRAINT [FK_PANEL_DEMANDA_PREVISIONAL_CONFECCION_USUARIOS] FOREIGN KEY ([CODEMP], [USER_CREACION]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);

