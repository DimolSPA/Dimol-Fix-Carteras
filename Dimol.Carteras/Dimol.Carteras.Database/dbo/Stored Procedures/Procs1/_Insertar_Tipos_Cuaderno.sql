CREATE Procedure [dbo].[_Insertar_Tipos_Cuaderno](
			@ID_CAUSA INTEGER,
			@ID_cuaderno integer, 
			@desc_cuaderno varchar (150),
			@esc_pend varchar(1)
)

 as   
declare @id int  

set @id = (select COUNT(*) from [PODER_JUDICIAL_CUADERNO] where ID_cuaderno = @ID_cuaderno)    

if @id = 0 
begin        
 
 INSERT INTO [PODER_JUDICIAL_CUADERNO]
           ([ID_CAUSA],[ID_CUADERNO]
           ,[DESC_CUADERNO]
           ,ESCRITOS_PENDIENTES)
     VALUES
           (@ID_CAUSA
           ,@ID_cuaderno
           ,@desc_cuaderno
           ,@esc_pend)
           
end
else
begin
	update [PODER_JUDICIAL_CUADERNO] set
	ESCRITOS_PENDIENTES = @esc_pend
	where [ID_CAUSA] = @ID_CAUSA and [ID_CUADERNO] = @ID_cuaderno
end