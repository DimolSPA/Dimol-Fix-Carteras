CREATE TABLE [dbo].[UTILITARIOS_PROVCLI] (
    [UTI_CODEMP]  INT          NOT NULL,
    [UTI_UTLID]   INT          NOT NULL,
    [UTI_PCLID]   NUMERIC (15) NOT NULL,
    [UTI_TIPCART] SMALLINT     DEFAULT ((0)) NOT NULL,
    [UTI_CADDOC]  CHAR (1)     DEFAULT ('N') NOT NULL,
    CONSTRAINT [PK_UTILITARIOS_PROVCLI] PRIMARY KEY CLUSTERED ([UTI_CODEMP] ASC, [UTI_UTLID] ASC, [UTI_PCLID] ASC),
    CONSTRAINT [CKC_UTI_CADDOC_UTILITAR] CHECK ([UTI_CADDOC]='N' OR [UTI_CADDOC]='S'),
    CONSTRAINT [CKC_UTI_TIPCART_UTILITAR] CHECK ([UTI_TIPCART]=(3) OR [UTI_TIPCART]=(2) OR [UTI_TIPCART]=(1) OR [UTI_TIPCART]=(0)),
    CONSTRAINT [FK_UTILITAR_PROVCLI_U_PROVCLI] FOREIGN KEY ([UTI_CODEMP], [UTI_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID]),
    CONSTRAINT [FK_UTILITAR_UTI_PROVC_UTILITAR] FOREIGN KEY ([UTI_CODEMP], [UTI_UTLID]) REFERENCES [dbo].[UTILITARIOS] ([UTL_CODEMP], [UTL_UTLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[UTILITARIOS_PROVCLI]([UTI_CODEMP] ASC, [UTI_UTLID] ASC, [UTI_PCLID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos tipos de parametros que utilizara para hacer cada proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UTILITARIOS_PROVCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se revisara la cartera que tiene asociada, si es 0 no revisara ninguna cartera de deudores y si revisara su propia cta cte.
   
   Con eso podremos revisar las facturas por vencer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UTILITARIOS_PROVCLI', @level2type = N'COLUMN', @level2name = N'UTI_CADDOC';

