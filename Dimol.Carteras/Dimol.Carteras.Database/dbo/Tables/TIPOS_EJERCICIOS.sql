CREATE TABLE [dbo].[TIPOS_EJERCICIOS] (
    [TEJ_CODEMP] INT           NOT NULL,
    [TEJ_TEJID]  INT           NOT NULL,
    [TEJ_NOMBRE] VARCHAR (200) NOT NULL,
    [TEJ_TIPO]   SMALLINT      NOT NULL,
    CONSTRAINT [PK_TIPOS_EJERCICIOS] PRIMARY KEY CLUSTERED ([TEJ_CODEMP] ASC, [TEJ_TEJID] ASC),
    CONSTRAINT [CKC_TEJ_TIPO_TIPOS_EJ] CHECK ([TEJ_TIPO]=(5) OR [TEJ_TIPO]=(4) OR [TEJ_TIPO]=(3) OR [TEJ_TIPO]=(2) OR [TEJ_TIPO]=(1)),
    CONSTRAINT [FK_TIPOS_EJ_EMP_TIPEJ_EMPRESA] FOREIGN KEY ([TEJ_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_EJERCICIOS]([TEJ_CODEMP] ASC, [TEJ_TEJID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOM]
    ON [dbo].[TIPOS_EJERCICIOS]([TEJ_CODEMP] ASC, [TEJ_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las distintas areas a las cuales se orienta el ejercicio
   
   ejemplo
   
   piernas, hombros, brazos, tricep....', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_EJERCICIOS';

