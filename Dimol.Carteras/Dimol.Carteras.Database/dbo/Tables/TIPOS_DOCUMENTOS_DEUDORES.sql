CREATE TABLE [dbo].[TIPOS_DOCUMENTOS_DEUDORES] (
    [TDD_CODEMP] INT           NOT NULL,
    [TDD_TDDID]  INT           NOT NULL,
    [TDD_NOMBRE] VARCHAR (100) NOT NULL,
    [TDD_TIPO]   CHAR (1)      DEFAULT ('R') NOT NULL,
    CONSTRAINT [PK_TIPOS_DOCUMENTOS_DEUDORES] PRIMARY KEY CLUSTERED ([TDD_CODEMP] ASC, [TDD_TDDID] ASC),
    CONSTRAINT [CKC_TDD_TIPO_TIPOS_DO] CHECK ([TDD_TIPO]='C' OR [TDD_TIPO]='R'),
    CONSTRAINT [FK_TIPOS_DO_EMPRESA_T_EMPRESA] FOREIGN KEY ([TDD_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TIPOS_DOCUMENTOS_DEUDORES]([TDD_CODEMP] ASC, [TDD_TDDID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TIPOS_DOCUMENTOS_DEUDORES]([TDD_CODEMP] ASC, [TDD_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos tipos de documentos que seran asociados al deudor, como por ejemplo
   
   Dicom, Historial de deuda, etc....', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_DOCUMENTOS_DEUDORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo, indica si el documento sera para todo el RUT o en especifico para cada cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TIPOS_DOCUMENTOS_DEUDORES', @level2type = N'COLUMN', @level2name = N'TDD_TIPO';

