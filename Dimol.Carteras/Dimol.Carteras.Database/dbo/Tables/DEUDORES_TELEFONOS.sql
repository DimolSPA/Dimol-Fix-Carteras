CREATE TABLE [dbo].[DEUDORES_TELEFONOS] (
    [DDT_CODEMP]    INT          NOT NULL,
    [DDT_CTCID]     NUMERIC (15) NOT NULL,
    [DDT_NUMERO]    NUMERIC (12) NOT NULL,
    [DDT_TIPO]      CHAR (1)     NOT NULL,
    [DDT_ESTADO]    CHAR (1)     CONSTRAINT [DF__DEUDORES___DDT_E__318258D2] DEFAULT ('A') NOT NULL,
    [DDT_PRIORIDAD] SMALLINT     CONSTRAINT [DF__DEUDORES___DDT_P__3D49DC90] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_DEUDORES_TELEFONOS] PRIMARY KEY CLUSTERED ([DDT_CODEMP] ASC, [DDT_CTCID] ASC, [DDT_NUMERO] ASC),
    CONSTRAINT [CKC_DDT_ESTADO_DEUDORES] CHECK ([DDT_ESTADO]='M' OR [DDT_ESTADO]='C' OR [DDT_ESTADO]='A'),
    CONSTRAINT [CKC_DDT_TIPO_DEUDORES] CHECK ([DDT_TIPO]='F' OR [DDT_TIPO]='O' OR [DDT_TIPO]='M' OR [DDT_TIPO]='C'),
    CONSTRAINT [FK_DEUDORES_DEUDORE_T_DEUDORES] FOREIGN KEY ([DDT_CODEMP], [DDT_CTCID]) REFERENCES [dbo].[DEUDORES] ([CTC_CODEMP], [CTC_CTCID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[DEUDORES_TELEFONOS]([DDT_CODEMP] ASC, [DDT_CTCID] ASC, [DDT_NUMERO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos telefonos para cada deudor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_TELEFONOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si es un telefono fijo o movil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_TELEFONOS', @level2type = N'COLUMN', @level2name = N'DDT_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica la prioridad en la que se llama, 
   
   indicara el ultimo numero llamado
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DEUDORES_TELEFONOS', @level2type = N'COLUMN', @level2name = N'DDT_PRIORIDAD';

