CREATE TABLE [dbo].[ACCIONES] (
    [ACC_CODEMP] INT          NOT NULL,
    [ACC_ACCID]  INT          NOT NULL,
    [ACC_NOMBRE] VARCHAR (80) NOT NULL,
    [ACC_AGRUPA] SMALLINT     NOT NULL,
    CONSTRAINT [PK_ACCIONES] PRIMARY KEY NONCLUSTERED ([ACC_CODEMP] ASC, [ACC_ACCID] ASC),
    CONSTRAINT [CKC_ACC_AGRUPA_ACCIONES] CHECK ([ACC_AGRUPA]=(6) OR [ACC_AGRUPA]=(5) OR [ACC_AGRUPA]=(4) OR [ACC_AGRUPA]=(3) OR [ACC_AGRUPA]=(2) OR [ACC_AGRUPA]=(1)),
    CONSTRAINT [FK_ACCIONES_EMPRESA_A_EMPRESA] FOREIGN KEY ([ACC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ACCIONES]([ACC_CODEMP] ASC, [ACC_ACCID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[ACCIONES]([ACC_CODEMP] ASC, [ACC_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos tipos de acciones para cada cartera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ACCIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica, como se comporta la accion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ACCIONES', @level2type = N'COLUMN', @level2name = N'ACC_AGRUPA';

