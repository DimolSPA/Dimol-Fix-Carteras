CREATE TABLE [dbo].[GESTOR] (
    [GES_CODEMP]           INT            NOT NULL,
    [GES_SUCID]            INT            NOT NULL,
    [GES_GESID]            INT            NOT NULL,
    [GES_NOMBRE]           VARCHAR (500)  NOT NULL,
    [GES_TELEFONO]         INT            NOT NULL,
    [GES_EMAIL]            VARCHAR (100)  NULL,
    [GES_TIPCART]          SMALLINT       NULL,
    [GES_COMKI]            DECIMAL (8, 4) DEFAULT ((0)) NULL,
    [GES_COMHON]           DECIMAL (8, 4) DEFAULT ((0)) NULL,
    [GES_EMPLID]           INT            NULL,
    [GES_REMOTO]           CHAR (1)       DEFAULT ('N') NULL,
    [GES_ESTADO]           CHAR (1)       DEFAULT ('A') NULL,
    [GES_COMJKI]           DECIMAL (8, 4) NULL,
    [GES_COMJHON]          DECIMAL (8, 4) NULL,
    [GES_VISITA_TERRENO]   VARCHAR (1)    CONSTRAINT [DF_GES_VISITA_TERRENO] DEFAULT ('N') NOT NULL,
    [GES_IMEI]             VARCHAR (100)  CONSTRAINT [DF_GES_IMEI] DEFAULT (NULL) NULL,
    [GES_TELEFONO_TERRENO] VARCHAR (9)    CONSTRAINT [DF_GES_TELEFONO_TERRENO] DEFAULT (NULL) NULL,
    CONSTRAINT [PK_GESTOR] PRIMARY KEY NONCLUSTERED ([GES_CODEMP] ASC, [GES_SUCID] ASC, [GES_GESID] ASC),
    CONSTRAINT [CKC_GES_ESTADO_GESTOR] CHECK ([GES_ESTADO] IS NULL OR ([GES_ESTADO]='N' OR [GES_ESTADO]='A')),
    CONSTRAINT [CKC_GES_REMOTO_GESTOR] CHECK ([GES_REMOTO] IS NULL OR ([GES_REMOTO]='N' OR [GES_REMOTO]='S')),
    CONSTRAINT [CKC_GES_TIPCART_GESTOR] CHECK ([GES_TIPCART] IS NULL OR ([GES_TIPCART]=(3) OR [GES_TIPCART]=(2) OR [GES_TIPCART]=(1))),
    CONSTRAINT [FK_GESTOR_EMPLEADOS_EMPLEADO] FOREIGN KEY ([GES_CODEMP], [GES_EMPLID]) REFERENCES [dbo].[EMPLEADOS] ([EPL_CODEMP], [EPL_EMPLID]),
    CONSTRAINT [FK_GESTOR_EMPSUC_GE_EMPRESA_] FOREIGN KEY ([GES_CODEMP], [GES_SUCID]) REFERENCES [dbo].[EMPRESA_SUCURSAL] ([ESU_CODEMP], [ESU_SUCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[GESTOR]([GES_GESID] ASC, [GES_SUCID] ASC, [GES_CODEMP] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos gestores que tiene la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GESTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, que tipo de cartera vera el gestor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GESTOR', @level2type = N'COLUMN', @level2name = N'GES_TIPCART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el gestor es o no remoto o sea esta gestionando de un sitio que no sea la oficina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GESTOR', @level2type = N'COLUMN', @level2name = N'GES_REMOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el gestor esta o no activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GESTOR', @level2type = N'COLUMN', @level2name = N'GES_ESTADO';

