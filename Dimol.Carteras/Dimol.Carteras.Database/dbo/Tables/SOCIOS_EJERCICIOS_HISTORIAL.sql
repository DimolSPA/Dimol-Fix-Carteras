﻿CREATE TABLE [dbo].[SOCIOS_EJERCICIOS_HISTORIAL] (
    [SEH_CODEMP]     INT      NOT NULL,
    [SEH_SOCID]      INT      NOT NULL,
    [SEH_EJCID]      INT      NOT NULL,
    [SEH_FECHA]      DATETIME NOT NULL,
    [SEH_TIEMPO]     SMALLINT NULL,
    [SEH_REPETICION] SMALLINT NULL,
    [SEH_PESO]       SMALLINT NULL,
    [SEH_REPID]      SMALLINT NULL,
    CONSTRAINT [PK_SOCIOS_EJERCICIOS_HISTORIAL] PRIMARY KEY CLUSTERED ([SEH_CODEMP] ASC, [SEH_SOCID] ASC, [SEH_EJCID] ASC, [SEH_FECHA] ASC),
    CONSTRAINT [FK_SOCIOS_E_EJER_SOCH_EJERCICI] FOREIGN KEY ([SEH_CODEMP], [SEH_EJCID]) REFERENCES [dbo].[EJERCICIOS] ([EJC_CODEMP], [EJC_EJCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[SOCIOS_EJERCICIOS_HISTORIAL]([SEH_CODEMP] ASC, [SEH_SOCID] ASC, [SEH_EJCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas repeticiones que va realizando cada socio o su avance', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOCIOS_EJERCICIOS_HISTORIAL';

