CREATE TABLE [dbo].[USUARIOS] (
    [USR_CODEMP]     INT           NOT NULL,
    [USR_USRID]      INT           NOT NULL,
    [USR_NOMBRE]     VARCHAR (200) NOT NULL,
    [USR_LOGIN]      VARCHAR (15)  NOT NULL,
    [USR_PASSWORD]   VARCHAR (25)  NOT NULL,
    [USR_FECING]     DATETIME      NOT NULL,
    [USR_GODLOG]     INT           NOT NULL,
    [USR_BADLOG]     INT           NOT NULL,
    [USR_FECULTLOG]  DATETIME      NOT NULL,
    [USR_FECBLOCK]   DATETIME      NULL,
    [USR_MAIL]       VARCHAR (80)  NULL,
    [USR_TIPQUEST]   SMALLINT      DEFAULT ((1)) NOT NULL,
    [USR_ANSWER]     VARCHAR (200) NOT NULL,
    [USR_SUCID]      INT           NOT NULL,
    [USR_PRFID]      INT           NOT NULL,
    [USR_PERMISOS]   SMALLINT      DEFAULT ((1)) NOT NULL,
    [USR_ESTADO]     CHAR (1)      DEFAULT ('H') NULL,
    [USR_CAMPASS]    CHAR (1)      DEFAULT ('N') NULL,
    [USR_FECCAMBIO]  DATETIME      NULL,
    [USR_PERMREMOTO] CHAR (1)      DEFAULT ('S') NULL,
    CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED ([USR_CODEMP] ASC, [USR_USRID] ASC),
    CONSTRAINT [CKC_USR_CAMPASS_USUARIOS] CHECK ([USR_CAMPASS] IS NULL OR ([USR_CAMPASS]='N' OR [USR_CAMPASS]='S')),
    CONSTRAINT [CKC_USR_ESTADO_USUARIOS] CHECK ([USR_ESTADO] IS NULL OR ([USR_ESTADO]='B' OR [USR_ESTADO]='H')),
    CONSTRAINT [CKC_USR_PERMISOS_USUARIOS] CHECK ([USR_PERMISOS]=(7) OR [USR_PERMISOS]=(6) OR [USR_PERMISOS]=(5) OR [USR_PERMISOS]=(4) OR [USR_PERMISOS]=(3) OR [USR_PERMISOS]=(2) OR [USR_PERMISOS]=(1)),
    CONSTRAINT [CKC_USR_TIPQUEST_USUARIOS] CHECK ([USR_TIPQUEST]=(6) OR [USR_TIPQUEST]=(5) OR [USR_TIPQUEST]=(4) OR [USR_TIPQUEST]=(3) OR [USR_TIPQUEST]=(2) OR [USR_TIPQUEST]=(1)),
    CONSTRAINT [FK_USUARIOS_PERFILES__PERFILES] FOREIGN KEY ([USR_CODEMP], [USR_PRFID]) REFERENCES [dbo].[PERFILES] ([PRF_CODEMP], [PRF_PRFID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[USUARIOS]([USR_CODEMP] ASC, [USR_USRID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_LOGIN]
    ON [dbo].[USUARIOS]([USR_CODEMP] ASC, [USR_LOGIN] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el tipo de pregunta secreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'USUARIOS', @level2type = N'COLUMN', @level2name = N'USR_TIPQUEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la sucursal por defecto a la cual entrara cada usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'USUARIOS', @level2type = N'COLUMN', @level2name = N'USR_SUCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el usuario, debe o no cambiar la password dependiendo de los dias que se definan en la configuracion de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'USUARIOS', @level2type = N'COLUMN', @level2name = N'USR_CAMPASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la fecha en la cual debe realizar el cambio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'USUARIOS', @level2type = N'COLUMN', @level2name = N'USR_FECCAMBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el usuario puede acceder desde internet o solo la intranet', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'USUARIOS', @level2type = N'COLUMN', @level2name = N'USR_PERMREMOTO';

