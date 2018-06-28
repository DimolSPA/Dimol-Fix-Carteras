
/****** Object:  Table [dbo].[Log_Buscar_Facturas]    Script Date: 05-04-2017 17:02:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Log_Buscar_Facturas](
	[rut] [varchar](20) NULL,
	[cliente] [varchar](200) NULL,
	[factura] [varchar](20) NULL,
	[fecha] [datetime] NULL,
	[ruta] [varchar](200) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

