CREATE TABLE [dbo].[DESPACHOS_DOCUMENTOS] (
    [DCD_CODEMP]   INT           NOT NULL,
    [DCD_SUCID]    INT           NOT NULL,
    [DCD_DPCID]    NUMERIC (15)  NOT NULL,
    [DCD_TPCID]    INT           NOT NULL,
    [DCD_NUMERO]   NUMERIC (15)  NOT NULL,
    [DCD_EDPID]    INT           NOT NULL,
    [DCD_FECDESP]  DATETIME      NOT NULL,
    [DCD_FECENT]   DATETIME      NOT NULL,
    [DCD_RECIBIDO] VARCHAR (200) NULL,
    CONSTRAINT [PK_DESPACHOS_DOCUMENTOS] PRIMARY KEY CLUSTERED ([DCD_CODEMP] ASC, [DCD_SUCID] ASC, [DCD_DPCID] ASC, [DCD_TPCID] ASC, [DCD_NUMERO] ASC),
    CONSTRAINT [FK_DESPACHO_CABCPBT_D_CABACERA] FOREIGN KEY ([DCD_CODEMP], [DCD_SUCID], [DCD_TPCID], [DCD_NUMERO]) REFERENCES [dbo].[CABACERA_COMPROBANTES] ([CBC_CODEMP], [CBC_SUCID], [CBC_TPCID], [CBC_NUMERO]),
    CONSTRAINT [FK_DESPACHO_DESPA_DOC_DESPACHO] FOREIGN KEY ([DCD_CODEMP], [DCD_SUCID], [DCD_DPCID]) REFERENCES [dbo].[DESPACHOS] ([DPC_CODEMP], [DPC_SUCID], [DPC_DPCID]),
    CONSTRAINT [FK_DESPACHO_ESTDSP_DO_ESTADOS_] FOREIGN KEY ([DCD_CODEMP], [DCD_EDPID]) REFERENCES [dbo].[ESTADOS_DESPACHOS] ([EDP_CODEMP], [EDP_EDPID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DESPACHOS_DOCUMENTOS]([DCD_CODEMP] ASC, [DCD_SUCID] ASC, [DCD_DPCID] ASC, [DCD_TPCID] ASC, [DCD_NUMERO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos documentos que seran despachados...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS_DOCUMENTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la persona que termino de recibir el documento, solo se habilitara en el estado final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DESPACHOS_DOCUMENTOS', @level2type = N'COLUMN', @level2name = N'DCD_RECIBIDO';

