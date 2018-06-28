CREATE TABLE [dbo].[CLIENTES_ESTADOS] (
    [CODEMP] INT      NOT NULL,
    [PCLID]  INT      NOT NULL,
    [ESTID]  SMALLINT NOT NULL,
    CONSTRAINT [PK_CLIENTES_ESTADOS] PRIMARY KEY CLUSTERED ([CODEMP] ASC, [PCLID] ASC, [ESTID] ASC),
    CONSTRAINT [FK_CLIENTES_ESTADOS_P_ESTADOS_] FOREIGN KEY ([CODEMP], [ESTID]) REFERENCES [dbo].[ESTADOS_CARTERA] ([ECT_CODEMP], [ECT_ESTID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esta tabla almacena los distintos estados de cobranza, que pueden ser permitido en el cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CLIENTES_ESTADOS';

