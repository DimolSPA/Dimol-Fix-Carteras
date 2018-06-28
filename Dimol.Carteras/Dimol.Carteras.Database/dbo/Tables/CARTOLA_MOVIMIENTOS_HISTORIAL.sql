CREATE TABLE [dbo].[CARTOLA_MOVIMIENTOS_HISTORIAL] (
    [HISTORIAL_ID]             INT             NOT NULL,
    [MOVIMIENTO_ID]            INT             NOT NULL,
    [CODEMP]                   INT             NOT NULL,
    [NUM_CUENTA]               VARCHAR (60)    NULL,
    [FEC_MOVIMIENTO]           DATETIME        NOT NULL,
    [MONTO]                    DECIMAL (15, 2) NOT NULL,
    [SUCURSAL]                 VARCHAR (100)   NULL,
    [NUM_COMPROBANTE_REF]      VARCHAR (100)   NULL,
    [TIPO_MOVIMIENTO_BANCO_ID] INT             NOT NULL,
    [TIPO_MOTIVO_BANCO_ID]     INT             NOT NULL,
    [TIPO_ESTADO_BANCO_ID]     INT             NOT NULL,
    [ESTATUS_ID]               INT             NOT NULL,
    [USRID_REGISTRO]           INT             NOT NULL,
    [FEC_REGISTRO]             DATETIME        CONSTRAINT [DF_CARTOLA_MOVIMIENTOS_HISTORIAL_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_CARTOLA_MOVIMIENTOS_HISTORIAL] PRIMARY KEY CLUSTERED ([HISTORIAL_ID] ASC)
);

