﻿CREATE TABLE [dbo].[PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA] (
    [ID]           INT             NOT NULL,
    [ITEM]         VARCHAR (400)   NOT NULL,
    [VALOR]        DECIMAL (15, 2) NOT NULL,
    [FEC_REGISTRO] DATETIME        CONSTRAINT [DF_PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA] PRIMARY KEY CLUSTERED ([ID] ASC)
);

