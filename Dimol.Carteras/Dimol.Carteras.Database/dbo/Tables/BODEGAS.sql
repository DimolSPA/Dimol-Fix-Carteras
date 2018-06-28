CREATE TABLE [dbo].[BODEGAS] (
    [BOD_CODEMP]     INT             NOT NULL,
    [BOD_BODID]      INT             NOT NULL,
    [BOD_NOMBRE]     VARCHAR (200)   NOT NULL,
    [BOD_AVANZADO]   CHAR (1)        NOT NULL,
    [BOD_CUBICAJE]   DECIMAL (15, 2) NOT NULL,
    [BOD_EXCLUYENTE] CHAR (1)        DEFAULT ('N') NOT NULL,
    [BOD_LARGO]      DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [BOD_ANCHO]      DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [BOD_ALTO]       DECIMAL (15, 2) DEFAULT ((0)) NULL,
    [BOD_UNMID]      INT             NULL,
    CONSTRAINT [PK_BODEGAS] PRIMARY KEY CLUSTERED ([BOD_CODEMP] ASC, [BOD_BODID] ASC),
    CONSTRAINT [CKC_BOD_EXCLUYENTE_BODEGAS] CHECK ([BOD_EXCLUYENTE]='N' OR [BOD_EXCLUYENTE]='S'),
    CONSTRAINT [FK_BODEGAS_EMPRESA_B_EMPRESA] FOREIGN KEY ([BOD_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_BODEGAS_UNIMED_BO_UNIDADES] FOREIGN KEY ([BOD_UNMID]) REFERENCES [dbo].[UNIDADES_MEDIDA] ([UNM_UNMID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[BODEGAS]([BOD_CODEMP] ASC, [BOD_BODID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[BODEGAS]([BOD_CODEMP] ASC, [BOD_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena la informacion necesaria para crear las bodegas se debe definir si la bodega sera de uso avanzado. Si sucede esto la bodega debe ser dimensionada, para poder crear los cubilucos y crear la 3 dimensiones de la bodega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si la bodega se utilizara o no de forma avanzada
   
   si es Si, se creara la tri dimension de las bodegas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS', @level2type = N'COLUMN', @level2name = N'BOD_AVANZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si esta bodega se utilizara o no para poder generar ventas o compras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BODEGAS', @level2type = N'COLUMN', @level2name = N'BOD_EXCLUYENTE';

