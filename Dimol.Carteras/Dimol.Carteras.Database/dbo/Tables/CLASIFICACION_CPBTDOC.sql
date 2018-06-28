CREATE TABLE [dbo].[CLASIFICACION_CPBTDOC] (
    [CLB_CODEMP]     INT         NOT NULL,
    [CLB_CLBID]      INT         NOT NULL,
    [CLB_CODIGO]     VARCHAR (4) NOT NULL,
    [CLB_TIPCPBTDOC] CHAR (1)    NOT NULL,
    [CLB_TIPPROD]    SMALLINT    NOT NULL,
    [CLB_COSTOS]     CHAR (1)    DEFAULT ('N') NOT NULL,
    [CLB_SELCPBT]    CHAR (1)    NOT NULL,
    [CLB_CARTCLI]    CHAR (1)    DEFAULT ('N') NOT NULL,
    [CLB_CONTABLE]   CHAR (1)    DEFAULT ('N') NOT NULL,
    [CLB_SELAPL]     CHAR (1)    DEFAULT ('N') NOT NULL,
    [CLB_APLICA]     CHAR (1)    DEFAULT ('N') NOT NULL,
    [CLB_CPTOCTBL]   CHAR (1)    NOT NULL,
    [CLB_FINDEUDA]   CHAR (1)    DEFAULT ('N') NOT NULL,
    [CLB_CANCELA]    CHAR (1)    DEFAULT ('N') NOT NULL,
    [CLB_LIBCOMPRA]  SMALLINT    DEFAULT ((0)) NOT NULL,
    [CLB_CAMBIODOC]  CHAR (1)    NULL,
    [CLB_REMESA]     CHAR (1)    DEFAULT ('N') NULL,
    [CLB_TIPSEL]     SMALLINT    NULL,
    [CLB_SINIMP]     CHAR (1)    DEFAULT ('N') NULL,
    [CLB_FORPAG]     CHAR (1)    DEFAULT ('S') NULL,
    [CLB_ORDCOMP]    CHAR (1)    DEFAULT ('N') NULL,
    CONSTRAINT [PK_CLASIFICACION_CPBTDOC] PRIMARY KEY NONCLUSTERED ([CLB_CODEMP] ASC, [CLB_CLBID] ASC),
    CONSTRAINT [CKC_CLB_APLICA_CLASIFIC] CHECK ([CLB_APLICA]='N' OR [CLB_APLICA]='S'),
    CONSTRAINT [CKC_CLB_CAMBIODOC_CLASIFIC] CHECK ([CLB_CAMBIODOC] IS NULL OR ([CLB_CAMBIODOC]='N' OR [CLB_CAMBIODOC]='S')),
    CONSTRAINT [CKC_CLB_CANCELA_CLASIFIC] CHECK ([CLB_CANCELA]='N' OR [CLB_CANCELA]='S'),
    CONSTRAINT [CKC_CLB_CARTCLI_CLASIFIC] CHECK ([CLB_CARTCLI]='N' OR [CLB_CARTCLI]='S'),
    CONSTRAINT [CKC_CLB_CONTABLE_CLASIFIC] CHECK ([CLB_CONTABLE]='N' OR [CLB_CONTABLE]='S'),
    CONSTRAINT [CKC_CLB_COSTOS_CLASIFIC] CHECK ([CLB_COSTOS]='N' OR [CLB_COSTOS]='S'),
    CONSTRAINT [CKC_CLB_CPTOCTBL_CLASIFIC] CHECK ([CLB_CPTOCTBL]='N' OR [CLB_CPTOCTBL]='T' OR [CLB_CPTOCTBL]='E' OR [CLB_CPTOCTBL]='I'),
    CONSTRAINT [CKC_CLB_FINDEUDA_CLASIFIC] CHECK ([CLB_FINDEUDA]='N' OR [CLB_FINDEUDA]='S'),
    CONSTRAINT [CKC_CLB_FORPAG_CLASIFIC] CHECK ([CLB_FORPAG] IS NULL OR ([CLB_FORPAG]='N' OR [CLB_FORPAG]='S')),
    CONSTRAINT [CKC_CLB_LIBCOMPRA_CLASIFIC] CHECK ([CLB_LIBCOMPRA]=(5) OR [CLB_LIBCOMPRA]=(4) OR [CLB_LIBCOMPRA]=(3) OR [CLB_LIBCOMPRA]=(2) OR [CLB_LIBCOMPRA]=(1) OR [CLB_LIBCOMPRA]=(0)),
    CONSTRAINT [CKC_CLB_REMESA_CLASIFIC] CHECK ([CLB_REMESA] IS NULL OR ([CLB_REMESA]='N' OR [CLB_REMESA]='S')),
    CONSTRAINT [CKC_CLB_SELAPL_CLASIFIC] CHECK ([CLB_SELAPL]='N' OR [CLB_SELAPL]='S'),
    CONSTRAINT [CKC_CLB_SELCPBT_CLASIFIC] CHECK ([CLB_SELCPBT]='N' OR [CLB_SELCPBT]='S'),
    CONSTRAINT [CKC_CLB_SINIMP_CLASIFIC] CHECK ([CLB_SINIMP] IS NULL OR ([CLB_SINIMP]='N' OR [CLB_SINIMP]='S')),
    CONSTRAINT [CKC_CLB_TIPCPBTDOC_CLASIFIC] CHECK ([CLB_TIPCPBTDOC]='D' OR [CLB_TIPCPBTDOC]='A' OR [CLB_TIPCPBTDOC]='T' OR [CLB_TIPCPBTDOC]='V' OR [CLB_TIPCPBTDOC]='C'),
    CONSTRAINT [CKC_CLB_TIPPROD_CLASIFIC] CHECK ([CLB_TIPPROD]=(3) OR [CLB_TIPPROD]=(2) OR [CLB_TIPPROD]=(1)),
    CONSTRAINT [CKC_CLB_TIPSEL_CLASIFIC] CHECK ([CLB_TIPSEL] IS NULL OR ([CLB_TIPSEL]=(4) OR [CLB_TIPSEL]=(3) OR [CLB_TIPSEL]=(2) OR [CLB_TIPSEL]=(1))),
    CONSTRAINT [FK_CLASIFIC_EMPRESA_C_EMPRESA] FOREIGN KEY ([CLB_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CLASIFICACION_CPBTDOC]([CLB_CODEMP] ASC, [CLB_CLBID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_CODIGO]
    ON [dbo].[CLASIFICACION_CPBTDOC]([CLB_CODEMP] ASC, [CLB_CODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, indicara la forma en que se comportara cada uno de los comprobantes y documentos diarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, un codigo identificatorio para cada clasificacion de comprobante
   
   ejemplo:
   
   FAC -> Facturas de Compras
   FAV -> Facturas de Ventas
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el comprobante manejara o no costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_COSTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el comprobante selecciona o no de algun comprobante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_SELCPBT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica
   si la clasificacion de comprobante es solo para la cartera de los clientes o no y se si seleccionaran documentos y comprobantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_CARTCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si es contable, la clasificacion de comprobante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_CONTABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si lo que se va a seleccionar es por cancelacion de Remesa, o sea de lo que se ha recuperado para poder devolver al cliente, solamente se mostrara este campo si se selecciona tipo Venta y si selecciona desde Cartera Cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_SELAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el documento diario aplica o no, contra algun documento o comprobante o entre ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_APLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esto indica, el tipo de asiento contable, si no es contable no hace nada pero si lo es.. debe exigir el campo
   
   Solo se utilizara para los TiposCpbtDoc, que sean Documentos ("D")', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_CPTOCTBL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si la deuda (Solo aplica para el modulo de cobranza) se dara por cancelada o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_FINDEUDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el comprobante podra ser seleccionada para realizar algun tipo de cancelacion, contra algun documento, comprobante, etc...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_CANCELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el comprobante aparecera o no en alguno de los tipos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_LIBCOMPRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el comprobante sera utilizado para realizar cambio de documentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_CAMBIODOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el documento, podra ser remesado esto solo se utiliza para el modulo de cobranza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_REMESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indicara si, al momento de seleccinar desde algun comprobante. Este sera un contable o no contable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_TIPSEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica que el comprobante tomara encuenta o no los impuestos
   
   por default es N', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_SINIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el comprobante solicita o no forma de pago
   
   generalmente se utilizara para los castigos, devoluciones etc. que no llevan forma de pagos
   
   por default es S', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_FORPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si se exige el campo orden de compra o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLASIFICACION_CPBTDOC', @level2type = N'COLUMN', @level2name = N'CLB_ORDCOMP';

