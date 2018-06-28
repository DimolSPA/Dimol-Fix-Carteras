CREATE TABLE [dbo].[EVENTOS] (
    [EVE_CODEMP] INT           NOT NULL,
    [EVE_EVEID]  INT           NOT NULL,
    [EVE_TITULO] VARCHAR (200) NOT NULL,
    [EVE_FECHA]  DATETIME      NOT NULL,
    CONSTRAINT [PK_EVENTOS] PRIMARY KEY CLUSTERED ([EVE_CODEMP] ASC, [EVE_EVEID] ASC),
    CONSTRAINT [FK_EVENTOS_EMPRESA_E_EMPRESA] FOREIGN KEY ([EVE_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[EVENTOS]([EVE_CODEMP] ASC, [EVE_EVEID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos, eventos que realiza o realizara la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EVENTOS';

