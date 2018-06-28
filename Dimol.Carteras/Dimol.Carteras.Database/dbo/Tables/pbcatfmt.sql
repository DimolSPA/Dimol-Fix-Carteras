CREATE TABLE [dbo].[pbcatfmt] (
    [pbf_name] VARCHAR (30)  NOT NULL,
    [pbf_frmt] VARCHAR (254) NULL,
    [pbf_type] SMALLINT      NULL,
    [pbf_cntr] INT           NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [pbcatf_x]
    ON [dbo].[pbcatfmt]([pbf_name] ASC);

