﻿CREATE TABLE [dbo].[MATERIA_ESTADOS] (
    [MEJ_CODEMP] INT      NOT NULL,
    [MEJ_ESJID]  INT      NOT NULL,
    [MEJ_ESTID]  SMALLINT NOT NULL,
    CONSTRAINT [PK_MATERIA_ESTADOS] PRIMARY KEY CLUSTERED ([MEJ_CODEMP] ASC, [MEJ_ESJID] ASC, [MEJ_ESTID] ASC),
    CONSTRAINT [FK_MATERIA__ESTCART_E_ESTADOS_] FOREIGN KEY ([MEJ_CODEMP], [MEJ_ESTID]) REFERENCES [dbo].[ESTADOS_CARTERA] ([ECT_CODEMP], [ECT_ESTID]),
    CONSTRAINT [FK_MATERIA__MATERIAES_MATERIA_] FOREIGN KEY ([MEJ_CODEMP], [MEJ_ESJID]) REFERENCES [dbo].[MATERIA_JUDICIAL] ([ESJ_CODEMP], [ESJ_ESJID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[MATERIA_ESTADOS]([MEJ_CODEMP] ASC, [MEJ_ESJID] ASC, [MEJ_ESTID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas materias, con sus respectivos estados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MATERIA_ESTADOS';
