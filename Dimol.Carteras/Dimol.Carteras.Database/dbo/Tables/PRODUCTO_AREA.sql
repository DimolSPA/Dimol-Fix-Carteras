CREATE TABLE [dbo].[PRODUCTO_AREA] (
    [PTA_CODEMP] INT          NOT NULL,
    [PTA_PTAID]  INT          NOT NULL,
    [PTA_NOMBRE] VARCHAR (50) NOT NULL,
    [PTA_ORDEN]  SMALLINT     DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRODUCTO_AREA] PRIMARY KEY NONCLUSTERED ([PTA_CODEMP] ASC, [PTA_PTAID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PRODUCTO_AREA]([PTA_CODEMP] ASC, [PTA_PTAID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, indica en que areas se maneja el subproducto
   
   Ejemplo:
   
   TF .-> Parte Activa
                 -> Bobina
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRODUCTO_AREA';

