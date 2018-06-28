CREATE TABLE [dbo].[CATEGORIAS] (
    [CAT_CODEMP]      INT          NOT NULL,
    [CAT_CATID]       INT          NOT NULL,
    [CAT_NOMBRE]      VARCHAR (50) NOT NULL,
    [CAT_UTILIZACION] SMALLINT     NULL,
    CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY NONCLUSTERED ([CAT_CODEMP] ASC, [CAT_CATID] ASC),
    CONSTRAINT [FK_CATEGORI_EMPRESA_C_EMPRESA] FOREIGN KEY ([CAT_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CATEGORIAS]([CAT_CODEMP] ASC, [CAT_CATID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[CATEGORIAS]([CAT_CODEMP] ASC, [CAT_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las distintas categorias por empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CATEGORIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la utilizacion de este campo ejemplo
   
   1.- Insumos
   2.- Productos
   3.- Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CATEGORIAS', @level2type = N'COLUMN', @level2name = N'CAT_UTILIZACION';

