CREATE TABLE [dbo].[PRODUCTOS_STOCK] (
    [PST_CODEMP]            INT             NOT NULL,
    [PST_PRODID]            NUMERIC (15)    NOT NULL,
    [PST_UNMLGT]            INT             NOT NULL,
    [PST_ALTO]              DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PST_ANCHO]             DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PST_LARGO]             DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PST_CUBICAJE]          DECIMAL (15, 2) NOT NULL,
    [PST_UNMPESO]           INT             NOT NULL,
    [PST_PESO]              DECIMAL (15, 2) NOT NULL,
    [PST_UNMSTOCK]          INT             NOT NULL,
    [PST_STOCK_CIERRE_ANIO] DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PST_STOCK_MINIMO]      DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PST_STOCK_MERMA]       DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PST_STOCK_TRANSITO]    DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PST_STOCK_RESERVADO]   DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PST_STOCK_TOTAL]       DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PST_ORDEN_BODEGA]      INT             DEFAULT ((5)) NOT NULL,
    [PST_ORDEN_OTRO]        INT             DEFAULT ((5)) NOT NULL,
    [PST_ARMADO]            CHAR (1)        DEFAULT ('N') NOT NULL,
    [PST_TIPARM]            SMALLINT        DEFAULT ((0)) NOT NULL,
    [PST_CLOSEOUT]          CHAR (1)        DEFAULT ('N') NOT NULL,
    [PST_STOCK_MAXIMO]      DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [PST_PACK]              SMALLINT        DEFAULT ((1)) NULL,
    [PST_PACKINT]           SMALLINT        DEFAULT ((1)) NULL,
    CONSTRAINT [PK_PRODUCTOS_STOCK] PRIMARY KEY NONCLUSTERED ([PST_CODEMP] ASC, [PST_PRODID] ASC),
    CONSTRAINT [CKC_PDT_ARMADO_PRODUCTO] CHECK ([PST_ARMADO]='N' OR [PST_ARMADO]='S'),
    CONSTRAINT [CKC_PDT_CLOSEOUT_PRODUCTO] CHECK ([PST_CLOSEOUT]='N' OR [PST_CLOSEOUT]='S'),
    CONSTRAINT [CKC_PDT_TIPARM_PRODUCTO] CHECK ([PST_TIPARM]=(2) OR [PST_TIPARM]=(1) OR [PST_TIPARM]=(0)),
    CONSTRAINT [FK_PRODUCTO_PRODUCTOS_PRODUCTO3] FOREIGN KEY ([PST_CODEMP], [PST_PRODID]) REFERENCES [dbo].[PRODUCTOS] ([PDT_CODEMP], [PDT_PRODID]),
    CONSTRAINT [FK_PRODUCTO_UNIM_PROD_UNIDADES] FOREIGN KEY ([PST_UNMPESO]) REFERENCES [dbo].[UNIDADES_MEDIDA] ([UNM_UNMID]),
    CONSTRAINT [FK_PRODUCTO_UNIMED_PR_UNIDADES] FOREIGN KEY ([PST_UNMSTOCK]) REFERENCES [dbo].[UNIDADES_MEDIDA] ([UNM_UNMID]),
    CONSTRAINT [FK_PRODUCTO_UNIMED_PS_UNIDADES] FOREIGN KEY ([PST_UNMLGT]) REFERENCES [dbo].[UNIDADES_MEDIDA] ([UNM_UNMID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PRODUCTOS_STOCK]([PST_CODEMP] ASC, [PST_PRODID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las diferentes especificaciones del producto
   
   ejemplo, stock, medidas, peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta unidad de medida indica, el alto,ancho y largo dependiendo de la medida de la caja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS_STOCK', @level2type = N'COLUMN', @level2name = N'PST_UNMLGT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el armado sera desde un insumo o un producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS_STOCK', @level2type = N'COLUMN', @level2name = N'PST_TIPARM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si es un producto CloseOut (descontinuado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS_STOCK', @level2type = N'COLUMN', @level2name = N'PST_CLOSEOUT';

