﻿CREATE TABLE [dbo].[DOCUMENTOS_CUSTODIA_HISTORIAL] (
    [HISTORIAL_ID]         INT             NOT NULL,
    [CUSTODIA_ID]          INT             NOT NULL,
    [CODEMP]               INT             NOT NULL,
    [NUM_CUENTA]           VARCHAR (60)    NULL,
    [PCLID]                NUMERIC (15)    NOT NULL,
    [CTCID]                NUMERIC (15)    NOT NULL,
    [GESTORID]             INT             NOT NULL,
    [NUM_DOCUMENTO]        VARCHAR (20)    NULL,
    [RECIBE]               VARCHAR (100)   NULL,
    [BANCO_ID]             INT             NOT NULL,
    [TIPO_ESTADO_BANCO_ID] INT             NOT NULL,
    [FEC_DOC]              DATETIME        NOT NULL,
    [FEC_PRORROGA]         DATETIME        NULL,
    [MONTO]                DECIMAL (15, 2) CONSTRAINT [DF_DOCUMENTOS_CUSTODIA_HISTORIAL_MONTO] DEFAULT ((0)) NOT NULL,
    [USRID_REGISTRO]       INT             NOT NULL,
    [FEC_REGISTRO]         DATETIME        CONSTRAINT [DF_DOCUMENTOS_CUSTODIA_HISTORIAL_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_DOCUMENTOS_CUSTODIA_HISTORIAL] PRIMARY KEY CLUSTERED ([HISTORIAL_ID] ASC),
    CONSTRAINT [FK_DOCUMENTOS_CUSTODIA_HISTORIAL_TESORERIA_TIPO_ESTADO_BANCO] FOREIGN KEY ([TIPO_ESTADO_BANCO_ID]) REFERENCES [dbo].[TESORERIA_TIPO_ESTADO_BANCO] ([TIPO_ESTADO_BANCO_ID])
);

