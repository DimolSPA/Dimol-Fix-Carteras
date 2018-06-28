﻿CREATE TABLE [dbo].[HELP] (
    [HLP_TRVID] INT NOT NULL,
    [HLP_HLPID] INT NOT NULL,
    CONSTRAINT [PK_HELP] PRIMARY KEY CLUSTERED ([HLP_TRVID] ASC, [HLP_HLPID] ASC),
    CONSTRAINT [FK_HELP_TREEVIEW__TREEVIEW] FOREIGN KEY ([HLP_TRVID]) REFERENCES [dbo].[TREEVIEW] ([TRV_TRVID])
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRI1]
    ON [dbo].[HELP]([HLP_TRVID] ASC, [HLP_HLPID] ASC);
