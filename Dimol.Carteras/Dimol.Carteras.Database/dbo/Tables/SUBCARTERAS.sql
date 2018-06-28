CREATE TABLE [dbo].[SUBCARTERAS] (
    [SBC_CODEMP]    INT           NOT NULL,
    [SBC_SBCID]     INT           NOT NULL,
    [SBC_RUT]       VARCHAR (20)  NOT NULL,
    [SBC_NOMBRE]    VARCHAR (400) NOT NULL,
    [SBC_COMID]     INT           NOT NULL,
    [SBC_DIRECCION] VARCHAR (200) NOT NULL,
    [SBC_TELEFONO]  VARCHAR (80)  NULL,
    CONSTRAINT [PK_SUBCARTERAS] PRIMARY KEY NONCLUSTERED ([SBC_CODEMP] ASC, [SBC_SBCID] ASC),
    CONSTRAINT [FK_SUBCARTE_COMUNA_SU_COMUNA] FOREIGN KEY ([SBC_COMID]) REFERENCES [dbo].[COMUNA] ([COM_COMID]),
    CONSTRAINT [FK_SUBCARTE_EMPRESA_S_EMPRESA] FOREIGN KEY ([SBC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[SUBCARTERAS]([SBC_CODEMP] ASC, [SBC_SBCID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_RUT]
    ON [dbo].[SUBCARTERAS]([SBC_CODEMP] ASC, [SBC_RUT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla se utilizara, para asociar unos subdeudas a clienetes que entregen servicios a otros, pero estos tambien tengan dedudas con las subcarteras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SUBCARTERAS';

