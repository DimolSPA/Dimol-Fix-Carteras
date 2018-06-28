using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace Mvc.HtmlHelpers
{
    public enum OPERATION
    {
        none,
        add,
        del,
        edit,
        excel
    }

    [ModelBinder(typeof(GridModelBinder))]
    public class GridSettings
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string sortColumn { get; set; }
        public string sortOrder { get; set; }
        public bool isSearch { get; set; }
        public string id { get; set; }
        public string param { get; set; }
        public string editOper { get; set; }
        public string addOper { get; set; }
        public string delOper { get; set; }
        public Filter where { get; set; }
        public OPERATION operation { get; set; }

        public string searchField { get; set; }
        public string searchOper { get; set; }
        public string searchString { get; set; }
    }

    public class GridModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            return new GridSettings()
            {
                isSearch = bool.Parse(request["_search"] ?? "false"),
                pageIndex = int.Parse(request["page"] ?? "1"),
                pageSize = int.Parse(request["rows"] ?? "10"),
                sortColumn = request["sidx"] ?? "",
                sortOrder = request["sord"] ?? "asc",
                id = request["id"] ?? "",
                param = request["oper"] ?? "",
                editOper = request["edit"] ?? "",
                addOper = request["add"] ?? "",
                delOper = request["del"] ?? "",
                searchField = request["searchField"] ?? "",
                searchOper = request["searchOper"] ?? "",
                searchString = request["searchString"] ?? "",
                where = new Filter(request["searchField"], request["searchOper"], request["searchString"], bool.Parse(request["_search"] ?? "false")),//Filter.Create(request["filters"] ?? ""),
                operation = (OPERATION)System.Enum.Parse(typeof(OPERATION), request["oper"] ?? "none")
            };
        }

    }

    [DataContract]
    public class Filter
    {
        [DataMember]
        public string groupOp { get; set; }
        [DataMember]
        public Rule[] rules { get; set; }

        public static Filter Create(string jsonData)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(Filter));
                System.IO.StringReader reader = new System.IO.StringReader(jsonData);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Default.GetBytes(jsonData));
                return serializer.ReadObject(ms) as Filter;
            }
            catch
            {
                return null;
            }
        }

        public Filter(string searchField, string searchOper, string searchString, bool isSearch)
        {
            try
            {
                string where = "";
                if (isSearch)
                {
                    
                    string oper = "";
                    string[] arrSearch1 = { "eq", "=" }; //equal
                    string[] arrSearch2 = { "ne", "<>" };//not equal
                    string[] arrSearch3 = { "lt", "<" }; //less than
                    string[] arrSearch4 = { "le", "<=" };//less than or equal
                    string[] arrSearch5 = { "gt", ">" }; //greater than
                    string[] arrSearch6 = { "ge", ">=" };//greater than or equal
                    string[] arrSearch7 = { "bw", "LIKE" }; //begins with
                    string[] arrSearch8 = { "bn", "NOT LIKE" }; //doesn't begin with
                    string[] arrSearch9 = { "in", "LIKE" }; //is in
                    string[] arrSearch10 = { "ni", "NOT LIKE" }; //is not in
                    string[] arrSearch11 = { "ew", "LIKE" }; //ends with
                    string[] arrSearch12 = { "en", "NOT LIKE" }; //doesn't end with
                    string[] arrSearch13 = { "cn", "LIKE" }; // contains
                    string[][] arrSearch = { arrSearch1, arrSearch2, arrSearch3, arrSearch4, arrSearch5, arrSearch6, arrSearch7, arrSearch8,
                                           arrSearch9, arrSearch10, arrSearch11, arrSearch12, arrSearch13 };

                    for (int i = 0; i < arrSearch.Length; i++)
                    {
                        if (searchOper == arrSearch[i][0])
                        {
                            oper = arrSearch[i][1];
                        }
                    }

                    if (searchOper == "bw" || searchOper == "bn") searchString += "%";
                    if (searchOper == "ew" || searchOper == "en") searchString = "%" + searchString;
                    if (searchOper == "cn" || searchOper == "nc" || searchOper == "in" || searchOper == "ni") searchString = "%" + searchString + "%";
                    where = " AND " + searchField + " " + oper + " '" + searchString + "'";
                }

                this.groupOp = where;
            }
            catch
            {
                this.groupOp = "";
            }
        }
    }

    [DataContract]
    public class Rule
    {
        [DataMember]
        public string field { get; set; }
        [DataMember]
        public string op { get; set; }
        [DataMember]
        public string data { get; set; }
    }
}