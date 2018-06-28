﻿CREATE TABLE [dbo].[GESTOR_CARTERA] (
    [GSC_CODEMP]  INT          NOT NULL,
    [GSC_SUCID]   INT          NOT NULL,
    [GSC_GESID]   INT          NOT NULL,
    [GSC_CTCID]   NUMERIC (15) NOT NULL,
    [GSC_FECASIG] DATETIME     NOT NULL,
    [GSC_PCLID]   INT          NOT NULL,
    CONSTRAINT [PK_GESTOR_CARTERA] PRIMARY KEY NONCLUSTERED ([GSC_CODEMP] ASC, [GSC_SUCID] ASC, [GSC_GESID] ASC, [GSC_CTCID] ASC, [GSC_PCLID] ASC),
    CONSTRAINT [FK_GESTOR_C_DEUDORES__DEUDORES] FOREIGN KEY ([GSC_CODEMP], [GSC_CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID]),
    CONSTRAINT [FK_GESTOR_C_GESTOR_CA_GESTOR] FOREIGN KEY ([GSC_CODEMP], [GSC_SUCID], [GSC_GESID]) REFERENCES [dbo].[GESTOR] ([GES_CODEMP], [GES_SUCID], [GES_GESID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[GESTOR_CARTERA]([GSC_CODEMP] ASC, [GSC_SUCID] ASC, [GSC_GESID] ASC, [GSC_CTCID] ASC, [GSC_PCLID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla indica, la cartera para cada gestor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GESTOR_CARTERA';

