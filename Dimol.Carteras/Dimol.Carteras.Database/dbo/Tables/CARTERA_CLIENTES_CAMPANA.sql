CREATE TABLE [dbo].[CARTERA_CLIENTES_CAMPANA] (
    [CCC_CODEMP]      INT           NOT NULL,
    [CCC_SUCID]       INT           NOT NULL,
    [CCC_CCCID]       INT           NOT NULL,
    [CCC_NOMBRE]      VARCHAR (150) NULL,
    [CCC_PRIORIDAD]   SMALLINT      DEFAULT ((2)) NOT NULL,
    [CCC_FECINI]      DATETIME      NULL,
    [CCC_FECFIN]      DATETIME      NULL,
    [CCC_FECFINREAL]  DATETIME      NULL,
    [CCC_ESTADO]      CHAR (1)      NOT NULL,
    [CCC_DESCRIPCION] TEXT          NULL,
    [CCC_USRID]       INT           NOT NULL,
    [CCC_GESID]       INT           NULL,
    [CCC_SUPGEST]     CHAR (1)      DEFAULT ('G') NULL,
    [CCC_PREDICTIVO]  CHAR (1)      DEFAULT ('N') NULL,
    CONSTRAINT [PK_CARTERA_CLIENTES_CAMPANA] PRIMARY KEY CLUSTERED ([CCC_CODEMP] ASC, [CCC_SUCID] ASC, [CCC_CCCID] ASC),
    CONSTRAINT [CKC_CCC_ESTADO_CARTERA_] CHECK ([CCC_ESTADO]='F' OR [CCC_ESTADO]='A' OR [CCC_ESTADO]='I'),
    CONSTRAINT [CKC_CCC_PRIORIDAD_CARTERA_] CHECK ([CCC_PRIORIDAD]=(4) OR [CCC_PRIORIDAD]=(3) OR [CCC_PRIORIDAD]=(2) OR [CCC_PRIORIDAD]=(1)),
    CONSTRAINT [CKC_CCC_SUPGEST_CARTERA_] CHECK ([CCC_SUPGEST] IS NULL OR ([CCC_SUPGEST]='S' OR [CCC_SUPGEST]='G')),
    CONSTRAINT [FK_CARTERA__EMPSUC_CA_EMPRESA_] FOREIGN KEY ([CCC_CODEMP], [CCC_SUCID]) REFERENCES [dbo].[EMPRESA_SUCURSAL] ([ESU_CODEMP], [ESU_SUCID]),
    CONSTRAINT [FK_CARTERA__GESTOR_CA_GESTOR] FOREIGN KEY ([CCC_CODEMP], [CCC_SUCID], [CCC_GESID]) REFERENCES [dbo].[GESTOR] ([GES_CODEMP], [GES_SUCID], [GES_GESID]),
    CONSTRAINT [FK_CARTERA__USUARIOS__USUARIOS] FOREIGN KEY ([CCC_CODEMP], [CCC_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CARTERA_CLIENTES_CAMPANA]([CCC_CODEMP] ASC, [CCC_SUCID] ASC, [CCC_CCCID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas campañas que se han gererado para cada cartera, tambien se indicara el nivel  de prioridad. Si lo genera un supervisor la campaña actual del gestor queda inhabilitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CAMPANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la proioridad
   
   si es 1, solo lo puede generar los supervidores
   
   1.- Super Campaña
   2.- Campaña', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CAMPANA', @level2type = N'COLUMN', @level2name = N'CCC_PRIORIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si la super campaña ha sido inicia o no tambien si esta finalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CAMPANA', @level2type = N'COLUMN', @level2name = N'CCC_ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si la campaña afectara a los gestores o al supervisor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CAMPANA', @level2type = N'COLUMN', @level2name = N'CCC_SUPGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si se activa o no el predictivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_CLIENTES_CAMPANA', @level2type = N'COLUMN', @level2name = N'CCC_PREDICTIVO';

