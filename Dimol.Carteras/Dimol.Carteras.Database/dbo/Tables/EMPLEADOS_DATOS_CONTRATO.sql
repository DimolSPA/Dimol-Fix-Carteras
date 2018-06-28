CREATE TABLE [dbo].[EMPLEADOS_DATOS_CONTRATO] (
    [EDC_CODEMP]       INT             NOT NULL,
    [EDC_EMPLID]       INT             NOT NULL,
    [EDC_CARID]        INT             NOT NULL,
    [EDC_TICID]        INT             NOT NULL,
    [EDC_AFPID]        INT             NOT NULL,
    [EDC_AFP]          DECIMAL (6, 2)  NOT NULL,
    [EDC_AFC]          DECIMAL (6, 2)  NOT NULL,
    [EDC_ISPID]        INT             NOT NULL,
    [EDC_ISAPRE]       DECIMAL (6, 2)  NOT NULL,
    [EDC_ISPMON]       CHAR (1)        NOT NULL,
    [EDC_CODMON]       INT             NULL,
    [EDC_VALMON]       DECIMAL (12, 2) NULL,
    [EDC_CJCID]        INT             NULL,
    [EDC_CAJA]         DECIMAL (6, 2)  NOT NULL,
    [EDC_FECING]       DATETIME        NOT NULL,
    [EDC_FECFIN]       DATETIME        NULL,
    [EDC_SUELDOBASE]   DECIMAL (12, 2) NOT NULL,
    [EDC_HORASSEMANA]  DECIMAL (10, 2) NOT NULL,
    [EDC_HORASDIARIAS] DECIMAL (10, 2) NOT NULL,
    [EDC_HORAENT]      VARCHAR (8)     NOT NULL,
    [EDC_HORASAL]      VARCHAR (8)     NOT NULL,
    [EDC_DIATRA1]      SMALLINT        DEFAULT ((0)) NOT NULL,
    [EDC_DIATRA2]      SMALLINT        DEFAULT ((0)) NOT NULL,
    [EDC_DIATRA3]      SMALLINT        DEFAULT ((0)) NOT NULL,
    [EDC_DIATRA4]      SMALLINT        DEFAULT ((0)) NOT NULL,
    [EDC_DIATRA5]      SMALLINT        DEFAULT ((0)) NOT NULL,
    [EDC_DIATRA6]      SMALLINT        DEFAULT ((0)) NOT NULL,
    [EDC_DIATRA7]      SMALLINT        DEFAULT ((0)) NOT NULL,
    [EDC_COLACION]     DECIMAL (18)    NULL,
    CONSTRAINT [PK_EMPLEADOS_DATOS_CONTRATO] PRIMARY KEY NONCLUSTERED ([EDC_CODEMP] ASC, [EDC_EMPLID] ASC),
    CONSTRAINT [CKC_EDC_DIATRA1_EMPLEADO] CHECK ([EDC_DIATRA1]=(1) OR [EDC_DIATRA1]=(0)),
    CONSTRAINT [CKC_EDC_DIATRA2_EMPLEADO] CHECK ([EDC_DIATRA2]=(1) OR [EDC_DIATRA2]=(0)),
    CONSTRAINT [CKC_EDC_DIATRA3_EMPLEADO] CHECK ([EDC_DIATRA3]=(1) OR [EDC_DIATRA3]=(0)),
    CONSTRAINT [CKC_EDC_DIATRA4_EMPLEADO] CHECK ([EDC_DIATRA4]=(1) OR [EDC_DIATRA4]=(0)),
    CONSTRAINT [CKC_EDC_DIATRA5_EMPLEADO] CHECK ([EDC_DIATRA5]=(1) OR [EDC_DIATRA5]=(0)),
    CONSTRAINT [CKC_EDC_DIATRA6_EMPLEADO] CHECK ([EDC_DIATRA6]=(1) OR [EDC_DIATRA6]=(0)),
    CONSTRAINT [CKC_EDC_DIATRA7_EMPLEADO] CHECK ([EDC_DIATRA7]=(1) OR [EDC_DIATRA7]=(0)),
    CONSTRAINT [FK_EMPLEADO_AFP_DATCO_AFP] FOREIGN KEY ([EDC_CODEMP], [EDC_AFPID]) REFERENCES [dbo].[AFP] ([AFP_CODEMP], [AFP_AFPID]),
    CONSTRAINT [FK_EMPLEADO_CAJACOMP__CAJA_COM] FOREIGN KEY ([EDC_CODEMP], [EDC_CJCID]) REFERENCES [dbo].[CAJA_COMPENSACION] ([CJC_CODEMP], [CJC_CJCID]),
    CONSTRAINT [FK_EMPLEADO_CARGO_DAT_CARGOS] FOREIGN KEY ([EDC_CODEMP], [EDC_CARID]) REFERENCES [dbo].[CARGOS] ([CAR_CODEMP], [CAR_CARID]),
    CONSTRAINT [FK_EMPLEADO_EMPLEADOS_EMPLEADO4] FOREIGN KEY ([EDC_CODEMP], [EDC_EMPLID]) REFERENCES [dbo].[EMPLEADOS] ([EPL_CODEMP], [EPL_EMPLID]),
    CONSTRAINT [FK_EMPLEADO_ISAPRES_D_ISAPRES] FOREIGN KEY ([EDC_CODEMP], [EDC_ISPID]) REFERENCES [dbo].[ISAPRES] ([ISP_CODEMP], [ISP_ISPID]),
    CONSTRAINT [FK_EMPLEADO_TIPCONT_D_TIPOS_CO] FOREIGN KEY ([EDC_CODEMP], [EDC_TICID]) REFERENCES [dbo].[TIPOS_CONTRATO] ([TIC_CODEMP], [TIC_TICID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[EMPLEADOS_DATOS_CONTRATO]([EDC_CODEMP] ASC, [EDC_EMPLID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En esta tabla, se almacena los distintos valores para los contratos se utilizara para realizar el proceso de sueldos, ya que esta tabla, almacena parte de la informacion vital para dichos calculos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_DATOS_CONTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el contrato de la isapre es en alguna moneda especifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_DATOS_CONTRATO', @level2type = N'COLUMN', @level2name = N'EDC_ISPMON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el dia lunes si lo trabaja o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_DATOS_CONTRATO', @level2type = N'COLUMN', @level2name = N'EDC_DIATRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el dia lunes si lo trabaja o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_DATOS_CONTRATO', @level2type = N'COLUMN', @level2name = N'EDC_DIATRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el dia lunes si lo trabaja o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_DATOS_CONTRATO', @level2type = N'COLUMN', @level2name = N'EDC_DIATRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el dia lunes si lo trabaja o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_DATOS_CONTRATO', @level2type = N'COLUMN', @level2name = N'EDC_DIATRA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el dia lunes si lo trabaja o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_DATOS_CONTRATO', @level2type = N'COLUMN', @level2name = N'EDC_DIATRA5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el dia lunes si lo trabaja o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_DATOS_CONTRATO', @level2type = N'COLUMN', @level2name = N'EDC_DIATRA6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el dia lunes si lo trabaja o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_DATOS_CONTRATO', @level2type = N'COLUMN', @level2name = N'EDC_DIATRA7';

