CREATE TABLE [dbo].[EJERCICIOS] (
    [EJC_CODEMP]     INT           NOT NULL,
    [EJC_EJCID]      INT           NOT NULL,
    [EJC_TEJID]      INT           NOT NULL,
    [EJC_NOMBRE]     VARCHAR (200) NOT NULL,
    [EJC_TIEMPO]     SMALLINT      DEFAULT ((0)) NOT NULL,
    [EJC_REPETICION] SMALLINT      DEFAULT ((0)) NOT NULL,
    [EJC_IMAGEN1]    IMAGE         NULL,
    [EJC_IMAGEN2]    IMAGE         NULL,
    [EJC_IMAGEN3]    IMAGE         NULL,
    CONSTRAINT [PK_EJERCICIOS] PRIMARY KEY CLUSTERED ([EJC_CODEMP] ASC, [EJC_EJCID] ASC),
    CONSTRAINT [FK_EJERCICI_REFERENCE_EMPRESA] FOREIGN KEY ([EJC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_EJERCICI_TIPEJER_E_TIPOS_EJ] FOREIGN KEY ([EJC_CODEMP], [EJC_TEJID]) REFERENCES [dbo].[TIPOS_EJERCICIOS] ([TEJ_CODEMP], [TEJ_TEJID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[EJERCICIOS]([EJC_CODEMP] ASC, [EJC_EJCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos ejercicios, la duracion y la repeticion de la misma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EJERCICIOS';

