﻿CREATE TABLE [dbo].[FORMAS_PAGO_IDIOMAS] (
    [FPI_CODEMP] INT           NOT NULL,
    [FPI_FRPID]  INT           NOT NULL,
    [FPI_IDID]   INT           NOT NULL,
    [FPI_NOMBRE] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_FORMAS_PAGO_IDIOMAS] PRIMARY KEY NONCLUSTERED ([FPI_CODEMP] ASC, [FPI_FRPID] ASC, [FPI_IDID] ASC),
    CONSTRAINT [FK_FORMAS_P_FORMPAG_I_FORMAS_P] FOREIGN KEY ([FPI_CODEMP], [FPI_FRPID]) REFERENCES [dbo].[FORMAS_PAGO] ([FRP_CODEMP], [FRP_FRPID]),
    CONSTRAINT [FK_FORMAS_P_IDIOMAS_F_IDIOMAS] FOREIGN KEY ([FPI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[FORMAS_PAGO_IDIOMAS]([FPI_CODEMP] ASC, [FPI_FRPID] ASC, [FPI_IDID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos formas de pago en su respectibo idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMAS_PAGO_IDIOMAS';

