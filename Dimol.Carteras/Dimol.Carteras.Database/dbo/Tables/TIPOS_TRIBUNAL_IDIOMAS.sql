﻿CREATE TABLE [dbo].[TIPOS_TRIBUNAL_IDIOMAS] (
    [TBI_CODEMP] INT          NOT NULL,
    [TBI_TTBID]  INT          NOT NULL,
    [TBI_IDID]   INT          NOT NULL,
    [TBI_NOMBRE] VARCHAR (70) NOT NULL,
    CONSTRAINT [PK_TIPOS_TRIBUNAL_IDIOMAS] PRIMARY KEY CLUSTERED ([TBI_CODEMP] ASC, [TBI_TTBID] ASC, [TBI_IDID] ASC),
    CONSTRAINT [FK_TIPOS_TR_IDI_TIPTR_IDIOMAS] FOREIGN KEY ([TBI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID]),
    CONSTRAINT [FK_TIPOS_TR_TIPTRIB_I_TIPOS_TR] FOREIGN KEY ([TBI_CODEMP], [TBI_TTBID]) REFERENCES [dbo].[TIPOS_TRIBUNAL] ([TTB_CODEMP], [TTB_TTBID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_TRIBUNAL_IDIOMAS]([TBI_CODEMP] ASC, [TBI_TTBID] ASC, [TBI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos tipos de tribunal en su respectivo idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_TRIBUNAL_IDIOMAS';

