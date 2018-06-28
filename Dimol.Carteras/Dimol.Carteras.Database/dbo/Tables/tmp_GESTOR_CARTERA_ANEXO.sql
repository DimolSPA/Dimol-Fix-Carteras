CREATE TABLE [dbo].[tmp_GESTOR_CARTERA_ANEXO] (
    [GSA_CODEMP]   INT             NOT NULL,
    [GSA_SUCID]    INT             NOT NULL,
    [GSA_GESID]    INT             NOT NULL,
    [GSA_CTCID]    NUMERIC (15)    NOT NULL,
    [GSA_GESID2]   INT             NOT NULL,
    [GSA_FECASIG]  DATETIME        NULL,
    [GSA_PORCOM]   DECIMAL (10, 4) DEFAULT ((0)) NULL,
    [GSA_PORCOMGP] DECIMAL (10, 4) DEFAULT ((0)) NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[tmp_GESTOR_CARTERA_ANEXO]([GSA_CODEMP] ASC, [GSA_SUCID] ASC, [GSA_GESID] ASC, [GSA_CTCID] ASC, [GSA_GESID2] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena otro gestor anexo a la cobranza que tiene un gestor en especial
   
   con esta tabla, se podra gestionar el mismo deudor par mas de 1 gestor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tmp_GESTOR_CARTERA_ANEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el procentaje de para el gestor padre, dueño de la cartera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tmp_GESTOR_CARTERA_ANEXO', @level2type = N'COLUMN', @level2name = N'GSA_PORCOMGP';

