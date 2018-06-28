CREATE TABLE [dbo].[DETALLE_COMPROBANTES] (
    [DCC_CODEMP]       INT             NOT NULL,
    [DCC_SUCID]        INT             NOT NULL,
    [DCC_TPCID]        INT             NOT NULL,
    [DCC_NUMERO]       NUMERIC (15)    NOT NULL,
    [DCC_ITEM]         SMALLINT        NOT NULL,
    [DCC_INSID]        NUMERIC (15)    NULL,
    [DCC_PRODID]       NUMERIC (15)    NULL,
    [DCC_PCLID]        NUMERIC (15)    NULL,
    [DCC_CTCID]        NUMERIC (15)    NULL,
    [DCC_CCBID]        INT             NULL,
    [DCC_PREREAL]      DECIMAL (13, 2) NOT NULL,
    [DCC_PRECIO]       DECIMAL (13, 2) DEFAULT ((0)) NOT NULL,
    [DCC_CANTIDAD]     DECIMAL (10, 2) DEFAULT ((0)) NOT NULL,
    [DCC_SALDO]        DECIMAL (13, 2) DEFAULT ((0)) NOT NULL,
    [DCC_NETO]         DECIMAL (17, 2) DEFAULT ((0)) NOT NULL,
    [DCC_IMPUESTO]     DECIMAL (10, 2) DEFAULT ((0)) NOT NULL,
    [DCC_RETENIDO]     CHAR (1)        DEFAULT ('N') NOT NULL,
    [DCC_TOTAL]        DECIMAL (17, 2) DEFAULT ((0)) NOT NULL,
    [DCC_INTERES]      DECIMAL (10, 2) DEFAULT ((0)) NOT NULL,
    [DCC_HONORARIO]    DECIMAL (10, 2) DEFAULT ((0)) NOT NULL,
    [DCC_GASTPRE]      DECIMAL (10, 2) DEFAULT ((0)) NOT NULL,
    [DCC_GASTJUD]      DECIMAL (10, 2) DEFAULT ((0)) NOT NULL,
    [DCC_PORCFACT]     DECIMAL (7, 4)  DEFAULT ((0)) NOT NULL,
    [DCC_PORCHON]      DECIMAL (7, 4)  DEFAULT ((0)) NOT NULL,
    [DCC_BODID]        INT             NULL,
    [DCC_BDSID]        INT             NULL,
    [DCC_POSICION]     SMALLINT        NULL,
    [DCC_TPCIDPAD]     INT             NULL,
    [DCC_NUMEROPAD]    NUMERIC (15)    NULL,
    [DCC_ITEMPAD]      SMALLINT        NULL,
    [DCC_BODIDDES]     INT             NULL,
    [DCC_BDSIDDES]     INT             NULL,
    [DCC_POSICIONDES]  SMALLINT        NULL,
    [DCC_NUMSERIE]     NUMERIC (12)    NULL,
    [DCC_NUMSERIEPROV] VARCHAR (30)    NULL,
    [DCC_CANTEBJ]      DECIMAL (10, 2) DEFAULT ((0)) NULL,
    [DCC_LTPID]        INT             NULL,
    [DCC_BSCID]        VARCHAR (10)    NULL,
    [DCC_BSCIDDES]     VARCHAR (10)    NULL,
    [DCC_ANIO]         INT             NULL,
    [DCC_NUMAPL]       NUMERIC (15)    NULL,
    [DCC_ITEMAPL]      INT             NULL,
    [DCC_VALREM]       DECIMAL (10, 3) DEFAULT ((0)) NULL,
    [DCC_COMENTARIO]   TEXT            NULL,
    [DCC_SUBITEM]      INT             NULL,
    [DCC_EXENTO]       DECIMAL (10, 2) DEFAULT ((0)) NULL,
    CONSTRAINT [PK_DETALLE_COMPROBANTES] PRIMARY KEY CLUSTERED ([DCC_CODEMP] ASC, [DCC_SUCID] ASC, [DCC_TPCID] ASC, [DCC_NUMERO] ASC, [DCC_ITEM] ASC),
    CONSTRAINT [CKC_DCC_RETENIDO_DETALLE_] CHECK ([DCC_RETENIDO]='N' OR [DCC_RETENIDO]='S'),
    CONSTRAINT [FK_DETALLE__APLI_DETC_APLICACI] FOREIGN KEY ([DCC_CODEMP], [DCC_SUCID], [DCC_ANIO], [DCC_NUMAPL], [DCC_ITEMAPL]) REFERENCES [dbo].[APLICACIONES_ITEMS] ([API_CODEMP], [API_SUCID], [API_ANIO], [API_NUMAPL], [API_ITEM]),
    CONSTRAINT [FK_DETALLE__BODSEC_DE_BODEGAS_] FOREIGN KEY ([DCC_CODEMP], [DCC_BODID], [DCC_BDSID]) REFERENCES [dbo].[BODEGAS_SECTOR] ([BDS_CODEMP], [BDS_BODID], [BDS_BDSID]),
    CONSTRAINT [FK_DETALLE__BODSECDES_BODEGAS_] FOREIGN KEY ([DCC_CODEMP], [DCC_BODIDDES], [DCC_BDSIDDES]) REFERENCES [dbo].[BODEGAS_SECTOR] ([BDS_CODEMP], [BDS_BODID], [BDS_BDSID]),
    CONSTRAINT [FK_DETALLE__CABCPBT_D_CABACERA] FOREIGN KEY ([DCC_CODEMP], [DCC_SUCID], [DCC_TPCID], [DCC_NUMERO]) REFERENCES [dbo].[CABACERA_COMPROBANTES] ([CBC_CODEMP], [CBC_SUCID], [CBC_TPCID], [CBC_NUMERO]),
    CONSTRAINT [FK_DETALLE__CARTCLI_D_CARTERA_] FOREIGN KEY ([DCC_CODEMP], [DCC_PCLID], [DCC_CTCID], [DCC_CCBID]) REFERENCES [dbo].[CARTERA_CLIENTES_CPBT_DOC] ([CCB_CODEMP], [CCB_PCLID], [CCB_CTCID], [CCB_CCBID]),
    CONSTRAINT [FK_DETALLE__DETCPBT_D_DETALLE_] FOREIGN KEY ([DCC_CODEMP], [DCC_SUCID], [DCC_TPCIDPAD], [DCC_NUMEROPAD], [DCC_ITEMPAD]) REFERENCES [dbo].[DETALLE_COMPROBANTES] ([DCC_CODEMP], [DCC_SUCID], [DCC_TPCID], [DCC_NUMERO], [DCC_ITEM]),
    CONSTRAINT [FK_DETALLE__INSU_DETC_INSUMOS] FOREIGN KEY ([DCC_CODEMP], [DCC_INSID]) REFERENCES [dbo].[INSUMOS] ([INS_CODEMP], [INS_INSID]),
    CONSTRAINT [FK_DETALLE__LISTPRE_D_LISTAS_P] FOREIGN KEY ([DCC_CODEMP], [DCC_LTPID]) REFERENCES [dbo].[LISTAS_PRECIOS] ([LTP_CODEMP], [LTP_LTPID]),
    CONSTRAINT [FK_DETALLE__PROD_DETC_PRODUCTO] FOREIGN KEY ([DCC_CODEMP], [DCC_PRODID]) REFERENCES [dbo].[PRODUCTOS] ([PDT_CODEMP], [PDT_PRODID])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DETALLE_COMPROBANTES]([DCC_CODEMP] ASC, [DCC_SUCID] ASC, [DCC_TPCID] ASC, [DCC_NUMERO] ASC, [DCC_ITEM] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacenara todo lo referente a los disntintos detalles que maneje cada comprobante
   
   Ejemplo
   
   Detalle de insumos
   Detalle de productos
   Detalle de Cartera Cliente
   Detalle de traspaso entre bodegas
   
   
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el impuesto es retenido o no, 
   
   Si es S, se suma a la parte de impuesto retenido
   
   N, al impuesto normal de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'DCC_RETENIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el porcentaje de facturacion sobre lo recuperado
   
   Se utilizara solo para el tema de las Cartera de clientes, al momento de remesar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'DCC_PORCFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el porcentaje de facturacion sobre lo recuperado
   
   Se utilizara solo para el tema de las Cartera de clientes, al momento de remesar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'DCC_PORCHON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo almacena los distintos numeros de serie (Este campo se utiliza, solo para el caso que este habilitado el modulo de produccion)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'DCC_NUMSERIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el numero de serie del proveedor, se utiliza en caso que se traiga maquinaria o productos a los cuales la empresa debe realizar mantenciones por garantia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'DCC_NUMSERIEPROV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la cantidad que ha sido embalado, 
   
   dependiendo de la caja en la cual se ha guardado
   
   no puede superar la cantidad, tanto en mayor cantidad o menor de lo que indica la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'DCC_CANTEBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el año de la aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'DCC_ANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo sera usado, para facturar los gastos ocasionados por las cartera de dudores....
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'DCC_SUBITEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo almacena lo exento ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'DCC_EXENTO';

