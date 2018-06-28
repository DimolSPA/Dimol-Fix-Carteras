CREATE TABLE [dbo].[DETALLE_COMPROBANTES_PROVCLI] (
    [DBP_CODEMP]  INT             NOT NULL,
    [DBP_SUCID]   INT             NOT NULL,
    [DBP_TPCID]   INT             NOT NULL,
    [DBP_NUMERO]  NUMERIC (15)    NOT NULL,
    [DBP_ITEM]    SMALLINT        NOT NULL,
    [DBP_SUBITEM] INT             NOT NULL,
    [DBP_PCLID]   NUMERIC (15)    NOT NULL,
    [DBP_CTCID]   NUMERIC (15)    NULL,
    [DBP_MONTO]   DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DETALLE_COMPROBANTES_PROVCL] PRIMARY KEY CLUSTERED ([DBP_CODEMP] ASC, [DBP_SUCID] ASC, [DBP_TPCID] ASC, [DBP_NUMERO] ASC, [DBP_ITEM] ASC, [DBP_SUBITEM] ASC),
    CONSTRAINT [FK_DETALLE__DCPBT_PRO_DETALLE_] FOREIGN KEY ([DBP_CODEMP], [DBP_SUCID], [DBP_TPCID], [DBP_NUMERO], [DBP_ITEM]) REFERENCES [dbo].[DETALLE_COMPROBANTES] ([DCC_CODEMP], [DCC_SUCID], [DCC_TPCID], [DCC_NUMERO], [DCC_ITEM]),
    CONSTRAINT [FK_DETALLE__PROVCLI_D_PROVCLI] FOREIGN KEY ([DBP_CODEMP], [DBP_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DETALLE_COMPROBANTES_PROVCLI]([DBP_CODEMP] ASC, [DBP_SUCID] ASC, [DBP_TPCID] ASC, [DBP_NUMERO] ASC, [DBP_ITEM] ASC, [DBP_SUBITEM] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos gastos que se imputaran al cliente, por los distintos conceptos que sean facturados por parte dela empresa.
   
   Ejemplo: Boleta de Honorario, Factura de Telefono, Factura de SMS, etc...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DETALLE_COMPROBANTES_PROVCLI';

