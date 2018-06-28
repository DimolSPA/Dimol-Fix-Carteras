CREATE TABLE [dbo].[INSUMO_EQUIVALENCIAS] (
    [IEQ_CODEMP]   INT            NOT NULL,
    [IEQ_INSID]    NUMERIC (15)   NOT NULL,
    [IEQ_UNMIDENT] INT            NOT NULL,
    [IEQ_VALORENT] DECIMAL (8, 2) NOT NULL,
    [IEQ_UNMIDSAL] INT            NOT NULL,
    [IEQ_VALORSAL] DECIMAL (8, 2) NOT NULL,
    [IEQ_FACTOR]   DECIMAL (8, 2) NOT NULL,
    CONSTRAINT [PK_INSUMO_EQUIVALENCIAS] PRIMARY KEY NONCLUSTERED ([IEQ_CODEMP] ASC, [IEQ_INSID] ASC),
    CONSTRAINT [FK_INSUMO_E_INSU_EQUI_INSUMOS] FOREIGN KEY ([IEQ_CODEMP], [IEQ_INSID]) REFERENCES [dbo].[INSUMOS] ([INS_CODEMP], [INS_INSID]),
    CONSTRAINT [FK_INSUMO_E_UNIMED_IN_UNIDADES] FOREIGN KEY ([IEQ_UNMIDENT]) REFERENCES [dbo].[UNIDADES_MEDIDA] ([UNM_UNMID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[INSUMO_EQUIVALENCIAS]([IEQ_CODEMP] ASC, [IEQ_INSID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla indica, como se comporta el producto indicando, en la forma que ingresa y sale
   
   Ejemplo:
   
   Ingresa :
   
   Kilo : 1000
   
   Sale:
   
   Metros :
   1
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMO_EQUIVALENCIAS';

