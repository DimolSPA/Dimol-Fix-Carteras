CREATE TABLE [dbo].[DEUDORES_CONTACTOS_MAIL] (
    [DCM_CODEMP] INT          NOT NULL,
    [DCM_CTCID]  NUMERIC (15) NOT NULL,
    [DCM_DDCID]  SMALLINT     NOT NULL,
    [DCM_MAIL]   VARCHAR (80) NOT NULL,
    [DCM_TIPO]   CHAR (1)     DEFAULT ('P') NOT NULL,
    [DCM_MASIVO] CHAR (1)     DEFAULT ('S') NULL,
    CONSTRAINT [PK_DEUDORES_CONTACTOS_MAIL] PRIMARY KEY CLUSTERED ([DCM_CODEMP] ASC, [DCM_CTCID] ASC, [DCM_DDCID] ASC, [DCM_MAIL] ASC),
    CONSTRAINT [CKC_DCM_TIPO_DEUDORES] CHECK ([DCM_TIPO]='E' OR [DCM_TIPO]='P'),
    CONSTRAINT [FK_DEUDORES_DEUCONT_M_DEUDORES] FOREIGN KEY ([DCM_CODEMP], [DCM_CTCID], [DCM_DDCID]) REFERENCES [dbo].[DEUDORES_CONTACTOS] ([DDC_CODEMP], [DDC_CTCID], [DDC_DDCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DEUDORES_CONTACTOS_MAIL]([DCM_CODEMP] ASC, [DCM_CTCID] ASC, [DCM_DDCID] ASC, [DCM_MAIL] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos, mail para cada contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_CONTACTOS_MAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica que tipo de mail es si, personal o empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_CONTACTOS_MAIL', @level2type = N'COLUMN', @level2name = N'DCM_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el mail sera utilizado para mail masivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_CONTACTOS_MAIL', @level2type = N'COLUMN', @level2name = N'DCM_MASIVO';

