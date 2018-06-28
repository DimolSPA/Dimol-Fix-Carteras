﻿CREATE TABLE [dbo].[PROCESO] (
    [CODEMP]               INT           NOT NULL,
    [PROCESO]              INT           IDENTITY (1, 1) NOT NULL,
    [TIPO_PROCESO]         INT           NOT NULL,
    [NOMBRE]               VARCHAR (50)  NOT NULL,
    [DESCRIPCION]          VARCHAR (150) NOT NULL,
    [SERVIDOR]             VARCHAR (50)  NOT NULL,
    [USUARIO_INGRESO]      INT           NOT NULL,
    [FECHA_INGRESO]        DATETIME      NOT NULL,
    [USUARIO_MODIFICACION] INT           NOT NULL,
    [FECHA_MODIFICACION]   DATETIME      NOT NULL,
    CONSTRAINT [PK_PROCESO] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [PROCESO] ASC),
    CONSTRAINT [FK_PROCESO_TIPO_PROCESO] FOREIGN KEY ([CODEMP], [TIPO_PROCESO]) REFERENCES [dbo].[TIPOS_PROCESO] ([CODEMP], [TIPO_PROCESO]),
    CONSTRAINT [FK_PROCESO_USUARIO_1] FOREIGN KEY ([CODEMP], [USUARIO_INGRESO]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID]),
    CONSTRAINT [FK_PROCESO_USUARIO_2] FOREIGN KEY ([CODEMP], [USUARIO_MODIFICACION]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);
