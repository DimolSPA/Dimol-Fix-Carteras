﻿CREATE TABLE [dbo].[SITREL_SUCURSAL] (
    [CODEMP] INT          NOT NULL,
    [PCLID]  INT          NOT NULL,
    [CODIGO] VARCHAR (20) NOT NULL,
    [NOMBRE] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SITREL_SUCURSAL] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [PCLID] ASC, [CODIGO] ASC)
);

