CREATE TABLE [dbo].[PROVCLI] (
    [PCL_CODEMP]        INT           NOT NULL,
    [PCL_PCLID]         NUMERIC (15)  NOT NULL,
    [PCL_TPCID]         INT           NOT NULL,
    [PCL_RUT]           VARCHAR (20)  NOT NULL,
    [PCL_NOMBRE]        VARCHAR (200) NOT NULL,
    [PCL_APEPAT]        VARCHAR (80)  NULL,
    [PCL_APEMAT]        VARCHAR (80)  NULL,
    [PCL_NOMFANT]       VARCHAR (800) NOT NULL,
    [PCL_GIRID]         INT           NOT NULL,
    [PCL_FECING]        DATETIME      NOT NULL,
    [PCL_ESTADO]        CHAR (1)      DEFAULT ('V') NOT NULL,
    [PCL_FECFIN]        DATETIME      NULL,
    [PCL_REPLEGAL]      VARCHAR (400) NULL,
    [PCL_RUTLEGAL]      VARCHAR (20)  NULL,
    [PCL_TIPCLI]        CHAR (1)      DEFAULT ('N') NULL,
    [PCL_LOGO]          IMAGE         NULL,
    [PCL_WEB]           CHAR (1)      NULL,
    [PCL_COMENTARIO]    TEXT          NULL,
    [PCL_TIPCART]       INT           DEFAULT ((2)) NULL,
    [PCL_TRANSPORTISTA] CHAR (1)      DEFAULT ('N') NOT NULL,
    [PCL_USRID]         INT           NULL,
    [PCL_NAVIERA]       CHAR (1)      DEFAULT ('N') NULL,
    [PCL_CODIGO]        VARCHAR (10)  NULL,
    CONSTRAINT [PK_PROVCLI] PRIMARY KEY NONCLUSTERED ([PCL_CODEMP] ASC, [PCL_PCLID] ASC),
    CONSTRAINT [CKC_PCL_ESTADO_PROVCLI] CHECK ([PCL_ESTADO]='P' OR [PCL_ESTADO]='B' OR [PCL_ESTADO]='E' OR [PCL_ESTADO]='V'),
    CONSTRAINT [CKC_PCL_NAVIERA_PROVCLI] CHECK ([PCL_NAVIERA] IS NULL OR ([PCL_NAVIERA]='N' OR [PCL_NAVIERA]='S')),
    CONSTRAINT [CKC_PCL_TIPCART_PROVCLI] CHECK ([PCL_TIPCART] IS NULL OR ([PCL_TIPCART]=(4) OR [PCL_TIPCART]=(3) OR [PCL_TIPCART]=(2) OR [PCL_TIPCART]=(1))),
    CONSTRAINT [CKC_PCL_TIPCLI_PROVCLI] CHECK ([PCL_TIPCLI] IS NULL OR ([PCL_TIPCLI]='E' OR [PCL_TIPCLI]='N' OR [PCL_TIPCLI]='P')),
    CONSTRAINT [CKC_PCL_TRANSPORTISTA_PROVCLI] CHECK ([PCL_TRANSPORTISTA]='N' OR [PCL_TRANSPORTISTA]='S'),
    CONSTRAINT [CKC_PCL_WEB_PROVCLI] CHECK ([PCL_WEB] IS NULL OR ([PCL_WEB]='N' OR [PCL_WEB]='S')),
    CONSTRAINT [FK_PROVCLI_EMPRESA_P_EMPRESA] FOREIGN KEY ([PCL_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_PROVCLI_GIROS_PRO_GIROS] FOREIGN KEY ([PCL_CODEMP], [PCL_GIRID]) REFERENCES [dbo].[GIROS] ([GIR_CODEMP], [GIR_GIRID]),
    CONSTRAINT [FK_PROVCLI_TIPPROVCL_TIPOS_PR] FOREIGN KEY ([PCL_CODEMP], [PCL_TPCID]) REFERENCES [dbo].[TIPOS_PROVCLI] ([TPC_CODEMP], [TPC_TPCID]),
    CONSTRAINT [FK_PROVCLI_USUARIOS__USUARIOS] FOREIGN KEY ([PCL_CODEMP], [PCL_USRID]) REFERENCES [dbo].[USUARIOS] ([USR_CODEMP], [USR_USRID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PROVCLI]([PCL_CODEMP] ASC, [PCL_PCLID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_RUT]
    ON [dbo].[PROVCLI]([PCL_CODEMP] ASC, [PCL_RUT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara los distintos clientes y proveedores tambien indicara si el cliente es un deudor o a la vez puede ser un deudor o cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica en que se encuetra el cliente o proveedor
   
   V -> Vigente
   E -> Eliminado
   B -> Bloqueado
   P -> Proceso de Aprobacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI', @level2type = N'COLUMN', @level2name = N'PCL_ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el cliente o proveedor es nacional o extranjero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI', @level2type = N'COLUMN', @level2name = N'PCL_TIPCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si se mostrara o no en la pagina web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI', @level2type = N'COLUMN', @level2name = N'PCL_WEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, que tipo de cartera, es la que se trabajara con este cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI', @level2type = N'COLUMN', @level2name = N'PCL_TIPCART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indicara si el proveedor es o no un transportista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI', @level2type = N'COLUMN', @level2name = N'PCL_TRANSPORTISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el proveedor es una naviera, se utilizara para el cuadro de embarque', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI', @level2type = N'COLUMN', @level2name = N'PCL_NAVIERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo se utiliza, para identificar elcodigo que nos dan nuestros clientes
   
   se utilizara para el tema de los despachos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI', @level2type = N'COLUMN', @level2name = N'PCL_CODIGO';

