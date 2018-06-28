CREATE TABLE [dbo].[COMUNA_PROVCLI] (
    [CPC_CODEMP] INT           NOT NULL,
    [CPC_PCLID]  NUMERIC (15)  NOT NULL,
    [CPC_CODIGO] INT           NOT NULL,
    [CPC_NOMBRE] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_COMUNA_PROVCLI] PRIMARY KEY CLUSTERED ([CPC_CODEMP] ASC, [CPC_PCLID] ASC, [CPC_CODIGO] ASC),
    CONSTRAINT [FK_COMUNA_P_PROVCLI_C_PROVCLI] FOREIGN KEY ([CPC_CODEMP], [CPC_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[COMUNA_PROVCLI]([CPC_CODEMP] ASC, [CPC_PCLID] ASC, [CPC_CODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas comunas, para los clientes o proveedores
   
   Esto se utilizara para cuando envian datos de cargas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COMUNA_PROVCLI';

