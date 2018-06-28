CREATE TABLE [dbo].[DIAS_FESTIVOS] (
    [DIF_CODEMP]  INT      NOT NULL,
    [DIF_DIFID]   INT      NOT NULL,
    [DIF_DIA]     SMALLINT NULL,
    [DIF_MES]     SMALLINT NULL,
    [DIF_REPETIR] CHAR (1) DEFAULT ('S') NOT NULL,
    [DIF_DIAESP]  DATETIME NULL,
    CONSTRAINT [PK_DIAS_FESTIVOS] PRIMARY KEY NONCLUSTERED ([DIF_CODEMP] ASC, [DIF_DIFID] ASC),
    CONSTRAINT [CKC_DIF_REPETIR_DIAS_FES] CHECK ([DIF_REPETIR]='N' OR [DIF_REPETIR]='S'),
    CONSTRAINT [FK_DIAS_FES_EMPRESA_D_EMPRESA] FOREIGN KEY ([DIF_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DIAS_FESTIVOS]([DIF_CODEMP] ASC, [DIF_DIFID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara los distintos dias festivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIAS_FESTIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si la fecha festiva se repite siempre o solo es por el año actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIAS_FESTIVOS', @level2type = N'COLUMN', @level2name = N'DIF_REPETIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si la fecha festiva es fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIAS_FESTIVOS', @level2type = N'COLUMN', @level2name = N'DIF_DIAESP';

