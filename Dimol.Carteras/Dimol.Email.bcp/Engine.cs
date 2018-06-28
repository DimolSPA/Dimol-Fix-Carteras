using Dimol.Email.dto.MailModels;
using RazorEngine.Templating;
using System;
using System.IO;

namespace Dimol.Email.bcp
{
    /// <summary>
    /// RazorEngine Implementation handle email templates
    /// </summary>
    public static class Engine
    {
        /// <summary>
        /// Render email template using .cshtml file and model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Bones">Html base markup</param>
        /// <param name="Model">Template related model</param>
        /// <returns>Email template</returns>
        public static string Render<T>(string Bones, BaseEmailModel<T> Model)
        {
            string Template;
            using (var service = new TemplateService())
            {  
                Template = service.Parse(Bones, Model, null, "446e72cb-fb60-46c5-b333-514dbe5e9431");
            }
           return Template;
        }

        /// <summary>
        /// Find template in server
        /// </summary>
        /// <param name="Name">Template Name</param>
        /// <returns>File Path</returns>
        public static string GetTemplate(int Cliente, string Name)
        {
            var ParentDirPath = GetTemplatesPath();
            return Path.Combine(ParentDirPath, "Dimol.Email.bcp", "Templates", Cliente.ToString(), Name + ".cshtml");
        }

        /// <summary>
        /// Returns Tamplate content as string
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>string</returns>
        public static string GetTemplateContent(int pclid, string filename)
        {
            try {
                string TemplatePath = GetTemplate(pclid, filename);
                return File.ReadAllText(TemplatePath);
            }catch(Exception E)
            {
                Console.WriteLine(E.StackTrace);
            }
            return null;
        }


        public static string GetTemplatesPath()
        {
            string DirPath = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetParent(DirPath).Parent.FullName;
        }
    }
}
