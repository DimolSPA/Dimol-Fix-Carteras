﻿CREATE TABLE [dbo].[CARTERA_CLIENTES] (
    [CTC_CODEMP] INT          NOT NULL,
    [CTC_PCLID]  NUMERIC (15) NOT NULL,
    [CTC_CTCID]  NUMERIC (15) NOT NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES] PRIMARY KEY NONCLUSTERED ([CTC_CODEMP] ASC, [CTC_PCLID] ASC, [CTC_CTCID] ASC),
    CONSTRAINT [FK_CARTERA__DEUDORES__DEUDORES] FOREIGN KEY ([CTC_CODEMP], [CTC_CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID]),
    CONSTRAINT [FK_CARTERA__PROVCLI_C_PROVCLI] FOREIGN KEY ([CTC_CODEMP], [CTC_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CARTERA_CLIENTES]([CTC_CODEMP] ASC, [CTC_PCLID] ASC, [CTC_CTCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena la cartera por cada cliente, en ella se podra almacenar todos los datos relacionados al deudor u otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES';

