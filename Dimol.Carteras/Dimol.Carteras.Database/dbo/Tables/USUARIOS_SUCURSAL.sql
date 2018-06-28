CREATE TABLE [dbo].[USUARIOS_SUCURSAL] (
    [USS_CODEMP]  INT      NOT NULL,
    [USS_USRID]   INT      NOT NULL,
    [USS_SUCID]   INT      NOT NULL,
    [USS_DEFAULT] CHAR (1) DEFAULT ('N') NULL,
    CONSTRAINT [PK_USUARIOS_SUCURSAL] PRIMARY KEY CLUSTERED ([USS_CODEMP] ASC, [USS_USRID] ASC, [USS_SUCID] ASC),
    CONSTRAINT [CKC_USS_DEFAULT_USUARIOS] CHECK ([USS_DEFAULT] IS NULL OR ([USS_DEFAULT]='N' OR [USS_DEFAULT]='S')),
    CONSTRAINT [FK_USUARIOS_EMPSUC_US_EMPRESA_] FOREIGN KEY ([USS_CODEMP], [USS_SUCID]) REFERENCES [dbo].[EMPRESA_SUCURSAL] ([ESU_CODEMP], [ESU_SUCID]),
    CONSTRAINT [FK_USUARIOS_USU_SUCUR_USUARIOS] FOREIGN KEY ([USS_CODEMP], [USS_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[USUARIOS_SUCURSAL]([USS_CODEMP] ASC, [USS_USRID] ASC, [USS_SUCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las sucursales a las cuales puede acceder el usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'USUARIOS_SUCURSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo almacena la sucursal que sera default a la cual puede acceder el usuario
   
   Solamente puede haber 1 sola default', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'USUARIOS_SUCURSAL', @level2type = N'COLUMN', @level2name = N'USS_DEFAULT';

