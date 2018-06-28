﻿CREATE TABLE [dbo].[PERFILES_IDIOMAS] (
    [PFI_CODEMP] INT           NOT NULL,
    [PFI_PRFID]  INT           NOT NULL,
    [PFI_IDID]   INT           NOT NULL,
    [PFI_NOMBRE] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_PERFILES_IDIOMAS] PRIMARY KEY CLUSTERED ([PFI_CODEMP] ASC, [PFI_PRFID] ASC, [PFI_IDID] ASC),
    CONSTRAINT [FK_PERFILES_IDIOMAS_P_IDIOMAS] FOREIGN KEY ([PFI_IDID]) REFERENCES [dbo].[IDIOMAS] ([IDI_IDID]),
    CONSTRAINT [FK_PERFILES_PERFILE_I_PERFILES] FOREIGN KEY ([PFI_CODEMP], [PFI_PRFID]) REFERENCES [dbo].[PERFILES] ([PRF_CODEMP], [PRF_PRFID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[PERFILES_IDIOMAS]([PFI_CODEMP] ASC, [PFI_PRFID] ASC, [PFI_IDID] ASC);

