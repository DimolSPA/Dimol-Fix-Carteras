﻿CREATE TABLE [dbo].[ACCIONES_IDIOMAS] (
    [ACI_CODEMP] INT          NOT NULL,
    [ACI_ACCID]  INT          NOT NULL,
    [ACI_IDID]   INT          NOT NULL,
    [ACI_NOMBRE] VARCHAR (90) NOT NULL,
    CONSTRAINT [PK_ACCIONES_IDIOMAS] PRIMARY KEY NONCLUSTERED ([ACI_CODEMP] ASC, [ACI_ACCID] ASC, [ACI_IDID] ASC),
    CONSTRAINT [FK_ACCIONES_ACCIONES__ACCIONES] FOREIGN KEY ([ACI_CODEMP], [ACI_ACCID]) REFERENCES [dbo].[ACCIONES] ([ACC_CODEMP], [ACC_ACCID]),
    CONSTRAINT [FK_ACCIONES_IDIOMAS_A_IDIOMAS] FOREIGN KEY ([ACI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ACCIONES_IDIOMAS]([ACI_CODEMP] ASC, [ACI_ACCID] ASC, [ACI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las distintas acciones dependiendo de su idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ACCIONES_IDIOMAS';

