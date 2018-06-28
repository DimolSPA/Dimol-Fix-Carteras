CREATE TABLE [dbo].[CLAUSULAS_CONTCART] (
    [CLC_CODEMP]   INT             NOT NULL,
    [CLC_CLCID]    INT             NOT NULL,
    [CLC_NOMBRE]   VARCHAR (200)   NOT NULL,
    [CLC_TIPO]     SMALLINT        DEFAULT ((1)) NOT NULL,
    [CLC_PORCMON]  CHAR (1)        DEFAULT ('P') NOT NULL,
    [CLC_CODMON]   INT             NULL,
    [CLC_VALOR]    DECIMAL (12, 3) DEFAULT ((0)) NOT NULL,
    [CLC_RANGO]    CHAR (1)        DEFAULT ('N') NOT NULL,
    [CLC_TIPRANGO] SMALLINT        DEFAULT ((0)) NOT NULL,
    [CLC_PREJUD]   CHAR (1)        DEFAULT ('P') NULL,
    [CLC_FACCAP]   CHAR (1)        DEFAULT ('N') NULL,
    [CLC_FACINT]   CHAR (1)        DEFAULT ('N') NULL,
    [CLC_FACHON]   CHAR (1)        DEFAULT ('N') NULL,
    [CLC_FACGPRE]  CHAR (1)        DEFAULT ('N') NULL,
    [CLC_FACGJUD]  CHAR (1)        DEFAULT ('S') NULL,
    [CLC_FIJA]     CHAR (1)        DEFAULT ('N') NULL,
    [CLC_ANUMAX]   CHAR (1)        DEFAULT ('N') NULL,
    CONSTRAINT [PK_CLAUSULAS_CONTCART] PRIMARY KEY CLUSTERED ([CLC_CODEMP] ASC, [CLC_CLCID] ASC),
    CONSTRAINT [CKC_CLC_FACCAP_CLAUSULA] CHECK ([CLC_FACCAP] IS NULL OR ([CLC_FACCAP]='N' OR [CLC_FACCAP]='S')),
    CONSTRAINT [CKC_CLC_FACGJUD_CLAUSULA] CHECK ([CLC_FACGJUD] IS NULL OR ([CLC_FACGJUD]='N' OR [CLC_FACGJUD]='S')),
    CONSTRAINT [CKC_CLC_FACGPRE_CLAUSULA] CHECK ([CLC_FACGPRE] IS NULL OR ([CLC_FACGPRE]='N' OR [CLC_FACGPRE]='S')),
    CONSTRAINT [CKC_CLC_FACHON_CLAUSULA] CHECK ([CLC_FACHON] IS NULL OR ([CLC_FACHON]='N' OR [CLC_FACHON]='S')),
    CONSTRAINT [CKC_CLC_FACINT_CLAUSULA] CHECK ([CLC_FACINT] IS NULL OR ([CLC_FACINT]='N' OR [CLC_FACINT]='S')),
    CONSTRAINT [CKC_CLC_PORCMON_CLAUSULA] CHECK ([CLC_PORCMON]='O' OR [CLC_PORCMON]='M' OR [CLC_PORCMON]='P'),
    CONSTRAINT [CKC_CLC_PREJUD_CLAUSULA] CHECK ([CLC_PREJUD] IS NULL OR ([CLC_PREJUD]='A' OR [CLC_PREJUD]='J' OR [CLC_PREJUD]='P')),
    CONSTRAINT [CKC_CLC_RANGO_CLAUSULA] CHECK ([CLC_RANGO]='N' OR [CLC_RANGO]='S'),
    CONSTRAINT [CKC_CLC_TIPO_CLAUSULA] CHECK ([CLC_TIPO]=(12) OR [CLC_TIPO]=(11) OR [CLC_TIPO]=(10) OR [CLC_TIPO]=(8) OR [CLC_TIPO]=(9) OR [CLC_TIPO]=(7) OR [CLC_TIPO]=(6) OR [CLC_TIPO]=(5) OR [CLC_TIPO]=(4) OR [CLC_TIPO]=(3) OR [CLC_TIPO]=(2) OR [CLC_TIPO]=(1)),
    CONSTRAINT [CKC_CLC_TIPRANGO_CLAUSULA] CHECK ([CLC_TIPRANGO]=(7) OR [CLC_TIPRANGO]=(6) OR [CLC_TIPRANGO]=(5) OR [CLC_TIPRANGO]=(4) OR [CLC_TIPRANGO]=(3) OR [CLC_TIPRANGO]=(2) OR [CLC_TIPRANGO]=(1) OR [CLC_TIPRANGO]=(0)),
    CONSTRAINT [FK_CLAUSULA_EMP_CLAUC_EMPRESA] FOREIGN KEY ([CLC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_CLAUSULA_MONEDAS_C_MONEDAS] FOREIGN KEY ([CLC_CODEMP], [CLC_CODMON]) REFERENCES [dbo].[MONEDAS] ([MON_CODEMP], [MON_CODMON])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CLAUSULAS_CONTCART]([CLC_CODEMP] ASC, [CLC_CLCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabl almacena las distintas clausulas que seran asociadas a un contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLAUSULAS_CONTCART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si es un porcentaje o un valor en alguna moneda especifico
   
   si es moneda, se debe indicar el valor de la moneda
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLAUSULAS_CONTCART', @level2type = N'COLUMN', @level2name = N'CLC_PORCMON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el valor sera aplicado por rangos, sera una mezcla del tipo de calculo y la moneda, agrrgando los rangos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLAUSULAS_CONTCART', @level2type = N'COLUMN', @level2name = N'CLC_RANGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el tipo de rango', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLAUSULAS_CONTCART', @level2type = N'COLUMN', @level2name = N'CLC_TIPRANGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, para que tipo de cartera sera utilizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLAUSULAS_CONTCART', @level2type = N'COLUMN', @level2name = N'CLC_PREJUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo solo se utilizara, para indicar si la calusula se aplicara sobre el capital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLAUSULAS_CONTCART', @level2type = N'COLUMN', @level2name = N'CLC_FACCAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo solo se utilizara, para indicar si la calusula se aplicara sobre el capital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLAUSULAS_CONTCART', @level2type = N'COLUMN', @level2name = N'CLC_FACINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el monto es fijo y no se concatena a los honorarios de facturacion
   
   especialmente se utilizara para el tema de los pagos directos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLAUSULAS_CONTCART', @level2type = N'COLUMN', @level2name = N'CLC_FIJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si la clausula anula la maxima convencional
   
   esto es especial para los honorarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLAUSULAS_CONTCART', @level2type = N'COLUMN', @level2name = N'CLC_ANUMAX';

