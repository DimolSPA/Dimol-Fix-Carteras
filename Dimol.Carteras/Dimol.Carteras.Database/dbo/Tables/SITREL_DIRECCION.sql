﻿CREATE TABLE [dbo].[SITREL_DIRECCION] (
    [CODEMP]          INT           NOT NULL,
    [ID_CARGA]        INT           NOT NULL,
    [PCLID]           INT           NOT NULL,
    [RUT]             VARCHAR (9)   NOT NULL,
    [DIRECCION]       VARCHAR (255) NOT NULL,
    [ZONA_GEOGRAFICA] VARCHAR (20)  NOT NULL,
    [TIPO_DIRECCION]  VARCHAR (20)  NOT NULL,
    [TIPO_PERSONA]    VARCHAR (7)   NOT NULL,
    CONSTRAINT [PK_SITREL_DIRECCION] PRIMARY KEY CLUSTERED ([ID_CARGA] ASC, [PCLID] ASC, [RUT] ASC, [DIRECCION] ASC)
);
