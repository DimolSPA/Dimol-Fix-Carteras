CREATE TABLE [dbo].[PRODUCTOS_DOCUMENTOS] (
    [PDC_CODEMP] INT          NOT NULL,
    [PDC_PRODID] NUMERIC (15) NOT NULL,
    [PDC_TPDID]  INT          NOT NULL,
    [PDC_ORDEN]  SMALLINT     DEFAULT ((5)) NOT NULL,
    CONSTRAINT [PK_PRODUCTOS_DOCUMENTOS] PRIMARY KEY NONCLUSTERED ([PDC_CODEMP] ASC, [PDC_TPDID] ASC, [PDC_PRODID] ASC),
    CONSTRAINT [FK_PRODUCTO_PRODUCTOS_PRODUCTO] FOREIGN KEY ([PDC_CODEMP], [PDC_PRODID]) REFERENCES [dbo].[PRODUCTOS] ([PDT_CODEMP], [PDT_PRODID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PRODUCTOS_DOCUMENTOS]([PDC_CODEMP] ASC, [PDC_TPDID] ASC, [PDC_PRODID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos documentos para los productos
   
   Podran ser manuales, especificaciones lo que sea, ya que depende de la tabla descripcion productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS_DOCUMENTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el orden en el cual se podra mostrar los distintos documentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS_DOCUMENTOS', @level2type = N'COLUMN', @level2name = N'PDC_ORDEN';

