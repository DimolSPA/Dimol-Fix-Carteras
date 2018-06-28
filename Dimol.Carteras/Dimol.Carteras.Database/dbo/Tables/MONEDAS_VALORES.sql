﻿CREATE TABLE [dbo].[MONEDAS_VALORES] (
    [MNV_CODEMP] INT             NOT NULL,
    [MNV_CODMON] INT             NOT NULL,
    [MNV_FECHA]  DATETIME        NOT NULL,
    [MNV_VALOR]  DECIMAL (10, 2) NOT NULL,
    CONSTRAINT [PK_MONEDAS_VALORES] PRIMARY KEY CLUSTERED ([MNV_CODEMP] ASC, [MNV_CODMON] ASC, [MNV_FECHA] ASC),
    CONSTRAINT [FK_MONEDAS__MONEDAS_V_MONEDAS] FOREIGN KEY ([MNV_CODEMP], [MNV_CODMON]) REFERENCES [dbo].[MONEDAS] ([MON_CODEMP], [MON_CODMON])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[MONEDAS_VALORES]([MNV_CODEMP] ASC, [MNV_CODMON] ASC, [MNV_FECHA] ASC);

