CREATE TABLE [dbo].[PROVCLI_EMBALAJE] (
    [PCE_CODEMP] INT          NOT NULL,
    [PCE_PCLID]  NUMERIC (15) NOT NULL,
    [PCE_ULTNUM] NUMERIC (15) NOT NULL,
    CONSTRAINT [PK_PROVCLI_EMBALAJE] PRIMARY KEY CLUSTERED ([PCE_CODEMP] ASC, [PCE_PCLID] ASC),
    CONSTRAINT [FK_PROVCLI__PROVCLI_E_PROVCLI] FOREIGN KEY ([PCE_CODEMP], [PCE_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PROVCLI_EMBALAJE]([PCE_CODEMP] ASC, [PCE_PCLID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena, el ultimo numero de la caja que fue enviada al cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI_EMBALAJE';

