﻿CREATE TABLE [dbo].[MATERIA_JUDICIAL_IDIOMAS] (
    [MJI_CODEMP] INT           NOT NULL,
    [MJI_ESJID]  INT           NOT NULL,
    [MJI_IDID]   INT           NOT NULL,
    [MJI_NOMBRE] VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_MATERIA_JUDICIAL_IDIOMAS] PRIMARY KEY CLUSTERED ([MJI_CODEMP] ASC, [MJI_ESJID] ASC, [MJI_IDID] ASC),
    CONSTRAINT [FK_MATERIA__IDIOMAS_M_IDIOMAS] FOREIGN KEY ([MJI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID]),
    CONSTRAINT [FK_MATERIA__MATJUD_ID_MATERIA_] FOREIGN KEY ([MJI_CODEMP], [MJI_ESJID]) REFERENCES [dbo].[MATERIA_JUDICIAL] ([ESJ_CODEMP], [ESJ_ESJID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[MATERIA_JUDICIAL_IDIOMAS]([MJI_CODEMP] ASC, [MJI_ESJID] ASC, [MJI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos idiomas las materias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MATERIA_JUDICIAL_IDIOMAS';

