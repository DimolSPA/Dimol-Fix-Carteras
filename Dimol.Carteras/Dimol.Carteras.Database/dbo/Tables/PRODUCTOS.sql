CREATE TABLE [dbo].[PRODUCTOS] (
    [PDT_CODEMP]    INT             NOT NULL,
    [PDT_PRODID]    NUMERIC (15)    NOT NULL,
    [PDT_CODFISICO] VARCHAR (20)    NOT NULL,
    [PDT_NOMBRE]    VARCHAR (200)   NOT NULL,
    [PDT_ESTADO]    CHAR (1)        DEFAULT ('U') NOT NULL,
    [PDT_TIPO]      SMALLINT        DEFAULT ((1)) NOT NULL,
    [PDT_MOSTRAR]   CHAR (1)        DEFAULT ('S') NOT NULL,
    [PDT_FECING]    DATETIME        NOT NULL,
    [PDT_FECFIN]    DATETIME        NULL,
    [PDT_PCTID]     INT             NULL,
    [PDT_INSID]     NUMERIC (15)    NULL,
    [PDT_COSTO]     DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [PDT_COSTOPROM] DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [PDT_CC]        SMALLINT        NULL,
    [PDT_EXENTO]    CHAR (1)        DEFAULT ('N') NULL,
    [PDT_IMPESP]    CHAR (1)        DEFAULT ('N') NULL,
    [PDT_STOCK]     CHAR (1)        DEFAULT ('S') NULL,
    [PDT_PERECIBLE] CHAR (1)        DEFAULT ('N') NOT NULL,
    [PDT_GASTJUD]   CHAR (1)        DEFAULT ('N') NOT NULL,
    [PDT_IMPDEU]    CHAR (1)        DEFAULT ('N') NOT NULL,
    [PDT_IMPCLI]    CHAR (1)        DEFAULT ('N') NOT NULL,
    [PDT_CATID]     INT             NULL,
    [PDT_CONBARIMG] IMAGE           NULL,
    [PDT_CODBARRA]  VARCHAR (20)    NULL,
    [PDT_SPCID]     INT             NULL,
    CONSTRAINT [PK_PRODUCTOS] PRIMARY KEY NONCLUSTERED ([PDT_CODEMP] ASC, [PDT_PRODID] ASC),
    CONSTRAINT [CKC_PDT_ESTADO_PRODUCTO] CHECK ([PDT_ESTADO]='E' OR [PDT_ESTADO]='U'),
    CONSTRAINT [CKC_PDT_EXENTO_PRODUCTO] CHECK ([PDT_EXENTO] IS NULL OR ([PDT_EXENTO]='S' OR [PDT_EXENTO]='N')),
    CONSTRAINT [CKC_PDT_GASTJUD_PRODUCTO] CHECK ([PDT_GASTJUD]='N' OR [PDT_GASTJUD]='S'),
    CONSTRAINT [CKC_PDT_IMPCLI_PRODUCTO] CHECK ([PDT_IMPCLI]='N' OR [PDT_IMPCLI]='S'),
    CONSTRAINT [CKC_PDT_IMPDEU_PRODUCTO] CHECK ([PDT_IMPDEU]='N' OR [PDT_IMPDEU]='S'),
    CONSTRAINT [CKC_PDT_IMPESP_PRODUCTO] CHECK ([PDT_IMPESP] IS NULL OR ([PDT_IMPESP]='S' OR [PDT_IMPESP]='N')),
    CONSTRAINT [CKC_PDT_PERECIBLE_PRODUCTO] CHECK ([PDT_PERECIBLE]='N' OR [PDT_PERECIBLE]='S'),
    CONSTRAINT [CKC_PDT_SENIAL_MOSTRA_PRODUCTO] CHECK ([PDT_MOSTRAR]='N' OR [PDT_MOSTRAR]='S'),
    CONSTRAINT [CKC_PDT_STOCK_PRODUCTO] CHECK ([PDT_STOCK] IS NULL OR ([PDT_STOCK]='N' OR [PDT_STOCK]='S')),
    CONSTRAINT [CKC_PDT_TIPO_PRODUCTO] CHECK ([PDT_TIPO]=(2) OR [PDT_TIPO]=(1)),
    CONSTRAINT [FK_PRODUCTO_CATE_PROD_CATEGORI] FOREIGN KEY ([PDT_CODEMP], [PDT_CATID]) REFERENCES [dbo].[CATEGORIAS] ([CAT_CODEMP], [CAT_CATID]),
    CONSTRAINT [FK_PRODUCTO_EMPRESA_P_EMPRESA] FOREIGN KEY ([PDT_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_PRODUCTO_INSU_PROD_INSUMOS] FOREIGN KEY ([PDT_CODEMP], [PDT_INSID]) REFERENCES [dbo].[INSUMOS] ([INS_CODEMP], [INS_INSID]),
    CONSTRAINT [FK_PRODUCTO_PLACTA_PR_PLAN_CUE] FOREIGN KEY ([PDT_CODEMP], [PDT_PCTID]) REFERENCES [dbo].[PLAN_CUENTAS] ([PCT_CODEMP], [PCT_PCTID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PRODUCTOS]([PDT_CODEMP] ASC, [PDT_PRODID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_CODIGO]
    ON [dbo].[PRODUCTOS]([PDT_CODEMP] ASC, [PDT_CODFISICO] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[PRODUCTOS]([PDT_CODEMP] ASC, [PDT_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacenara, los distintos tipos de productos, los cuales contendran distintan informacion dependiendo de las necesidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacenara, los distintos tipos de productos, los cuales contendran distintan informacion dependiendo de las necesidades
   
   1.- Stock
   2.- Sin Stock
   3.- Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS', @level2type = N'COLUMN', @level2name = N'PDT_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el producto es Exento de impuestos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS', @level2type = N'COLUMN', @level2name = N'PDT_EXENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el Producto, maneja o no otro impuesto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS', @level2type = N'COLUMN', @level2name = N'PDT_IMPESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el producto sera con stock o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS', @level2type = N'COLUMN', @level2name = N'PDT_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el producto es perecible
   
   si es perecible, se debe indicar la fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS', @level2type = N'COLUMN', @level2name = N'PDT_PERECIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el producto es o no un gasto judicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS', @level2type = N'COLUMN', @level2name = N'PDT_GASTJUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el gasto judicial sera imputado al deudor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS', @level2type = N'COLUMN', @level2name = N'PDT_IMPDEU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el gasto judicial sera o no imputado al cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS', @level2type = N'COLUMN', @level2name = N'PDT_IMPCLI';

