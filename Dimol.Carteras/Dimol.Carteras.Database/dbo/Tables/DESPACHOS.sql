CREATE TABLE [dbo].[DESPACHOS] (
    [DPC_CODEMP]    INT           NOT NULL,
    [DPC_SUCID]     INT           NOT NULL,
    [DPC_DPCID]     NUMERIC (15)  NOT NULL,
    [DPC_TRANSPRO]  CHAR (1)      NOT NULL,
    [DPC_TPTID]     INT           NOT NULL,
    [DPC_TRAID]     INT           NULL,
    [DPC_PCLID]     NUMERIC (15)  NULL,
    [DPC_NOMBRE]    VARCHAR (200) NOT NULL,
    [DPC_NUMERO]    VARCHAR (20)  NOT NULL,
    [DPC_DESCTRANS] VARCHAR (500) NULL,
    [DPC_PATENTE2]  VARCHAR (20)  NULL,
    [DPC_NROSELLO1] VARCHAR (20)  NULL,
    [DPC_NROSELLO2] VARCHAR (20)  NULL,
    [DPC_DESDE]     DATETIME      NOT NULL,
    [DPC_HASTA]     DATETIME      NOT NULL,
    [DPC_NROCITA]   INT           NOT NULL,
    [DPC_PCLIDDESP] NUMERIC (15)  NOT NULL,
    [PCD_PCDID]     INT           NOT NULL,
    [DPC_IMPID]     INT           NULL,
    CONSTRAINT [PK_DESPACHOS] PRIMARY KEY CLUSTERED ([DPC_CODEMP] ASC, [DPC_SUCID] ASC, [DPC_DPCID] ASC),
    CONSTRAINT [FK_DESPACHO_EMPSUC_DE_EMPRESA_] FOREIGN KEY ([DPC_CODEMP], [DPC_SUCID]) REFERENCES [dbo].[EMPRESA_SUCURSAL] ([ESU_CODEMP], [ESU_SUCID]),
    CONSTRAINT [FK_DESPACHO_IMP_DESP_IMPORTAC] FOREIGN KEY ([DPC_CODEMP], [DPC_IMPID]) REFERENCES [dbo].[IMPORTACION] ([IMP_CODEMP], [IMP_IMPID]),
    CONSTRAINT [FK_DESPACHO_PROVCLI_D_PROVCLI] FOREIGN KEY ([DPC_CODEMP], [DPC_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID]),
    CONSTRAINT [FK_DESPACHO_PROVCLI_D_PROVCLI_] FOREIGN KEY ([DPC_CODEMP], [DPC_PCLIDDESP], [PCD_PCDID]) REFERENCES [dbo].[PROVCLI_DESPACHOS] ([PCD_CODEMP], [PCD_PCLID], [PCD_PCDID]),
    CONSTRAINT [FK_DESPACHO_TIPTRANS__TIPOS_TR] FOREIGN KEY ([DPC_CODEMP], [DPC_TPTID]) REFERENCES [dbo].[TIPOS_TRANSPORTE] ([TPT_CODEMP], [TPT_TPTID]),
    CONSTRAINT [FK_DESPACHO_TRANS_DES_TRANSPOR] FOREIGN KEY ([DPC_CODEMP], [DPC_TPTID], [DPC_TRAID]) REFERENCES [dbo].[TRANSPORTE] ([TRA_CODEMP], [TRA_TPTID], [TRA_TRAID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DESPACHOS]([DPC_CODEMP] ASC, [DPC_SUCID] ASC, [DPC_DPCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacenara los distintos datos para despacho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el transporte es propio o de otra empresa, si es de otra empresa se debe ingresar la empresa transportista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS', @level2type = N'COLUMN', @level2name = N'DPC_TRANSPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, las descripciones del transporte
   
   Ejemplo : Camion 50 mt con rampla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS', @level2type = N'COLUMN', @level2name = N'DPC_DESCTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, los datos en caso que utilice una rampla el camion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS', @level2type = N'COLUMN', @level2name = N'DPC_PATENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, la hora de inicio de despacho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS', @level2type = N'COLUMN', @level2name = N'DPC_DESDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este cmzpo indica la hora de fin del despacho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS', @level2type = N'COLUMN', @level2name = N'DPC_HASTA';

