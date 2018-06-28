CREATE TABLE [dbo].[BAJAS_CPBT_DOC] (
    [ID_BAJAS]      INT             IDENTITY (1, 1) NOT NULL,
    [PCLID]         NUMERIC (15)    NOT NULL,
    [CTCID]         NUMERIC (15)    NOT NULL,
    [CCBID]         INT             NOT NULL,
    [NUMERO]        VARCHAR (30)    NOT NULL,
    [FECHA_RECLAMO] DATETIME        NOT NULL,
    [SALDO]         DECIMAL (15, 2) NOT NULL,
    [USRID]         INT             NOT NULL,
    [FECHA_PAGO]    DATETIME        NOT NULL,
    [TIPO_BANCO]    INT             NOT NULL,
    [ID_CUENTA]     INT             NOT NULL,
    [OBSERVACIONES] TEXT            NULL,
    [CODMON]        INT             NOT NULL,
    [CODID]         INT             NOT NULL,
    [ESTID]         INT             NOT NULL,
    [FECHA]         DATETIME        NOT NULL,
    [COMENTARIO]    TEXT            NULL,
    CONSTRAINT [PK_BAJAS_CPBT_DOC] PRIMARY KEY CLUSTERED ([ID_BAJAS] ASC, [PCLID] ASC, [CTCID] ASC, [CCBID] ASC)
);

