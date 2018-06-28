﻿CREATE TABLE [dbo].[PRODUCTOS_IMPUESTOS] (
    [PDI_CODEMP] INT          NOT NULL,
    [PDI_PRODID] NUMERIC (15) NOT NULL,
    [PDI_IPTID]  SMALLINT     NOT NULL,
    CONSTRAINT [PK_PRODUCTOS_IMPUESTOS] PRIMARY KEY NONCLUSTERED ([PDI_CODEMP] ASC, [PDI_PRODID] ASC),
    CONSTRAINT [FK_PRODUCTO_IMPU_PROD_IMPUESTO] FOREIGN KEY ([PDI_CODEMP], [PDI_IPTID]) REFERENCES [dbo].[IMPUESTOS] ([IPT_CODEMP], [IPT_IPTID]),
    CONSTRAINT [FK_PRODUCTO_PRODUSTOS_PRODUCTO] FOREIGN KEY ([PDI_CODEMP], [PDI_PRODID]) REFERENCES [dbo].[PRODUCTOS] ([PDT_CODEMP], [PDT_PRODID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PRODUCTOS_IMPUESTOS]([PDI_CODEMP] ASC, [PDI_PRODID] ASC, [PDI_IPTID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara los distintos impuestos para cada producto, en caso que maneje un impuesto especifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTOS_IMPUESTOS';
