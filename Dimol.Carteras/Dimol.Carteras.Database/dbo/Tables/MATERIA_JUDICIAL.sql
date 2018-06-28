CREATE TABLE [dbo].[MATERIA_JUDICIAL] (
    [ESJ_CODEMP] INT           NOT NULL,
    [ESJ_ESJID]  INT           NOT NULL,
    [ESJ_NOMBRE] VARCHAR (120) NOT NULL,
    [ESJ_ORDEN]  SMALLINT      NULL,
    CONSTRAINT [PK_MATERIA_JUDICIAL] PRIMARY KEY CLUSTERED ([ESJ_CODEMP] ASC, [ESJ_ESJID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[MATERIA_JUDICIAL]([ESJ_CODEMP] ASC, [ESJ_ESJID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las siguientes materias para cada grupo de estados judiciales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MATERIA_JUDICIAL';

