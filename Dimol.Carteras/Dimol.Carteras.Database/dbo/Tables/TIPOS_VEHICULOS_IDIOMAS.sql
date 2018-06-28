﻿CREATE TABLE [dbo].[TIPOS_VEHICULOS_IDIOMAS] (
    [TVI_CODEMP] INT          NOT NULL,
    [TVI_TVCID]  INT          NOT NULL,
    [TVI_IDID]   INT          NOT NULL,
    [TVI_NOMBRE] VARCHAR (60) NOT NULL,
    CONSTRAINT [PK_TIPOS_VEHICULOS_IDIOMAS] PRIMARY KEY CLUSTERED ([TVI_CODEMP] ASC, [TVI_TVCID] ASC, [TVI_IDID] ASC),
    CONSTRAINT [FK_TIPOS_VE_IDIOMAS_T_IDIOMAS] FOREIGN KEY ([TVI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID]),
    CONSTRAINT [FK_TIPOS_VE_TVC_IDIOM_TIPOS_VE] FOREIGN KEY ([TVI_CODEMP], [TVI_TVCID]) REFERENCES [dbo].[TIPOS_VEHICULOS] ([TVC_CODEMP], [TVC_TVCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_VEHICULOS_IDIOMAS]([TVI_CODEMP] ASC, [TVI_TVCID] ASC, [TVI_IDID] ASC);

