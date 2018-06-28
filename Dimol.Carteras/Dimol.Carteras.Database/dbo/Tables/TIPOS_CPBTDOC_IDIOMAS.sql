CREATE TABLE [dbo].[TIPOS_CPBTDOC_IDIOMAS] (
    [TCI_CODEMP] INT           NOT NULL,
    [TCI_TPCID]  INT           NOT NULL,
    [TCI_IDID]   INT           NOT NULL,
    [TCI_NOMBRE] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TIPOS_CPBTDOC_IDIOMAS] PRIMARY KEY NONCLUSTERED ([TCI_CODEMP] ASC, [TCI_TPCID] ASC, [TCI_IDID] ASC),
    CONSTRAINT [FK_TIPOS_CP_TIPCPBT_I_TIPOS_CP] FOREIGN KEY ([TCI_CODEMP], [TCI_TPCID]) REFERENCES [dbo].[TIPOS_CPBTDOC] ([TPC_CODEMP], [TPC_TPCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_CPBTDOC_IDIOMAS]([TCI_CODEMP] ASC, [TCI_TPCID] ASC, [TCI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos tipos de comprobantes en su respectivo idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CPBTDOC_IDIOMAS';

