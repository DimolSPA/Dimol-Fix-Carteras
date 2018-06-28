CREATE TABLE [dbo].[PERIODOS_CONTABLES] (
    [PEC_CODEMP]     INT      NOT NULL,
    [PEC_ANIO]       INT      NOT NULL,
    [PEC_HABILITADO] CHAR (1) DEFAULT ('N') NOT NULL,
    [PEC_FINALIZADO] CHAR (1) DEFAULT ('N') NOT NULL,
    CONSTRAINT [PK_PERIODOS_CONTABLES2] PRIMARY KEY NONCLUSTERED ([PEC_CODEMP] ASC, [PEC_ANIO] ASC),
    CONSTRAINT [CKC_PEC_FINALIZADO_PERIODOS2] CHECK ([PEC_FINALIZADO]='N' OR [PEC_FINALIZADO]='S'),
    CONSTRAINT [CKC_PEC_HABILITADO_PERIODOS2] CHECK ([PEC_HABILITADO]='N' OR [PEC_HABILITADO]='S'),
    CONSTRAINT [FK_PERIODOS_EMPRESA_P_EMPRESA] FOREIGN KEY ([PEC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PERIODOS_CONTABLES]([PEC_CODEMP] ASC, [PEC_ANIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos periodos contables dependiendo de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PERIODOS_CONTABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el año esta habilitado, para realizar procesos contables', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PERIODOS_CONTABLES', @level2type = N'COLUMN', @level2name = N'PEC_HABILITADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el año contable, esta o no finalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PERIODOS_CONTABLES', @level2type = N'COLUMN', @level2name = N'PEC_FINALIZADO';

