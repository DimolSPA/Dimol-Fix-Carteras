CREATE TABLE [dbo].[CARTERA_CLIENTES_CAMPANA_CPBTDOC] (
    [CCD_CODEMP] INT          NOT NULL,
    [CCD_SUCID]  INT          NOT NULL,
    [CCD_CCCID]  INT          NOT NULL,
    [CCD_PCLID]  NUMERIC (15) NOT NULL,
    [CCD_CTCID]  NUMERIC (15) NOT NULL,
    [CCD_CCBID]  INT          NOT NULL,
    [CCD_ESTADO] CHAR (1)     DEFAULT ('N') NOT NULL,
    [CCD_ESTID]  SMALLINT     NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES_CAMPANA_CP] PRIMARY KEY CLUSTERED ([CCD_CODEMP] ASC, [CCD_SUCID] ASC, [CCD_CCCID] ASC, [CCD_PCLID] ASC, [CCD_CTCID] ASC, [CCD_CCBID] ASC),
    CONSTRAINT [CKC_CCD_ESTADO_CARTERA_] CHECK ([CCD_ESTADO]='N' OR [CCD_ESTADO]='S'),
    CONSTRAINT [FK_CARTERA__CARTCLICA_CARTERA_] FOREIGN KEY ([CCD_CODEMP], [CCD_SUCID], [CCD_CCCID]) REFERENCES [dbo].[CARTERA_CLIENTES_CAMPANA] ([CCC_CODEMP], [CCC_SUCID], [CCC_CCCID]),
    CONSTRAINT [FK_CARTERA__CARTCLICP_CARTERA_] FOREIGN KEY ([CCD_CODEMP], [CCD_PCLID], [CCD_CTCID], [CCD_CCBID]) REFERENCES [dbo].[CARTERA_CLIENTES_CPBT_DOC] ([CCB_CODEMP], [CCB_PCLID], [CCB_CTCID], [CCB_CCBID]),
    CONSTRAINT [FK_CARTERA__ESTCART_C_ESTADOS_] FOREIGN KEY ([CCD_CODEMP], [CCD_ESTID]) REFERENCES [dbo].[ESTADOS_CARTERA] ([ECT_CODEMP], [ECT_ESTID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CARTERA_CLIENTES_CAMPANA_CPBTDOC]([CCD_CODEMP] ASC, [CCD_SUCID] ASC, [CCD_CCCID] ASC, [CCD_PCLID] ASC, [CCD_CTCID] ASC, [CCD_CCBID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos documentos que contendra cada campaña, con la cual se sabra el valor total de la campaña tambien, si el documento o no fue gestionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CAMPANA_CPBTDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si se gestiono o no, el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CAMPANA_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CCD_ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el estado final del documento al realizar la campaña, servira para ver la cantidad de cambios y que tan efectiva resulto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CAMPANA_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CCD_ESTID';

