CREATE TABLE [dbo].[GARANTIA_PROPIEDAD] (
    [GTP_CODEMP]    INT           NOT NULL,
    [GTP_GRTID]     INT           NOT NULL,
    [GTP_ROL]       VARCHAR (100) NOT NULL,
    [GTP_TPPID]     INT           NOT NULL,
    [GTP_COMID]     INT           NOT NULL,
    [GTP_DIRECCION] VARCHAR (300) NOT NULL,
    [GTP_INSFOJ]    VARCHAR (100) NULL,
    [GTP_INSNRO]    VARCHAR (100) NULL,
    [GTP_INSANIO]   SMALLINT      NULL,
    [GTP_HIPFOJ]    VARCHAR (100) NULL,
    [GTP_HIPNRO]    VARCHAR (100) NULL,
    [GTP_HIPANIO]   SMALLINT      NULL,
    [GTP_PROFOJ]    VARCHAR (100) NULL,
    [GTP_PRONRO]    VARCHAR (100) NULL,
    [GTP_PROANIO]   SMALLINT      NULL,
    [GTP_CERTDESDE] DATETIME      NULL,
    [GTP_CERTHASTA] DATETIME      NULL,
    CONSTRAINT [PK_GARANTIA_PROPIEDAD] PRIMARY KEY CLUSTERED ([GTP_CODEMP] ASC, [GTP_GRTID] ASC, [GTP_ROL] ASC),
    CONSTRAINT [FK_GARANTIA_GAR_PROPI_GARANTIA] FOREIGN KEY ([GTP_CODEMP], [GTP_GRTID]) REFERENCES [dbo].[GARANTIA] ([GRT_CODEMP], [GRT_GRTID]),
    CONSTRAINT [FK_GARANTIA_REFERENCE_COMUNA] FOREIGN KEY ([GTP_COMID]) REFERENCES [dbo].[COMUNA] ([COM_COMID]),
    CONSTRAINT [FK_GARANTIA_TIPPROP_G_TIPOS_PR] FOREIGN KEY ([GTP_CODEMP], [GTP_TPPID]) REFERENCES [dbo].[TIPOS_PROPIEDADES] ([TPP_CODEMP], [TPP_TPPID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[GARANTIA_PROPIEDAD]([GTP_CODEMP] ASC, [GTP_GRTID] ASC, [GTP_ROL] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla alamcena las distintas garantias por propiedades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GARANTIA_PROPIEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el ceertificado de dominio vigente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GARANTIA_PROPIEDAD', @level2type = N'COLUMN', @level2name = N'GTP_CERTDESDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica el ceertificado de dominio vigente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GARANTIA_PROPIEDAD', @level2type = N'COLUMN', @level2name = N'GTP_CERTHASTA';

