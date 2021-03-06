﻿CREATE TABLE [dbo].[PANEL_QUIEBRA] (
    [QUIEBRA_ID]     INT             NOT NULL,
    [CODEMP]         INT             NOT NULL,
    [ROLID]          INT             NOT NULL,
    [ROLNUMERO]      VARCHAR (20)    NOT NULL,
    [SBCID]          INT             NULL,
    [CUANTIA]        DECIMAL (15, 2) NULL,
    [USRID_REGISTRO] INT             NOT NULL,
    [FEC_REGISTRO]   DATETIME        CONSTRAINT [DF_PANEL_QUIEBRA_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PANEL_QUIEBRA] PRIMARY KEY CLUSTERED ([QUIEBRA_ID] ASC)
);

