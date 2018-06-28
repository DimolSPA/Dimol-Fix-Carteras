CREATE TABLE [dbo].[INSUMOS] (
    [INS_CODEMP]            INT             NOT NULL,
    [INS_INSID]             NUMERIC (15)    NOT NULL,
    [INS_CODIGO]            VARCHAR (30)    NOT NULL,
    [INS_NOMBRE]            VARCHAR (200)   NOT NULL,
    [INS_ESTADO]            CHAR (1)        DEFAULT ('U') NOT NULL,
    [INS_STOCK_TOTAL]       DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_STOCK_RESERVADO]   DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_STOCK_TRANSITO]    DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_STOCK_MERMA]       DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_STOCK_MINIMO]      DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_STOCK_MAXIMO]      DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_STOCK_CIERRE_ANIO] DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_ORDEN_BODEGA]      INT             NOT NULL,
    [INS_ORDEN_OTRO]        INT             NOT NULL,
    [INS_FECINGRESO]        DATETIME        NOT NULL,
    [INS_FECFIN]            SMALLINT        NULL,
    [INS_ARANCEL]           CHAR (1)        DEFAULT ('N') NOT NULL,
    [INS_PORCARAN]          DECIMAL (8, 2)  DEFAULT ((0)) NOT NULL,
    [INS_EXENTO]            CHAR (1)        DEFAULT ('S') NOT NULL,
    [INS_TIPO]              SMALLINT        DEFAULT ((1)) NOT NULL,
    [INS_TERMINADO]         CHAR (1)        DEFAULT ('N') NOT NULL,
    [INS_COSTO]             DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_COSTO_PROM]        DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_TIPING]            CHAR (1)        DEFAULT ('P') NOT NULL,
    [INS_TIPID]             INT             NOT NULL,
    [INS_CATID]             INT             NOT NULL,
    [INS_CUBICAJE]          DECIMAL (15, 2) NOT NULL,
    [INS_UNMLGT]            INT             NULL,
    [INS_ALTO]              DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_ANCHO]             DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_LARGO]             DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [INS_PERECIBLE]         CHAR (1)        DEFAULT ('N') NOT NULL,
    [INS_FECVENC]           DATETIME        NULL,
    [INS_GASTJUD]           CHAR (1)        DEFAULT ('N') NULL,
    [INS_IMPDEU]            CHAR (1)        DEFAULT ('N') NULL,
    [INS_IMPCLI]            CHAR (1)        DEFAULT ('N') NULL,
    [INS_PCTID]             INT             NULL,
    [INS_PACK]              SMALLINT        DEFAULT ((1)) NULL,
    [INS_PACKINT]           SMALLINT        DEFAULT ((1)) NULL,
    [INS_SPCID]             INT             NULL,
    [INS_ABREVIADO]         VARCHAR (50)    NULL,
    CONSTRAINT [PK_INSUMOS] PRIMARY KEY CLUSTERED ([INS_CODEMP] ASC, [INS_INSID] ASC),
    CONSTRAINT [CKC_INS_ARANCEL_INSUMOS] CHECK ([INS_ARANCEL]='N' OR [INS_ARANCEL]='S'),
    CONSTRAINT [CKC_INS_ESTADO_INSUMOS] CHECK ([INS_ESTADO]='E' OR [INS_ESTADO]='U'),
    CONSTRAINT [CKC_INS_EXENTO_INSUMOS] CHECK ([INS_EXENTO]='N' OR [INS_EXENTO]='S'),
    CONSTRAINT [CKC_INS_GASTJUD_INSUMOS] CHECK ([INS_GASTJUD] IS NULL OR ([INS_GASTJUD]='N' OR [INS_GASTJUD]='S')),
    CONSTRAINT [CKC_INS_IMPCLI_INSUMOS] CHECK ([INS_IMPCLI] IS NULL OR ([INS_IMPCLI]='N' OR [INS_IMPCLI]='S')),
    CONSTRAINT [CKC_INS_IMPDEU_INSUMOS] CHECK ([INS_IMPDEU] IS NULL OR ([INS_IMPDEU]='N' OR [INS_IMPDEU]='S')),
    CONSTRAINT [CKC_INS_PERECIBLE_INSUMOS] CHECK ([INS_PERECIBLE]='N' OR [INS_PERECIBLE]='S'),
    CONSTRAINT [CKC_INS_TERMINADO_INSUMOS] CHECK ([INS_TERMINADO]='N' OR [INS_TERMINADO]='S'),
    CONSTRAINT [CKC_INS_TIPING_INSUMOS] CHECK ([INS_TIPING]='I' OR [INS_TIPING]='P'),
    CONSTRAINT [CKC_INS_TIPO_INSUMOS] CHECK ([INS_TIPO]=(2) OR [INS_TIPO]=(1)),
    CONSTRAINT [FK_INSUMOS_CATEGORIA_CATEGORI] FOREIGN KEY ([INS_CODEMP], [INS_CATID]) REFERENCES [dbo].[CATEGORIAS] ([CAT_CODEMP], [CAT_CATID]),
    CONSTRAINT [FK_INSUMOS_EMPRESA_I_EMPRESA] FOREIGN KEY ([INS_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_INSUMOS_PLACTA_IN_PLAN_CUE] FOREIGN KEY ([INS_CODEMP], [INS_PCTID]) REFERENCES [dbo].[PLAN_CUENTAS] ([PCT_CODEMP], [PCT_PCTID]),
    CONSTRAINT [FK_INSUMOS_TIPINS_IN_TIPOS_IN] FOREIGN KEY ([INS_CODEMP], [INS_TIPID]) REFERENCES [dbo].[TIPOS_INSUMO] ([TPI_CODEMP], [TPI_TIPID]),
    CONSTRAINT [FK_INSUMOS_UNIMED_IN_UNIDADES] FOREIGN KEY ([INS_UNMLGT]) REFERENCES [dbo].[UNIDADES_MEDIDA] ([UNM_UNMID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[INSUMOS]([INS_CODEMP] ASC, [INS_INSID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_CODIGO]
    ON [dbo].[INSUMOS]([INS_CODEMP] ASC, [INS_CODIGO] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[INSUMOS]([INS_CODEMP] ASC, [INS_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena todos los tipos de insumos que maneja el sistema
   para crear los productos de la compañia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el producto paga un arancel especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_ARANCEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si es exento de impuesto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_EXENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo me indica de que tipo es el insumo
   
   ejemplo:
   
   1. - Stock
   2.- Sin Stock
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si es un insumo o es un insumo terminado
   
   Si es Si = Television, Camara
   si es No = Tornillo, Alambre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_TERMINADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si la compra actualiza directamente al Insumo o al Producto
   
   I = Insumo
   P= Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_TIPING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el alto, ancho y largo de la medida del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_UNMLGT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el producto es perecible
   
   si es perecible, se debe indicar la fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_PERECIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el gasto sera para algo judicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_GASTJUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el gasto sera imputado al deudor, solo se utiliza para la parte de cartera cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_IMPDEU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si este gasto sera imputado al cliente, solo se utiliza en el modulo cartera cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMOS', @level2type = N'COLUMN', @level2name = N'INS_IMPCLI';

