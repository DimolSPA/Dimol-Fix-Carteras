CREATE TABLE [dbo].[REPORTES] (
    [RPT_TRVID]   INT           NOT NULL,
    [RPT_RPTID]   SMALLINT      NOT NULL,
    [RPT_NOMBRE]  VARCHAR (200) NOT NULL,
    [RPT_TIPO]    CHAR (1)      DEFAULT ('C') NOT NULL,
    [RPT_CARTERA] CHAR (1)      DEFAULT ('N') NULL,
    [RPT_TIPCART] SMALLINT      DEFAULT ((0)) NULL,
    CONSTRAINT [PK_REPORTES] PRIMARY KEY CLUSTERED ([RPT_TRVID] ASC, [RPT_RPTID] ASC),
    CONSTRAINT [CKC_RPT_CARTERA_REPORTES] CHECK ([RPT_CARTERA] IS NULL OR ([RPT_CARTERA]='N' OR [RPT_CARTERA]='S')),
    CONSTRAINT [CKC_RPT_TIPCART_REPORTES] CHECK ([RPT_TIPCART] IS NULL OR ([RPT_TIPCART]=(5) OR [RPT_TIPCART]=(4) OR [RPT_TIPCART]=(3) OR [RPT_TIPCART]=(2) OR [RPT_TIPCART]=(1) OR [RPT_TIPCART]=(0))),
    CONSTRAINT [CKC_RPT_TIPO_REPORTES] CHECK ([RPT_TIPO]='O' OR [RPT_TIPO]='W' OR [RPT_TIPO]='C'),
    CONSTRAINT [FK_REPORTES_REPORTE_T_TREEVIEW] FOREIGN KEY ([RPT_TRVID]) REFERENCES [dbo].[TREEVIEW] ([TRV_TRVID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[REPORTES]([RPT_TRVID] ASC, [RPT_RPTID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOM]
    ON [dbo].[REPORTES]([RPT_TRVID] ASC, [RPT_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos reportes, que se utilizaran en cada ventana tambien el tipo e reporte
   
   estos reportes seran standar para cada empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'REPORTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el reporte sera utilizado para las cartera de clientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'REPORTES', @level2type = N'COLUMN', @level2name = N'RPT_CARTERA';

