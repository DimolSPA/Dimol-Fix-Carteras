CREATE TABLE [dbo].[TIPOS_DESCRIPCION_PRODUCTO] (
    [TDP_CODEMP] INT          NOT NULL,
    [TDP_TPDID]  INT          NOT NULL,
    [TPD_NOMBRE] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TIPO_DESCRIPCION_PRODUCTO] PRIMARY KEY NONCLUSTERED ([TDP_CODEMP] ASC, [TDP_TPDID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_DESCRIPCION_PRODUCTO]([TDP_CODEMP] ASC, [TDP_TPDID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TIPOS_DESCRIPCION_PRODUCTO]([TDP_CODEMP] ASC, [TPD_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos tipos de descripcion de los productos
   
   Ejemplo : Especificacion Tecnica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_DESCRIPCION_PRODUCTO';

