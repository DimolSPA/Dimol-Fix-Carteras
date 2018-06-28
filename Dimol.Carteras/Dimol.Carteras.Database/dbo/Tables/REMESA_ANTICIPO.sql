CREATE TABLE [dbo].[REMESA_ANTICIPO] (
    [REMESA_ANTICIPO_ID] INT             NOT NULL,
    [REMESA_ID]          INT             NOT NULL,
    [CODEMP]             INT             NOT NULL,
    [PCLID]              INT             NOT NULL,
    [CTCID]              INT             NOT NULL,
    [ANTICIPO]           DECIMAL (15, 2) NOT NULL,
    [DEBITADO]           DECIMAL (15, 2) NOT NULL,
    [DOCUMENTOID]        INT             NOT NULL,
    [USRID_REGISTRO]     INT             NOT NULL,
    [FEC_REGISTRO]       DATETIME        CONSTRAINT [DF_REMESA_ANTICIPO_FEC_REGISTRO] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_REMESA_ANTICIPO] PRIMARY KEY CLUSTERED ([REMESA_ANTICIPO_ID] ASC)
);

