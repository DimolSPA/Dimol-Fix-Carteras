﻿CREATE TABLE [dbo].[ESTADOS_CARTERA_IDIOMAS] (
    [ECI_CODEMP] INT           NOT NULL,
    [ECI_ESTID]  SMALLINT      NOT NULL,
    [ECI_IDID]   INT           NOT NULL,
    [ECI_NOMBRE] VARCHAR (350) NOT NULL,
    CONSTRAINT [PK_ESTADOS_CARTERA_IDIOMAS] PRIMARY KEY NONCLUSTERED ([ECI_CODEMP] ASC, [ECI_ESTID] ASC, [ECI_IDID] ASC),
    CONSTRAINT [FK_ESTADOS__ESTCART_I_ESTADOS_] FOREIGN KEY ([ECI_CODEMP], [ECI_ESTID]) REFERENCES [dbo].[ESTADOS_CARTERA] ([ECT_CODEMP], [ECT_ESTID]),
    CONSTRAINT [FK_ESTADOS__IDIO_ESTC_IDIOMAS] FOREIGN KEY ([ECI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ESTADOS_CARTERA_IDIOMAS]([ECI_ESTID] ASC, [ECI_CODEMP] ASC, [ECI_IDID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[ESTADOS_CARTERA_IDIOMAS]([ECI_CODEMP] ASC, [ECI_IDID] ASC, [ECI_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena el estado de las carteras en su respectivo idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESTADOS_CARTERA_IDIOMAS';

