CREATE TABLE [dbo].[NOTICIAS] (
    [NTE_CODEMP] INT           NOT NULL,
    [NTE_NTEID]  INT           NOT NULL,
    [NTE_TITULO] VARCHAR (800) NOT NULL,
    [NTE_FECHA]  DATETIME      NOT NULL,
    [NTE_FUENTE] VARCHAR (200) NULL,
    [NTE_ENLACE] VARCHAR (800) NULL,
    [NTE_FOTO]   VARCHAR (100) NULL,
    CONSTRAINT [PK_NOTICIAS] PRIMARY KEY CLUSTERED ([NTE_CODEMP] ASC, [NTE_NTEID] ASC),
    CONSTRAINT [FK_NOTICIAS_EMPRESA_N_EMPRESA] FOREIGN KEY ([NTE_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[NOTICIAS]([NTE_CODEMP] ASC, [NTE_NTEID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacenara, los distintos datos de noticias para la web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NOTICIAS';

