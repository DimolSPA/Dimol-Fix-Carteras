create Procedure [dbo].[_Insertar_Receptor_Poder_Judicial](
			@id_causa int,
			@cuaderno varchar(200),
			@estado varchar(200),
			@receptor varchar(200),
			@fecha datetime
)

 as   
declare @id_cuaderno int  = 0

set @id_cuaderno = (select top 1 id_cuaderno from poder_judicial_cuaderno where id_causa = @id_causa and desc_cuaderno = @cuaderno)    

if @id_cuaderno != 0 
begin        
 

INSERT INTO [PODER_JUDICIAL_RECEPTOR]
           ([ID_CAUSA]
           ,[ID_CUADERNO]
           ,[CUADERNO]
           ,[ESTADO]
           ,[RECEPTOR]
           ,[FECHA])
     VALUES
           (@id_causa
           ,@id_cuaderno
           ,@cuaderno
           ,@estado
           ,@receptor
           ,@fecha)



           
end






