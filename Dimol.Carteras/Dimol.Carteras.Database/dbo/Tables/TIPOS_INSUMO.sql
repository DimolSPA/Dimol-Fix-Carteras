CREATE TABLE [dbo].[TIPOS_INSUMO] (
    [TPI_CODEMP] INT          NOT NULL,
    [TPI_TIPID]  INT          NOT NULL,
    [TPI_NOMBRE] VARCHAR (80) NOT NULL,
    CONSTRAINT [PK_TIPOS_INSUMO] PRIMARY KEY NONCLUSTERED ([TPI_CODEMP] ASC, [TPI_TIPID] ASC),
    CONSTRAINT [FK_TIPOS_IN_EMP_TIPIN_EMPRESA] FOREIGN KEY ([TPI_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_INSUMO]([TPI_CODEMP] ASC, [TPI_TIPID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TIPOS_INSUMO]([TPI_CODEMP] ASC, [TPI_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos tipos de insumo
   
   Ej. 
   
   1.- Insumos
   2.- Materias Primas
   3.- Maquinaria
   etc...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_INSUMO';

