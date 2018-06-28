CREATE TABLE [dbo].[ROL_INFORMES] (
    [RIF_CODEMP]    INT            NOT NULL,
    [RIF_ROLID]     INT            NOT NULL,
    [RIF_ITEM]      INT            NOT NULL,
    [RIF_TIFID]     INT            NOT NULL,
    [RIF_NOMBRE]    VARCHAR (800)  NOT NULL,
    [RIF_UBICACION] VARCHAR (1200) NOT NULL,
    CONSTRAINT [PK_ROL_INFORMES] PRIMARY KEY CLUSTERED ([RIF_CODEMP] ASC, [RIF_ROLID] ASC, [RIF_ITEM] ASC),
    CONSTRAINT [FK_ROL_INFO_ROL_INFOR_ROL] FOREIGN KEY ([RIF_CODEMP], [RIF_ROLID]) REFERENCES [dbo].[ROL] ([ROL_CODEMP], [ROL_ROLID]),
    CONSTRAINT [FK_ROL_INFO_TIPINF_RO_TIPOS_IN] FOREIGN KEY ([RIF_CODEMP], [RIF_TIFID]) REFERENCES [dbo].[TIPOS_INFORMES] ([TIF_CODEMP], [TIF_TIFID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ROL_INFORMES]([RIF_CODEMP] ASC, [RIF_ROLID] ASC, [RIF_ITEM] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena la informacion del documento o informa fisico, el nombre y donde esta ubicado dentro del servidor... ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ROL_INFORMES';

