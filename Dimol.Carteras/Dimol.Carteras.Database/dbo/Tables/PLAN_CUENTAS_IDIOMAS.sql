CREATE TABLE [dbo].[PLAN_CUENTAS_IDIOMAS] (
    [PCI_CODEMP] INT            NOT NULL,
    [PCI_PCTID]  INT            NOT NULL,
    [PCI_IDID]   INT            NOT NULL,
    [PCI_NOMBRE] VARCHAR (1500) NOT NULL,
    CONSTRAINT [PK_PLAN_CUENTAS_IDIOMAS] PRIMARY KEY NONCLUSTERED ([PCI_CODEMP] ASC, [PCI_PCTID] ASC, [PCI_IDID] ASC),
    CONSTRAINT [FK_PLAN_CUE_PLACTA_ID_PLAN_CUE] FOREIGN KEY ([PCI_CODEMP], [PCI_PCTID]) REFERENCES [dbo].[PLAN_CUENTAS] ([PCT_CODEMP], [PCT_PCTID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PLAN_CUENTAS_IDIOMAS]([PCI_IDID] ASC, [PCI_PCTID] ASC, [PCI_CODEMP] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara las distintas cuentas en su respectivo idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PLAN_CUENTAS_IDIOMAS';

