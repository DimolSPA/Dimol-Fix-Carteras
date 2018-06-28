﻿CREATE TABLE [dbo].[TIPOS_CONTRATO_IDIOMAS] (
    [TCI_CODEMP] INT           NOT NULL,
    [TCI_TICID]  INT           NOT NULL,
    [TCI_IDID]   INT           NOT NULL,
    [TCI_NOMBRE] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TIPOS_CONTRATO_IDIOMAS] PRIMARY KEY NONCLUSTERED ([TCI_CODEMP] ASC, [TCI_TICID] ASC, [TCI_IDID] ASC),
    CONSTRAINT [FK_TIPOS_CO_IDIOMAS_T_IDIOMAS] FOREIGN KEY ([TCI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID]),
    CONSTRAINT [FK_TIPOS_CO_TIPCONT_I_TIPOS_CO] FOREIGN KEY ([TCI_CODEMP], [TCI_TICID]) REFERENCES [dbo].[TIPOS_CONTRATO] ([TIC_CODEMP], [TIC_TICID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_CONTRATO_IDIOMAS]([TCI_CODEMP] ASC, [TCI_TICID] ASC, [TCI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena, los distintos tipos de contrato en su respectivo Idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CONTRATO_IDIOMAS';

