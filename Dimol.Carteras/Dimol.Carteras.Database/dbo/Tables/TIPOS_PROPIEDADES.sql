﻿CREATE TABLE [dbo].[TIPOS_PROPIEDADES] (
    [TPP_CODEMP] INT           NOT NULL,
    [TPP_TPPID]  INT           NOT NULL,
    [TPP_NOMBRE] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TIPOS_PROPIEDADES] PRIMARY KEY CLUSTERED ([TPP_CODEMP] ASC, [TPP_TPPID] ASC),
    CONSTRAINT [FK_TIPOS_PR_EMP_TIPPR_EMPRESA] FOREIGN KEY ([TPP_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_PROPIEDADES]([TPP_CODEMP] ASC, [TPP_TPPID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOM]
    ON [dbo].[TIPOS_PROPIEDADES]([TPP_CODEMP] ASC, [TPP_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos tipos de propiedades
   
   ya sea:
   
   depto, casa, etc', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_PROPIEDADES';
