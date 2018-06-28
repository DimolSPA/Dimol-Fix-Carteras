CREATE TABLE [dbo].[PAIS] (
    [PAI_PAIID]  INT           NOT NULL,
    [PAI_NOMBRE] VARCHAR (150) NOT NULL,
    [PAI_CODTEL] SMALLINT      NULL,
    CONSTRAINT [PK_PAIS] PRIMARY KEY CLUSTERED ([PAI_PAIID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PAIS]([PAI_PAIID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[PAIS]([PAI_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos paises para el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PAIS';

