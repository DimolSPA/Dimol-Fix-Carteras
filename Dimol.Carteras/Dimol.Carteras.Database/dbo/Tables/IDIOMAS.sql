CREATE TABLE [dbo].[IDIOMAS] (
    [IDI_IDID]   INT          NOT NULL,
    [IDI_NOMBRE] VARCHAR (60) NOT NULL,
    [IDI_IDISRC] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_IDIOMAS] PRIMARY KEY CLUSTERED ([IDI_IDID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[IDIOMAS]([IDI_IDID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[IDIOMAS]([IDI_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacenara los idiomas del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IDIOMAS';

