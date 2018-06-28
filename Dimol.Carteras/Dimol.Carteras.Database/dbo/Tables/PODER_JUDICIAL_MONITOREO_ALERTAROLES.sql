﻿CREATE TABLE [dbo].[PODER_JUDICIAL_MONITOREO_ALERTAROLES] (
    [ROL]           INT           NOT NULL,
    [ANIO]          INT           NOT NULL,
    [TRIBUNALID]    INT           NOT NULL,
    [TRIBUNAL]      VARCHAR (400) NOT NULL,
    [ROLENCONTRADO] INT           NULL,
    [MINROL]        INT           NOT NULL,
    [MAXROL]        INT           NOT NULL,
    [FEC_REGISTRO]  DATETIME      CONSTRAINT [DF_PODER_JUDICIAL_MONITOREO_ALERTAROLES_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PODER_JUDICIAL_MONITOREO_ALERTAROLES] PRIMARY KEY CLUSTERED ([ROL] ASC, [ANIO] ASC, [TRIBUNALID] ASC)
);
