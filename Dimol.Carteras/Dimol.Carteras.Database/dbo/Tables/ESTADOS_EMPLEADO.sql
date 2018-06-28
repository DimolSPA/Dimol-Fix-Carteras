CREATE TABLE [dbo].[ESTADOS_EMPLEADO] (
    [EEM_CODEMP] INT          NOT NULL,
    [EEM_EEMID]  INT          NOT NULL,
    [EEM_NOMBRE] VARCHAR (40) NOT NULL,
    [EEM_ACCION] CHAR (1)     DEFAULT ('A') NOT NULL,
    CONSTRAINT [PK_ESTADOS_EMPLEADO] PRIMARY KEY NONCLUSTERED ([EEM_CODEMP] ASC, [EEM_EEMID] ASC),
    CONSTRAINT [CKC_EEM_ACCION_ESTADOS_] CHECK ([EEM_ACCION]='D' OR [EEM_ACCION]='A'),
    CONSTRAINT [FK_ESTADOS__EMP_ESTEM_EMPRESA] FOREIGN KEY ([EEM_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ESTADOS_EMPLEADO]([EEM_CODEMP] ASC, [EEM_EEMID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos estados de los empleados en la empresa
   por default sera 1, que indicara trabajando', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESTADOS_EMPLEADO';

