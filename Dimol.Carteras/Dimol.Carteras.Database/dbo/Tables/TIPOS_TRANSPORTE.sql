CREATE TABLE [dbo].[TIPOS_TRANSPORTE] (
    [TPT_CODEMP] INT           NOT NULL,
    [TPT_TPTID]  INT           NOT NULL,
    [TPT_NOMBRE] VARCHAR (150) NOT NULL,
    [TPT_TIPO]   SMALLINT      DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TIPOS_TRANSPORTE] PRIMARY KEY CLUSTERED ([TPT_CODEMP] ASC, [TPT_TPTID] ASC),
    CONSTRAINT [CKC_TPT_TIPO_TIPOS_TR] CHECK ([TPT_TIPO]=(3) OR [TPT_TIPO]=(2) OR [TPT_TIPO]=(1)),
    CONSTRAINT [FK_TIPOS_TR_EMP_TIPTR_EMPRESA] FOREIGN KEY ([TPT_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_TRANSPORTE]([TPT_CODEMP] ASC, [TPT_TPTID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TIPOS_TRANSPORTE]([TPT_CODEMP] ASC, [TPT_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos tipos de transportes definidos para cada despacho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_TRANSPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el transporte es aereo, terrestre o maritimo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_TRANSPORTE', @level2type = N'COLUMN', @level2name = N'TPT_TIPO';

