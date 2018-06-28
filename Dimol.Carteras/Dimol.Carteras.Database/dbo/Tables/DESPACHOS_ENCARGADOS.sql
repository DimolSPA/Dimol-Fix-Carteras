CREATE TABLE [dbo].[DESPACHOS_ENCARGADOS] (
    [DPE_CODEMP] INT           NOT NULL,
    [DPE_SUCID]  INT           NOT NULL,
    [DPE_DPCID]  NUMERIC (15)  NOT NULL,
    [DPE_RUT]    VARCHAR (20)  NOT NULL,
    [DPE_NOMBRE] VARCHAR (200) NOT NULL,
    [DPE_EMPLID] INT           NULL,
    [DPE_NIVEL]  SMALLINT      NOT NULL,
    CONSTRAINT [PK_DESPACHOS_ENCARGADOS] PRIMARY KEY CLUSTERED ([DPE_CODEMP] ASC, [DPE_SUCID] ASC, [DPE_DPCID] ASC, [DPE_RUT] ASC),
    CONSTRAINT [CKC_DPE_NIVEL_DESPACHO] CHECK ([DPE_NIVEL]=(8) OR [DPE_NIVEL]=(7) OR [DPE_NIVEL]=(6) OR [DPE_NIVEL]=(5) OR [DPE_NIVEL]=(4) OR [DPE_NIVEL]=(3) OR [DPE_NIVEL]=(2) OR [DPE_NIVEL]=(1)),
    CONSTRAINT [FK_DESPACHO_DESP_ENCA_DESPACHO] FOREIGN KEY ([DPE_CODEMP], [DPE_SUCID], [DPE_DPCID]) REFERENCES [dbo].[DESPACHOS] ([DPC_CODEMP], [DPC_SUCID], [DPC_DPCID]),
    CONSTRAINT [FK_DESPACHO_EMPLEADOS_EMPLEADO] FOREIGN KEY ([DPE_CODEMP], [DPE_EMPLID]) REFERENCES [dbo].[EMPLEADOS] ([EPL_CODEMP], [EPL_EMPLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DESPACHOS_ENCARGADOS]([DPE_CODEMP] ASC, [DPE_SUCID] ASC, [DPE_DPCID] ASC, [DPE_RUT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las personas que seran las encargadas del despachos
   
   indicando, el controlador del despacho, el que despacha, chofer, peonetas, etc....', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS_ENCARGADOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, el nivel de responsabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS_ENCARGADOS', @level2type = N'COLUMN', @level2name = N'DPE_NIVEL';

