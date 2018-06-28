CREATE TABLE [dbo].[BODEGAS_SECTOR_PRODUCTOS] (
    [BSP_CODEMP]    INT             NOT NULL,
    [BSP_BODID]     INT             NOT NULL,
    [BSP_BDSID]     INT             NOT NULL,
    [BSP_PRODID]    NUMERIC (15)    NOT NULL,
    [BSP_POSICION]  SMALLINT        NOT NULL,
    [BSP_STOCK]     DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSP_MERMA]     DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSP_RESERVADO] DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSP_TRANSITO]  DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSP_BSCID]     VARCHAR (10)    NULL,
    CONSTRAINT [PK_BODEGAS_SECTOR_PRODUCTOS] PRIMARY KEY CLUSTERED ([BSP_CODEMP] ASC, [BSP_BODID] ASC, [BSP_BDSID] ASC, [BSP_PRODID] ASC, [BSP_POSICION] ASC),
    CONSTRAINT [FK_BODEGAS__BODSEC_PR_BODEGAS_] FOREIGN KEY ([BSP_CODEMP], [BSP_BODID], [BSP_BDSID]) REFERENCES [dbo].[BODEGAS_SECTOR] ([BDS_CODEMP], [BDS_BODID], [BDS_BDSID]),
    CONSTRAINT [FK_BODEGAS__BODSECTOR_BODEGAS_] FOREIGN KEY ([BSP_CODEMP], [BSP_BODID], [BSP_BDSID], [BSP_BSCID]) REFERENCES [dbo].[BODEGAS_SECTOR_CUBICULO] ([BSC_CODEMP], [BSC_BODID], [BSC_BDSID], [BSC_BSCID]),
    CONSTRAINT [FK_BODEGAS__PRODUCTOS_PRODUCTO] FOREIGN KEY ([BSP_CODEMP], [BSP_PRODID]) REFERENCES [dbo].[PRODUCTOS] ([PDT_CODEMP], [PDT_PRODID])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[BODEGAS_SECTOR_PRODUCTOS]([BSP_CODEMP] ASC, [BSP_BODID] ASC, [BSP_BDSID] ASC, [BSP_PRODID] ASC, [BSP_POSICION] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los stock de cada producto dependiendo del sector, bodega y cubiculo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS_SECTOR_PRODUCTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, la posicion que estara en el cubiculo
   
   Si no hay cubiculo, la posicion original sera 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS_SECTOR_PRODUCTOS', @level2type = N'COLUMN', @level2name = N'BSP_POSICION';

