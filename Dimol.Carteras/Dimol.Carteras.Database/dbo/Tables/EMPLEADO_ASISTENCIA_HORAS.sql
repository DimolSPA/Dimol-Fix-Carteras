CREATE TABLE [dbo].[EMPLEADO_ASISTENCIA_HORAS] (
    [EAH_CODEMP]    INT      NOT NULL,
    [EAH_SUCID]     INT      NOT NULL,
    [EAH_EMPLID]    INT      NOT NULL,
    [EAH_DIA]       DATETIME NOT NULL,
    [EAH_ITEM]      SMALLINT NOT NULL,
    [EAH_TIPOID]    INT      NOT NULL,
    [EAH_ENTRADA]   DATETIME NOT NULL,
    [EAH_SALIDA]    DATETIME NOT NULL,
    [EAH_AUTHORA]   CHAR (1) DEFAULT ('N') NOT NULL,
    [EAH_PAGSUELDO] CHAR (1) DEFAULT ('N') NOT NULL,
    CONSTRAINT [PK_EMPLEADO_ASISTENCIA_HORAS] PRIMARY KEY NONCLUSTERED ([EAH_CODEMP] ASC, [EAH_SUCID] ASC, [EAH_EMPLID] ASC, [EAH_DIA] ASC, [EAH_ITEM] ASC),
    CONSTRAINT [CKC_EAH_AUTHORA_EMPLEADO] CHECK ([EAH_AUTHORA]='N' OR [EAH_AUTHORA]='S'),
    CONSTRAINT [CKC_EAH_PAGSUELDO_EMPLEADO] CHECK ([EAH_PAGSUELDO]='N' OR [EAH_PAGSUELDO]='S'),
    CONSTRAINT [FK_EMPLEADO_EMPLASIST_EMPLEADO] FOREIGN KEY ([EAH_CODEMP], [EAH_SUCID], [EAH_EMPLID], [EAH_DIA]) REFERENCES [dbo].[EMPLEADO_ASISTENCIA] ([EPA_CODEMP], [EPA_SUCID], [EPA_EMPLID], [EPA_DIA]),
    CONSTRAINT [FK_EMPLEADO_TIPASIST__TIPOS_AS] FOREIGN KEY ([EAH_CODEMP], [EAH_TIPOID]) REFERENCES [dbo].[TIPOS_ASISTENCIA] ([TIA_CODEMP], [TIA_TIPOID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[EMPLEADO_ASISTENCIA_HORAS]([EAH_CODEMP] ASC, [EAH_SUCID] ASC, [EAH_EMPLID] ASC, [EAH_DIA] ASC, [EAH_ITEM] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena las distintas horas de asistencia dependiendo del dia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADO_ASISTENCIA_HORAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se autorizaran o no las horas extras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADO_ASISTENCIA_HORAS', @level2type = N'COLUMN', @level2name = N'EAH_AUTHORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se pagara o no sueldo, dependiendo del permiso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EMPLEADO_ASISTENCIA_HORAS', @level2type = N'COLUMN', @level2name = N'EAH_PAGSUELDO';

