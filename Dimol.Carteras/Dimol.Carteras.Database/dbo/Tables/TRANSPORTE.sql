﻿CREATE TABLE [dbo].[TRANSPORTE] (
    [TRA_CODEMP]      INT           NOT NULL,
    [TRA_TPTID]       INT           NOT NULL,
    [TRA_TRAID]       INT           NOT NULL,
    [TRA_NOMBRE]      VARCHAR (200) NOT NULL,
    [TRA_MARCA]       VARCHAR (80)  NOT NULL,
    [TRA_NUMERO]      VARCHAR (20)  NOT NULL,
    [TRA_DESCRIPCION] VARCHAR (500) NULL,
    [TRA_ESTADO]      CHAR (1)      DEFAULT ('A') NOT NULL,
    CONSTRAINT [PK_TRANSPORTE] PRIMARY KEY CLUSTERED ([TRA_CODEMP] ASC, [TRA_TPTID] ASC, [TRA_TRAID] ASC),
    CONSTRAINT [CKC_TRA_ESTADO_TRANSPOR] CHECK ([TRA_ESTADO]='I' OR [TRA_ESTADO]='A'),
    CONSTRAINT [FK_TRANSPOR_TIPTRA_TR_TIPOS_TR] FOREIGN KEY ([TRA_CODEMP], [TRA_TPTID]) REFERENCES [dbo].[TIPOS_TRANSPORTE] ([TPT_CODEMP], [TPT_TPTID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TRANSPORTE]([TRA_CODEMP] ASC, [TRA_TPTID] ASC, [TRA_TRAID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos transportes que tiene la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TRANSPORTE';

