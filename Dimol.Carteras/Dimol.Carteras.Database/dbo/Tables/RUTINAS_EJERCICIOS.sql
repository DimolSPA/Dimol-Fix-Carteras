CREATE TABLE [dbo].[RUTINAS_EJERCICIOS] (
    [RTE_CODEMP]  INT         NOT NULL,
    [RTE_RTNID]   INT         NOT NULL,
    [RTE_EJCID]   INT         NOT NULL,
    [RTE_TIEMPO]  SMALLINT    NULL,
    [RTE_DIA1]    NUMERIC (1) DEFAULT ((0)) NOT NULL,
    [RTE_DIA2]    NUMERIC (1) NOT NULL,
    [RTE_DIA3]    NUMERIC (1) NOT NULL,
    [RTE_DIA4]    NUMERIC (1) NOT NULL,
    [RTE_DIA5]    NUMERIC (1) NOT NULL,
    [RTE_DIA6]    NUMERIC (1) NOT NULL,
    [RTE_DIA7]    NUMERIC (1) NOT NULL,
    [RTE_ORDEN]   SMALLINT    DEFAULT ((5)) NOT NULL,
    [RTE_CANTREP] SMALLINT    NULL,
    CONSTRAINT [PK_RUTINAS_EJERCICIOS] PRIMARY KEY CLUSTERED ([RTE_CODEMP] ASC, [RTE_RTNID] ASC, [RTE_EJCID] ASC),
    CONSTRAINT [CKC_RTE_DIA1_RUTINAS_] CHECK ([RTE_DIA1]=(1) OR [RTE_DIA1]=(0)),
    CONSTRAINT [FK_RUTINAS__EJERCICIO_EJERCICI] FOREIGN KEY ([RTE_CODEMP], [RTE_EJCID]) REFERENCES [dbo].[EJERCICIOS] ([EJC_CODEMP], [EJC_EJCID]),
    CONSTRAINT [FK_RUTINAS__RUTINA_EJ_RUTINAS] FOREIGN KEY ([RTE_CODEMP], [RTE_RTNID]) REFERENCES [dbo].[RUTINAS] ([RTN_CODEMP], [RTN_RTNID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[RUTINAS_EJERCICIOS]([RTE_CODEMP] ASC, [RTE_RTNID] ASC, [RTE_EJCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, define los ejerccios para cada rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RUTINAS_EJERCICIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el orden de cada ejercicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RUTINAS_EJERCICIOS', @level2type = N'COLUMN', @level2name = N'RTE_ORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, la cantidad de repeticiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RUTINAS_EJERCICIOS', @level2type = N'COLUMN', @level2name = N'RTE_CANTREP';

