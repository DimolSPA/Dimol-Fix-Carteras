﻿CREATE TABLE [dbo].[ASIENTOS_CONTABLES_CPBTDOC_APL] (
    [ADA_CODEMP]  INT          NOT NULL,
    [ADA_ANIO]    INT          NOT NULL,
    [ADA_TIPO]    CHAR (1)     NOT NULL,
    [ADA_NUMERO]  NUMERIC (15) NOT NULL,
    [ADA_ITEM]    SMALLINT     NOT NULL,
    [ADA_SUCID]   INT          NOT NULL,
    [ADA_TPCID]   INT          NULL,
    [ADA_NUMCPBT] NUMERIC (15) NULL,
    [ADA_ANIODOC] SMALLINT     NULL,
    [ADA_NUMDOC]  NUMERIC (15) NULL,
    [ADA_ANIOAPL] INT          NULL,
    [ADA_NUMAPL]  NUMERIC (15) NULL,
    CONSTRAINT [PK_ASIENTOS_CONTABLES_CPBTDOC_] PRIMARY KEY CLUSTERED ([ADA_CODEMP] ASC, [ADA_ANIO] ASC, [ADA_TIPO] ASC, [ADA_NUMERO] ASC, [ADA_ITEM] ASC),
    CONSTRAINT [CKC_ADA_TIPO_ASIENTOS] CHECK ([ADA_TIPO]='A' OR [ADA_TIPO]='T' OR [ADA_TIPO]='E' OR [ADA_TIPO]='I'),
    CONSTRAINT [FK_ASIENTOS_APLI_ASIC_APLICACI] FOREIGN KEY ([ADA_CODEMP], [ADA_SUCID], [ADA_ANIOAPL], [ADA_NUMAPL]) REFERENCES [dbo].[APLICACIONES] ([APL_CODEMP], [APL_SUCID], [APL_ANIO], [APL_NUMAPL]),
    CONSTRAINT [FK_ASIENTOS_ASICONT_C_ASIENTOS] FOREIGN KEY ([ADA_CODEMP], [ADA_ANIO], [ADA_TIPO], [ADA_NUMERO]) REFERENCES [dbo].[ASIENTOS_CONTABLES] ([AST_CODEMP], [AST_ANIO], [AST_TIPO], [AST_NUMERO]),
    CONSTRAINT [FK_ASIENTOS_CABCPBT_A_CABACERA] FOREIGN KEY ([ADA_CODEMP], [ADA_SUCID], [ADA_TPCID], [ADA_NUMCPBT]) REFERENCES [dbo].[CABACERA_COMPROBANTES] ([CBC_CODEMP], [CBC_SUCID], [CBC_TPCID], [CBC_NUMERO]),
    CONSTRAINT [FK_ASIENTOS_DOCDIA_AS_DOCUMENT] FOREIGN KEY ([ADA_CODEMP], [ADA_SUCID], [ADA_ANIODOC], [ADA_NUMDOC]) REFERENCES [dbo].[DOCUMENTOS_DIARIOS] ([DDI_CODEMP], [DDI_SUCID], [DDI_ANIO], [DDI_NUMDOC]),
    CONSTRAINT [FK_ASIENTOS_EMPSUC_CP_EMPRESA_] FOREIGN KEY ([ADA_CODEMP], [ADA_SUCID]) REFERENCES [dbo].[EMPRESA_SUCURSAL] ([ESU_CODEMP], [ESU_SUCID])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ASIENTOS_CONTABLES_CPBTDOC_APL]([ADA_CODEMP] ASC, [ADA_ANIO] ASC, [ADA_TIPO] ASC, [ADA_NUMERO] ASC, [ADA_ITEM] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena que documento o comprobante genero el asiento contable o aplicacion realizo la siguiente modificacion en el asiento contable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ASIENTOS_CONTABLES_CPBTDOC_APL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el año de la aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ASIENTOS_CONTABLES_CPBTDOC_APL', @level2type = N'COLUMN', @level2name = N'ADA_ANIOAPL';

