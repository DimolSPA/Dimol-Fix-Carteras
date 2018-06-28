﻿CREATE TABLE [dbo].[EMPLEADO_ASISTENCIA] (
    [EPA_CODEMP] INT      NOT NULL,
    [EPA_SUCID]  INT      NOT NULL,
    [EPA_EMPLID] INT      NOT NULL,
    [EPA_DIA]    DATETIME NOT NULL,
    CONSTRAINT [PK_EMPLEADO_ASISTENCIA] PRIMARY KEY NONCLUSTERED ([EPA_CODEMP] ASC, [EPA_SUCID] ASC, [EPA_EMPLID] ASC, [EPA_DIA] ASC),
    CONSTRAINT [FK_EMPLEADO_EMPLEADOS_EMPLEADO2] FOREIGN KEY ([EPA_CODEMP], [EPA_EMPLID]) REFERENCES [dbo].[EMPLEADOS] ([EPL_CODEMP], [EPL_EMPLID]),
    CONSTRAINT [FK_EMPLEADO_EMPSUC_AS_EMPRESA_] FOREIGN KEY ([EPA_CODEMP], [EPA_SUCID]) REFERENCES [dbo].[EMPRESA_SUCURSAL] ([ESU_CODEMP], [ESU_SUCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[EMPLEADO_ASISTENCIA]([EPA_CODEMP] ASC, [EPA_SUCID] ASC, [EPA_EMPLID] ASC, [EPA_DIA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los dias de asistencia del empleado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADO_ASISTENCIA';

