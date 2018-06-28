CREATE TABLE [dbo].[TIPOS_ASISTENCIA] (
    [TIA_CODEMP] INT          NOT NULL,
    [TIA_TIPOID] INT          NOT NULL,
    [TIA_NOMBRE] VARCHAR (50) NOT NULL,
    [TIA_TIPO]   SMALLINT     DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TIPOS_ASISTENCIA] PRIMARY KEY CLUSTERED ([TIA_CODEMP] ASC, [TIA_TIPOID] ASC),
    CONSTRAINT [CKC_TIA_TIPO_TIPOS_AS] CHECK ([TIA_TIPO]=(10) OR [TIA_TIPO]=(9) OR [TIA_TIPO]=(8) OR [TIA_TIPO]=(7) OR [TIA_TIPO]=(6) OR [TIA_TIPO]=(5) OR [TIA_TIPO]=(4) OR [TIA_TIPO]=(3) OR [TIA_TIPO]=(2) OR [TIA_TIPO]=(1)),
    CONSTRAINT [FK_TIPOS_AS_EMPRESA_T_EMPRESA] FOREIGN KEY ([TIA_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_ASISTENCIA]([TIA_CODEMP] ASC, [TIA_TIPOID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TIPOS_ASISTENCIA]([TIA_CODEMP] ASC, [TIA_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los datos relacionados a los distintos tipos de asietncia que se podran definir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_ASISTENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica en la forma que se comportara el tipo de asitencia
   
   ejemplo : Dia labolar, Dia festivo, Permiso Horas, etc..', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_ASISTENCIA', @level2type = N'COLUMN', @level2name = N'TIA_TIPO';

