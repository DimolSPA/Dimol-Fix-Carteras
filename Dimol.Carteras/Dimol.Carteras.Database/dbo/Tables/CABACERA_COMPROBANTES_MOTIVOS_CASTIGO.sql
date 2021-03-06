﻿CREATE TABLE [dbo].[CABACERA_COMPROBANTES_MOTIVOS_CASTIGO] (
    [CBM_CODEMP] INT          NOT NULL,
    [CBM_SUCID]  INT          NOT NULL,
    [CBM_TPCID]  INT          NOT NULL,
    [CBM_NUMERO] NUMERIC (15) NOT NULL,
    [CBM_TMCID]  INT          NOT NULL,
    [CBM_CTCID]  NUMERIC (15) NOT NULL,
    CONSTRAINT [PK_CABACERA_COMPROBANTES_MOTIV] PRIMARY KEY CLUSTERED ([CBM_CODEMP] ASC, [CBM_SUCID] ASC, [CBM_TPCID] ASC, [CBM_NUMERO] ASC, [CBM_TMCID] ASC, [CBM_CTCID] ASC),
    CONSTRAINT [FK_CABACERA_CABCPBT_M_CABACERA] FOREIGN KEY ([CBM_CODEMP], [CBM_SUCID], [CBM_TPCID], [CBM_NUMERO]) REFERENCES [dbo].[CABACERA_COMPROBANTES] ([CBC_CODEMP], [CBC_SUCID], [CBC_TPCID], [CBC_NUMERO]),
    CONSTRAINT [FK_CABACERA_TIPMOT_CA_TIPOS_MO] FOREIGN KEY ([CBM_CODEMP], [CBM_TMCID]) REFERENCES [dbo].[TIPOS_MOTIVOS_CASTIGOS] ([TMC_CODEMP], [TMC_TMCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CABACERA_COMPROBANTES_MOTIVOS_CASTIGO]([CBM_CODEMP] ASC, [CBM_SUCID] ASC, [CBM_TPCID] ASC, [CBM_NUMERO] ASC, [CBM_TMCID] ASC, [CBM_CTCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos motivos, del castigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CABACERA_COMPROBANTES_MOTIVOS_CASTIGO';

