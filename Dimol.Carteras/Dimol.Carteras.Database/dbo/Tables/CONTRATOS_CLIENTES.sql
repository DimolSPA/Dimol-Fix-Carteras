CREATE TABLE [dbo].[CONTRATOS_CLIENTES] (
    [CTC_CODEMP]     INT           NOT NULL,
    [CTC_CCTID]      INT           NOT NULL,
    [CTC_PCLID]      NUMERIC (15)  NOT NULL,
    [CTC_FECINI]     DATETIME      NOT NULL,
    [CTC_FECFIN]     DATETIME      NULL,
    [CTC_INDEFINIDO] CHAR (1)      DEFAULT ('N') NOT NULL,
    [CTC_RUT]        VARCHAR (20)  NULL,
    [CTC_NOMBRE]     VARCHAR (200) NULL,
    [CTC_INTCLI]     CHAR (1)      DEFAULT ('S') NULL,
    [CTC_HONCLI]     CHAR (1)      DEFAULT ('N') NULL,
    CONSTRAINT [PK_CONTRATOS_CLIENTES] PRIMARY KEY CLUSTERED ([CTC_CODEMP] ASC, [CTC_CCTID] ASC, [CTC_PCLID] ASC),
    CONSTRAINT [CKC_CTC_HONCLI_CONTRATO] CHECK ([CTC_HONCLI] IS NULL OR ([CTC_HONCLI]='N' OR [CTC_HONCLI]='S')),
    CONSTRAINT [CKC_CTC_INDEFINIDO_CONTRATO] CHECK ([CTC_INDEFINIDO]='N' OR [CTC_INDEFINIDO]='S'),
    CONSTRAINT [CKC_CTC_INTCLI_CONTRATO] CHECK ([CTC_INTCLI] IS NULL OR ([CTC_INTCLI]='N' OR [CTC_INTCLI]='S')),
    CONSTRAINT [FK_CONTRATO_CONTCART__CONTRATO] FOREIGN KEY ([CTC_CODEMP], [CTC_CCTID]) REFERENCES [dbo].[CONTRATOS_CARTERA] ([CCT_CODEMP], [CCT_CCTID]),
    CONSTRAINT [FK_CONTRATO_PROVCLI_C_PROVCLI] FOREIGN KEY ([CTC_CODEMP], [CTC_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CONTRATOS_CLIENTES]([CTC_CODEMP] ASC, [CTC_CCTID] ASC, [CTC_PCLID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos contratos para cada cliente, se indicara la duracion de dicho contrato etc...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONTRATOS_CLIENTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el contrato tiene o no fecha de termino
   
   S -> Si se debe ingresar la fecha de termino
   N -> No es necesario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONTRATOS_CLIENTES', @level2type = N'COLUMN', @level2name = N'CTC_INDEFINIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el representante legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONTRATOS_CLIENTES', @level2type = N'COLUMN', @level2name = N'CTC_RUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el nombre del representante legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONTRATOS_CLIENTES', @level2type = N'COLUMN', @level2name = N'CTC_NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si los intereses, seran o no del cliente
   
   por default es SI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONTRATOS_CLIENTES', @level2type = N'COLUMN', @level2name = N'CTC_INTCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si los honorarios de recuperacion seran o no del cliente, por default es No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONTRATOS_CLIENTES', @level2type = N'COLUMN', @level2name = N'CTC_HONCLI';

