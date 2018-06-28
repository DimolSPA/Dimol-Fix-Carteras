CREATE TABLE [dbo].[CLASIFICACION_CPBTDOC_CONTABLE] (
    [CCT_CODEMP]     INT      NOT NULL,
    [CCT_CLBID]      INT      NOT NULL,
    [CCT_DEBHAB]     SMALLINT NOT NULL,
    [CCT_LIBCOMVEN]  CHAR (1) DEFAULT ('N') NOT NULL,
    [CCT_HONORARIOS] CHAR (1) DEFAULT ('N') NOT NULL,
    [CCT_PCTID]      INT      NOT NULL,
    [CCT_PCTID2]     INT      NULL,
    CONSTRAINT [PK_CLASIFICACION_CPBTDOC_CONTA] PRIMARY KEY CLUSTERED ([CCT_CODEMP] ASC, [CCT_CLBID] ASC),
    CONSTRAINT [CKC_CCT_DEBHAB_CLASIFIC] CHECK ([CCT_DEBHAB]=(-1) OR [CCT_DEBHAB]=(1)),
    CONSTRAINT [CKC_CCT_HONORARIOS_CLASIFIC] CHECK ([CCT_HONORARIOS]='N' OR [CCT_HONORARIOS]='S'),
    CONSTRAINT [CKC_CCT_LIBCOMVEN_CLASIFIC] CHECK ([CCT_LIBCOMVEN]='N' OR [CCT_LIBCOMVEN]='S'),
    CONSTRAINT [FK_CLASIFIC_CLACPBT_C_CLASIFIC] FOREIGN KEY ([CCT_CODEMP], [CCT_CLBID]) REFERENCES [dbo].[CLASIFICACION_CPBTDOC] ([CLB_CODEMP], [CLB_CLBID]),
    CONSTRAINT [FK_CLASIFIC_PLACTA_CC_PLAN_CUE] FOREIGN KEY ([CCT_CODEMP], [CCT_PCTID]) REFERENCES [dbo].[PLAN_CUENTAS] ([PCT_CODEMP], [PCT_PCTID]),
    CONSTRAINT [FK_CLASIFIC_PLACTA_CL_PLAN_CUE] FOREIGN KEY ([CCT_CODEMP], [CCT_PCTID2]) REFERENCES [dbo].[PLAN_CUENTAS] ([PCT_CODEMP], [PCT_PCTID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CLASIFICACION_CPBTDOC_CONTABLE]([CCT_CODEMP] ASC, [CCT_CLBID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos datos de la clasificacion de cpbt para los conceptos contables', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC_CONTABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el concepto contable que hare, si ira al debe o haber
   
   ejemplo
   
   1 Debe
   -1 Haber', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC_CONTABLE', @level2type = N'COLUMN', @level2name = N'CCT_DEBHAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si estos documentos apereceran o no en el libro de compra y venta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC_CONTABLE', @level2type = N'COLUMN', @level2name = N'CCT_LIBCOMVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si la clasificacion es para honorarios o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC_CONTABLE', @level2type = N'COLUMN', @level2name = N'CCT_HONORARIOS';

