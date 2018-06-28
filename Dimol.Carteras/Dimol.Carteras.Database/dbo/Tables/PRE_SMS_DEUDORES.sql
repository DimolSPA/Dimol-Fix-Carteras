﻿CREATE TABLE [dbo].[PRE_SMS_DEUDORES] (
    [CODEMP]            INT          NOT NULL,
    [CTCID]             NUMERIC (15) NOT NULL,
    [NUMERO]            NUMERIC (12) NOT NULL,
    [FECHA_CREACION]    DATETIME     NOT NULL,
    [USUARIO_CREACION]  INT          NOT NULL,
    [FECHA_ENVIO]       DATETIME     NOT NULL,
    [ESTADO]            CHAR (1)     NOT NULL,
    [FECHA_ULT_MODIF]   DATETIME     NOT NULL,
    [USUARIO_ULT_MODIF] INT          NOT NULL,
    CONSTRAINT [PK_PRE_SMS_DEUDORES] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [CTCID] ASC, [NUMERO] ASC),
    CONSTRAINT [CKC_PRE_SMS_DEUDORES_ESTADO] CHECK ([ESTADO]='V' OR [ESTADO]='N'),
    CONSTRAINT [FK_PRE_SMS_DEUDORES_DEUDORES] FOREIGN KEY ([CODEMP], [CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID])
);
