CREATE TABLE [dbo].[PROVCLI_SUCURSAL] (
    [PCS_CODEMP]     INT           NOT NULL,
    [PCS_PCLID]      NUMERIC (15)  NOT NULL,
    [PCS_PCSID]      INT           NOT NULL,
    [PCS_NOMBRE]     VARCHAR (150) NOT NULL,
    [PCS_COMID]      INT           NOT NULL,
    [PCS_DIRECCION]  VARCHAR (500) NOT NULL,
    [PCS_TELEFONO]   VARCHAR (100) NOT NULL,
    [PCS_FAX]        VARCHAR (100) NULL,
    [PCS_MAIL]       VARCHAR (100) NULL,
    [PCS_CASAMATRIZ] CHAR (1)      DEFAULT ('S') NOT NULL,
    [PCS_BCOID]      INT           NULL,
    [PCS_TIPCTA]     CHAR (1)      NULL,
    [PCS_NUMCTA]     VARCHAR (20)  NULL,
    [PCS_CODIGO]     VARCHAR (10)  NULL,
    CONSTRAINT [PK_PROVCLI_SUCURSAL] PRIMARY KEY NONCLUSTERED ([PCS_CODEMP] ASC, [PCS_PCLID] ASC, [PCS_PCSID] ASC),
    CONSTRAINT [CKC_PCS_CASAMATRIZ_PROVCLI_] CHECK ([PCS_CASAMATRIZ]='N' OR [PCS_CASAMATRIZ]='S'),
    CONSTRAINT [CKC_PCS_TIPCTA_PROVCLI_] CHECK ([PCS_TIPCTA] IS NULL OR ([PCS_TIPCTA]='A' OR [PCS_TIPCTA]='O' OR [PCS_TIPCTA]='D' OR [PCS_TIPCTA]='R' OR [PCS_TIPCTA]='V' OR [PCS_TIPCTA]='C')),
    CONSTRAINT [FK_PROVCLI__COMUNA_PR_COMUNA] FOREIGN KEY ([PCS_COMID]) REFERENCES [dbo].[COMUNA] ([COM_COMID]),
    CONSTRAINT [FK_PROVCLI__PROVCLI_S_PROVCLI] FOREIGN KEY ([PCS_CODEMP], [PCS_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PROVCLI_SUCURSAL]([PCS_CODEMP] ASC, [PCS_PCLID] ASC, [PCS_PCSID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos datos de los proveedores y clientes dependiendo de la sucursal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI_SUCURSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si la sucursal es la casa matriz o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI_SUCURSAL', @level2type = N'COLUMN', @level2name = N'PCS_CASAMATRIZ';

