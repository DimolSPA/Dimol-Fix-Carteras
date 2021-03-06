﻿CREATE TABLE [dbo].[tmp_TIPOS_VEHICULOS_IDIOMAS] (
    [TVI_CODEMP] INT            NOT NULL,
    [TVI_TVCID]  INT            NOT NULL,
    [TVI_IDID]   INT            NOT NULL,
    [TVI_NOMBRE] VARBINARY (60) NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[tmp_TIPOS_VEHICULOS_IDIOMAS]([TVI_CODEMP] ASC, [TVI_TVCID] ASC, [TVI_IDID] ASC);

