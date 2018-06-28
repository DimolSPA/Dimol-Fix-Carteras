﻿CREATE TABLE [dbo].[PANEL_QUIEBRA_DOCUMENTOS] (
    [QUIEBRA_ID]     INT          NOT NULL,
    [CODEMP]         INT          NOT NULL,
    [PCLID]          NUMERIC (15) NOT NULL,
    [CTCID]          NUMERIC (15) NOT NULL,
    [CCBID]          INT          NOT NULL,
    [ESTADO]         VARCHAR (3)  CONSTRAINT [DF_PANEL_QUIEBRA_DOCUMENTOS_ESTADO] DEFAULT ('ACT') NOT NULL,
    [USRID_REGISTRO] INT          NOT NULL,
    [FEC_REGISTRO]   DATETIME     CONSTRAINT [DF_PANEL_QUIEBRA_DOCUMENTOS_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PANEL_QUIEBRA_DOCUMENTOS] PRIMARY KEY CLUSTERED ([QUIEBRA_ID] ASC, [CODEMP] ASC, [PCLID] ASC, [CTCID] ASC, [CCBID] ASC),
    CONSTRAINT [FK_PANEL_QUIEBRA_DOCUMENTOS_CARTERA_CLIENTES_CPBT_DOC] FOREIGN KEY ([CODEMP], [PCLID], [CTCID], [CCBID]) REFERENCES [dbo].[CARTERA_CLIENTES_CPBT_DOC] ([CCB_CODEMP], [CCB_PCLID], [CCB_CTCID], [CCB_CCBID]),
    CONSTRAINT [FK_PANEL_QUIEBRA_DOCUMENTOS_PANEL_QUIEBRA] FOREIGN KEY ([QUIEBRA_ID]) REFERENCES [dbo].[PANEL_QUIEBRA] ([QUIEBRA_ID])
);

