using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using NTST.ScriptLinkService.Objects;

namespace NTST.ScriptLinkService.Web
{
    public static class CreateHTMLPage
    {

        public static string GetHTMLPage(OptionObject OptionObj)
        {
            var fileName = Path.GetRandomFileName() + ".html";
            var file = CreateFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Path"]) + "\\" + fileName);

            file.WriteLine(CreateHeader());
            file.WriteLine(CreateTable());

            file.WriteLine(CreateTableRow("SystemCode", OptionObj.SystemCode.ToString()));
            file.WriteLine(CreateTableRow("Facility", OptionObj.Facility));
            file.WriteLine(CreateTableRow("OptionUserID", OptionObj.OptionUserId));
            file.WriteLine(CreateTableRow("OptionStaffID", OptionObj.OptionStaffId));
            file.WriteLine(CreateTableRow("OptionID", OptionObj.OptionId));
            file.WriteLine(CreateTableRow("EntityID", OptionObj.EntityID));
            file.WriteLine(CreateTableRow("EpisodeNumber", OptionObj.EpisodeNumber.ToString()));
            file.WriteLine("<tr><td class=\"grey\" colspan=\"2\">Forms</td></tr>");

            string formtables = string.Empty;

            foreach (var optionForm in OptionObj.Forms)
            {
                formtables = CreateTable();
                formtables += CreateTableRow("FormID", optionForm.FormId);
                formtables += CreateTableRow("MultipleIteration", optionForm.MultipleIteration.ToString());
                formtables += "<tr><td class=\"grey\" colspan=\"2\">CurrentRow</td></tr>";

                string formFields;
                formFields = CreateTable();
                formFields += "<tr><td class=grey>FieldNumber</td><td class=grey>FieldValue</td><td class=grey>Enabled</td><td class=grey>Lock</td><td class=grey>Required</td></tr>";

                foreach (var optionField in optionForm.CurrentRow.Fields)
                {
                    formFields += "<tr><td>" + optionField.FieldNumber + "&nbsp;</td><td>" + optionField.FieldValue + "&nbsp;</td><td>" + optionField.Enabled + "&nbsp;</td><td>" + optionField.Lock + "&nbsp;</td><td>" + optionField.Required + "&nbsp;</td></tr>";
                }

                formFields += EndTable();

                formtables += CreateTableRow("", CreateTable() +
                                              CreateTableRow("RowID", optionForm.CurrentRow.RowId) +
                                              CreateTableRow("RowAction", optionForm.CurrentRow.RowAction) +
                                              CreateTableRow("ParentRowID", optionForm.CurrentRow.ParentRowId) +
                                              "<tr><td class=\"grey\" colspan=\"2\" style=\"width: 100%\">Fields</td></tr>" +
                                              CreateTableRow("", formFields) + EndTable());


                if (optionForm.OtherRows != null)
                {
                    formtables += "<tr><td class=\"grey\" colspan=\"2\">OtherRows</td></tr>";


                    foreach (var optionRow in optionForm.OtherRows)
                    {
                        formFields = CreateTable();
                        formFields += "<tr><td class=\"grey\">FieldNumber</td><td class=grey>FieldValue</td><td class=grey>Enabled</td><td class=grey>Lock</td><td class=grey>Required</td></tr>";

                        foreach (var optionField in optionRow.Fields)
                        {
                            formFields += "<tr><td>" + optionField.FieldNumber + "&nbsp;</td><td>" + optionField.FieldValue + "&nbsp;</td><td>" + optionField.Enabled + "&nbsp;</td><td>" + optionField.Lock + "&nbsp;</td><td>" + optionField.Required + "&nbsp;</td></tr>";
                        }
                        formFields += EndTable();

                        formtables += CreateTableRow("", CreateTable() +
                                                      CreateTableRow("RowID", optionRow.RowId) +
                                                      CreateTableRow("RowAction", optionRow.RowAction) +
                                                      CreateTableRow("ParentRowID", optionRow.ParentRowId) +
                                                      "<tr><td class=\"grey\" colspan=\"2\" style=\"width: 100%\">Fields</td></tr>" +
                                                      CreateTableRow("", formFields) +
                                                      EndTable());
                    }
                }

                formtables += EndTable();

                file.WriteLine(CreateTableRow("", formtables));
                formtables = string.Empty;
            }
            file.WriteLine(EndTable());
            file.Flush();
            file.Close();

            return (ConfigurationManager.AppSettings["Path"] + "/" + fileName);
        }


        private static StreamWriter CreateFile(string fileName)
        {
            var file = System.IO.File.CreateText(fileName);
            return file;
        }


        private static string CreateHeader()
        {
            var rtn = new StringBuilder(string.Empty);
            rtn.Append("<HTML>\r\n");
            rtn.Append("<head>\r\n");
            rtn.Append("<title></title>\r\n");
            rtn.Append("<style type=\"text/css\">\r\n");
            rtn.Append("   .style1\r\n");
            rtn.Append("   {\r\n");
            rtn.Append("    width: 100%;\r\n");
            rtn.Append("   }\r\n");
            rtn.Append("   .style2\r\n");
            rtn.Append("   {\r\n");
            rtn.Append("    width: 110px;\r\n");
            rtn.Append("   }\r\n");
            rtn.Append("   .style3\r\n");
            rtn.Append("   {\r\n");
            rtn.Append("    width: 101px;\r\n");
            rtn.Append("   }\r\n");
            rtn.Append("   td {padding: 1pt 3pt 2pt 3pt; border-style: solid; border-width: 1} \r\n");
            rtn.Append("   table {border-style: solid ; border-width: thin; border-color: White; border-collapse: collapse ; font-size: 11pt; text-align: justify } \r\n");
            rtn.Append("   table.grey {border-color: Silver; }\r\n");
            rtn.Append("   td.grey {background-color: Gray; color: white; border-color: Silver; font-weight: bold; text-align: center }\r\n");
            rtn.Append("</style>\r\n");
            rtn.Append("</head>\r\n");
            rtn.Append("<Body>\r\n");
            return rtn.ToString();
        }

        private static string CreateFooter()
        {
            return "</Body></HTML>";
        }

        private static string CreateTable()
        {
            return "<table class=\"style1\">";
        }

        private static string EndTable()
        {
            return "</table>";
        }

        private static string CreateTableRow(string Column1, string Column2)
        {
            var rtn = new StringBuilder(string.Empty);
            rtn.Append("<tr>");
            if (Column1.Length > 0)
            {
                rtn.Append("<td class=\"grey\">" + Column1 + "</td>");
            }
            else
            {
                rtn.Append("<td class=\"style2\">" + Column1 + "</td>");
            }
            rtn.Append("<td>" + Column2 + "&nbsp;</td>");
            rtn.Append("</tr>");

            return rtn.ToString();
        }

    }
}