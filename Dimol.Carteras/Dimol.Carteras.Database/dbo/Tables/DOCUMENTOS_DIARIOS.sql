CREATE TABLE [dbo].[DOCUMENTOS_DIARIOS] (
    [DDI_CODEMP]     INT             NOT NULL,
    [DDI_SUCID]      INT             NOT NULL,
    [DDI_ANIO]       SMALLINT        NOT NULL,
    [DDI_NUMDOC]     NUMERIC (15)    NOT NULL,
    [DDI_TPCID]      INT             NOT NULL,
    [DDI_TIPMOV]     CHAR (1)        NOT NULL,
    [DDI_PCLID]      NUMERIC (15)    NULL,
    [DDI_PROPIO]     CHAR (1)        DEFAULT ('N') NOT NULL,
    [DDI_BCOID]      INT             NULL,
    [DDI_CTACTE]     VARCHAR (40)    NOT NULL,
    [DDI_FECING]     DATETIME        NOT NULL,
    [DDI_FECDOC]     DATETIME        NOT NULL,
    [DDI_FECVENC]    DATETIME        NOT NULL,
    [DDI_EDCID]      INT             NOT NULL,
    [DDI_NUMCTA]     VARCHAR (40)    NOT NULL,
    [DDI_MONTO]      DECIMAL (15, 2) NOT NULL,
    [DDI_SALDO]      DECIMAL (15, 2) NOT NULL,
    [DDI_CODMON]     INT             NOT NULL,
    [DDI_TIPCAMBIO]  DECIMAL (15, 2) NOT NULL,
    [DDI_TITULAR]    CHAR (1)        DEFAULT ('S') NOT NULL,
    [DDI_RUTPAG]     VARCHAR (20)    NULL,
    [DDI_NOMPAG]     VARCHAR (200)   NULL,
    [DDI_CTCID]      NUMERIC (15)    NULL,
    [DDI_EMPLEADO]   CHAR (1)        DEFAULT ('N') NOT NULL,
    [DDI_EMPLID]     INT             NULL,
    [DDI_CUSTODIA]   CHAR (1)        DEFAULT ('N') NOT NULL,
    [DDI_DOCEMP]     CHAR (1)        DEFAULT ('N') NOT NULL,
    [DDI_PAGDIR]     CHAR (1)        DEFAULT ('N') NOT NULL,
    [DDI_VECESDEP]   SMALLINT        NOT NULL,
    [DDI_FECHADEP]   DATETIME        NULL,
    [DDI_DEPOSITAR]  CHAR (1)        NOT NULL,
    [DDI_RUTDEP]     VARCHAR (20)    NULL,
    [DDI_NOMDEP]     VARCHAR (20)    NULL,
    [DDI_NRODEP]     VARCHAR (20)    NULL,
    [DDI_FECDEP]     DATETIME        NULL,
    [DDI_PENDIENTE]  CHAR (1)        DEFAULT ('S') NULL,
    [DDI_ANIONEG]    SMALLINT        NULL,
    [DDI_NEGID]      INT             NULL,
    [DDI_COMENTARIO] TEXT            NULL,
    CONSTRAINT [PK_DOCUMENTOS_DIARIOS] PRIMARY KEY NONCLUSTERED ([DDI_CODEMP] ASC, [DDI_SUCID] ASC, [DDI_ANIO] ASC, [DDI_NUMDOC] ASC),
    CONSTRAINT [CKC_DDI_CUSTODIA_DOCUMENT] CHECK ([DDI_CUSTODIA]='N' OR [DDI_CUSTODIA]='S'),
    CONSTRAINT [CKC_DDI_DEPOSITAR_DOCUMENT] CHECK ([DDI_DEPOSITAR]='N' OR [DDI_DEPOSITAR]='S'),
    CONSTRAINT [CKC_DDI_DOCEMP_DOCUMENT] CHECK ([DDI_DOCEMP]='N' OR [DDI_DOCEMP]='S'),
    CONSTRAINT [CKC_DDI_EMPLEADO_DOCUMENT] CHECK ([DDI_EMPLEADO]='N' OR [DDI_EMPLEADO]='S'),
    CONSTRAINT [CKC_DDI_PAGDIR_DOCUMENT] CHECK ([DDI_PAGDIR]='N' OR [DDI_PAGDIR]='S'),
    CONSTRAINT [CKC_DDI_PENDIENTE_DOCUMENT] CHECK ([DDI_PENDIENTE] IS NULL OR ([DDI_PENDIENTE]='N' OR [DDI_PENDIENTE]='S')),
    CONSTRAINT [CKC_DDI_PROPIO_DOCUMENT] CHECK ([DDI_PROPIO]='N' OR [DDI_PROPIO]='S'),
    CONSTRAINT [CKC_DDI_TIPMOV_DOCUMENT] CHECK ([DDI_TIPMOV]='T' OR [DDI_TIPMOV]='E' OR [DDI_TIPMOV]='I'),
    CONSTRAINT [CKC_DDI_TITULAR_DOCUMENT] CHECK ([DDI_TITULAR]='N' OR [DDI_TITULAR]='S'),
    CONSTRAINT [FK_DOCUMENT_BANCOS_DO_BANCOS] FOREIGN KEY ([DDI_CODEMP], [DDI_BCOID]) REFERENCES [dbo].[BANCOS] ([BCO_CODEMP], [BCO_BCOID]),
    CONSTRAINT [FK_DOCUMENT_DEUDORES__DEUDORES] FOREIGN KEY ([DDI_CODEMP], [DDI_CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID]),
    CONSTRAINT [FK_DOCUMENT_EMPLEADOS_EMPLEADO] FOREIGN KEY ([DDI_CODEMP], [DDI_EMPLID]) REFERENCES [dbo].[EMPLEADOS] ([EPL_CODEMP], [EPL_EMPLID]),
    CONSTRAINT [FK_DOCUMENT_ESTDOCDIA_ESTADOS_] FOREIGN KEY ([DDI_CODEMP], [DDI_EDCID]) REFERENCES [dbo].[ESTADOS_DOCUMENTOS_DIARIOS] ([EDC_CODEMP], [EDC_EDCID]),
    CONSTRAINT [FK_DOCUMENT_MONEDAS_D_MONEDAS] FOREIGN KEY ([DDI_CODEMP], [DDI_CODMON]) REFERENCES [dbo].[MONEDAS] ([MON_CODEMP], [MON_CODMON]),
    CONSTRAINT [FK_DOCUMENT_NEGOCIACI_NEGOCIAC] FOREIGN KEY ([DDI_CODEMP], [DDI_ANIONEG], [DDI_NEGID]) REFERENCES [dbo].[NEGOCIACION] ([NEG_CODEMP], [NEG_ANIO], [NEG_NEGID]),
    CONSTRAINT [FK_DOCUMENT_PROVCLI_D_PROVCLI] FOREIGN KEY ([DDI_CODEMP], [DDI_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID]),
    CONSTRAINT [FK_DOCUMENT_TIPDOCCPB_TIPOS_CP] FOREIGN KEY ([DDI_CODEMP], [DDI_TPCID]) REFERENCES [dbo].[TIPOS_CPBTDOC] ([TPC_CODEMP], [TPC_TPCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DOCUMENTOS_DIARIOS]([DDI_CODEMP] ASC, [DDI_SUCID] ASC, [DDI_ANIO] ASC, [DDI_NUMDOC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena todos los moviemintos que suceden en el dia a dia con los documentos diarios.
   Sera el encargado de realizar los cruces entre documentos, comprobantes tec...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el documento es propio o de terceros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_PROPIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el documento es de un titular o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_TITULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el documento sera para un empleado 
   
   ejemplo:
   
   Fondos por rendir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_EMPLEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el documento, estara en custodia por la empresa o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_CUSTODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el documento que se esta ingresando es a nombre de la empresa, pero es para cancelar deudas de deudores en relacion a nuestros clientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_DOCEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si, sera o no un pago directo
   
   Pago Directo: Cliente recibe nuestros intereses y honorarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_PAGDIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica cuantas veces, se ha redepositado el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_VECESDEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el documento sera depositado
   
   si es depositado se debe pedir el banco al cual sera depositado, la cta cte del banco
   a quien se deposita y el numero de deposito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_DEPOSITAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el Rut al cual se esta depositando', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_RUTDEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre a quien se deposita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_NOMDEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo almacena el numero del deposito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_NRODEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el documento diario estara pendiente para ser aplicado contra algun documento
   en algunos casos el documento se puede cancelar inmediatamente.
   
   por default es Si
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS', @level2type = N'COLUMN', @level2name = N'DDI_PENDIENTE';

