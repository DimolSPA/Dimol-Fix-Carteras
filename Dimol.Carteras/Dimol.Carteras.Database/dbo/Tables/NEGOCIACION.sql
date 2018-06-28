CREATE TABLE [dbo].[NEGOCIACION] (
    [NEG_CODEMP] INT            NOT NULL,
    [NEG_ANIO]   SMALLINT       NOT NULL,
    [NEG_NEGID]  INT            NOT NULL,
    [NEG_USRID]  INT            NOT NULL,
    [NEG_CTCID]  NUMERIC (15)   NOT NULL,
    [NEG_FECINI] DATETIME       NOT NULL,
    [NEG_FECFIN] DATETIME       NOT NULL,
    [NEG_ESTADO] CHAR (1)       DEFAULT ('E') NOT NULL,
    [NEG_INTFUT] DECIMAL (5, 2) DEFAULT ((0)) NOT NULL,
    [NEG_DIAS]   SMALLINT       DEFAULT ((0)) NULL,
    CONSTRAINT [PK_NEGOCIACION] PRIMARY KEY CLUSTERED ([NEG_CODEMP] ASC, [NEG_ANIO] ASC, [NEG_NEGID] ASC),
    CONSTRAINT [CKC_NEG_ESTADO_NEGOCIAC] CHECK ([NEG_ESTADO]='N' OR [NEG_ESTADO]='F' OR [NEG_ESTADO]='R' OR [NEG_ESTADO]='A' OR [NEG_ESTADO]='E'),
    CONSTRAINT [FK_NEGOCIAC_DEUDORES__DEUDORES] FOREIGN KEY ([NEG_CODEMP], [NEG_CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID]),
    CONSTRAINT [FK_NEGOCIAC_EMPRESA_N_EMPRESA] FOREIGN KEY ([NEG_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_NEGOCIAC_USUARIOS__USUARIOS] FOREIGN KEY ([NEG_CODEMP], [NEG_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[NEGOCIACION]([NEG_CODEMP] ASC, [NEG_ANIO] ASC, [NEG_NEGID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos datos de la negociacion, 
   
   Se indicara el inicio y el fin de la negociacion, tambien si esta aprobada o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NEGOCIACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica los intereses futuros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NEGOCIACION', @level2type = N'COLUMN', @level2name = N'NEG_INTFUT';

