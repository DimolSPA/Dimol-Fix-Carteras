CREATE TABLE [dbo].[DEUDORES] (
    [CTC_CODEMP]           INT           NOT NULL,
    [CTC_CTCID]            NUMERIC (15)  NOT NULL,
    [CTC_RUT]              VARCHAR (20)  NOT NULL,
    [CTC_NUMERO]           NUMERIC (12)  NOT NULL,
    [CTC_DIGITO]           CHAR (1)      NOT NULL,
    [CTC_NOMBRE]           VARCHAR (400) NOT NULL,
    [CTC_APEPAT]           VARCHAR (100) NULL,
    [CTC_APEMAT]           VARCHAR (100) NULL,
    [CTC_NOMFANT]          VARCHAR (600) NOT NULL,
    [CTC_COMID]            INT           NOT NULL,
    [CTC_DIRECCION]        VARCHAR (800) NOT NULL,
    [CTC_PARTEMP]          CHAR (1)      NOT NULL,
    [CTC_FECING]           DATETIME      DEFAULT (getdate()) NOT NULL,
    [CTC_SOCID]            NUMERIC (15)  NULL,
    [CTC_ESTDIR]           SMALLINT      DEFAULT ((1)) NULL,
    [CTC_QUIEBRA]          CHAR (1)      NULL,
    [CTC_NACEXT]           CHAR (1)      DEFAULT ('N') NULL,
    [CTC_SOLICITA_QUIEBRA] VARCHAR (1)   CONSTRAINT [DF_CTC_SOLICITA_QUIEBRA] DEFAULT ('N') NOT NULL,
    CONSTRAINT [PK_DEUDORES] PRIMARY KEY CLUSTERED ([CTC_CODEMP] ASC, [CTC_CTCID] ASC),
    CONSTRAINT [CKC_CTC_ESTDIR_DEUDORES] CHECK ([CTC_ESTDIR] IS NULL OR ([CTC_ESTDIR]=(5) OR [CTC_ESTDIR]=(4) OR [CTC_ESTDIR]=(3) OR [CTC_ESTDIR]=(2) OR [CTC_ESTDIR]=(1))),
    CONSTRAINT [FK_DEUDORES_COM_DEUDO_COMUNA] FOREIGN KEY ([CTC_COMID]) REFERENCES [dbo].[COMUNA] ([COM_COMID]),
    CONSTRAINT [FK_DEUDORES_DEUDORE_S_DEUDORES] FOREIGN KEY ([CTC_CODEMP], [CTC_SOCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID]),
    CONSTRAINT [FK_DEUDORES_EMPRESA_D_EMPRESA] FOREIGN KEY ([CTC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DEUDORES]([CTC_CODEMP] ASC, [CTC_CTCID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_RUT]
    ON [dbo].[DEUDORES]([CTC_CODEMP] ASC, [CTC_RUT] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMFANT]
    ON [dbo].[DEUDORES]([CTC_CODEMP] ASC, [CTC_NOMFANT] ASC, [CTC_RUT] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NUMERO]
    ON [dbo].[DEUDORES]([CTC_CODEMP] ASC, [CTC_NUMERO] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_DEUDORES_5_382624406__K2_K1_4]
    ON [dbo].[DEUDORES]([CTC_CTCID] ASC, [CTC_CODEMP] ASC)
    INCLUDE([CTC_NUMERO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena todos los datos relacionados con los deudores, los cuales se utilizaran para asociarlos a una cartera de cobranza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el doudor es un particular o una empresa
   
   P -> Particular
   E -> Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES', @level2type = N'COLUMN', @level2name = N'CTC_PARTEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo se utilizara en caso que el deudor tnga algo que vcer con otro,
   
   Ejemplo: Sociedades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES', @level2type = N'COLUMN', @level2name = N'CTC_SOCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si el deudor esta o no en quiebra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES', @level2type = N'COLUMN', @level2name = N'CTC_QUIEBRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el deudor es nacional o extranjero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES', @level2type = N'COLUMN', @level2name = N'CTC_NACEXT';

