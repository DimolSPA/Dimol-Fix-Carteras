CREATE TABLE [dbo].[PROVCLI_MANDATO] (
    [PCM_CODEMP]     INT          NOT NULL,
    [PCM_PCLID]      NUMERIC (15) NOT NULL,
    [PCM_NOTID]      INT          NOT NULL,
    [PCM_NUMREP]     VARCHAR (15) NOT NULL,
    [PCM_INDEFINIDO] CHAR (1)     DEFAULT ('N') NOT NULL,
    [PCM_FECVENC]    DATETIME     NULL,
    [PCM_FECASIG]    DATETIME     NULL,
    CONSTRAINT [PK_PROVCLI_MANDATO] PRIMARY KEY CLUSTERED ([PCM_CODEMP] ASC, [PCM_PCLID] ASC, [PCM_NOTID] ASC, [PCM_NUMREP] ASC),
    CONSTRAINT [CKC_PCM_INDEFINIDO_PROVCLI_] CHECK ([PCM_INDEFINIDO]='N' OR [PCM_INDEFINIDO]='S'),
    CONSTRAINT [FK_PROVCLI__NOTARIA_M_NOTARIAS] FOREIGN KEY ([PCM_CODEMP], [PCM_NOTID]) REFERENCES [dbo].[NOTARIAS] ([NOT_CODEMP], [NOT_NOTID]),
    CONSTRAINT [FK_PROVCLI__PROVCLI_M_PROVCLI] FOREIGN KEY ([PCM_CODEMP], [PCM_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PROVCLI_MANDATO]([PCM_CODEMP] ASC, [PCM_PCLID] ASC, [PCM_NOTID] ASC, [PCM_NUMREP] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos mandatos que entrega el cliente para realizar las demandas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI_MANDATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el mandato tiene fecha de caducidad, si es si se debe ingresdar la fecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI_MANDATO', @level2type = N'COLUMN', @level2name = N'PCM_INDEFINIDO';

