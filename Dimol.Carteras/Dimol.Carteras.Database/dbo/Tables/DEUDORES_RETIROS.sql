CREATE TABLE [dbo].[DEUDORES_RETIROS] (
    [DDR_CODEMP]     INT           NOT NULL,
    [DDR_CTCID]      NUMERIC (15)  NOT NULL,
    [DDR_DDRID]      INT           NOT NULL,
    [DDR_NOMBRE]     VARCHAR (120) NOT NULL,
    [DDR_CONTACTO]   VARCHAR (250) NOT NULL,
    [DDR_COMID]      INT           NOT NULL,
    [DDR_DIRECCION]  VARCHAR (300) NOT NULL,
    [DDR_HORDES]     DATETIME      NOT NULL,
    [DDR_HORHAS]     DATETIME      NOT NULL,
    [DDR_BCOID]      INT           NULL,
    [DDR_SUCURSAL]   VARCHAR (200) NULL,
    [DDR_COMENTARIO] TEXT          NULL,
    [DDR_HORCALL01]  DATETIME      NULL,
    [DDR_HORCALL02]  DATETIME      NULL,
    CONSTRAINT [PK_DEUDORES_RETIROS] PRIMARY KEY CLUSTERED ([DDR_CODEMP] ASC, [DDR_CTCID] ASC, [DDR_DDRID] ASC),
    CONSTRAINT [FK_DEUDORES_BANCO_DEU_BANCOS] FOREIGN KEY ([DDR_CODEMP], [DDR_BCOID]) REFERENCES [dbo].[BANCOS] ([BCO_CODEMP], [BCO_BCOID]),
    CONSTRAINT [FK_DEUDORES_COM_DEURE_COMUNA] FOREIGN KEY ([DDR_COMID]) REFERENCES [dbo].[COMUNA] ([COM_COMID]),
    CONSTRAINT [FK_DEUDORES_DEU_DEURE_DEUDORES] FOREIGN KEY ([DDR_CODEMP], [DDR_CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI]
    ON [dbo].[DEUDORES_RETIROS]([DDR_CODEMP] ASC, [DDR_CTCID] ASC, [DDR_DDRID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[DEUDORES_RETIROS]([DDR_CODEMP] ASC, [DDR_CTCID] ASC, [DDR_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos datos para retiro, en distintas sucursales, bancos etc....', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_RETIROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el horario en el cual se puede realizar el retiro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_RETIROS', @level2type = N'COLUMN', @level2name = N'DDR_HORDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hasta que hora se puede realizar el retiro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_RETIROS', @level2type = N'COLUMN', @level2name = N'DDR_HORHAS';

