CREATE TABLE [dbo].[COMUNA] (
    [COM_CIUID]   INT           NOT NULL,
    [COM_COMID]   INT           NOT NULL,
    [COM_NOMBRE]  VARCHAR (200) NOT NULL,
    [COM_CODPOST] VARCHAR (20)  NULL,
    CONSTRAINT [PK_COMUNA] PRIMARY KEY CLUSTERED ([COM_COMID] ASC),
    CONSTRAINT [FK_COMUNA_CIUDAD_CO_CIUDAD] FOREIGN KEY ([COM_CIUID]) REFERENCES [dbo].[CIUDAD] ([CIU_CIUID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[COMUNA]([COM_COMID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI2]
    ON [dbo].[COMUNA]([COM_CIUID] ASC, [COM_COMID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas comunas para cada ciudad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COMUNA';

