CREATE TABLE [dbo].[EMPRESA] (
    [EMP_CODEMP]    INT            NOT NULL,
    [EMP_RUT]       VARCHAR (20)   NOT NULL,
    [EMP_NOMBRE]    VARCHAR (200)  NOT NULL,
    [EMP_RUTREPLEG] VARCHAR (20)   NOT NULL,
    [EMP_REPLEGAL]  VARCHAR (150)  NOT NULL,
    [EMP_GIRO]      VARCHAR (1000) NULL,
    [EMP_LOGO]      IMAGE          NULL,
    [EMP_MENACT]    VARCHAR (1000) NULL,
    [EMP_FECUTL]    DATETIME       NULL,
    [EMP_CODBARR]   IMAGE          NULL,
    [EMP_MENU]      VARCHAR (1000) NULL,
    CONSTRAINT [PK_EMPRESA] PRIMARY KEY CLUSTERED ([EMP_CODEMP] ASC)
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[EMPRESA]([EMP_CODEMP] ASC);


GO
CREATE NONCLUSTERED INDEX [INDEX_RUT]
    ON [dbo].[EMPRESA]([EMP_RUT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los datos basicos para cada empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPRESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la cantidad de menus que tiene activos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPRESA', @level2type = N'COLUMN', @level2name = N'EMP_MENACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, la fecha en que se ejecuto la ultima actualizacion de la parte de utilitarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPRESA', @level2type = N'COLUMN', @level2name = N'EMP_FECUTL';

