CREATE TABLE [dbo].[PLAN_CUENTAS] (
    [PCT_CODEMP]  INT            NOT NULL,
    [PCT_PCTID]   INT            NOT NULL,
    [PCT_CODIGO]  VARCHAR (20)   NOT NULL,
    [PCT_NOMBRE]  VARCHAR (1000) NOT NULL,
    [PCT_CTPID]   INT            NOT NULL,
    [PCT_CCS]     CHAR (1)       DEFAULT ('N') NOT NULL,
    [PCT_ACTIVOS] CHAR (1)       DEFAULT ('N') NULL,
    [PCT_ESTADO]  CHAR (1)       DEFAULT ('S') NULL,
    CONSTRAINT [PK_PLAN_CUENTAS] PRIMARY KEY NONCLUSTERED ([PCT_CODEMP] ASC, [PCT_PCTID] ASC),
    CONSTRAINT [CKC_PCT_ACTIVOS_PLAN_CUE] CHECK ([PCT_ACTIVOS] IS NULL OR ([PCT_ACTIVOS]='N' OR [PCT_ACTIVOS]='S')),
    CONSTRAINT [CKC_PCT_CCS_PLAN_CUE] CHECK ([PCT_CCS]='N' OR [PCT_CCS]='S'),
    CONSTRAINT [FK_PLAN_CUE_CTAPAD_PL_CUENTAS_] FOREIGN KEY ([PCT_CODEMP], [PCT_CTPID]) REFERENCES [dbo].[CUENTAS_PADRES] ([CTP_CODEMP], [CTP_CTPID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PLAN_CUENTAS]([PCT_CODEMP] ASC, [PCT_PCTID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_CODIGO]
    ON [dbo].[PLAN_CUENTAS]([PCT_CODEMP] ASC, [PCT_CODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena todo el plan de cuenta de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PLAN_CUENTAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si la cuenta, manejara o no Centro de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PLAN_CUENTAS', @level2type = N'COLUMN', @level2name = N'PCT_CCS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si la cuenta manejara o no activos fijos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PLAN_CUENTAS', @level2type = N'COLUMN', @level2name = N'PCT_ACTIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si la cuenta esta activa o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PLAN_CUENTAS', @level2type = N'COLUMN', @level2name = N'PCT_ESTADO';

