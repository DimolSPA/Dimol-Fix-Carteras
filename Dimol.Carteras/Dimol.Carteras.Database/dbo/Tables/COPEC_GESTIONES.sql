﻿CREATE TABLE [dbo].[COPEC_GESTIONES] (
    [CGS_CGSID]  INT      NOT NULL,
    [CGS_CODEMP] INT      NOT NULL,
    [CGS_ESTID]  SMALLINT NOT NULL,
    [CGS_PREJUD] CHAR (1) NOT NULL,
    [CGS_CESID]  INT      NOT NULL,
    [CGS_CETID]  INT      NOT NULL,
    CONSTRAINT [PK_COPEC_GESTIONES] PRIMARY KEY CLUSTERED ([CGS_CGSID] ASC, [CGS_CODEMP] ASC, [CGS_ESTID] ASC),
    CONSTRAINT [FK_COPEC_GESTIONES_COPEC_ESTADO_JUICIO] FOREIGN KEY ([CGS_CESID]) REFERENCES [dbo].[COPEC_ESTADO_JUICIO] ([CES_CESID])
);

