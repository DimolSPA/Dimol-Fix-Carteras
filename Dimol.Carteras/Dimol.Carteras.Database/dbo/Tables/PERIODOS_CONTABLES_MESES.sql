CREATE TABLE [dbo].[PERIODOS_CONTABLES_MESES] (
    [PCM_CODEMP]     INT         NOT NULL,
    [PCM_ANIO]       INT         NOT NULL,
    [PCM_MES]        NUMERIC (2) NOT NULL,
    [PCM_INICIO]     DATETIME    NOT NULL,
    [PCM_FIN]        DATETIME    NOT NULL,
    [PCM_APEINI]     INT         NOT NULL,
    [PCM_APEFIN]     INT         NOT NULL,
    [PCM_INGINI]     INT         NOT NULL,
    [PCM_INGFIN]     INT         NOT NULL,
    [PCM_EGREINI]    INT         NOT NULL,
    [PCM_EGREFIN]    INT         NOT NULL,
    [PCM_TRASINI]    INT         NOT NULL,
    [PCM_TRASFIN]    INT         NOT NULL,
    [PCM_HABILITADO] CHAR (1)    DEFAULT ('N') NOT NULL,
    [PCM_FINALIZADO] CHAR (1)    DEFAULT ('N') NOT NULL,
    CONSTRAINT [PK_PERIODOS_CONTABLES] PRIMARY KEY NONCLUSTERED ([PCM_CODEMP] ASC, [PCM_ANIO] ASC, [PCM_MES] ASC),
    CONSTRAINT [CKC_PEC_FINALIZADO_PERIODOS] CHECK ([PCM_FINALIZADO]='N' OR [PCM_FINALIZADO]='S'),
    CONSTRAINT [CKC_PEC_HABILITADO_PERIODOS] CHECK ([PCM_HABILITADO]='N' OR [PCM_HABILITADO]='S'),
    CONSTRAINT [FK_PERIODOS_PERCONT_M_PERIODOS] FOREIGN KEY ([PCM_CODEMP], [PCM_ANIO]) REFERENCES [dbo].[PERIODOS_CONTABLES] ([PEC_CODEMP], [PEC_ANIO])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PERIODOS_CONTABLES_MESES]([PCM_CODEMP] ASC, [PCM_ANIO] ASC, [PCM_MES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos periodos contables, los cuales indicaran si se puede seguir imputando o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PERIODOS_CONTABLES_MESES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el mes del periodo contable esta habilitado o no, si no esta habilitado no se puede imputar nada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PERIODOS_CONTABLES_MESES', @level2type = N'COLUMN', @level2name = N'PCM_HABILITADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el periodo esta finalizado, si este campo esta finalizado es que se ha realizado ya el cierre de mes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PERIODOS_CONTABLES_MESES', @level2type = N'COLUMN', @level2name = N'PCM_FINALIZADO';

