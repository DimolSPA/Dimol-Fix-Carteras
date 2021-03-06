﻿CREATE TABLE [dbo].[PODER_JUDICIAL_RECEPTOR] (
    [ID_CAUSA]    INT           NOT NULL,
    [ID_CUADERNO] INT           NOT NULL,
    [CUADERNO]    VARCHAR (200) NOT NULL,
    [ESTADO]      VARCHAR (200) NOT NULL,
    [RECEPTOR]    VARCHAR (200) NOT NULL,
    [FECHA]       DATETIME      NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IDX_ID_CAUSA_PJR]
    ON [dbo].[PODER_JUDICIAL_RECEPTOR]([ID_CAUSA] ASC);

