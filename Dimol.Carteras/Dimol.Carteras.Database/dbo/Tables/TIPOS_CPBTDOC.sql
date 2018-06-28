CREATE TABLE [dbo].[TIPOS_CPBTDOC] (
    [TPC_CODEMP]    INT          NOT NULL,
    [TPC_TPCID]     INT          NOT NULL,
    [TPC_CLBID]     INT          NOT NULL,
    [TPC_NOMBRE]    VARCHAR (80) NOT NULL,
    [TPC_TALONARIO] CHAR (1)     DEFAULT ('N') NOT NULL,
    [TPC_ULTNUM]    NUMERIC (15) DEFAULT ((0)) NOT NULL,
    [TPC_LINEAS]    SMALLINT     DEFAULT ((0)) NOT NULL,
    [TPC_CODIGO]    VARCHAR (20) NULL,
    [TPC_TIPDIG]    SMALLINT     DEFAULT ((0)) NULL,
    CONSTRAINT [PK_TIPOS_CPBTDOC] PRIMARY KEY NONCLUSTERED ([TPC_CODEMP] ASC, [TPC_TPCID] ASC),
    CONSTRAINT [CKC_TPC_TALONARIO_TIPOS_CP] CHECK ([TPC_TALONARIO]='N' OR [TPC_TALONARIO]='S'),
    CONSTRAINT [CKC_TPC_TIPDIG_TIPOS_CP] CHECK ([TPC_TIPDIG] IS NULL OR ([TPC_TIPDIG]=(9) OR [TPC_TIPDIG]=(8) OR [TPC_TIPDIG]=(7) OR [TPC_TIPDIG]=(6) OR [TPC_TIPDIG]=(5) OR [TPC_TIPDIG]=(4) OR [TPC_TIPDIG]=(3) OR [TPC_TIPDIG]=(2) OR [TPC_TIPDIG]=(1) OR [TPC_TIPDIG]=(0))),
    CONSTRAINT [FK_TIPOS_CP_CLACPBT_T_CLASIFIC] FOREIGN KEY ([TPC_CODEMP], [TPC_CLBID]) REFERENCES [dbo].[CLASIFICACION_CPBTDOC] ([CLB_CODEMP], [CLB_CLBID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_CPBTDOC]([TPC_TPCID] ASC, [TPC_CODEMP] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Est atabla, almacena los distintos tipos de comprobante y documentos para cada empresa, dependiendo su comportamiento a la clasificacion
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CPBTDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el comprobante utilizara o no talonario, dependiendo de la sucursal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CPBTDOC', @level2type = N'COLUMN', @level2name = N'TPC_TALONARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica cuantas lineas manejara cada comprobante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CPBTDOC', @level2type = N'COLUMN', @level2name = N'TPC_LINEAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, de que tipo de documento digital es ("Factura electronica", "Guia", etc)
   
   por default es 0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_CPBTDOC', @level2type = N'COLUMN', @level2name = N'TPC_TIPDIG';

