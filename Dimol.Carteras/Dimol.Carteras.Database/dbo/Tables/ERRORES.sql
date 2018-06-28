CREATE TABLE [dbo].[ERRORES] (
    [ERR_ERRID]       INT           NOT NULL,
    [ERR_CODIGO]      VARCHAR (20)  NOT NULL,
    [ERR_DESCRIPCION] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_ERRORES] PRIMARY KEY CLUSTERED ([ERR_ERRID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ERRORES]([ERR_ERRID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_CODIGO]
    ON [dbo].[ERRORES]([ERR_CODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos errores tanto del sistema como de base de dsatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ERRORES';

