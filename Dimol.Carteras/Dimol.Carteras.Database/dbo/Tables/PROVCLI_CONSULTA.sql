CREATE TABLE [dbo].[PROVCLI_CONSULTA] (
    [PCC_CODEMP]      INT          NOT NULL,
    [PCC_USRID]       NUMERIC (15) NOT NULL,
    [PCC_PCLID_VER]   NUMERIC (15) NOT NULL,
    [PCC_ESTADOS_VER] VARCHAR (50) NULL,
    CONSTRAINT [PK_PROVCLI_CONSULTA] PRIMARY KEY CLUSTERED ([PCC_CODEMP] ASC, [PCC_USRID] ASC, [PCC_PCLID_VER] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esta tabla almacena los clientes que puede consultar un usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PROVCLI_CONSULTA';

