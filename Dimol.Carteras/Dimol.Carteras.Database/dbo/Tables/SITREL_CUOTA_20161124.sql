CREATE TABLE [dbo].[SITREL_CUOTA_20161124] (
    [CODEMP]            INT             NOT NULL,
    [ID_CARGA]          INT             NOT NULL,
    [PCLID]             INT             NOT NULL,
    [RUT]               VARCHAR (9)     NULL,
    [NUMERO_OPERACION]  VARCHAR (24)    NOT NULL,
    [PRODUCTO]          VARCHAR (20)    NOT NULL,
    [NUMERO_CUOTA]      INT             NOT NULL,
    [FECHA_VENCIMIENTO] INT             NOT NULL,
    [MONTO_DETALLE]     DECIMAL (15, 2) NOT NULL,
    [CAPITAL]           DECIMAL (15, 2) NOT NULL,
    [INTERESES]         DECIMAL (15, 2) NOT NULL,
    [GASTOS]            DECIMAL (15, 2) NOT NULL,
    [DIAS_MORA]         INT             NULL
);

