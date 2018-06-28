CREATE TABLE [dbo].[TIPOS_PROVCLI] (
    [TPC_CODEMP] INT          NOT NULL,
    [TPC_TPCID]  INT          NOT NULL,
    [TPC_NOMBRE] VARCHAR (40) NOT NULL,
    [TPC_AGRUPA] CHAR (1)     NOT NULL,
    CONSTRAINT [PK_TIPOS_PROVCLI] PRIMARY KEY NONCLUSTERED ([TPC_CODEMP] ASC, [TPC_TPCID] ASC),
    CONSTRAINT [CKC_TPC_AGRUPA_TIPOS_PR] CHECK ([TPC_AGRUPA]='O' OR [TPC_AGRUPA]='D' OR [TPC_AGRUPA]='A' OR [TPC_AGRUPA]='C' OR [TPC_AGRUPA]='V'),
    CONSTRAINT [FK_TIPOS_PR_EMPRESA_T_EMPRESA] FOREIGN KEY ([TPC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_PROVCLI]([TPC_CODEMP] ASC, [TPC_TPCID] ASC);


GO
CREATE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TIPOS_PROVCLI]([TPC_CODEMP] ASC, [TPC_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara los distintos tipos de Proveedores o Clientes, tambien indicara al grupo que pertenecen...
   
   Ventas o Compras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_PROVCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la forma en que se comportara dicho tipo de cliente o proveedor
   
   V - > Ventas
   C - > Compras
   A - > Ambos
   D - > Deudor
   O - > Otros
   
   El estado deudor se utilizara para hacer el tema de las cobranzas
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_PROVCLI', @level2type = N'COLUMN', @level2name = N'TPC_AGRUPA';

