﻿CREATE TABLE [dbo].[TIPO_BANCO] (
    [ID_TIPO_BANCO] INT           IDENTITY (1, 1) NOT NULL,
    [NOMBRE]        VARCHAR (200) NULL,
    CONSTRAINT [PK_TIPO_BANCO] PRIMARY KEY CLUSTERED ([ID_TIPO_BANCO] ASC)
);

