CREATE TABLE [dbo].[TIPOS_CPBTDOC_REPORT] (
    [TRC_CODEMP]  INT           NOT NULL,
    [TRC_TPCID]   INT           NOT NULL,
    [TRC_TRCID]   INT           NOT NULL,
    [TRC_REPORTE] VARCHAR (800) NOT NULL,
    [TRC_NOMBRE]  VARCHAR (200) NOT NULL,
    [TRC_REPPAD]  VARCHAR (800) NULL,
    CONSTRAINT [PK_TIPOS_CPBTDOC_REPORT] PRIMARY KEY CLUSTERED ([TRC_CODEMP] ASC, [TRC_TPCID] ASC, [TRC_TRCID] ASC),
    CONSTRAINT [FK_TIPOS_CP_TIPCPBT_R_TIPOS_CP] FOREIGN KEY ([TRC_CODEMP], [TRC_TPCID]) REFERENCES [dbo].[TIPOS_CPBTDOC] ([TPC_CODEMP], [TPC_TPCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_CPBTDOC_REPORT]([TRC_CODEMP] ASC, [TRC_TPCID] ASC, [TRC_TRCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos reportes para cada tipo de comprobante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CPBTDOC_REPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el reporte padre del cual fue heredado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CPBTDOC_REPORT', @level2type = N'COLUMN', @level2name = N'TRC_REPPAD';

