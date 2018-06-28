CREATE TABLE [dbo].[REGION] (
    [REG_PAIID]  INT           NOT NULL,
    [REG_REGID]  INT           NOT NULL,
    [REG_NOMBRE] VARCHAR (200) NOT NULL,
    [REG_ORDEN]  SMALLINT      DEFAULT ((5)) NOT NULL,
    CONSTRAINT [PK_REGION] PRIMARY KEY CLUSTERED ([REG_REGID] ASC),
    CONSTRAINT [FK_REGION_PAIS_REGI_PAIS] FOREIGN KEY ([REG_PAIID]) REFERENCES [dbo].[PAIS] ([PAI_PAIID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[REGION]([REG_REGID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI2]
    ON [dbo].[REGION]([REG_PAIID] ASC, [REG_REGID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas regiones dependiendo del pais', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'REGION';

