CREATE TABLE [dbo].[TABLA_IMPORTACION] (
    [TBI_NOMBRE]    VARCHAR (250) NOT NULL,
    [TBI_ORDEN]     SMALLINT      NOT NULL,
    [TBI_PROCESADA] CHAR (1)      DEFAULT ('N') NOT NULL,
    [TBI_FECPROC]   DATETIME      NULL,
    [TBI_BASE]      VARCHAR (100) NULL,
    CONSTRAINT [PK_TABLA_IMPORTACION] PRIMARY KEY CLUSTERED ([TBI_NOMBRE] ASC),
    CONSTRAINT [CKC_TBI_PROCESADA_TABLA_IM] CHECK ([TBI_PROCESADA]='P' OR [TBI_PROCESADA]='N' OR [TBI_PROCESADA]='S')
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TABLA_IMPORTACION]([TBI_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las tablas que seran importadas y el orden que debe realizarce, y si ha sido ya importada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TABLA_IMPORTACION';

