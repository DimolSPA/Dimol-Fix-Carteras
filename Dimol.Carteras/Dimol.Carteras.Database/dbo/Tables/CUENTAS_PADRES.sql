CREATE TABLE [dbo].[CUENTAS_PADRES] (
    [CTP_CODEMP] INT           NOT NULL,
    [CTP_CTPID]  INT           NOT NULL,
    [CTP_CODIGO] VARCHAR (20)  NOT NULL,
    [CTP_NOMBRE] VARCHAR (100) NOT NULL,
    [CTP_AGRUPA] NUMERIC (2)   NOT NULL,
    CONSTRAINT [PK_CUENTAS_PADRES] PRIMARY KEY NONCLUSTERED ([CTP_CODEMP] ASC, [CTP_CTPID] ASC),
    CONSTRAINT [CKC_CTP_AGRUPA_CUENTAS_] CHECK ([CTP_AGRUPA]=(4) OR [CTP_AGRUPA]=(3) OR [CTP_AGRUPA]=(2) OR [CTP_AGRUPA]=(1)),
    CONSTRAINT [FK_CUENTAS__EMPRESA_C_EMPRESA] FOREIGN KEY ([CTP_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CUENTAS_PADRES]([CTP_CODEMP] ASC, [CTP_CTPID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_CODIGO]
    ON [dbo].[CUENTAS_PADRES]([CTP_CODEMP] ASC, [CTP_CODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las distitntas cuentas padres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CUENTAS_PADRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indicara de que tipo sera la cuenta padre, si sera de 
   
   1.- Activo
   2.- Pasivo
   3.- Perdida
   4.- Ganancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CUENTAS_PADRES', @level2type = N'COLUMN', @level2name = N'CTP_AGRUPA';

