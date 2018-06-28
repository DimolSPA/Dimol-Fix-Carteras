using RazorEngine.Templating;
using System.IO;
using System.Reflection;

namespace Dimol.Email.Templating
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
        public static string Render<T>(string Bones, T Model)
        {
            var Template = RazorEngine.Engine.Razor.Run(Bones, null, Model);
            if (string.IsNullOrEmpty(Template))
            {
                return null;
            }
            return Template;
        }

        /// <summary>
        /// Find template in server
        /// </summary>
        /// <param name="Name">Template Name</param>
        /// <returns>File Path</returns>
        public static string GetTemplate(string Name)
        {
            string DirPath = Assembly.GetExecutingAssembly().Location;
            DirPath = Path.GetDirectoryName(DirPath);
            return Path.GetFullPath(Path.Combine(DirPath, "/Templates/" + Name + ".cshtml"));
        }
    }
}
