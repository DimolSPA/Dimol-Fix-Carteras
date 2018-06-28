CREATE TABLE [dbo].[ETIQUETAS] (
    [ETQ_ETQID]       INT           NOT NULL,
    [ETQ_CODIGO]      VARCHAR (8)   NOT NULL,
    [ETQ_DESCRIPCION] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_ETIQUETAS] PRIMARY KEY CLUSTERED ([ETQ_ETQID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ETIQUETAS]([ETQ_ETQID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI2]
    ON [dbo].[ETIQUETAS]([ETQ_CODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacenara una etiqueta Identificativa para los idiomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ETIQUETAS';

