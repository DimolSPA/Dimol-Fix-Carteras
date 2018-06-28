CREATE TABLE [dbo].[CABACERA_COMPROBANTES] (
    [CBC_CODEMP]     INT             NOT NULL,
    [CBC_SUCID]      INT             NOT NULL,
    [CBC_TPCID]      INT             NOT NULL,
    [CBC_NUMERO]     NUMERIC (15)    NOT NULL,
    [CBC_NUMPROVCLI] VARCHAR (20)    NULL,
    [CBC_PCLID]      NUMERIC (15)    NULL,
    [CBC_FECEMI]     DATETIME        NOT NULL,
    [CBC_FECCPBT]    DATETIME        NOT NULL,
    [CBC_FECVENC]    DATETIME        NOT NULL,
    [CBC_FECENT]     DATETIME        NULL,
    [CBC_CODMON]     INT             NULL,
    [CBC_TIPCAMBIO]  DECIMAL (15, 2) DEFAULT ((1)) NOT NULL,
    [CBC_FRPID]      INT             NOT NULL,
    [CBC_ANIO]       INT             NULL,
    [CBC_MES]        NUMERIC (2)     NULL,
    [CBC_GLOSA]      VARCHAR (250)   NULL,
    [CBC_PORCDESC]   DECIMAL (8, 2)  DEFAULT ((0)) NOT NULL,
    [CBC_NETO]       DECIMAL (12, 2) NOT NULL,
    [CBC_IMPUESTOS]  DECIMAL (12, 2) NOT NULL,
    [CBC_RETENIDO]   DECIMAL (12, 2) NOT NULL,
    [CBC_DESCUENTOS] DECIMAL (12, 2) NOT NULL,
    [CBC_FINAL]      DECIMAL (15, 2) NOT NULL,
    [CBC_SALDO]      DECIMAL (15, 2) NOT NULL,
    [CBC_ORDCOMP]    VARCHAR (20)    NULL,
    [CBT_GASTJUD]    CHAR (1)        DEFAULT ('N') NULL,
    [CBT_VDEID]      INT             NULL,
    [CBT_ESTADO]     CHAR (1)        DEFAULT ('E') NULL,
    [CBT_TNTID]      INT             NULL,
    [CBT_TGDID]      INT             NULL,
    [CBT_TTLID]      INT             NULL,
    [CBC_EXENTO]     DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [CBC_PCSID]      INT             NULL,
    [CBC_FECCONT]    DATETIME        NULL,
    [CBC_FECOC]      DATETIME        NULL,
    CONSTRAINT [PK_CABACERA_COMPROBANTES] PRIMARY KEY CLUSTERED ([CBC_CODEMP] ASC, [CBC_SUCID] ASC, [CBC_TPCID] ASC, [CBC_NUMERO] ASC),
    CONSTRAINT [CKC_CBT_ESTADO_CABACERA] CHECK ([CBT_ESTADO] IS NULL OR ([CBT_ESTADO]='X' OR [CBT_ESTADO]='C' OR [CBT_ESTADO]='B' OR [CBT_ESTADO]='F' OR [CBT_ESTADO]='A' OR [CBT_ESTADO]='E')),
    CONSTRAINT [CKC_CBT_GASTJUD_CABACERA] CHECK ([CBT_GASTJUD] IS NULL OR ([CBT_GASTJUD]='J' OR [CBT_GASTJUD]='P' OR [CBT_GASTJUD]='N')),
    CONSTRAINT [FK_CABACERA_CABCPBT_T_TIPOS_CA] FOREIGN KEY ([CBC_CODEMP], [CBT_TNTID]) REFERENCES [dbo].[TIPOS_CAUSA_NCND] ([TNT_CODEMP], [TNT_TNTID]),
    CONSTRAINT [FK_CABACERA_EMPSUC_CA_EMPRESA_] FOREIGN KEY ([CBC_CODEMP], [CBC_SUCID]) REFERENCES [dbo].[EMPRESA_SUCURSAL] ([ESU_CODEMP], [ESU_SUCID]),
    CONSTRAINT [FK_CABACERA_FORMPAG_C_FORMAS_P] FOREIGN KEY ([CBC_CODEMP], [CBC_FRPID]) REFERENCES [dbo].[FORMAS_PAGO] ([FRP_CODEMP], [FRP_FRPID]),
    CONSTRAINT [FK_CABACERA_MONEDAS_C_MONEDAS] FOREIGN KEY ([CBC_CODEMP], [CBC_CODMON]) REFERENCES [dbo].[MONEDAS] ([MON_CODEMP], [MON_CODMON]),
    CONSTRAINT [FK_CABACERA_PERCONT_C_PERIODOS] FOREIGN KEY ([CBC_CODEMP], [CBC_ANIO], [CBC_MES]) REFERENCES [dbo].[PERIODOS_CONTABLES_MESES] ([PCM_CODEMP], [PCM_ANIO], [PCM_MES]),
    CONSTRAINT [FK_CABACERA_PROVCLI_C_PROVCLI] FOREIGN KEY ([CBC_CODEMP], [CBC_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID]),
    CONSTRAINT [FK_CABACERA_PROVSUC_C_PROVCLI_] FOREIGN KEY ([CBC_CODEMP], [CBC_PCLID], [CBC_PCSID]) REFERENCES [dbo].[PROVCLI_SUCURSAL] ([PCS_CODEMP], [PCS_PCLID], [PCS_PCSID]),
    CONSTRAINT [FK_CABACERA_TIPCPBT_C_TIPOS_CP] FOREIGN KEY ([CBC_CODEMP], [CBC_TPCID]) REFERENCES [dbo].[TIPOS_CPBTDOC] ([TPC_CODEMP], [TPC_TPCID]),
    CONSTRAINT [FK_CABACERA_TIPGD_CAB_TIPOS_CA] FOREIGN KEY ([CBC_CODEMP], [CBT_TGDID]) REFERENCES [dbo].[TIPOS_CAUSA_GUIAS] ([TGD_CODEMP], [TGD_TGDID]),
    CONSTRAINT [FK_CABACERA_TIPTRASL__TIPOS_TR] FOREIGN KEY ([CBC_CODEMP], [CBT_TTLID]) REFERENCES [dbo].[TIPOS_TRASLADO] ([TTL_CODEMP], [TTL_TTLID]),
    CONSTRAINT [FK_CABACERA_VEND_CABC_VENDEDOR] FOREIGN KEY ([CBC_CODEMP], [CBC_SUCID], [CBT_VDEID]) REFERENCES [dbo].[VENDEDORES] ([VDE_CODEMP], [VDE_SUCID], [VDE_VDEID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CABACERA_COMPROBANTES]([CBC_CODEMP] ASC, [CBC_SUCID] ASC, [CBC_TPCID] ASC, [CBC_NUMERO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas cabeceras para cada comprobante que generara el sistema
   
   pueden ser tanto para clientes, deudores o cartera de deudores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CABACERA_COMPROBANTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se genera el comprobante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CABACERA_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'CBC_FECEMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del comprobante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CABACERA_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'CBC_FECCPBT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Vencimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CABACERA_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'CBC_FECVENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la cual se entrega los productos (Solo para venta)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CABACERA_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'CBC_FECENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, la fecha de la OC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CABACERA_COMPROBANTES', @level2type = N'COLUMN', @level2name = N'CBC_FECOC';

