﻿CREATE TABLE [dbo].[TIPOS_CAUSA_NCND_IDIOMAS] (
    [TNI_CODEMP] INT           NOT NULL,
    [TNI_TNTID]  INT           NOT NULL,
    [TNI_IDID]   INT           NOT NULL,
    [TNI_NOMBRE] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TIPOS_CAUSA_NCND_IDIOMAS] PRIMARY KEY CLUSTERED ([TNI_CODEMP] ASC, [TNI_TNTID] ASC, [TNI_IDID] ASC),
    CONSTRAINT [FK_TIPOS_CA_IDIOM_TIP_IDIOMAS] FOREIGN KEY ([TNI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID]),
    CONSTRAINT [FK_TIPOS_CA_TIPNCND_I_TIPOS_CA] FOREIGN KEY ([TNI_CODEMP], [TNI_TNTID]) REFERENCES [dbo].[TIPOS_CAUSA_NCND] ([TNT_CODEMP], [TNT_TNTID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_CAUSA_NCND_IDIOMAS]([TNI_CODEMP] ASC, [TNI_TNTID] ASC, [TNI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos tipos en su respectivo idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CAUSA_NCND_IDIOMAS';

