CREATE TABLE [dbo].[UTILITARIOS] (
    [UTL_CODEMP] INT           NOT NULL,
    [UTL_UTLID]  INT           NOT NULL,
    [UTL_NOMBRE] VARCHAR (200) NOT NULL,
    [UTL_TIPO]   SMALLINT      DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_UTILITARIOS] PRIMARY KEY CLUSTERED ([UTL_CODEMP] ASC, [UTL_UTLID] ASC),
    CONSTRAINT [CKC_UTL_TIPO_UTILITAR] CHECK ([UTL_TIPO]=(6) OR [UTL_TIPO]=(5) OR [UTL_TIPO]=(4) OR [UTL_TIPO]=(3) OR [UTL_TIPO]=(2) OR [UTL_TIPO]=(1)),
    CONSTRAINT [FK_UTILITAR_EMPRESA_U_EMPRESA] FOREIGN KEY ([UTL_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[UTILITARIOS]([UTL_CODEMP] ASC, [UTL_UTLID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos utilitarios, dependiendo que se quiera realizar
   
   Si sera, por Cliente, Deudor o Proovedor
   
   Por ejemplo:
   
   Cliente: Alertas Caducidad documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UTILITARIOS';

