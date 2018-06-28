CREATE TABLE [dbo].[TIPOS_COSTO] (
    [TCO_CODEMP] INT          NOT NULL,
    [TCO_TCOID]  INT          NOT NULL,
    [TCO_NOMBRE] VARCHAR (50) NOT NULL,
    [TCO_AGRUPA] SMALLINT     NULL,
    CONSTRAINT [PK_TIPOS_COSTO] PRIMARY KEY CLUSTERED ([TCO_CODEMP] ASC, [TCO_TCOID] ASC),
    CONSTRAINT [CKC_TCO_AGRUPA_TIPOS_CO] CHECK ([TCO_AGRUPA] IS NULL OR ([TCO_AGRUPA]=(12) OR [TCO_AGRUPA]=(11) OR [TCO_AGRUPA]=(10) OR [TCO_AGRUPA]=(9) OR [TCO_AGRUPA]=(8) OR [TCO_AGRUPA]=(7) OR [TCO_AGRUPA]=(6) OR [TCO_AGRUPA]=(5) OR [TCO_AGRUPA]=(4) OR [TCO_AGRUPA]=(3) OR [TCO_AGRUPA]=(2) OR [TCO_AGRUPA]=(1))),
    CONSTRAINT [FK_TIPOS_CO_EMPRESA_C_EMPRESA] FOREIGN KEY ([TCO_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_COSTO]([TCO_CODEMP] ASC, [TCO_TCOID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TIPOS_COSTO]([TCO_CODEMP] ASC, [TCO_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos tipos de costos, que se asociaran al comprobante dependiendo de la importacion
   
   Ejemplo:
   
   Handling, FOB, etc', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_COSTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, como se agruparan los distintos tipos de costos
   
   Ejemplo
   
   Flete, Handling...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_COSTO', @level2type = N'COLUMN', @level2name = N'TCO_AGRUPA';

