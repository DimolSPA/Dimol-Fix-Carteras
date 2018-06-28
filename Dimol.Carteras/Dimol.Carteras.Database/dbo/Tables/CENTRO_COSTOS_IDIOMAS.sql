CREATE TABLE [dbo].[CENTRO_COSTOS_IDIOMAS] (
    [CSI_CODEMP] INT           NOT NULL,
    [CSI_CCSID]  INT           NOT NULL,
    [CSI_IDID]   INT           NOT NULL,
    [CSI_NOMBRE] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_CENTRO_COSTOS_IDIOMAS] PRIMARY KEY NONCLUSTERED ([CSI_CODEMP] ASC, [CSI_CCSID] ASC, [CSI_IDID] ASC),
    CONSTRAINT [FK_CENTRO_C_CENTCOST__CENTRO_C] FOREIGN KEY ([CSI_CODEMP], [CSI_CCSID]) REFERENCES [dbo].[CENTRO_COSTOS] ([CCS_CODEMP], [CCS_CCSID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CENTRO_COSTOS_IDIOMAS]([CSI_CODEMP] ASC, [CSI_IDID] ASC, [CSI_CCSID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos centros de costos en su respectivo idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CENTRO_COSTOS_IDIOMAS';

