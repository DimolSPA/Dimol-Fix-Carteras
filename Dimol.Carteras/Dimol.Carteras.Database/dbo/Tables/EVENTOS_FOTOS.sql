CREATE TABLE [dbo].[EVENTOS_FOTOS] (
    [EVF_CODEMP] INT           NOT NULL,
    [EVF_EVEID]  INT           NOT NULL,
    [EVF_EVFID]  INT           NOT NULL,
    [EVF_NOMBRE] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_EVENTOS_FOTOS] PRIMARY KEY CLUSTERED ([EVF_CODEMP] ASC, [EVF_EVEID] ASC, [EVF_EVFID] ASC),
    CONSTRAINT [FK_EVENTOS__EVENTOS_F_EVENTOS] FOREIGN KEY ([EVF_CODEMP], [EVF_EVEID]) REFERENCES [dbo].[EVENTOS] ([EVE_CODEMP], [EVE_EVEID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[EVENTOS_FOTOS]([EVF_CODEMP] ASC, [EVF_EVEID] ASC, [EVF_EVFID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta tabla almacena las distintas fotos, que seran enlazadas a un evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EVENTOS_FOTOS';

