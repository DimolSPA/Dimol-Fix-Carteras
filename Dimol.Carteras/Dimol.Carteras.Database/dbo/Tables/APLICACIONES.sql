CREATE TABLE [dbo].[APLICACIONES] (
    [APL_CODEMP] INT          NOT NULL,
    [APL_SUCID]  INT          NOT NULL,
    [APL_ANIO]   INT          NOT NULL,
    [APL_MES]    NUMERIC (2)  NOT NULL,
    [APL_NUMAPL] NUMERIC (15) NOT NULL,
    [APL_TIPO]   SMALLINT     DEFAULT ((1)) NOT NULL,
    [APL_FECING] DATETIME     NOT NULL,
    [APL_FECAPL] DATETIME     NOT NULL,
    [APL_ACCION] SMALLINT     DEFAULT ((-1)) NOT NULL,
    [APL_USRID]  INT          NOT NULL,
    CONSTRAINT [PK_APLICACIONES] PRIMARY KEY NONCLUSTERED ([APL_CODEMP] ASC, [APL_SUCID] ASC, [APL_ANIO] ASC, [APL_NUMAPL] ASC),
    CONSTRAINT [CKC_APL_ACCION_APLICACI] CHECK ([APL_ACCION]=(1) OR [APL_ACCION]=(-1)),
    CONSTRAINT [CKC_APL_TIPO_APLICACI] CHECK ([APL_TIPO]=(5) OR [APL_TIPO]=(4) OR [APL_TIPO]=(3) OR [APL_TIPO]=(2) OR [APL_TIPO]=(1)),
    CONSTRAINT [FK_APLICACI_EMPSUC_AP_EMPRESA_] FOREIGN KEY ([APL_CODEMP], [APL_SUCID]) REFERENCES [dbo].[EMPRESA_SUCURSAL] ([ESU_CODEMP], [ESU_SUCID]),
    CONSTRAINT [FK_APLICACI_USU__USUARIOS] FOREIGN KEY ([APL_CODEMP], [APL_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[APLICACIONES]([APL_CODEMP] ASC, [APL_SUCID] ASC, [APL_ANIO] ASC, [APL_NUMAPL] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena todas las aplicaciones, de los documentos ejemplo:
   
   Doc. Diarios     -> Comprobantes
   Doc. Diarios     -> Doc. Diarios
   Comprobantes  -> Comprobantes
   
   Pero solamente su cabecera, tambien indicara si fue una aplicacion o una desaplicacion
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'APLICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el año de la aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'APLICACIONES', @level2type = N'COLUMN', @level2name = N'APL_ANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica en que mes se realizo la aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'APLICACIONES', @level2type = N'COLUMN', @level2name = N'APL_MES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, que tipo de aplicacion se realiza
   
   Ejemplo:
   
   1. - Doc. Diarios     -> Comprobantes
   2. - Doc. Diarios     -> Doc. Diarios
   3.-  Comprobantes  -> Comprobantes
   4.- Doc. Diarios      -> Cartera
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'APLICACIONES', @level2type = N'COLUMN', @level2name = N'APL_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la accion que se lleva acabo, ejemplo
   
   -1 - Aplicacion
   1 - Desaplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'APLICACIONES', @level2type = N'COLUMN', @level2name = N'APL_ACCION';

