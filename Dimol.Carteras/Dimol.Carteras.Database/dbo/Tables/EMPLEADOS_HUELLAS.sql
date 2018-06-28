﻿CREATE TABLE [dbo].[EMPLEADOS_HUELLAS] (
    [EPH_CODEMP] INT            NOT NULL,
    [EPH_EMPLID] INT            NOT NULL,
    [EPH_EPHID]  SMALLINT       NOT NULL,
    [EPH_HUELLA] VARCHAR (8000) NOT NULL,
    CONSTRAINT [PK_EMPLEADOS_HUELLAS] PRIMARY KEY CLUSTERED ([EPH_CODEMP] ASC, [EPH_EMPLID] ASC, [EPH_EPHID] ASC),
    CONSTRAINT [FK_EMPLEADO_EMPLEADOS_EMPLEADO] FOREIGN KEY ([EPH_CODEMP], [EPH_EMPLID]) REFERENCES [dbo].[EMPLEADOS] ([EPL_CODEMP], [EPL_EMPLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[EMPLEADOS_HUELLAS]([EPH_CODEMP] ASC, [EPH_EMPLID] ASC, [EPH_EPHID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas huellas digitales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADOS_HUELLAS';
