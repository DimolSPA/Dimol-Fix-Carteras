CREATE TABLE [dbo].[ASIENTOS_CONTABLES] (
    [AST_CODEMP]     INT             NOT NULL,
    [AST_ANIO]       INT             NOT NULL,
    [AST_TIPO]       CHAR (1)        NOT NULL,
    [AST_NUMERO]     NUMERIC (15)    NOT NULL,
    [AST_NUMFIN]     NUMERIC (15)    NOT NULL,
    [AST_MES]        NUMERIC (2)     NOT NULL,
    [AST_FECEMISION] DATETIME        NOT NULL,
    [AST_FECPERIODO] DATETIME        NOT NULL,
    [AST_ESTADO]     CHAR (1)        DEFAULT ('V') NOT NULL,
    [AST_GLOSA]      TEXT            NOT NULL,
    [AST_TOT_DEBE]   DECIMAL (25, 2) DEFAULT ((0)) NOT NULL,
    [AST_TOT_HABER]  DECIMAL (25, 2) DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ASIENTOS_CONTABLES] PRIMARY KEY CLUSTERED ([AST_CODEMP] ASC, [AST_ANIO] ASC, [AST_TIPO] ASC, [AST_NUMERO] ASC),
    CONSTRAINT [CKC_ASC_ESTADO_ASIENTO_] CHECK ([AST_ESTADO]='X' OR [AST_ESTADO]='P' OR [AST_ESTADO]='N' OR [AST_ESTADO]='V'),
    CONSTRAINT [CKC_AST_TIPO_ASIENTOS] CHECK ([AST_TIPO]='A' OR [AST_TIPO]='T' OR [AST_TIPO]='E' OR [AST_TIPO]='I'),
    CONSTRAINT [FK_ASIENTOS_EMPRESA_A_EMPRESA] FOREIGN KEY ([AST_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_ASIENTOS_PERCONT_A_PERIODOS] FOREIGN KEY ([AST_CODEMP], [AST_ANIO], [AST_MES]) REFERENCES [dbo].[PERIODOS_CONTABLES_MESES] ([PCM_CODEMP], [PCM_ANIO], [PCM_MES])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ASIENTOS_CONTABLES]([AST_CODEMP] ASC, [AST_ANIO] ASC, [AST_TIPO] ASC, [AST_NUMERO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos asientos contables de la empresa, la cual podra ser separado por sucursal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ASIENTOS_CONTABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este sera el numero final del asiento contable, el cual sera unico por año y mes, solamente sera cargado al momento de realizar el cierre de mes contable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ASIENTOS_CONTABLES', @level2type = N'COLUMN', @level2name = N'AST_NUMFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la fecha en la cual fue emitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ASIENTOS_CONTABLES', @level2type = N'COLUMN', @level2name = N'AST_FECEMISION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la fecha en la cual sera contabilizado (reportes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ASIENTOS_CONTABLES', @level2type = N'COLUMN', @level2name = N'AST_FECPERIODO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vigente
   Nulo
   Procesado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ASIENTOS_CONTABLES', @level2type = N'COLUMN', @level2name = N'AST_ESTADO';

