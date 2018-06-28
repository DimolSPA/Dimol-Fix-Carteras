CREATE TABLE [dbo].[BANCOS] (
    [BCO_CODEMP]   INT           NOT NULL,
    [BCO_BCOID]    INT           NOT NULL,
    [BCO_RUT]      VARCHAR (20)  NOT NULL,
    [BCO_NOMBRE]   VARCHAR (200) NOT NULL,
    [BCO_PROTESTO] TEXT          NULL,
    CONSTRAINT [PK_BANCOS] PRIMARY KEY NONCLUSTERED ([BCO_CODEMP] ASC, [BCO_BCOID] ASC),
    CONSTRAINT [FK_BANCOS_EMPRESA_B_EMPRESA] FOREIGN KEY ([BCO_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[BANCOS]([BCO_BCOID] ASC, [BCO_CODEMP] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_RUT]
    ON [dbo].[BANCOS]([BCO_CODEMP] ASC, [BCO_RUT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena todos lso bancos que utilizara cada empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BANCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo se utilizara como formato para las demandas automaticas, para el formato de los protestos de los distintos bancos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BANCOS', @level2type = N'COLUMN', @level2name = N'BCO_PROTESTO';

