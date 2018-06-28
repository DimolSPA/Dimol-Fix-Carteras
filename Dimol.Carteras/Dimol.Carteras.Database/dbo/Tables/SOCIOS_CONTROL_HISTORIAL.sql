CREATE TABLE [dbo].[SOCIOS_CONTROL_HISTORIAL] (
    [SCH_CODEMP] INT             NOT NULL,
    [SCH_SOCID]  INT             NOT NULL,
    [SCH_FECHA]  DATETIME        NOT NULL,
    [SCH_PESO]   DECIMAL (10, 2) NOT NULL,
    CONSTRAINT [PK_SOCIOS_CONTROL_HISTORIAL] PRIMARY KEY CLUSTERED ([SCH_CODEMP] ASC, [SCH_SOCID] ASC, [SCH_FECHA] ASC)
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[SOCIOS_CONTROL_HISTORIAL]([SCH_CODEMP] ASC, [SCH_SOCID] ASC, [SCH_FECHA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los datos de los controles realizados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOCIOS_CONTROL_HISTORIAL';

