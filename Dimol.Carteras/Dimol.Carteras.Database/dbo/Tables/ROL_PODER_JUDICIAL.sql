CREATE TABLE [dbo].[ROL_PODER_JUDICIAL] (
    [RPJ_CODEMP]       INT         NULL,
    [RPJ_ROLID]        INT         NULL,
    [RPJ_TIPO]         VARCHAR (1) NULL,
    [RPJ_ID_CAUSA]     INT         NOT NULL,
    [RPJ_FECH_ULT_ACT] DATETIME    NOT NULL,
    [RPJ_TRIBUNAL]     INT         NULL
);

