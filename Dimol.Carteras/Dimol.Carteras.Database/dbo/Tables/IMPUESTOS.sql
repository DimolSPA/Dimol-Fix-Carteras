CREATE TABLE [dbo].[IMPUESTOS] (
    [IPT_CODEMP]   INT            NOT NULL,
    [IPT_IPTID]    SMALLINT       NOT NULL,
    [IPT_NOMCORT]  VARCHAR (10)   NOT NULL,
    [IPT_NOMBRE]   VARCHAR (80)   NOT NULL,
    [IPT_PCTID]    INT            NOT NULL,
    [IPT_RETENIDO] CHAR (1)       DEFAULT ('N') NOT NULL,
    [IPT_MONTO]    DECIMAL (5, 2) DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_IMPUESTOS] PRIMARY KEY CLUSTERED ([IPT_CODEMP] ASC, [IPT_IPTID] ASC),
    CONSTRAINT [CKC_IPT_RETENIDO_IMPUESTO] CHECK ([IPT_RETENIDO]='N' OR [IPT_RETENIDO]='S'),
    CONSTRAINT [FK_IMPUESTO_EMPRESA_I_EMPRESA] FOREIGN KEY ([IPT_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP]),
    CONSTRAINT [FK_IMPUESTO_PLACTA_IM_PLAN_CUE] FOREIGN KEY ([IPT_CODEMP], [IPT_PCTID]) REFERENCES [dbo].[PLAN_CUENTAS] ([PCT_CODEMP], [PCT_PCTID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[IMPUESTOS]([IPT_CODEMP] ASC, [IPT_IPTID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMCORT]
    ON [dbo].[IMPUESTOS]([IPT_CODEMP] ASC, [IPT_NOMCORT] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[IMPUESTOS]([IPT_CODEMP] ASC, [IPT_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabl almacena los distintos impuestos, con su respectiva cta. ctble', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IMPUESTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si el impuesto es retenido o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IMPUESTOS', @level2type = N'COLUMN', @level2name = N'IPT_RETENIDO';

