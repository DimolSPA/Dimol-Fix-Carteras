CREATE TABLE [dbo].[ROL_DEMANDADOS] (
    [RLD_CODEMP] INT           NOT NULL,
    [RLD_ROLID]  INT           NOT NULL,
    [RLD_RUT]    VARCHAR (20)  NOT NULL,
    [RLD_NOMBRE] VARCHAR (350) NOT NULL,
    [RLD_REPLEG] CHAR (1)      DEFAULT ('N') NOT NULL,
    CONSTRAINT [PK_ROL_DEMANDADOS] PRIMARY KEY CLUSTERED ([RLD_CODEMP] ASC, [RLD_ROLID] ASC, [RLD_RUT] ASC),
    CONSTRAINT [CKC_RLD_REPLEG_ROL_DEMA] CHECK ([RLD_REPLEG]='N' OR [RLD_REPLEG]='S'),
    CONSTRAINT [FK_ROL_DEMA_ROL_DEMAN_ROL] FOREIGN KEY ([RLD_CODEMP], [RLD_ROLID]) REFERENCES [dbo].[ROL] ([ROL_CODEMP], [ROL_ROLID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[ROL_DEMANDADOS]([RLD_CODEMP] ASC, [RLD_ROLID] ASC, [RLD_RUT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla, almacena los distintos demandados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ROL_DEMANDADOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo indica si es el representante legal o no de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ROL_DEMANDADOS', @level2type = N'COLUMN', @level2name = N'RLD_REPLEG';

