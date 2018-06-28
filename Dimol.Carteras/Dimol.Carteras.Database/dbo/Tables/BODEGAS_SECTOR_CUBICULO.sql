﻿CREATE TABLE [dbo].[BODEGAS_SECTOR_CUBICULO] (
    [BSC_CODEMP]     INT             NOT NULL,
    [BSC_BODID]      INT             NOT NULL,
    [BSC_BDSID]      INT             NOT NULL,
    [BSC_BSCID]      VARCHAR (10)    NOT NULL,
    [BSC_CUBICAJE]   DECIMAL (15, 2) NOT NULL,
    [BSC_POSICIONES] SMALLINT        NOT NULL,
    [BSC_STOCK]      DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSC_MERMA]      DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSC_RESERVADO]  DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSC_TRANSITO]   DECIMAL (15, 2) DEFAULT ((0)) NOT NULL,
    [BSC_LARGO]      DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [BSC_ANCHO]      DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [BSC_ALTO]       DECIMAL (15, 2) DEFAULT ((0)) NULL,
    CONSTRAINT [PK_BODEGAS_SECTOR_CUBICULO] PRIMARY KEY CLUSTERED ([BSC_CODEMP] ASC, [BSC_BODID] ASC, [BSC_BDSID] ASC, [BSC_BSCID] ASC),
    CONSTRAINT [FK_BODEGAS__BODSEC_CU_BODEGAS_] FOREIGN KEY ([BSC_CODEMP], [BSC_BODID], [BSC_BDSID]) REFERENCES [dbo].[BODEGAS_SECTOR] ([BDS_CODEMP], [BDS_BODID], [BDS_BDSID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[BODEGAS_SECTOR_CUBICULO]([BSC_CODEMP] ASC, [BSC_BODID] ASC, [BSC_BDSID] ASC, [BSC_BSCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos cubiculos que tendra cada sector', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS_SECTOR_CUBICULO';
