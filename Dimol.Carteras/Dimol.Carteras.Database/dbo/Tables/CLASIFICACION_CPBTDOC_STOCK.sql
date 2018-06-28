CREATE TABLE [dbo].[CLASIFICACION_CPBTDOC_STOCK] (
    [CCS_CODEMP]   INT      NOT NULL,
    [CCS_CLBID]    INT      NOT NULL,
    [CCS_STOCK]    SMALLINT NOT NULL,
    [CCS_SALDOS]   CHAR (1) DEFAULT ('N') NOT NULL,
    [CCS_RESERVA]  CHAR (1) DEFAULT ('N') NOT NULL,
    [CCS_TRANSITO] CHAR (1) DEFAULT ('N') NULL,
    CONSTRAINT [PK_CLASIFICACION_CPBTDOC_STOCK] PRIMARY KEY CLUSTERED ([CCS_CODEMP] ASC, [CCS_CLBID] ASC),
    CONSTRAINT [CKC_CCS_RESERVA_CLASIFIC] CHECK ([CCS_RESERVA]='N' OR [CCS_RESERVA]='S'),
    CONSTRAINT [CKC_CCS_SALDOS_CLASIFIC] CHECK ([CCS_SALDOS]='N' OR [CCS_SALDOS]='S'),
    CONSTRAINT [CKC_CCS_STOCK_CLASIFIC] CHECK ([CCS_STOCK]=(0) OR [CCS_STOCK]=(-1) OR [CCS_STOCK]=(1)),
    CONSTRAINT [CKC_CCS_TRANSITO_CLASIFIC] CHECK ([CCS_TRANSITO] IS NULL OR ([CCS_TRANSITO]='N' OR [CCS_TRANSITO]='S')),
    CONSTRAINT [FK_CLASIFIC_CLACPBT_S_CLASIFIC] FOREIGN KEY ([CCS_CODEMP], [CCS_CLBID]) REFERENCES [dbo].[CLASIFICACION_CPBTDOC] ([CLB_CODEMP], [CLB_CLBID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CLASIFICACION_CPBTDOC_STOCK]([CCS_CODEMP] ASC, [CCS_CLBID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los datos referentes a stock por cada clasificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica que accion realizara el stock
   
   1 Suma
   -1 Resta
   0 Nada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC_STOCK', @level2type = N'COLUMN', @level2name = N'CCS_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si, el comprobante manejara saldos tanto de insumos como productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC_STOCK', @level2type = N'COLUMN', @level2name = N'CCS_SALDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si se reserva o no stock', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC_STOCK', @level2type = N'COLUMN', @level2name = N'CCS_RESERVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si sera utilizado los stock como transito
   
   generalmente se utilizara para los cuadros de embarque', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC_STOCK', @level2type = N'COLUMN', @level2name = N'CCS_TRANSITO';

