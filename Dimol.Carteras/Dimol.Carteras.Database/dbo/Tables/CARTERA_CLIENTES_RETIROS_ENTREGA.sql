CREATE TABLE [dbo].[CARTERA_CLIENTES_RETIROS_ENTREGA] (
    [CRE_CODEMP]     INT           NOT NULL,
    [CRE_PCLID]      NUMERIC (15)  NOT NULL,
    [CRE_CTCID]      NUMERIC (15)  NOT NULL,
    [CRE_CCBID]      INT           NOT NULL,
    [CRE_FECHA]      DATETIME      NOT NULL,
    [CRE_TREID]      INT           NOT NULL,
    [CRE_HORINI]     DATETIME      NOT NULL,
    [CRE_HORFIN]     DATETIME      NOT NULL,
    [CRE_COMID]      INT           NULL,
    [CRE_DIRECCION]  VARCHAR (400) NULL,
    [CRE_TELEFONO]   VARCHAR (80)  NULL,
    [CRE_CONTACTO]   VARCHAR (200) NULL,
    [CRE_COMENTARIO] TEXT          NULL,
    [CRE_TIPO]       CHAR (1)      NULL,
    [CRE_COPIA]      CHAR (1)      NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES_RETIROS_EN] PRIMARY KEY NONCLUSTERED ([CRE_CODEMP] ASC, [CRE_PCLID] ASC, [CRE_CTCID] ASC, [CRE_CCBID] ASC, [CRE_FECHA] ASC),
    CONSTRAINT [CKC_CRE_COPIA_CARTERA_] CHECK ([CRE_COPIA] IS NULL OR ([CRE_COPIA]='N' OR [CRE_COPIA]='S')),
    CONSTRAINT [CKC_CRE_TIPO_CARTERA_] CHECK ([CRE_TIPO] IS NULL OR ([CRE_TIPO]='E' OR [CRE_TIPO]='R')),
    CONSTRAINT [FK_CARTERA__CARTCLICP_CARTERA_2] FOREIGN KEY ([CRE_CODEMP], [CRE_PCLID], [CRE_CTCID], [CRE_CCBID]) REFERENCES [dbo].[CARTERA_CLIENTES_CPBT_DOC] ([CCB_CODEMP], [CCB_PCLID], [CCB_CTCID], [CCB_CCBID]),
    CONSTRAINT [FK_CARTERA__COMUNA_RE_COMUNA] FOREIGN KEY ([CRE_CODEMP]) REFERENCES [dbo].[COMUNA] ([COM_COMID]),
    CONSTRAINT [FK_CARTERA__TIPRETENT_TIPOS_RE] FOREIGN KEY ([CRE_CODEMP], [CRE_TREID]) REFERENCES [dbo].[TIPOS_RETIRO_ENTREGA] ([TRE_CODEMP], [TRE_TREID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CARTERA_CLIENTES_RETIROS_ENTREGA]([CRE_CODEMP] ASC, [CRE_CTCID] ASC, [CRE_CCBID] ASC, [CRE_PCLID] ASC, [CRE_FECHA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos tipos de entrega o retiros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_RETIROS_ENTREGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si es un retiro o una entrega
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_RETIROS_ENTREGA', @level2type = N'COLUMN', @level2name = N'CRE_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se debe llevar o no cuarta copia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_RETIROS_ENTREGA', @level2type = N'COLUMN', @level2name = N'CRE_COPIA';

