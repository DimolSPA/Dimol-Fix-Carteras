CREATE TABLE [dbo].[CARTERA_CLIENTES_ANEXAR] (
    [CCA_CODEMP] INT          NOT NULL,
    [CCA_CCAID]  INT          NOT NULL,
    [CCA_PCLID]  NUMERIC (15) NOT NULL,
    [CCA_CTCID]  NUMERIC (15) NOT NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES_ANEXAR] PRIMARY KEY CLUSTERED ([CCA_CODEMP] ASC, [CCA_CCAID] ASC, [CCA_PCLID] ASC, [CCA_CTCID] ASC),
    CONSTRAINT [FK_CARTERA__CARTCLI_A_CARTERA_] FOREIGN KEY ([CCA_CODEMP], [CCA_PCLID], [CCA_CTCID]) REFERENCES [dbo].[CARTERA_CLIENTES] ([CTC_CODEMP], [CTC_PCLID], [CTC_CTCID]),
    CONSTRAINT [FK_CARTERA__EMPRESA_C_EMPRESA] FOREIGN KEY ([CCA_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CARTERA_CLIENTES_ANEXAR]([CCA_CODEMP] ASC, [CCA_CCAID] ASC, [CCA_PCLID] ASC, [CCA_CTCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, anexa casos de distintos deudores y los permite trabajar como 1 solo
   
   Ejemplo:
   
   El mismo deudor esta en Cocha, Copec y Continental, el caso podra ser negociado como 1 solo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_ANEXAR';

