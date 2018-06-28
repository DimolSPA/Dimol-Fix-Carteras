CREATE TABLE [dbo].[TIPOS_TRIBUNAL] (
    [TTB_CODEMP] INT          NOT NULL,
    [TTB_TTBID]  INT          NOT NULL,
    [TTB_NOMBRE] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TIPOS_TRIBUNAL] PRIMARY KEY CLUSTERED ([TTB_CODEMP] ASC, [TTB_TTBID] ASC),
    CONSTRAINT [FK_TIPOS_TR_EMPRE_TIP_EMPRESA] FOREIGN KEY ([TTB_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_TRIBUNAL]([TTB_CODEMP] ASC, [TTB_TTBID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos tipos de tribunales
   
   ejemplo:
   
   Justicia, Trabajo, Familia, etc...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_TRIBUNAL';

