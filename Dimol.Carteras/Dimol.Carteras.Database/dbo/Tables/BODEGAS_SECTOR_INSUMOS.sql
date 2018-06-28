CREATE TABLE [dbo].[BODEGAS_SECTOR_INSUMOS] (
    [BSI_CODEMP]    INT             NOT NULL,
    [BSI_BODID]     INT             NOT NULL,
    [BSI_BDSID]     INT             NOT NULL,
    [BSI_INSID]     NUMERIC (15)    NOT NULL,
    [BSI_POSICION]  SMALLINT        DEFAULT ((0)) NOT NULL,
    [BSI_STOCK]     DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSI_MERMA]     DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSI_RESERVADO] DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSI_TRANSITO]  DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSI_BSCID]     VARCHAR (10)    NULL,
    CONSTRAINT [PK_BODEGAS_SECTOR_INSUMOS] PRIMARY KEY CLUSTERED ([BSI_CODEMP] ASC, [BSI_BODID] ASC, [BSI_BDSID] ASC, [BSI_INSID] ASC, [BSI_POSICION] ASC),
    CONSTRAINT [FK_BODEGAS__BODSEC_IN_BODEGAS_] FOREIGN KEY ([BSI_CODEMP], [BSI_BODID], [BSI_BDSID]) REFERENCES [dbo].[BODEGAS_SECTOR] ([BDS_CODEMP], [BDS_BODID], [BDS_BDSID]),
    CONSTRAINT [FK_BODEGAS__BODSECCUB_BODEGAS_] FOREIGN KEY ([BSI_CODEMP], [BSI_BODID], [BSI_BDSID], [BSI_BSCID]) REFERENCES [dbo].[BODEGAS_SECTOR_CUBICULO] ([BSC_CODEMP], [BSC_BODID], [BSC_BDSID], [BSC_BSCID]),
    CONSTRAINT [FK_BODEGAS__INSUMOS_S_INSUMOS] FOREIGN KEY ([BSI_CODEMP], [BSI_INSID]) REFERENCES [dbo].[INSUMOS] ([INS_CODEMP], [INS_INSID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[BODEGAS_SECTOR_INSUMOS]([BSI_CODEMP] ASC, [BSI_BODID] ASC, [BSI_BDSID] ASC, [BSI_INSID] ASC, [BSI_POSICION] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos insumos que estaran asociados a cada sector
   
   Si la, bodega es de uso avanzado se asociara un cubiculo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS_SECTOR_INSUMOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica en que posicion del cubiculo estara
   
   Si no hay cubiculo, la posicion original sera 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS_SECTOR_INSUMOS', @level2type = N'COLUMN', @level2name = N'BSI_POSICION';

