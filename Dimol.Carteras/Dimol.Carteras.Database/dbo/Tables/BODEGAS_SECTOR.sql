CREATE TABLE [dbo].[BODEGAS_SECTOR] (
    [BDS_CODEMP]   INT             NOT NULL,
    [BDS_BODID]    INT             NOT NULL,
    [BDS_BDSID]    INT             NOT NULL,
    [BDS_NOMBRE]   VARCHAR (200)   NOT NULL,
    [BDS_BODEGAJE] CHAR (1)        DEFAULT ('P') NOT NULL,
    [BDS_CUBICAJE] DECIMAL (15, 2) NOT NULL,
    [BDS_COLUMNAS] VARCHAR (10)    DEFAULT ('A') NOT NULL,
    [BDS_FILAS]    INT             DEFAULT ((0)) NOT NULL,
    [BDS_TIPOALMA] SMALLINT        DEFAULT ((1)) NOT NULL,
    [BDS_LARGO]    DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [BDS_ANCHO]    DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [BDS_ALTO]     DECIMAL (15, 2) DEFAULT ((0)) NULL,
    CONSTRAINT [PK_BODEGAS_SECTOR] PRIMARY KEY CLUSTERED ([BDS_CODEMP] ASC, [BDS_BODID] ASC, [BDS_BDSID] ASC),
    CONSTRAINT [CKC_BDS_BODEGAJE_BODEGAS_] CHECK ([BDS_BODEGAJE]='I' OR [BDS_BODEGAJE]='P'),
    CONSTRAINT [CKC_BDS_TIPOALMA_BODEGAS_] CHECK ([BDS_TIPOALMA]=(5) OR [BDS_TIPOALMA]=(4) OR [BDS_TIPOALMA]=(3) OR [BDS_TIPOALMA]=(2) OR [BDS_TIPOALMA]=(1)),
    CONSTRAINT [FK_BODEGAS__BODEGA_SE_BODEGAS] FOREIGN KEY ([BDS_CODEMP], [BDS_BODID]) REFERENCES [dbo].[BODEGAS] ([BOD_CODEMP], [BOD_BODID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[BODEGAS_SECTOR]([BDS_CODEMP] ASC, [BDS_BODID] ASC, [BDS_BDSID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[BODEGAS_SECTOR]([BDS_CODEMP] ASC, [BDS_BODID] ASC, [BDS_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos sectores que utilizara la bodega
   
   El funcionamiento es similar a una planilla excel
   
   donde las filas seran desde 1 hasta 999999
   y las columnas seran desde a hasta az...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS_SECTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica que tipo de productos almacenara, si seran insumos o productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS_SECTOR', @level2type = N'COLUMN', @level2name = N'BDS_BODEGAJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica que tipo de productos almacenara si seran buenos, malos
   
   1 Buenos
   2 Malos
   3
   ....', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS_SECTOR', @level2type = N'COLUMN', @level2name = N'BDS_TIPOALMA';

