CREATE TABLE [dbo].[CENTRO_COSTOS] (
    [CCS_CODEMP] INT           NOT NULL,
    [CCS_CCSID]  INT           NOT NULL,
    [CCS_NOMBRE] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_CENTRO_COSTOS] PRIMARY KEY NONCLUSTERED ([CCS_CODEMP] ASC, [CCS_CCSID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CENTRO_COSTOS]([CCS_CODEMP] ASC, [CCS_CCSID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara los distintos centros de costos para cada cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CENTRO_COSTOS';

