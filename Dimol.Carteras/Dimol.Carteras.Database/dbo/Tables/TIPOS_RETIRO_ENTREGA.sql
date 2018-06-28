CREATE TABLE [dbo].[TIPOS_RETIRO_ENTREGA] (
    [TRE_CODEMP] INT          NOT NULL,
    [TRE_TREID]  INT          NOT NULL,
    [TRE_NOMBRE] VARCHAR (80) NOT NULL,
    CONSTRAINT [PK_TIPOS_RETIRO_ENTREGA] PRIMARY KEY NONCLUSTERED ([TRE_CODEMP] ASC, [TRE_TREID] ASC),
    CONSTRAINT [FK_TIPOS_RE_EMPRESA_T_EMPRESA] FOREIGN KEY ([TRE_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_RETIRO_ENTREGA]([TRE_CODEMP] ASC, [TRE_TREID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TIPOS_RETIRO_ENTREGA]([TRE_CODEMP] ASC, [TRE_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, indicara los distintos tipos de retiros y entregas de documentos
   
   si el campo solicita retiro entrega de la tabla cartera_cpbt_doc es R o E, se debera indicar si se debe llevar o no un documento o algo similar, que estar en esta tabla definido
   
   ejemplo:
   
   1.-Cuarta Copia
   2.-Cheque', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_RETIRO_ENTREGA';

