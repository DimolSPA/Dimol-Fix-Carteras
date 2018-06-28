CREATE TABLE [dbo].[DEUDORES_MAIL] (
    [DDM_CODEMP] INT          NOT NULL,
    [DDM_CTCID]  NUMERIC (15) NOT NULL,
    [DDM_MAIL]   VARCHAR (80) NOT NULL,
    [DDM_TIPO]   CHAR (1)     DEFAULT ('P') NOT NULL,
    [DDM_MASIVO] CHAR (1)     DEFAULT ('S') NULL,
    CONSTRAINT [PK_DEUDORES_MAIL] PRIMARY KEY CLUSTERED ([DDM_CODEMP] ASC, [DDM_CTCID] ASC, [DDM_MAIL] ASC),
    CONSTRAINT [CKC_DDM_TIPO_DEUDORES] CHECK ([DDM_TIPO]='E' OR [DDM_TIPO]='P'),
    CONSTRAINT [FK_DEUDORES_DEUDORE_M_DEUDORES] FOREIGN KEY ([DDM_CODEMP], [DDM_CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DEUDORES_MAIL]([DDM_CODEMP] ASC, [DDM_CTCID] ASC, [DDM_MAIL] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica que tipo de mail es si, personal o empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_MAIL', @level2type = N'COLUMN', @level2name = N'DDM_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si se utilizara o no para envio de mail masivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_MAIL', @level2type = N'COLUMN', @level2name = N'DDM_MASIVO';

