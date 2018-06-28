CREATE TABLE [dbo].[CARTERA_CLIENTES_CPBT_DOC] (
    [CCB_CODEMP]     INT             NOT NULL,
    [CCB_PCLID]      NUMERIC (15)    NOT NULL,
    [CCB_CTCID]      NUMERIC (15)    NOT NULL,
    [CCB_CCBID]      INT             NOT NULL,
    [CCB_TPCID]      INT             NOT NULL,
    [CCB_TIPCART]    SMALLINT        NOT NULL,
    [CCB_NUMERO]     VARCHAR (30)    NOT NULL,
    [CCB_FECING]     DATETIME        NOT NULL,
    [CCB_FECDOC]     DATETIME        NOT NULL,
    [CCB_FECVENC]    DATETIME        NOT NULL,
    [CCB_FECULTGEST] DATETIME        NOT NULL,
    [CCB_FECPLAZO]   DATETIME        NULL,
    [CCB_FECCALCINT] DATETIME        NULL,
    [CCB_FECCAST]    DATETIME        NULL,
    [CCB_ESTID]      SMALLINT        NULL,
    [CCB_ESTCPBT]    CHAR (1)        DEFAULT ('V') NOT NULL,
    [CCB_CODMON]     INT             NOT NULL,
    [CCB_TIPCAMBIO]  DECIMAL (10, 2) DEFAULT ((1)) NOT NULL,
    [CCB_ASIGNADO]   DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [CCB_MONTO]      DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [CCB_SALDO]      DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [CCB_GASTJUD]    DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [CCB_GASTOTRO]   DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [CCB_INTERESES]  DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [CCB_HONORARIOS] DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [CCB_CALCHON]    CHAR (1)        DEFAULT ('N') NULL,
    [CCB_BCOID]      INT             NULL,
    [CCB_RUTGIR]     VARCHAR (20)    NULL,
    [CCB_NOMGIR]     VARCHAR (200)   NULL,
    [CCB_MTCID]      INT             NULL,
    [CCB_COMENTARIO] TEXT            NULL,
    [CCB_RETENT]     CHAR (1)        NULL,
    [CCB_CODID]      INT             NULL,
    [CCB_NUMESP]     VARCHAR (40)    NULL,
    [CCB_NUMAGRUPA]  VARCHAR (40)    NULL,
    [CCB_CARTA]      SMALLINT        DEFAULT ((0)) NOT NULL,
    [CCB_COBRABLE]   CHAR (1)        DEFAULT ('S') NOT NULL,
    [CCB_CCTID]      INT             NOT NULL,
    [CCB_SBCID]      INT             NULL,
    [CCB_DOCORI]     CHAR (1)        DEFAULT ('N') NULL,
    [CCB_DOCANT]     CHAR (1)        DEFAULT ('N') NULL,
    [CCB_COMPROMISO] DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [CCB_ESTIDJ]     SMALLINT        NULL,
    [TERCEROID]      INT             NULL,
    [CCB_IDCUENTA]   VARCHAR (20)    NULL,
    [CCB_DESCCUENTA] VARCHAR (200)   NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES_CPBT_DOC] PRIMARY KEY NONCLUSTERED ([CCB_CODEMP] ASC, [CCB_PCLID] ASC, [CCB_CTCID] ASC, [CCB_CCBID] ASC),
    CONSTRAINT [CKC_CCB_CALCHON_CARTERA_] CHECK ([CCB_CALCHON] IS NULL OR ([CCB_CALCHON]='N' OR [CCB_CALCHON]='S')),
    CONSTRAINT [CKC_CCB_COBRABLE_CARTERA_] CHECK ([CCB_COBRABLE]='N' OR [CCB_COBRABLE]='S'),
    CONSTRAINT [CKC_CCB_DOCANT_CARTERA_] CHECK ([CCB_DOCANT] IS NULL OR ([CCB_DOCANT]='N' OR [CCB_DOCANT]='S')),
    CONSTRAINT [CKC_CCB_DOCORI_CARTERA_] CHECK ([CCB_DOCORI] IS NULL OR ([CCB_DOCORI]='N' OR [CCB_DOCORI]='S')),
    CONSTRAINT [CKC_CCB_ESTCPBT_CARTERA_] CHECK ([CCB_ESTCPBT]='J' OR [CCB_ESTCPBT]='X' OR [CCB_ESTCPBT]='F' OR [CCB_ESTCPBT]='V'),
    CONSTRAINT [CKC_CCB_RETENT_CARTERA_] CHECK ([CCB_RETENT] IS NULL OR ([CCB_RETENT]='E' OR [CCB_RETENT]='R')),
    CONSTRAINT [CKC_CCB_TIPCAMBIO_CARTERA_] CHECK ([CCB_TIPCAMBIO]>=(1)),
    CONSTRAINT [CKC_CCB_TIPCART_CARTERA_] CHECK ([CCB_TIPCART]=(4) OR [CCB_TIPCART]=(3) OR [CCB_TIPCART]=(2) OR [CCB_TIPCART]=(1)),
    FOREIGN KEY ([CCB_CODEMP], [TERCEROID]) REFERENCES [dbo].[CARTERA_CLIENTES_CPBT_DOC_TERCEROS] ([CODEMP], [TERCEROID]),
    CONSTRAINT [FK_CARTERA__BANCOS_CP_BANCOS] FOREIGN KEY ([CCB_CODEMP], [CCB_BCOID]) REFERENCES [dbo].[BANCOS] ([BCO_CODEMP], [BCO_BCOID]),
    CONSTRAINT [FK_CARTERA__CODCARG_C_PROVCLI_] FOREIGN KEY ([CCB_CODEMP], [CCB_PCLID], [CCB_CODID]) REFERENCES [dbo].[PROVCLI_CODIGO_CARGA] ([PCC_CODEMP], [PCC_PCLID], [PCC_CODID]),
    CONSTRAINT [FK_CARTERA__CONTCART__CONTRATO] FOREIGN KEY ([CCB_CODEMP], [CCB_CCTID]) REFERENCES [dbo].[CONTRATOS_CARTERA] ([CCT_CODEMP], [CCT_CCTID]),
    CONSTRAINT [FK_CARTERA__ESTCART2__ESTADOS_] FOREIGN KEY ([CCB_CODEMP], [CCB_ESTID]) REFERENCES [dbo].[ESTADOS_CARTERA] ([ECT_CODEMP], [ECT_ESTID]),
    CONSTRAINT [FK_CARTERA__ESTCART2__ESTADOS_J] FOREIGN KEY ([CCB_CODEMP], [CCB_ESTIDJ]) REFERENCES [dbo].[ESTADOS_CARTERA] ([ECT_CODEMP], [ECT_ESTID]),
    CONSTRAINT [FK_CARTERA__MOTIVCOB__MOTIVO_C] FOREIGN KEY ([CCB_CODEMP], [CCB_MTCID]) REFERENCES [dbo].[MOTIVO_COBRANZA] ([MTC_CODEMP], [MTC_MTCID]),
    CONSTRAINT [FK_CARTERA__REFERENCE_CARTERA_3] FOREIGN KEY ([CCB_CODEMP], [CCB_PCLID], [CCB_CTCID]) REFERENCES [dbo].[CARTERA_CLIENTES] ([CTC_CODEMP], [CTC_PCLID], [CTC_CTCID]),
    CONSTRAINT [FK_CARTERA__SUBCART_C_SUBCARTE] FOREIGN KEY ([CCB_CODEMP], [CCB_SBCID]) REFERENCES [dbo].[SUBCARTERAS] ([SBC_CODEMP], [SBC_SBCID]),
    CONSTRAINT [FK_CARTERA__TIPCPBTDO_TIPOS_CP] FOREIGN KEY ([CCB_CODEMP], [CCB_TPCID]) REFERENCES [dbo].[TIPOS_CPBTDOC] ([TPC_CODEMP], [TPC_TPCID]),
    CONSTRAINT [FK_CARTERA_CARTERA_CLIENTES_CPBT_DOC_TERCEROS] FOREIGN KEY ([CCB_CODEMP], [TERCEROID]) REFERENCES [dbo].[CARTERA_CLIENTES_CPBT_DOC_TERCEROS] ([CODEMP], [TERCEROID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CARTERA_CLIENTES_CPBT_DOC]([CCB_CODEMP] ASC, [CCB_PCLID] ASC, [CCB_CTCID] ASC, [CCB_CCBID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NRODOC]
    ON [dbo].[CARTERA_CLIENTES_CPBT_DOC]([CCB_CODEMP] ASC, [CCB_PCLID] ASC, [CCB_CTCID] ASC, [CCB_TPCID] ASC, [CCB_NUMERO] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_CARTERA_CLIENTES_CPBT_DOC_5_1781581385__K2_K1_K7_K3_K17_K5]
    ON [dbo].[CARTERA_CLIENTES_CPBT_DOC]([CCB_PCLID] ASC, [CCB_CODEMP] ASC, [CCB_NUMERO] ASC, [CCB_CTCID] ASC, [CCB_CODMON] ASC, [CCB_TPCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara los distintos comprobantes y documentos para cada cartera de clientes
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el tipo de cartera, si es masiva o de cobranza dura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_TIPCART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica una fecha especifica para determinar el dia de pago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_FECPLAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, hasta cuando se congelaran el calculo de intereses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_FECCALCINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la fecha en la cual el documento fue castigado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_FECCAST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el estado del comprobante, si esta vigente, terminado o nulo
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_ESTCPBT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si se debe o no recualcular los honorarios, al momento de cancelarlos en las aplicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_CALCHON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo. indica el rut de la persona que ha girado el documento, esto solamente se utilizara para los cheques', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_RUTGIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si se solicitara retiro o entrega de algun documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_RETENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si se envio la primera Carta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_CARTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si es cobrable, cuando de realiza el traspaso a judicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_COBRABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el contrato en el cual esta asociado el documemnto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_CCTID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el documento que esta en la empresa esta el original o una foto copia o esta en otra parte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_DOCORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si hay antecedentes para este documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_DOCANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el monto comprometido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CPBT_DOC', @level2type = N'COLUMN', @level2name = N'CCB_COMPROMISO';

