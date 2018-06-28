CREATE TABLE [dbo].[GESTOR_CARTERA_ANEXO] (
    [GSA_CODEMP]   INT             NOT NULL,
    [GSA_SUCID]    INT             NOT NULL,
    [GSA_GESID]    INT             NOT NULL,
    [GSA_CTCID]    NUMERIC (15)    NOT NULL,
    [GSC_PCLID]    INT             NOT NULL,
    [GSA_GESID2]   INT             NOT NULL,
    [GSA_FECASIG]  DATETIME        NULL,
    [GSA_PORCOM]   DECIMAL (10, 4) DEFAULT ((0)) NULL,
    [GSA_PORCOMGP] DECIMAL (10, 4) DEFAULT ((0)) NULL,
    CONSTRAINT [PK_GESTOR_CARTERA_ANEXO] PRIMARY KEY CLUSTERED ([GSA_CODEMP] ASC, [GSA_SUCID] ASC, [GSA_GESID] ASC, [GSA_CTCID] ASC, [GSC_PCLID] ASC, [GSA_GESID2] ASC),
    CONSTRAINT [FK_GESTOR_C_GESTCART__GESTOR_C] FOREIGN KEY ([GSA_CODEMP], [GSA_SUCID], [GSA_GESID], [GSA_CTCID], [GSC_PCLID]) REFERENCES [dbo].[GESTOR_CARTERA] ([GSC_CODEMP], [GSC_SUCID], [GSC_GESID], [GSC_CTCID], [GSC_PCLID]),
    CONSTRAINT [FK_GESTOR_C_GESTOR_GE_GESTOR] FOREIGN KEY ([GSA_CODEMP], [GSA_SUCID], [GSA_GESID2]) REFERENCES [dbo].[GESTOR] ([GES_CODEMP], [GES_SUCID], [GES_GESID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[GESTOR_CARTERA_ANEXO]([GSA_CODEMP] ASC, [GSA_SUCID] ASC, [GSA_GESID] ASC, [GSA_CTCID] ASC, [GSA_GESID2] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena otro gestor anexo a la cobranza que tiene un gestor en especial
   
   con esta tabla, se podra gestionar el mismo deudor par mas de 1 gestor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GESTOR_CARTERA_ANEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el procentaje de para el gestor padre, dueño de la cartera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GESTOR_CARTERA_ANEXO', @level2type = N'COLUMN', @level2name = N'GSA_PORCOMGP';

