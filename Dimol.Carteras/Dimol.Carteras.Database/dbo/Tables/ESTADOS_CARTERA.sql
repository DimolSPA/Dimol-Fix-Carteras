CREATE TABLE [dbo].[ESTADOS_CARTERA] (
    [ECT_CODEMP]     INT           NOT NULL,
    [ECT_ESTID]      SMALLINT      NOT NULL,
    [ECT_NOMBRE]     VARCHAR (300) NOT NULL,
    [ECT_AGRUPA]     SMALLINT      NOT NULL,
    [ECT_UTILIZA]    CHAR (1)      DEFAULT ('D') NOT NULL,
    [ECT_PREJUD]     CHAR (1)      NOT NULL,
    [ECT_SOLFECHA]   CHAR (1)      DEFAULT ('N') NOT NULL,
    [ECT_GENRET]     CHAR (1)      DEFAULT ('N') NOT NULL,
    [ECT_COMPROMISO] CHAR (1)      DEFAULT ('N') NULL,
    [ECT_HABILITADO] CHAR (1)      NULL,
    [ECT_CATEGORIA]  CHAR (2)      NULL,
    [ECT_COLOR]      CHAR (2)      NULL,
    CONSTRAINT [PK_ESTADOS_CARTERA] PRIMARY KEY NONCLUSTERED ([ECT_CODEMP] ASC, [ECT_ESTID] ASC),
    CONSTRAINT [CKC_ECT_AGRUPA_ESTADOS_] CHECK ([ECT_AGRUPA]=(9) OR [ECT_AGRUPA]=(8) OR [ECT_AGRUPA]=(7) OR [ECT_AGRUPA]=(6) OR [ECT_AGRUPA]=(5) OR [ECT_AGRUPA]=(4) OR [ECT_AGRUPA]=(3) OR [ECT_AGRUPA]=(2) OR [ECT_AGRUPA]=(1)),
    CONSTRAINT [CKC_ECT_COMPROMISO_ESTADOS_] CHECK ([ECT_COMPROMISO] IS NULL OR ([ECT_COMPROMISO]='N' OR [ECT_COMPROMISO]='S')),
    CONSTRAINT [CKC_ECT_GENRET_ESTADOS_] CHECK ([ECT_GENRET]='N' OR [ECT_GENRET]='S'),
    CONSTRAINT [CKC_ECT_PREJUD_ESTADOS_] CHECK ([ECT_PREJUD]='A' OR [ECT_PREJUD]='J' OR [ECT_PREJUD]='P'),
    CONSTRAINT [CKC_ECT_SOLFECHA_ESTADOS_] CHECK ([ECT_SOLFECHA]='N' OR [ECT_SOLFECHA]='S'),
    CONSTRAINT [CKC_ECT_UTILIZA_ESTADOS_] CHECK ([ECT_UTILIZA]='R' OR [ECT_UTILIZA]='D'),
    CONSTRAINT [FK_ESTADOS__EMPRESA_E_EMPRESA] FOREIGN KEY ([ECT_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ESTADOS_CARTERA]([ECT_ESTID] ASC, [ECT_CODEMP] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[ESTADOS_CARTERA]([ECT_CODEMP] ASC, [ECT_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacenara los distintos tipos de estados par acada documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESTADOS_CARTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indicara, la forma de comportarse de cada estado
   
   ejemplo:
   
   1.- Sin Gestionar
   2.- Bueno
   3.- Regular
   4.- Malo
   5.- Finalizado
   6.- Castigo
   7.- Devolucion al Cliente o Retiro del Cliente
   
   El estado finalizado, o sea todos los mayores o igual a 5 solo se puede ingresar por medio de programa no se puede ingresar por gestor
   
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESTADOS_CARTERA', @level2type = N'COLUMN', @level2name = N'ECT_AGRUPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se utilizara en los documentos o en los rut', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESTADOS_CARTERA', @level2type = N'COLUMN', @level2name = N'ECT_UTILIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si los estados se utilizaran en prejudicial o judicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESTADOS_CARTERA', @level2type = N'COLUMN', @level2name = N'ECT_PREJUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si se debe solicitar fecha o no 
   
   es para el tema de los compromisos de pago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESTADOS_CARTERA', @level2type = N'COLUMN', @level2name = N'ECT_SOLFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica, si se debe ingresar o no un retiro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESTADOS_CARTERA', @level2type = N'COLUMN', @level2name = N'ECT_GENRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si al hacer un compromiso, se debe ingresar un monto el cual sera el monto comprometido a pagar. De esta forma sabremos a de forma exacta los montos a recuperar en un futuro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESTADOS_CARTERA', @level2type = N'COLUMN', @level2name = N'ECT_COMPROMISO';

