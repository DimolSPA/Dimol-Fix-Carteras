CREATE TABLE [dbo].[GARANTIA] (
    [GRT_CODEMP]     INT             NOT NULL,
    [GRT_GRTID]      INT             NOT NULL,
    [GRT_TIPO]       SMALLINT        NOT NULL,
    [GRT_CTCID]      NUMERIC (15)    NOT NULL,
    [GRT_ESGID]      INT             NOT NULL,
    [GRT_CODIGO]     VARCHAR (30)    NULL,
    [GRT_FECACT]     DATETIME        NULL,
    [GRT_FECVENC]    DATETIME        NULL,
    [GRT_FECEST]     DATETIME        NULL,
    [GRT_CODMON]     INT             NOT NULL,
    [GRT_MONTO]      DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [GRT_SEGURO]     CHAR (1)        DEFAULT ('N') NOT NULL,
    [GRT_FECVENCAVA] DATETIME        NULL,
    [GRT_COMENTARIO] TEXT            NULL,
    CONSTRAINT [PK_GARANTIA] PRIMARY KEY CLUSTERED ([GRT_CODEMP] ASC, [GRT_GRTID] ASC),
    CONSTRAINT [CKC_GRT_SEGURO_GARANTIA] CHECK ([GRT_SEGURO]='N' OR [GRT_SEGURO]='S'),
    CONSTRAINT [CKC_GRT_TIPO_GARANTIA] CHECK ([GRT_TIPO]=(7) OR [GRT_TIPO]=(6) OR [GRT_TIPO]=(5) OR [GRT_TIPO]=(4) OR [GRT_TIPO]=(3) OR [GRT_TIPO]=(2) OR [GRT_TIPO]=(1)),
    CONSTRAINT [FK_GARANTIA_DEU_GARAN_DEUDORES] FOREIGN KEY ([GRT_CODEMP], [GRT_CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID]),
    CONSTRAINT [FK_GARANTIA_ESTGAR_GA_ESTADOS_] FOREIGN KEY ([GRT_CODEMP], [GRT_ESGID]) REFERENCES [dbo].[ESTADOS_GARANTIAS] ([ESG_CODEMP], [ESG_ESGID]),
    CONSTRAINT [FK_GARANTIA_MONEDA_GA_MONEDAS] FOREIGN KEY ([GRT_CODEMP], [GRT_CODMON]) REFERENCES [dbo].[MONEDAS] ([MON_CODEMP], [MON_CODMON])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[GARANTIA]([GRT_CODEMP] ASC, [GRT_GRTID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacenara las distintas garantias del deudor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GARANTIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el tipo de garantia
   
   Ejemplo:
   
   Mercantil
   Hipoteca, etc', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GARANTIA', @level2type = N'COLUMN', @level2name = N'GRT_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si tiene o no seguro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GARANTIA', @level2type = N'COLUMN', @level2name = N'GRT_SEGURO';

