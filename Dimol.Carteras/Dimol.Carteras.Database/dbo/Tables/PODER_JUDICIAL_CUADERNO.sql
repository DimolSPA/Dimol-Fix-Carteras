﻿CREATE TABLE [dbo].[PODER_JUDICIAL_CUADERNO] (
    [ID_CAUSA]            INT           NOT NULL,
    [ID_CUADERNO]         INT           NOT NULL,
    [DESC_CUADERNO]       VARCHAR (150) NOT NULL,
    [ESCRITOS_PENDIENTES] VARCHAR (1)   NULL
);
