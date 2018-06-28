CREATE TABLE [dbo].[TIPOS_DOCUMENTOS_ENVIOS] (
    [TDE_CODEMP] INT           NOT NULL,
    [TDE_TDEID]  INT           NOT NULL,
    [TDE_NOMBRE] VARCHAR (150) NOT NULL,
    [TDE_TIPO]   CHAR (1)      NOT NULL,
    CONSTRAINT [PK_TIPOS_DOCUMENTOS_ENVIOS] PRIMARY KEY CLUSTERED ([TDE_CODEMP] ASC, [TDE_TDEID] ASC),
    CONSTRAINT [CKC_TDE_TIPO_TIPOS_DO] CHECK ([TDE_TIPO]='A' OR [TDE_TIPO]='J' OR [TDE_TIPO]='P'),
    CONSTRAINT [FK_TIPOS_DO_EMP_TIPDE_EMPRESA] FOREIGN KEY ([TDE_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_DOCUMENTOS_ENVIOS]([TDE_CODEMP] ASC, [TDE_TDEID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos tipos de documtops que seran enviados, por carta u otro documento tanto a los clientes, deudores, procuradores, etc...
   
   
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_DOCUMENTOS_ENVIOS';

