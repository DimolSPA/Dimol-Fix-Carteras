CREATE TABLE [dbo].[ROL] (
    [ROL_CODEMP]     INT             NOT NULL,
    [ROL_ROLID]      INT             NOT NULL,
    [ROL_PCLID]      NUMERIC (15)    NOT NULL,
    [ROL_CTCID]      INT             NOT NULL,
    [ROL_NUMERO]     VARCHAR (20)    NOT NULL,
    [ROL_TRBID]      INT             NOT NULL,
    [ROL_TCAID]      INT             NOT NULL,
    [ROL_ESTID]      SMALLINT        NOT NULL,
    [ROL_FECEMI]     DATETIME        NOT NULL,
    [ROL_FECDEM]     DATETIME        NULL,
    [ROL_FECROL]     DATETIME        NULL,
    [ROL_TOTAL]      DECIMAL (18, 2) NOT NULL,
    [ROL_COMENTARIO] TEXT            NULL,
    [ROL_ESJID]      INT             NULL,
    [ROL_FECJUD]     DATETIME        NULL,
    [ROL_FECULTGEST] DATETIME        NOT NULL,
    [ROL_BLOQUEO]    CHAR (1)        DEFAULT ('N') NULL,
    [ROL_PREQUIEBRA] CHAR (1)        DEFAULT ('N') NULL,
    [ROL_TIPO_ROL]   CHAR (1)        NULL,
    CONSTRAINT [PK_ROL] PRIMARY KEY CLUSTERED ([ROL_CODEMP] ASC, [ROL_ROLID] ASC),
    CONSTRAINT [FK_ROL_EMPRESA_R_EMPRESA] FOREIGN KEY ([ROL_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_ROL_MATEST_RO_MATERIA_] FOREIGN KEY ([ROL_CODEMP], [ROL_ESJID], [ROL_ESTID]) REFERENCES [dbo].[MATERIA_ESTADOS] ([MEJ_CODEMP], [MEJ_ESJID], [MEJ_ESTID]),
    CONSTRAINT [FK_ROL_TIPCAU_RO_TIPOS_CA] FOREIGN KEY ([ROL_CODEMP], [ROL_TCAID]) REFERENCES [dbo].[TIPOS_CAUSA] ([TCA_CODEMP], [TCA_TCAID]),
    CONSTRAINT [FK_ROL_TRIBUNALE_TRIBUNAL] FOREIGN KEY ([ROL_CODEMP], [ROL_TRBID]) REFERENCES [dbo].[TRIBUNALES] ([TRB_CODEMP], [TRB_TRBID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ROL]([ROL_CODEMP] ASC, [ROL_ROLID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NUMERO]
    ON [dbo].[ROL]([ROL_CODEMP] ASC, [ROL_TRBID] ASC, [ROL_NUMERO] ASC, [ROL_PCLID] ASC, [ROL_CTCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena el ROL de la demanda, incluira todo lo referente al proceso de demanda, el cliente, el demandante, el demandado, etc...
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el numero de ROL que asigna el tribunal para el proceso de demandas, en su primera version el sistema colocara el numero en negativo para poder continuar con su proceso de seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ROL', @level2type = N'COLUMN', @level2name = N'ROL_NUMERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo bloquea el rol impidiendo grabados tanto del rol o estados por niveles de permiso inferiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ROL', @level2type = N'COLUMN', @level2name = N'ROL_BLOQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el rol, esta o no en proceso de quiebra
   
   por default es No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ROL', @level2type = N'COLUMN', @level2name = N'ROL_PREQUIEBRA';

