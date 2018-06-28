CREATE TABLE [dbo].[GESTOR_RESTRICCION_NULA] (
    [GRN_CODEMP] INT      NOT NULL,
    [GRN_USRID]  INT      NOT NULL,
    [GRN_SUCID]  INT      NOT NULL,
    [GRN_GESID]  INT      NOT NULL,
    [GRN_DESDE]  DATETIME NOT NULL,
    [GRN_HASTA]  DATETIME NOT NULL,
    CONSTRAINT [PK_GESTOR_RESTRICCION_NULA] PRIMARY KEY CLUSTERED ([GRN_CODEMP] ASC, [GRN_USRID] ASC, [GRN_SUCID] ASC, [GRN_GESID] ASC),
    CONSTRAINT [FK_GESTOR_R_GESTOR_RE_GESTOR] FOREIGN KEY ([GRN_CODEMP], [GRN_SUCID], [GRN_GESID]) REFERENCES [dbo].[GESTOR] ([GES_CODEMP], [GES_SUCID], [GES_GESID]),
    CONSTRAINT [FK_GESTOR_R_USUARIOS__USUARIOS] FOREIGN KEY ([GRN_CODEMP], [GRN_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[GESTOR_RESTRICCION_NULA]([GRN_CODEMP] ASC, [GRN_USRID] ASC, [GRN_SUCID] ASC, [GRN_GESID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los gestores que podran ver las carteras de los otros en caso que el supervisor o algun usuario avanzado, de el permiso desde cuando comienza hasta cuando termina la anulacion de la restriccion.
   
   Siempre y cuando el gestor pertenezca a un grupo...
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GESTOR_RESTRICCION_NULA';

