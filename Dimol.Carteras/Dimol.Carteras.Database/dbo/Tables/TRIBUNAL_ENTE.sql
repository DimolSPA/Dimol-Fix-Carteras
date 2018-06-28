﻿CREATE TABLE [dbo].[TRIBUNAL_ENTE] (
    [CODEMP] INT  NOT NULL,
    [TRBID]  INT  NOT NULL,
    [ETJID]  INT  NOT NULL,
    [PCLID]  INT  NOT NULL,
    [FECHA]  DATE NULL,
    [USRID]  INT  NULL,
    CONSTRAINT [PK_TRIBUNAL_ENTE] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [TRBID] ASC, [ETJID] ASC, [PCLID] ASC)
);

