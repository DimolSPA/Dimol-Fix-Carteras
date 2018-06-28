CREATE TABLE [dbo].[CIUDAD] (
    [CIU_REGID]   INT           NOT NULL,
    [CIU_CIUID]   INT           NOT NULL,
    [CIU_NOMBRE]  VARCHAR (200) NOT NULL,
    [CIU_CODAREA] SMALLINT      NULL,
    CONSTRAINT [PK_CIUDAD] PRIMARY KEY CLUSTERED ([CIU_CIUID] ASC),
    CONSTRAINT [FK_CIUDAD_REGION_CI_REGION] FOREIGN KEY ([CIU_REGID]) REFERENCES [dbo].[REGION] ([REG_REGID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CIUDAD]([CIU_CIUID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI2]
    ON [dbo].[CIUDAD]([CIU_REGID] ASC, [CIU_CIUID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas ciudades para cada region', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CIUDAD';

