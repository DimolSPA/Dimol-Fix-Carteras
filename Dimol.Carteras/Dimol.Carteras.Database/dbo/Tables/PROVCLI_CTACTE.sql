CREATE TABLE [dbo].[PROVCLI_CTACTE] (
    [PCT_CODEMP]            INT             NOT NULL,
    [PCT_TPCID]             INT             NOT NULL,
    [PCT_PCLID]             NUMERIC (15)    NOT NULL,
    [PCT_FRPID]             INT             NOT NULL,
    [PCT_CREDITO]           CHAR (1)        DEFAULT ('N') NOT NULL,
    [PCT_LIMITE_CREDITO]    DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PCT_CREDITO_CONSUMIDO] DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [PCT_ESTADO]            CHAR (1)        DEFAULT ('A') NOT NULL,
    [PCT_COMENTARIOS]       TEXT            NOT NULL,
    CONSTRAINT [PK_PROVCLI_CTACTE] PRIMARY KEY NONCLUSTERED ([PCT_CODEMP] ASC, [PCT_TPCID] ASC, [PCT_PCLID] ASC),
    CONSTRAINT [CKC_PCT_CREDITO_PROVCLI_] CHECK ([PCT_CREDITO]='N' OR [PCT_CREDITO]='S'),
    CONSTRAINT [CKC_PCT_ESTADO_PROVCLI_] CHECK ([PCT_ESTADO]='P' OR [PCT_ESTADO]='B' OR [PCT_ESTADO]='M' OR [PCT_ESTADO]='A'),
    CONSTRAINT [FK_PROVCLI__PROVCLI_C_PROVCLI2] FOREIGN KEY ([PCT_CODEMP], [PCT_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID]),
    CONSTRAINT [FK_PROVCLI__TIPOSPROV_TIPOS_PR] FOREIGN KEY ([PCT_CODEMP], [PCT_TPCID]) REFERENCES [dbo].[TIPOS_PROVCLI] ([TPC_CODEMP], [TPC_TPCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PROVCLI_CTACTE]([PCT_CODEMP] ASC, [PCT_TPCID] ASC, [PCT_PCLID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, indicara todo lo relacionado al Cliente o proveedor, si tendra o no cuenta corriente, limite de credito, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI_CTACTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si manejara Credito si o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI_CTACTE', @level2type = N'COLUMN', @level2name = N'PCT_CREDITO';

