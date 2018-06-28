CREATE TABLE [dbo].[NEGOCIACION_PAGOS] (
    [NGP_CODEMP]    INT             NOT NULL,
    [NGP_ANIO]      SMALLINT        NOT NULL,
    [NGP_NEGID]     INT             NOT NULL,
    [NGP_NGPID]     SMALLINT        NOT NULL,
    [NGP_FRPID]     INT             NOT NULL,
    [NGP_FECHAS]    DATETIME        NOT NULL,
    [NGP_MONTO]     DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [NGP_PAGDIR]    CHAR (1)        DEFAULT ('N') NULL,
    [NGP_PAGCLI]    CHAR (1)        DEFAULT ('N') NULL,
    [NGP_CODMON]    INT             NULL,
    [NGP_TIPCAMBIO] DECIMAL (10, 2) NULL,
    CONSTRAINT [PK_NEGOCIACION_PAGOS] PRIMARY KEY CLUSTERED ([NGP_CODEMP] ASC, [NGP_ANIO] ASC, [NGP_NEGID] ASC, [NGP_NGPID] ASC),
    CONSTRAINT [FK_NEGOCIAC_FORMPAG_N_FORMAS_P] FOREIGN KEY ([NGP_CODEMP], [NGP_FRPID]) REFERENCES [dbo].[FORMAS_PAGO] ([FRP_CODEMP], [FRP_FRPID]),
    CONSTRAINT [FK_NEGOCIAC_MONEDA_NE_MONEDAS] FOREIGN KEY ([NGP_CODEMP], [NGP_CODMON]) REFERENCES [dbo].[MONEDAS] ([MON_CODEMP], [MON_CODMON]),
    CONSTRAINT [FK_NEGOCIAC_NEGO_PAGO_NEGOCIAC] FOREIGN KEY ([NGP_CODEMP], [NGP_ANIO], [NGP_NEGID]) REFERENCES [dbo].[NEGOCIACION] ([NEG_CODEMP], [NEG_ANIO], [NEG_NEGID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[NEGOCIACION_PAGOS]([NGP_CODEMP] ASC, [NGP_ANIO] ASC, [NGP_NEGID] ASC, [NGP_NGPID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas formas de pago y los montos a cancelar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NEGOCIACION_PAGOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, la fecha en la cual sera realizada los pagos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NEGOCIACION_PAGOS', @level2type = N'COLUMN', @level2name = N'NGP_FECHAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si es pago directo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NEGOCIACION_PAGOS', @level2type = N'COLUMN', @level2name = N'NGP_PAGDIR';

