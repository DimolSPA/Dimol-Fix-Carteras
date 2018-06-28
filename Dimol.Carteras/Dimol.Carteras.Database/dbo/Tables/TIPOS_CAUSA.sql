CREATE TABLE [dbo].[TIPOS_CAUSA] (
    [TCA_CODEMP] INT           NOT NULL,
    [TCA_TCAID]  INT           NOT NULL,
    [TCA_NOMBRE] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_TIPOS_CAUSA] PRIMARY KEY CLUSTERED ([TCA_CODEMP] ASC, [TCA_TCAID] ASC),
    CONSTRAINT [FK_TIPOS_CA_EMPRESA_T_EMPRESA] FOREIGN KEY ([TCA_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_CAUSA]([TCA_CODEMP] ASC, [TCA_TCAID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOM]
    ON [dbo].[TIPOS_CAUSA]([TCA_CODEMP] ASC, [TCA_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las distintas causas para realizar la demanda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CAUSA';

