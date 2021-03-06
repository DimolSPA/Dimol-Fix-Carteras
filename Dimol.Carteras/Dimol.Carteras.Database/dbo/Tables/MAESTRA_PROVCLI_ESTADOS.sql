﻿CREATE TABLE [dbo].[MAESTRA_PROVCLI_ESTADOS] (
    [MPE_CODEMP] INT           NOT NULL,
    [MPE_PCLID]  NUMERIC (15)  NOT NULL,
    [MPE_ESTID]  SMALLINT      NOT NULL,
    [MPE_ESTADO] VARCHAR (200) NOT NULL,
    [MPE_CODIGO] VARCHAR (10)  NULL,
    CONSTRAINT [PK_MAESTRA_PROVCLI_ESTADOS] PRIMARY KEY CLUSTERED ([MPE_CODEMP] ASC, [MPE_PCLID] ASC, [MPE_ESTID] ASC, [MPE_ESTADO] ASC),
    CONSTRAINT [FK_MAESTRA__ESTCART_M_ESTADOS_] FOREIGN KEY ([MPE_CODEMP], [MPE_ESTID]) REFERENCES [dbo].[ESTADOS_CARTERA] ([ECT_CODEMP], [ECT_ESTID]),
    CONSTRAINT [FK_MAESTRA__PROVMAEST_PROVCLI] FOREIGN KEY ([MPE_CODEMP], [MPE_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[MAESTRA_PROVCLI_ESTADOS]([MPE_CODEMP] ASC, [MPE_PCLID] ASC, [MPE_ESTID] ASC, [MPE_ESTADO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos estados que utiliza nuestro cliente para sacar reportes por sus propios estados y poder agruparlos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MAESTRA_PROVCLI_ESTADOS';

