CREATE TABLE [dbo].[MOTIVO_COBRANZA] (
    [MTC_CODEMP] INT          NOT NULL,
    [MTC_MTCID]  INT          NOT NULL,
    [MTC_NOMBRE] VARCHAR (80) NOT NULL,
    CONSTRAINT [PK_MOTIVO_COBRANZA] PRIMARY KEY NONCLUSTERED ([MTC_CODEMP] ASC, [MTC_MTCID] ASC),
    CONSTRAINT [FK_MOTIVO_C_EMPRESA_M_EMPRESA] FOREIGN KEY ([MTC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[MOTIVO_COBRANZA]([MTC_MTCID] ASC, [MTC_CODEMP] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOM]
    ON [dbo].[MOTIVO_COBRANZA]([MTC_CODEMP] ASC, [MTC_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos motivos para la cobranza, generalmente se utilizara mas en la parte de documentos:
   
   Ejemplo
   
   1.-Falta de fondos
   2.-Orden de no pago
   3.-Etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MOTIVO_COBRANZA';

