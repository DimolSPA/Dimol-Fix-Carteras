using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dimol.ApiServices.Models
{
    public class FormularioModel
    {
        public DateTime FechaVisita { get; set; }
        public string Gestor { get; set; }
        public int FormularioId { get; set; }
        public string Deudor { get; set; }
        public string NombreFormulario { get; set; }
        public string Posicion { get; set; }
        public string Direccion { get; set; }
        public string Foto1 { get; set; }
        public string Foto2 { get; set; }
        public string Foto3 { get; set; }
        public string Foto4 { get; set; }
        public string EstadoVisita { get; set; }
        public string Visita { get; set; }
        public string DireccionActual { get; set; }
        public string Comentarios { get; set; }
        public string Direccion1 { get; set; }
        public string Comuna1 { get; set; }
        public string Direccion2 { get; set; }
        public string Comuna2 { get; set; }
        public string Procesado { get; set; }
        public string mensaje { get; set; }
    }
    public class FormularioID
    {
        public string formId { get; set; } 
    }

    public class Photos
    {
        public string c2 { get; set; }
        public string c6 { get; set; }
        public string c12 { get; set; }
        public string c18 { get; set; }
    }

    public class Coordinates
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class FormObject
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string device { get; set; }
        public string poiName { get; set; }
        public int ts { get; set; }
        public Photos photos { get; set; }
        public Coordinates coordinates { get; set; }
        public string address { get; set; }
        public string form { get; set; }
    }
    public class Field
    {
        public string label { get; set; }
        public string field_type { get; set; }
        public bool required { get; set; }
        public object field_options { get; set; }
        public string cid { get; set; }
        public object value { get; set; }
    }

    public class FormItemObject
    {
        public List<Field> fields { get; set; }
    }
    //si field_type = "radio"
    public class Option
    {
        public string label { get; set; }
        public bool @checked { get; set; }
    }

    public class RadioObject
    {
        public List<Option> options { get; set; }
    }
    //si field_type = "dropdown"
    public class DropdownObject
    {
        public List<Option> options { get; set; }
        public bool include_blank_option { get; set; }
    }
    public class PostObtenerFormId
    {
        public string user { get; set; }
        public string pass { get; set; }
        public string formId { get; set; }
    }
}