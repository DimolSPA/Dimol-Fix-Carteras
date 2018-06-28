CREATE TABLE [dbo].[pbcattbl] (
    [pbt_tnam] CHAR (129)    NOT NULL,
    [pbt_tid]  INT           NULL,
    [pbt_ownr] CHAR (129)    NOT NULL,
    [pbd_fhgt] SMALLINT      NULL,
    [pbd_fwgt] SMALLINT      NULL,
    [pbd_fitl] CHAR (1)      NULL,
    [pbd_funl] CHAR (1)      NULL,
    [pbd_fchr] SMALLINT      NULL,
    [pbd_fptc] SMALLINT      NULL,
    [pbd_ffce] CHAR (18)     NULL,
    [pbh_fhgt] SMALLINT      NULL,
    [pbh_fwgt] SMALLINT      NULL,
    [pbh_fitl] CHAR (1)      NULL,
    [pbh_funl] CHAR (1)      NULL,
    [pbh_fchr] SMALLINT      NULL,
    [pbh_fptc] SMALLINT      NULL,
    [pbh_ffce] CHAR (18)     NULL,
    [pbl_fhgt] SMALLINT      NULL,
    [pbl_fwgt] SMALLINT      NULL,
    [pbl_fitl] CHAR (1)      NULL,
    [pbl_funl] CHAR (1)      NULL,
    [pbl_fchr] SMALLINT      NULL,
    [pbl_fptc] SMALLINT      NULL,
    [pbl_ffce] CHAR (18)     NULL,
    [pbt_cmnt] VARCHAR (254) NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [pbcatt_x]
    ON [dbo].[pbcattbl]([pbt_tnam] ASC, [pbt_ownr] ASC);

