﻿CREATE TABLE [dbo].[CONFIGURACION_SISTEMA] (
    [CFS_CFSID]  INT             NOT NULL,
    [CFS_NOMBRE] VARCHAR (400)   NOT NULL,
    [CFS_VALNUM] NUMERIC (30, 6) DEFAULT ((0)) NOT NULL,
    [CFS_VALTXT] VARCHAR (1000)  DEFAULT ('0') NOT NULL,
    CONSTRAINT [PK_CONFIGURACION_SISTEMA] PRIMARY KEY CLUSTERED ([CFS_CFSID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CONFIGURACION_SISTEMA]([CFS_CFSID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[CONFIGURACION_SISTEMA]([CFS_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta es la configuracion estandar para todo el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONFIGURACION_SISTEMA';

