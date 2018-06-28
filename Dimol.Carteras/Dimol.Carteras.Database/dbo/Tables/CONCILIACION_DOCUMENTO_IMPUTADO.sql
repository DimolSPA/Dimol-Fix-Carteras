CREATE TABLE [dbo].[CONCILIACION_DOCUMENTO_IMPUTADO] (
    [CONCILIACION_DOCUMENTO_IMPUTADO_ID] INT             NOT NULL,
    [CODEMP]                             INT             NOT NULL,
    [CONCILIACION_ID]                    INT             NOT NULL,
    [CCBID]                              INT             NOT NULL,
    [SALDO]                              DECIMAL (15, 2) NOT NULL,
    [INTERES]                            DECIMAL (15, 2) NOT NULL,
    [HONORARIO]                          DECIMAL (15, 2) NOT NULL,
    [GASTOPRE]                           DECIMAL (15, 2) NOT NULL,
    [GASTOJUD]                           DECIMAL (15, 2) NOT NULL,
    [USRID_REGISTRO]                     INT             NOT NULL,
    [FEC_REGISTRO]                       DATETIME        CONSTRAINT [DF_CONCILIACION_DOCUMENTO_IMPUTADO_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    [ESTADO]                             CHAR (1)        NULL,
    CONSTRAINT [PK_CONCILIACION_DOCUMENTO_IMPUTADO] PRIMARY KEY CLUSTERED ([CONCILIACION_DOCUMENTO_IMPUTADO_ID] ASC),
    CONSTRAINT [FK_IMPUTACION_CONCILIACION] FOREIGN KEY ([CONCILIACION_ID]) REFERENCES [dbo].[CONCILIACION_MOVIMIENTOS_DOCUMENTOS] ([CONCILIACION_ID])
);

