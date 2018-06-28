CREATE TABLE [dbo].[pbcatvld] (
    [pbv_name] VARCHAR (30)  NOT NULL,
    [pbv_vald] VARCHAR (254) NULL,
    [pbv_type] SMALLINT      NULL,
    [pbv_cntr] INT           NULL,
    [pbv_msg]  VARCHAR (254) NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [pbcatv_x]
    ON [dbo].[pbcatvld]([pbv_name] ASC);

