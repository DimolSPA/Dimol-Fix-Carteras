﻿CREATE TABLE [dbo].[SOCIOS_EJERCICIOS] (
    [SOE_CODEMP]  INT         NOT NULL,
    [SOE_SOCID]   INT         NOT NULL,
    [SOE_EJCID]   INT         NOT NULL,
    [SOE_TIEMPO]  SMALLINT    NULL,
    [SOE_DIA1]    NUMERIC (1) DEFAULT ((0)) NOT NULL,
    [SOE_DIA2]    NUMERIC (1) DEFAULT ((0)) NOT NULL,
    [SOE_DIA3]    NUMERIC (1) DEFAULT ((0)) NOT NULL,
    [SOE_DIA4]    NUMERIC (1) DEFAULT ((0)) NOT NULL,
    [SOE_DIA5]    NUMERIC (1) DEFAULT ((0)) NOT NULL,
    [SOE_DIA6]    NUMERIC (1) DEFAULT ((0)) NOT NULL,
    [SOE_DIA7]    NUMERIC (1) DEFAULT ((0)) NOT NULL,
    [SOE_ORDEN]   SMALLINT    DEFAULT ((5)) NOT NULL,
    [SOE_CANTREP] SMALLINT    NULL,
    CONSTRAINT [FK_SOCIOS_E_EJER_SOCI_EJERCICI] FOREIGN KEY ([SOE_CODEMP], [SOE_EJCID]) REFERENCES [dbo].[EJERCICIOS] ([EJC_CODEMP], [EJC_EJCID]),
    CONSTRAINT [FK_SOCIOS_E_SOC_EJERC_SOCIOS] FOREIGN KEY ([SOE_CODEMP], [SOE_SOCID]) REFERENCES [dbo].[SOCIOS] ([SOC_CODEMP], [SOC_SOCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[SOCIOS_EJERCICIOS]([SOE_CODEMP] ASC, [SOE_SOCID] ASC, [SOE_EJCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos, ejercicios que realizara cada socio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOCIOS_EJERCICIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el orden de los ejercicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOCIOS_EJERCICIOS', @level2type = N'COLUMN', @level2name = N'SOE_ORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la cantidad de repeticiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOCIOS_EJERCICIOS', @level2type = N'COLUMN', @level2name = N'SOE_CANTREP';

