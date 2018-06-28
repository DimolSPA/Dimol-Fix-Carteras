CREATE TABLE [dbo].[PERFILES] (
    [PRF_CODEMP]        INT           NOT NULL,
    [PRF_PRFID]         INT           NOT NULL,
    [PRF_NOMBRE]        VARCHAR (200) NOT NULL,
    [PRF_ADMINISTRADOR] CHAR (1)      DEFAULT ('N') NOT NULL,
    CONSTRAINT [PK_PERFILES] PRIMARY KEY CLUSTERED ([PRF_CODEMP] ASC, [PRF_PRFID] ASC),
    CONSTRAINT [CKC_PRF_ADMINISTRADOR_PERFILES] CHECK ([PRF_ADMINISTRADOR]='N' OR [PRF_ADMINISTRADOR]='S'),
    CONSTRAINT [FK_PERFILES_EMPRESA_P_EMPRESA] FOREIGN KEY ([PRF_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PERFILES]([PRF_CODEMP] ASC, [PRF_PRFID] ASC);


GO
CREATE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[PERFILES]([PRF_CODEMP] ASC, [PRF_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos perfiles que manejara la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PERFILES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el perfil es de administrador o sea puede realizar todos los cambios, solo puede haber solo 1 perfil de administrador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PERFILES', @level2type = N'COLUMN', @level2name = N'PRF_ADMINISTRADOR';

