﻿CREATE TABLE [dbo].[SOCIOS] (
    [SOC_CODEMP]    INT           NOT NULL,
    [SOC_SOCID]     INT           NOT NULL,
    [SOC_USRID]     INT           NOT NULL,
    [SOC]           VARCHAR (20)  NOT NULL,
    [SOC_RUT]       INT           NOT NULL,
    [SOC_DV]        CHAR (1)      NOT NULL,
    [SOC_NOMBRE]    VARCHAR (200) NOT NULL,
    [SOC_APEPAT]    VARCHAR (100) NOT NULL,
    [SOC_APEMAT]    VARCHAR (100) NULL,
    [SOC_COMID]     INT           NOT NULL,
    [SOC_DIRECCION] VARCHAR (200) NOT NULL,
    [SOC_TELEFONO1] NUMERIC (10)  NULL,
    [SOC_TELEFONO2] NUMERIC (10)  NULL,
    [SOC_CELULAR]   NUMERIC (10)  NULL,
    [SOC_MAIL1]     VARCHAR (80)  NULL,
    [SOC_MAIL2]     VARCHAR (80)  NULL,
    [SOC_PRFID]     INT           NULL,
    [SOC_ESTATURA]  SMALLINT      NULL,
    [SOC_FECING]    DATETIME      NULL,
    [SOC_FECFIN]    DATETIME      NULL,
    [SOC_ESTADO]    CHAR (1)      DEFAULT ('H') NULL,
    [SOC_FOTO]      IMAGE         NULL,
    CONSTRAINT [PK_SOCIOS] PRIMARY KEY CLUSTERED ([SOC_CODEMP] ASC, [SOC_SOCID] ASC),
    CONSTRAINT [CKC_SOC_ESTADO_SOCIOS] CHECK ([SOC_ESTADO] IS NULL OR ([SOC_ESTADO]='B' OR [SOC_ESTADO]='H')),
    CONSTRAINT [FK_SOCIOS_COMUNA_SO_COMUNA] FOREIGN KEY ([SOC_COMID]) REFERENCES [dbo].[COMUNA] ([COM_COMID]),
    CONSTRAINT [FK_SOCIOS_EMPRESA_S_EMPRESA] FOREIGN KEY ([SOC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_SOCIOS_PROF_SOCI_PROFESOR] FOREIGN KEY ([SOC_CODEMP], [SOC_PRFID]) REFERENCES [dbo].[PROFESORES] ([PRF_CODEMP], [PRF_PRFID]),
    CONSTRAINT [FK_SOCIOS_USARIOS_S_USUARIOS] FOREIGN KEY ([SOC_CODEMP], [SOC_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[SOCIOS]([SOC_CODEMP] ASC, [SOC_SOCID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_USU]
    ON [dbo].[SOCIOS]([SOC_CODEMP] ASC, [SOC_USRID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos socios, del gimnasio con sus datos personales y algunos datos anexos para el tema de seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOCIOS';

