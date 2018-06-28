﻿CREATE TABLE [dbo].[INSUMO_IMAGENES] (
    [ISI_CODEMP]  INT          NOT NULL,
    [ISI_INSID]   NUMERIC (15) NOT NULL,
    [ISI_ISIID]   INT          NOT NULL,
    [ISI_DEFAULT] CHAR (1)     DEFAULT ('N') NOT NULL,
    [ISI_ORDEN]   SMALLINT     NOT NULL,
    [ISI_IMAGEN]  IMAGE        NULL,
    CONSTRAINT [PK_INSUMO_IMAGENES] PRIMARY KEY NONCLUSTERED ([ISI_CODEMP] ASC, [ISI_INSID] ASC, [ISI_ISIID] ASC),
    CONSTRAINT [CKC_ISI_DEFAULT_INSUMO_I] CHECK ([ISI_DEFAULT]='N' OR [ISI_DEFAULT]='S'),
    CONSTRAINT [FK_INSUMO_I_INSU_IMAG_INSUMOS] FOREIGN KEY ([ISI_CODEMP], [ISI_INSID]) REFERENCES [dbo].[INSUMOS] ([INS_CODEMP], [INS_INSID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[INSUMO_IMAGENES]([ISI_CODEMP] ASC, [ISI_INSID] ASC, [ISI_ISIID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las distintas imagenes para este insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSUMO_IMAGENES';
