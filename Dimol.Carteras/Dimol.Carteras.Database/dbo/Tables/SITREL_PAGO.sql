CREATE TABLE [dbo].[SITREL_PAGO] (
    [CODEMP]           INT             NOT NULL,
    [ID_CARGA]         INT             NOT NULL,
    [PCLID]            INT             NOT NULL,
    [RUT]              VARCHAR (9)     NOT NULL,
    [NUMERO_OPERACION] VARCHAR (24)    NOT NULL,
    [CODIGO_PRODUCTO]  VARCHAR (20)    NOT NULL,
    [FECHA_PAGO]       INT             NOT NULL,
    [MONTO_PAGO]       DECIMAL (15, 2) NOT NULL
);

