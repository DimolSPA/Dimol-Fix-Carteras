CREATE TABLE [dbo].[TALONARIO_CPBTDOC] (
    [TAC_CODEMP] INT           NOT NULL,
    [TAC_TACID]  INT           NOT NULL,
    [TAC_NOMBRE] VARCHAR (150) NOT NULL,
    [TAC_NUMERO] NUMERIC (15)  DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TALONARIO_CPBTDOC] PRIMARY KEY CLUSTERED ([TAC_CODEMP] ASC, [TAC_TACID] ASC),
    CONSTRAINT [FK_TALONARI_EMP_TALO_EMPRESA] FOREIGN KEY ([TAC_CODEMP]) REFERENCES [dbo].[EMPRESA] ([EMP_CODEMP])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[TALONARIO_CPBTDOC]([TAC_CODEMP] ASC, [TAC_TACID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_NOMBRE]
    ON [dbo].[TALONARIO_CPBTDOC]([TAC_CODEMP] ASC, [TAC_NOMBRE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena los distintos talonarios y su numero, es el padre del cual contendra muchos tipos asociados y que comparten el mismo talonario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TALONARIO_CPBTDOC';

