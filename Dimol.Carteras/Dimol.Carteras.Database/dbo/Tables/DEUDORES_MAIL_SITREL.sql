﻿CREATE TABLE [dbo].[DEUDORES_MAIL_SITREL] (
    [CODEMP]  INT           NOT NULL,
    [CTCID]   NUMERIC (15)  NOT NULL,
    [MAIL]    VARCHAR (255) NOT NULL,
    [FECHA]   DATETIME      NOT NULL,
    [ORIGEN]  CHAR (1)      CONSTRAINT [DF__DEUDORES___ORIGE__34B4968F] DEFAULT ('C') NULL,
    [ENVIADO] CHAR (1)      CONSTRAINT [DF__DEUDORES___ENVIA__35A8BAC8] DEFAULT ('N') NULL,
    CONSTRAINT [PK_DEUDORES_MAIL_SITREL] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [CTCID] ASC, [MAIL] ASC),
    CONSTRAINT [CKC_ORIGEN] CHECK ([ORIGEN]='C' OR [ORIGEN]='G'),
    CONSTRAINT [FK_DEUDORES_SITREL] FOREIGN KEY ([CODEMP], [CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID])
);
