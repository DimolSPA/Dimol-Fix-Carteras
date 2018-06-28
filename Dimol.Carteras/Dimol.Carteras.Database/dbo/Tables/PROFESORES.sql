CREATE TABLE [dbo].[PROFESORES] (
    [PRF_CODEMP] INT          NOT NULL,
    [PRF_PRFID]  INT          NOT NULL,
    [PRF_PCLID]  NUMERIC (15) NULL,
    [PRF_EMPLID] INT          NULL,
    CONSTRAINT [PK_PROFESORES] PRIMARY KEY CLUSTERED ([PRF_CODEMP] ASC, [PRF_PRFID] ASC),
    CONSTRAINT [FK_PROFESOR_EMPL_PROF_EMPLEADO] FOREIGN KEY ([PRF_CODEMP], [PRF_EMPLID]) REFERENCES [dbo].[EMPLEADOS] ([EPL_CODEMP], [EPL_EMPLID]),
    CONSTRAINT [FK_PROFESOR_PROVCLI_P_PROVCLI] FOREIGN KEY ([PRF_CODEMP], [PRF_PCLID]) REFERENCES [dbo].[PROVCLI] ([PCL_CODEMP], [PCL_PCLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PROFESORES]([PRF_CODEMP] ASC, [PRF_PRFID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacenara los distintos datos, para los profesores
   
   si seran de planta o foraneos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROFESORES';

