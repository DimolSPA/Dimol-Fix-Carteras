CREATE TABLE [dbo].[FORMAS_PAGO] (
    [FRP_CODEMP]   INT           NOT NULL,
    [FRP_FRPID]    INT           NOT NULL,
    [FRP_NOMBRE]   VARCHAR (200) NOT NULL,
    [FRP_DIASVENC] SMALLINT      NOT NULL,
    [FRP_FECESP]   CHAR (1)      DEFAULT ('N') NOT NULL,
    [FRP_CUOTAS]   CHAR (1)      DEFAULT ('N') NULL,
    [FRP_TIPCPBT]  INT           NULL,
    CONSTRAINT [PK_FORMAS_PAGO] PRIMARY KEY NONCLUSTERED ([FRP_CODEMP] ASC, [FRP_FRPID] ASC),
    CONSTRAINT [CKC_FRP_CUOTAS_FORMAS_P] CHECK ([FRP_CUOTAS] IS NULL OR ([FRP_CUOTAS]='N' OR [FRP_CUOTAS]='S')),
    CONSTRAINT [CKC_FRP_FECESP_FORMAS_P] CHECK ([FRP_FECESP]='N' OR [FRP_FECESP]='S'),
    CONSTRAINT [FK_FORMAS_P_EMPRESA_F_EMPRESA] FOREIGN KEY ([FRP_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[FORMAS_PAGO]([FRP_CODEMP] ASC, [FRP_FRPID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[FORMAS_PAGO]([FRP_CODEMP] ASC, [FRP_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara las distintas formas de pago que utilizaran cada comprobante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMAS_PAGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, la cantidad de dias que tendra antes de vencer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMAS_PAGO', @level2type = N'COLUMN', @level2name = N'FRP_DIASVENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el comprobante tendra que ingresar ono una fecha especifica de vencimientoi', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMAS_PAGO', @level2type = N'COLUMN', @level2name = N'FRP_FECESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si la forma de pago sera en cuotas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMAS_PAGO', @level2type = N'COLUMN', @level2name = N'FRP_CUOTAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo se utilizara, para hacer documentos diarios automaticos
   
   en especial, para el tema de la negociacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMAS_PAGO', @level2type = N'COLUMN', @level2name = N'FRP_TIPCPBT';

