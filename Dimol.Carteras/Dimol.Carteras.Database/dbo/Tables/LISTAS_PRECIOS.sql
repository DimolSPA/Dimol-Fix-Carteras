CREATE TABLE [dbo].[LISTAS_PRECIOS] (
    [LTP_CODEMP]    INT            NOT NULL,
    [LTP_LTPID]     INT            NOT NULL,
    [LTP_NOMBRE]    VARCHAR (200)  NOT NULL,
    [LTP_TIPO]      CHAR (1)       DEFAULT ('A') NOT NULL,
    [LTP_VIGENCIA]  CHAR (1)       DEFAULT ('I') NOT NULL,
    [LTP_DESDE]     DATETIME       NULL,
    [LTP_HASTA]     DATETIME       NULL,
    [LTP_DESCUENTO] DECIMAL (5, 2) DEFAULT ((0)) NOT NULL,
    [LTP_GASTJUD]   CHAR (1)       DEFAULT ('N') NOT NULL,
    [LTP_CODMON]    INT            NULL,
    CONSTRAINT [PK_LISTAS_PRECIOS] PRIMARY KEY CLUSTERED ([LTP_CODEMP] ASC, [LTP_LTPID] ASC),
    CONSTRAINT [CKC_LTP_GASTJUD_LISTAS_P] CHECK ([LTP_GASTJUD]='N' OR [LTP_GASTJUD]='S'),
    CONSTRAINT [CKC_LTP_TIPO_LISTAS_P] CHECK ([LTP_TIPO]='A' OR [LTP_TIPO]='S' OR [LTP_TIPO]='C'),
    CONSTRAINT [CKC_LTP_VIGENCIA_LISTAS_P] CHECK ([LTP_VIGENCIA]='D' OR [LTP_VIGENCIA]='I'),
    CONSTRAINT [FK_LISTAS_P_EMPRESA_L_EMPRESA] FOREIGN KEY ([LTP_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_LISTAS_P_MONEDAS_L_MONEDAS] FOREIGN KEY ([LTP_CODEMP], [LTP_CODMON]) REFERENCES [dbo].[MONEDAS] ([MON_CODEMP], [MON_CODMON])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[LISTAS_PRECIOS]([LTP_CODEMP] ASC, [LTP_LTPID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas listas de precio, su duracion tipo etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LISTAS_PRECIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Con stock, Sin Stock, Ambas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LISTAS_PRECIOS', @level2type = N'COLUMN', @level2name = N'LTP_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la vigencia de la lista de precio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LISTAS_PRECIOS', @level2type = N'COLUMN', @level2name = N'LTP_VIGENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si los productos que contendra seran de la parte judicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LISTAS_PRECIOS', @level2type = N'COLUMN', @level2name = N'LTP_GASTJUD';

