﻿CREATE TABLE [dbo].[TIPOS_PROVCLI_IDIOMAS] (
    [TPI_CODEMP] INT          NOT NULL,
    [TPI_TPCID]  INT          NOT NULL,
    [TPI_IDID]   INT          NOT NULL,
    [TPI_NOMBRE] VARCHAR (60) NOT NULL,
    CONSTRAINT [PK_TIPOS_PROVCLI_IDIOMAS] PRIMARY KEY NONCLUSTERED ([TPI_CODEMP] ASC, [TPI_TPCID] ASC, [TPI_IDID] ASC),
    CONSTRAINT [FK_TIPOS_PR_PROVCLI_I_TIPOS_PR] FOREIGN KEY ([TPI_CODEMP], [TPI_TPCID]) REFERENCES [dbo].[TIPOS_PROVCLI] ([TPC_CODEMP], [TPC_TPCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_PROVCLI_IDIOMAS]([TPI_CODEMP] ASC, [TPI_TPCID] ASC, [TPI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos tipos de provcli en su respectivo idoma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_PROVCLI_IDIOMAS';
