namespace Dimol.Carteras.bcp
{
    public class Torta : Carteras.dto.Torta
    {
        public void ListarTortaAgrupada(dto.Torta obj)
        {
            dao.TortaAgrupada.ListarDocumentosDetalleTodo(obj);
        }
    }
}
