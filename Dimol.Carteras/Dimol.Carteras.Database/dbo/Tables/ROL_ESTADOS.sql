﻿CREATE TABLE [dbo].[ROL_ESTADOS] (
    [RLE_CODEMP]     INT          NOT NULL,
    [RLE_ROLID]      INT          NOT NULL,
    [RLE_ESTID]      SMALLINT     NOT NULL,
    [RLE_ESJID]      INT          NOT NULL,
    [RLE_FECHA]      DATETIME     NOT NULL,
    [RLE_USRID]      INT          NOT NULL,
    [RLE_IPRED]      VARCHAR (30) NOT NULL,
    [RLE_IPMAQUINA]  VARCHAR (30) NOT NULL,
    [RLE_COMENTARIO] TEXT         NOT NULL,
    [RLE_FECJUD]     DATETIME     NOT NULL,
    CONSTRAINT [PK_ROL_ESTADOS] PRIMARY KEY CLUSTERED ([RLE_CODEMP] ASC, [RLE_ROLID] ASC, [RLE_ESTID] ASC, [RLE_ESJID] ASC, [RLE_FECHA] ASC),
    CONSTRAINT [FK_ROL_ESTA_MATEST_RO_MATERIA_] FOREIGN KEY ([RLE_CODEMP], [RLE_ESJID], [RLE_ESTID]) REFERENCES [dbo].[MATERIA_ESTADOS] ([MEJ_CODEMP], [MEJ_ESJID], [MEJ_ESTID]),
    CONSTRAINT [FK_ROL_ESTA_ROL_ESTAD_ROL] FOREIGN KEY ([RLE_CODEMP], [RLE_ROLID]) REFERENCES [dbo].[ROL] ([ROL_CODEMP], [ROL_ROLID]),
    CONSTRAINT [FK_ROL_ESTA_USUARIOS__USUARIOS] FOREIGN KEY ([RLE_CODEMP], [RLE_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ROL_ESTADOS]([RLE_CODEMP] ASC, [RLE_ROLID] ASC, [RLE_ESTID] ASC, [RLE_ESJID] ASC, [RLE_FECHA] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_FECJUDEST]
    ON [dbo].[ROL_ESTADOS]([RLE_CODEMP] ASC, [RLE_ROLID] ASC, [RLE_ESTID] ASC, [RLE_ESJID] ASC, [RLE_FECJUD] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos estados que han ido sucediendo por cada uno de los estados del ROL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ROL_ESTADOS';
