CREATE TABLE [dbo].[VENDEDORES] (
    [VDE_CODEMP]   INT            NOT NULL,
    [VDE_SUCID]    INT            NOT NULL,
    [VDE_VDEID]    INT            NOT NULL,
    [VDE_NOMBRE]   VARCHAR (200)  NOT NULL,
    [VDE_TELEFONO] INT            NOT NULL,
    [VDE_EMAIL]    VARCHAR (100)  NULL,
    [VDE_COMK]     DECIMAL (8, 4) DEFAULT ((0)) NOT NULL,
    [VDE_COMO]     DECIMAL (8, 4) DEFAULT ((0)) NOT NULL,
    [VDE_EMPLID]   INT            NULL,
    CONSTRAINT [PK_VENDEDORES] PRIMARY KEY CLUSTERED ([VDE_CODEMP] ASC, [VDE_SUCID] ASC, [VDE_VDEID] ASC),
    CONSTRAINT [FK_VENDEDOR_EMPL_VEND_EMPLEADO] FOREIGN KEY ([VDE_CODEMP], [VDE_EMPLID]) REFERENCES [dbo].[EMPLEADOS] ([EPL_CODEMP], [EPL_EMPLID]),
    CONSTRAINT [FK_VENDEDOR_EMPSUC_VE_EMPRESA_] FOREIGN KEY ([VDE_CODEMP], [VDE_SUCID]) REFERENCES [dbo].[EMPRESA_SUCURSAL] ([ESU_CODEMP], [ESU_SUCID])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[VENDEDORES]([VDE_CODEMP] ASC, [VDE_SUCID] ASC, [VDE_VDEID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos vendedores asociados a la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VENDEDORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la comision, sobre la venta neta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VENDEDORES', @level2type = N'COLUMN', @level2name = N'VDE_COMK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comision sobre otro monto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VENDEDORES', @level2type = N'COLUMN', @level2name = N'VDE_COMO';

