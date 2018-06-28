CREATE TABLE [dbo].[tmp_GESTOR_CARTERA] (
    [GSC_CODEMP]  INT          NOT NULL,
    [GSC_SUCID]   INT          NOT NULL,
    [GSC_GESID]   INT          NOT NULL,
    [GSC_CTCID]   NUMERIC (15) NOT NULL,
    [GSC_FECASIG] DATETIME     NOT NULL,
    [GSC_PCLID]   INT          NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[tmp_GESTOR_CARTERA]([GSC_CODEMP] ASC, [GSC_SUCID] ASC, [GSC_GESID] ASC, [GSC_CTCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla indica, la cartera para cada gestor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tmp_GESTOR_CARTERA';

