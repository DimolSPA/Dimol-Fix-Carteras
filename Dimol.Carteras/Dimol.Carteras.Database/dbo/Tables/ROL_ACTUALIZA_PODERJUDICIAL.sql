﻿CREATE TABLE [dbo].[ROL_ACTUALIZA_PODERJUDICIAL] (
    [CODEMP]             INT         NOT NULL,
    [ROLID]              INT         NOT NULL,
    [FLAG_PODERJUDICIAL] VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ROL_ACTUALIZA_PODERJUDICIAL] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [ROLID] ASC)
);

