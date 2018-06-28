CREATE TABLE [dbo].[TIPOS_CONTRATO] (
    [TIC_CODEMP]     INT          NOT NULL,
    [TIC_TICID]      INT          NOT NULL,
    [TIC_NOMBRE]     VARCHAR (50) NOT NULL,
    [TIC_DURACION]   INT          NULL,
    [TIC_INDEFINIDO] CHAR (1)     DEFAULT ('N') NULL,
    CONSTRAINT [PK_TIPOS_CONTRATO] PRIMARY KEY NONCLUSTERED ([TIC_CODEMP] ASC, [TIC_TICID] ASC),
    CONSTRAINT [CKC_TIC_INDEFINIDO_TIPOS_CO] CHECK ([TIC_INDEFINIDO] IS NULL OR ([TIC_INDEFINIDO]='N' OR [TIC_INDEFINIDO]='S')),
    CONSTRAINT [FK_TIPOS_CO_EMP_TIPCO_EMPRESA] FOREIGN KEY ([TIC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_CONTRATO]([TIC_TICID] ASC, [TIC_CODEMP] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara los distintos tipos de contrato y la cantidad de dias de vigencia que tendra, tambien indicara la cantidad de dias que dure dicho contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CONTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el contrato tiene o no dias de termino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CONTRATO', @level2type = N'COLUMN', @level2name = N'TIC_INDEFINIDO';

