namespace Dimol.Email.dto.MailModels
{
    public class EmailTemplate
    {
        public int TemplateId { get; set; }
        public int Cliente { get; set; }
        public string Alias { get; set; }
        public string Filename { get; set; }
    }
}
