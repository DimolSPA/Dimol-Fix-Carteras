﻿CREATE TABLE [dbo].[TIPOS_CONTACTO_IDIOMAS] (
    [TCI_CODEMP] INT           NOT NULL,
    [TCI_TICID]  INT           NOT NULL,
    [TCI_IDID]   INT           NOT NULL,
    [TCI_NOMBRE] VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_TIPOS_CONTACTO_IDIOMAS] PRIMARY KEY NONCLUSTERED ([TCI_CODEMP] ASC, [TCI_TICID] ASC, [TCI_IDID] ASC),
    CONSTRAINT [FK_TIPOS_CO_TIPCONTAC_TIPOS_CO] FOREIGN KEY ([TCI_CODEMP], [TCI_TICID]) REFERENCES [dbo].[TIPOS_CONTACTO] ([TIC_CODEMP], [TIC_TICID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_CONTACTO_IDIOMAS]([TCI_CODEMP] ASC, [TCI_TICID] ASC, [TCI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos tipos de contacto dependiendo del idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CONTACTO_IDIOMAS';

