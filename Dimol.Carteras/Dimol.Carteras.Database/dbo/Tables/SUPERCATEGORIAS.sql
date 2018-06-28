CREATE TABLE [dbo].[SUPERCATEGORIAS] (
    [SPC_CODEMP]      INT          NOT NULL,
    [SPC_SPCID]       INT          NOT NULL,
    [SPC_NOMBRE]      VARCHAR (50) NOT NULL,
    [SPC_ORDEN]       SMALLINT     DEFAULT ((5)) NOT NULL,
    [SPC_UTILIZACION] SMALLINT     NULL,
    CONSTRAINT [PK_SUPERCATEGORIAS] PRIMARY KEY NONCLUSTERED ([SPC_CODEMP] ASC, [SPC_SPCID] ASC),
    CONSTRAINT [FK_SUPERCAT_EMPRESA_S_EMPRESA] FOREIGN KEY ([SPC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[SUPERCATEGORIAS]([SPC_CODEMP] ASC, [SPC_SPCID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[SUPERCATEGORIAS]([SPC_CODEMP] ASC, [SPC_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las distintas super Categorias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SUPERCATEGORIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica en donde se utilizara esta super categoria ejemplo
   
   1.- Insumos
   2.- Productos
   3.- Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SUPERCATEGORIAS', @level2type = N'COLUMN', @level2name = N'SPC_UTILIZACION';

