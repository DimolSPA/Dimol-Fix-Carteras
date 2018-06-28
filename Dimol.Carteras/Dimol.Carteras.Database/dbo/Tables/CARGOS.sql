CREATE TABLE [dbo].[CARGOS] (
    [CAR_CODEMP] INT          NOT NULL,
    [CAR_CARID]  INT          NOT NULL,
    [CAR_NOMBRE] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CARGOS] PRIMARY KEY NONCLUSTERED ([CAR_CODEMP] ASC, [CAR_CARID] ASC),
    CONSTRAINT [FK_CARGOS_EMPRESA_C_EMPRESA] FOREIGN KEY ([CAR_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[CARGOS]([CAR_CARID] ASC, [CAR_CODEMP] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos cargos que seran asignados a los empleados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARGOS';

