﻿CREATE TABLE [dbo].[SITREL_TELEFONO] (
    [CODEMP]        INT          NOT NULL,
    [ID_CARGA]      INT          NOT NULL,
    [PCLID]         INT          NOT NULL,
    [RUT]           VARCHAR (9)  NOT NULL,
    [NUMERO]        VARCHAR (20) NOT NULL,
    [CODIGO_AREA]   VARCHAR (20) NULL,
    [ANEXO]         VARCHAR (10) NULL,
    [TIPO_TELEFONO] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_SITREL_TELEFONO] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [ID_CARGA] ASC, [PCLID] ASC, [RUT] ASC, [NUMERO] ASC, [TIPO_TELEFONO] ASC)
);

