﻿CREATE TABLE [dbo].[PROVCLI_SUCURSAL_CONTACTO] (
    [PSC_CODEMP]   INT           NOT NULL,
    [PSC_PCLID]    NUMERIC (15)  NOT NULL,
    [PSC_PCSID]    INT           NOT NULL,
    [PSC_PSCID]    INT           NOT NULL,
    [PSC_TICID]    INT           NOT NULL,
    [PSC_NOMBRE]   VARCHAR (250) NOT NULL,
    [PSC_TELEFONO] VARCHAR (100) NOT NULL,
    [PSC_ANEXO]    VARCHAR (10)  NULL,
    [PSC_FAX]      VARCHAR (100) NULL,
    [PSC_CELULAR]  VARCHAR (100) NULL,
    [PSC_MAIL]     VARCHAR (120) NULL,
    CONSTRAINT [PK_PROVCLI_SUCURSAL_CONTACTO] PRIMARY KEY NONCLUSTERED ([PSC_CODEMP] ASC, [PSC_PCLID] ASC, [PSC_PCSID] ASC, [PSC_PSCID] ASC),
    CONSTRAINT [FK_PROVCLI__SUCURSAL__PROVCLI_] FOREIGN KEY ([PSC_CODEMP], [PSC_PCLID], [PSC_PCSID]) REFERENCES [dbo].[PROVCLI_SUCURSAL] ([PCS_CODEMP], [PCS_PCLID], [PCS_PCSID]),
    CONSTRAINT [FK_PROVCLI__TIPCONTAC_TIPOS_CO] FOREIGN KEY ([PSC_CODEMP], [PSC_TICID]) REFERENCES [dbo].[TIPOS_CONTACTO] ([TIC_CODEMP], [TIC_TICID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PROVCLI_SUCURSAL_CONTACTO]([PSC_CODEMP] ASC, [PSC_PCLID] ASC, [PSC_PCSID] ASC, [PSC_PSCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos contactos dependiendo de la sucursal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI_SUCURSAL_CONTACTO';
