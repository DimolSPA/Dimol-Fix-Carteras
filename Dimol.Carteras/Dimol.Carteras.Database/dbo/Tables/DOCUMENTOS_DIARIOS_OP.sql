CREATE TABLE [dbo].[DOCUMENTOS_DIARIOS_OP] (
    [DDO_CODEMP]     INT             NOT NULL,
    [DDO_SUCID]      INT             NOT NULL,
    [DDO_ANIO]       SMALLINT        NOT NULL,
    [DDO_NUMDOC]     NUMERIC (15)    NOT NULL,
    [DDO_TPCID]      INT             NULL,
    [DDO_TIPMOV]     CHAR (1)        NULL,
    [DDO_PCLID]      NUMERIC (15)    NULL,
    [DDO_PROPIO]     CHAR (1)        NULL,
    [DDO_BCOID]      INT             NULL,
    [DDO_CTACTE]     VARCHAR (50)    NULL,
    [DDO_FECING]     DATETIME        NULL,
    [DDO_FECDOC]     DATETIME        NULL,
    [DDO_FECVENC]    DATETIME        NULL,
    [DDO_EDCID]      INT             NULL,
    [DDO_NUMCTA]     VARCHAR (50)    NULL,
    [DDO_MONTO]      DECIMAL (15, 2) NULL,
    [DDO_SALDO]      DECIMAL (15, 2) NULL,
    [DDO_CODMON]     INT             NULL,
    [DDO_TIPCAMBIO]  DECIMAL (15, 2) NULL,
    [DDO_TITULAR]    CHAR (1)        NULL,
    [DDO_RUTPAG]     VARCHAR (20)    NULL,
    [DDO_NOMPAG]     VARCHAR (200)   NULL,
    [DDO_CTCID]      NUMERIC (15)    NULL,
    [DDO_EMPLEADO]   CHAR (1)        NULL,
    [DDO_EMPLID]     INT             NULL,
    [DDO_CUSTODIA]   CHAR (1)        NULL,
    [DDO_DOCEMP]     CHAR (1)        NULL,
    [DDO_PAGDIR]     CHAR (1)        NULL,
    [DDO_VECESDEP]   SMALLINT        NULL,
    [DDO_FECHADEP]   DATETIME        NULL,
    [DDO_DEPOSITAR]  CHAR (1)        NULL,
    [DDO_RUTDEP]     VARCHAR (20)    NULL,
    [DDO_NOMDEP]     VARCHAR (20)    NULL,
    [DDO_NRODEP]     VARCHAR (20)    NULL,
    [DDO_FECDEP]     DATETIME        NULL,
    [DDO_PENDIENTE]  CHAR (1)        NULL,
    [DDO_IDID]       INT             NULL,
    [DDO_TCINOMBRE]  VARCHAR (100)   NULL,
    [DDO_PCLRUT]     VARCHAR (20)    NULL,
    [DDO_PCLNOMFANT] VARCHAR (500)   NULL,
    [DDO_BCORUT]     VARCHAR (20)    NULL,
    [DDO_NOMBREBCO]  VARCHAR (200)   NULL,
    [DDO_IDIID]      INT             NULL,
    [DDO_ESTADO]     VARCHAR (100)   NULL,
    [DDO_MONNOMBRE]  VARCHAR (100)   NULL,
    [DDO_CTCRUT]     VARCHAR (20)    NULL,
    [DDO_CTCNOMFANT] VARCHAR (500)   NULL,
    [DDO_EPLRUT]     VARCHAR (20)    NULL,
    [DDO_EPLNOMBRE]  VARCHAR (100)   NULL,
    [DDO_EPLAPEPAT]  VARCHAR (100)   NULL,
    [DDO_ANIONEG]    SMALLINT        NULL,
    [DDO_NEGID]      INT             NULL,
    [DDO_CTCNUMERO]  NUMERIC (12)    NULL,
    [DDO_CTCDIGITO]  CHAR (1)        NULL,
    CONSTRAINT [PK_DOCUMENTOS_DIARIOS_OP] PRIMARY KEY CLUSTERED ([DDO_CODEMP] ASC, [DDO_SUCID] ASC, [DDO_ANIO] ASC, [DDO_NUMDOC] ASC),
    CONSTRAINT [FK_DOCUMENT_DOCDIA_OP_DOCUMENT] FOREIGN KEY ([DDO_CODEMP], [DDO_SUCID], [DDO_ANIO], [DDO_NUMDOC]) REFERENCES [dbo].[DOCUMENTOS_DIARIOS] ([DDI_CODEMP], [DDI_SUCID], [DDI_ANIO], [DDI_NUMDOC])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DOCUMENTOS_DIARIOS_OP]([DDO_CODEMP] ASC, [DDO_SUCID] ASC, [DDO_ANIO] ASC, [DDO_NUMDOC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos datos, optimizados de la tabla de documentos diarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DOCUMENTOS_DIARIOS_OP';

