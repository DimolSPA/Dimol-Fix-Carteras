﻿CREATE TABLE [dbo].[PERFILES_MENU] (
    [PFM_CODEMP] INT NOT NULL,
    [PFM_PRFID]  INT NOT NULL,
    [PFM_TRVID]  INT NOT NULL,
    CONSTRAINT [PK_PERFILES_MENU] PRIMARY KEY CLUSTERED ([PFM_CODEMP] ASC, [PFM_PRFID] ASC, [PFM_TRVID] ASC),
    CONSTRAINT [FK_PERFILES_PERILES_M_PERFILES] FOREIGN KEY ([PFM_CODEMP], [PFM_PRFID]) REFERENCES [dbo].[PERFILES] ([PRF_CODEMP], [PRF_PRFID]),
    CONSTRAINT [FK_PERFILES_TREVIEW_P_TREEVIEW] FOREIGN KEY ([PFM_TRVID]) REFERENCES [dbo].[TREEVIEW] ([TRV_TRVID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PERFILES_MENU]([PFM_CODEMP] ASC, [PFM_PRFID] ASC, [PFM_TRVID] ASC);
