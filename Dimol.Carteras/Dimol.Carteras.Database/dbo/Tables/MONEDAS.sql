CREATE TABLE [dbo].[MONEDAS] (
    [MON_CODEMP]    INT             NOT NULL,
    [MON_CODMON]    INT             NOT NULL,
    [MON_NOMBRE]    VARCHAR (50)    NOT NULL,
    [MON_SIMBOLO]   VARCHAR (5)     NOT NULL,
    [MON_DEFAULT]   CHAR (1)        DEFAULT ('N') NOT NULL,
    [MON_PORCINT]   DECIMAL (10, 3) DEFAULT ((0)) NULL,
    [MON_DECIMALES] SMALLINT        NULL,
    CONSTRAINT [PK_MONEDAS] PRIMARY KEY CLUSTERED ([MON_CODEMP] ASC, [MON_CODMON] ASC),
    CONSTRAINT [CKC_MON_DEFAULT_MONEDAS] CHECK ([MON_DEFAULT]='N' OR [MON_DEFAULT]='S'),
    CONSTRAINT [FK_MONEDAS_EMPRESA_M_EMPRESA] FOREIGN KEY ([MON_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[MONEDAS]([MON_CODEMP] ASC, [MON_CODMON] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[MONEDAS]([MON_CODEMP] ASC, [MON_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica cual moneda es la default de la empresa
   
   Solo puede haber 1 sola moneda default', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MONEDAS', @level2type = N'COLUMN', @level2name = N'MON_DEFAULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo almacena los distintos porcentajes de interes para cada moneda, esto se utilizara para realizar el calculo de intereses de los deudores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MONEDAS', @level2type = N'COLUMN', @level2name = N'MON_PORCINT';

