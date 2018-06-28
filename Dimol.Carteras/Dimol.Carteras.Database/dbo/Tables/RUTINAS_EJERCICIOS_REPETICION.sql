﻿CREATE TABLE [dbo].[RUTINAS_EJERCICIOS_REPETICION] (
    [RRP_CODEMP]     INT             NOT NULL,
    [RRP_RTNID]      INT             NOT NULL,
    [RRP_EJCID]      INT             NOT NULL,
    [RRP_REPID]      SMALLINT        NOT NULL,
    [RRP_REPETICION] SMALLINT        NOT NULL,
    [RRP_PESO]       NUMERIC (15, 2) NOT NULL,
    CONSTRAINT [PK_RUTINAS_EJERCICIOS_REPETICI] PRIMARY KEY CLUSTERED ([RRP_CODEMP] ASC, [RRP_RTNID] ASC, [RRP_EJCID] ASC, [RRP_REPID] ASC),
    CONSTRAINT [FK_RUTINAS__RUTEJER_R_RUTINAS_] FOREIGN KEY ([RRP_CODEMP], [RRP_RTNID], [RRP_EJCID]) REFERENCES [dbo].[RUTINAS_EJERCICIOS] ([RTE_CODEMP], [RTE_RTNID], [RTE_EJCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[RUTINAS_EJERCICIOS_REPETICION]([RRP_CODEMP] ASC, [RRP_RTNID] ASC, [RRP_EJCID] ASC, [RRP_REPID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena, la repeticion
   
   con su respectivo peso y numero de repeticiones
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RUTINAS_EJERCICIOS_REPETICION';

