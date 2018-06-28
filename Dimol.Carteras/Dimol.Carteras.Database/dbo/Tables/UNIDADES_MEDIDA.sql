CREATE TABLE [dbo].[UNIDADES_MEDIDA] (
    [UNM_UNMID]   INT          NOT NULL,
    [UNM_NOMBRE]  VARCHAR (20) NOT NULL,
    [UNM_SIMBOLO] VARCHAR (5)  NOT NULL,
    [UNM_AGRUPA]  SMALLINT     NOT NULL,
    CONSTRAINT [PK_UNIDADES_MEDIDA] PRIMARY KEY NONCLUSTERED ([UNM_UNMID] ASC),
    CONSTRAINT [CKC_UNM_AGRUPA_UNIDADES] CHECK ([UNM_AGRUPA]=(8) OR [UNM_AGRUPA]=(7) OR [UNM_AGRUPA]=(6) OR [UNM_AGRUPA]=(5) OR [UNM_AGRUPA]=(4) OR [UNM_AGRUPA]=(3) OR [UNM_AGRUPA]=(2) OR [UNM_AGRUPA]=(1))
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[UNIDADES_MEDIDA]([UNM_UNMID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[UNIDADES_MEDIDA]([UNM_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tablaalmacena, los distintos tipos de unidades de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UNIDADES_MEDIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indicara en que forma se sgruparan
   
   Ejemplo
   
   Longitud: Metro, Centimetro, Kilometros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UNIDADES_MEDIDA', @level2type = N'COLUMN', @level2name = N'UNM_AGRUPA';

