﻿CREATE TABLE [dbo].[USUARIOS_PJ_RUTAS] (
    [ID]    INT           IDENTITY (1, 1) NOT NULL,
    [PCLID] INT           NOT NULL,
    [RUTA]  VARCHAR (500) NULL,
    CONSTRAINT [PK_USUARIOS_PJ_RUTAS] PRIMARY KEY CLUSTERED ([ID] ASC)
);
