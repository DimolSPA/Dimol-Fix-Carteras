CREATE TABLE [dbo].[CONTRATOS_CARTERA] (
    [CCT_CODEMP] INT           NOT NULL,
    [CCT_CCTID]  INT           NOT NULL,
    [CCT_NOMBRE] VARCHAR (200) NOT NULL,
    [CCT_TIPO]   SMALLINT      DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CONTRATOS_CARTERA] PRIMARY KEY CLUSTERED ([CCT_CODEMP] ASC, [CCT_CCTID] ASC),
    CONSTRAINT [CKC_CCT_TIPO_CONTRATO] CHECK ([CCT_TIPO]=(4) OR [CCT_TIPO]=(3) OR [CCT_TIPO]=(2) OR [CCT_TIPO]=(1)),
    CONSTRAINT [FK_CONTRATO_EMPRESA_C_EMPRESA] FOREIGN KEY ([CCT_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CONTRATOS_CARTERA]([CCT_CODEMP] ASC, [CCT_CCTID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos contratos que utilizara el sistema para poder realizar los calculos de distintas partes de la cartera.
   
   ej.
   
   Intereses, honorarios, remesas, etc, pago directo....', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONTRATOS_CARTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica para que tipo de cartera sera utilizado el contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONTRATOS_CARTERA', @level2type = N'COLUMN', @level2name = N'CCT_TIPO';

