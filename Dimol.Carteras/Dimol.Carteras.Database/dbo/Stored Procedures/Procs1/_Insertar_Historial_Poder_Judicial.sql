CREATE Procedure [dbo].[_Insertar_Historial_Poder_Judicial](
			@id_causa int,
			@id_cuaderno int,
			@folio int,
			@ruta_documento varchar(500),
			@etapa varchar(200),
			@tramite varchar(200),
			@desc_tramite varchar(200),
			@fecha_tramite datetime,
			@foja int
)

 as   
      
 

INSERT INTO [PODER_JUDICIAL_HISTORIAL]
           ([ID_CAUSA]
           ,[ID_CUADERNO]
           ,[FOLIO]
           ,[RUTA_DOCUMENTO]
           ,[ETAPA]
           ,[TRAMITE]
           ,[DESC_TRAMITE]
           ,[FECHA_TRAMITE]
           ,[FOJA]
           ,[FECHA_HISTORIAL])
     VALUES
           (@id_causa,
			@id_cuaderno,
			@folio,
			@ruta_documento,
			@etapa,
			@tramite,
			@desc_tramite,
			dateadd(hour,12,@fecha_tramite),
			@foja,
			GETDATE())
           





