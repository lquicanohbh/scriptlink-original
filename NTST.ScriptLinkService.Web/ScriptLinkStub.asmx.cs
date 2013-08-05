using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NTST.ScriptLinkService.Objects;

namespace NTST.ScriptLinkService.Stub
{
    /// <summary>
    /// Summary description for ScriptLinkStub
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ScriptLinkStub : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetVersion()
        {
            return "Version 1.0";
        }

        [WebMethod]
        public OptionObject RunScript(OptionObject optionObject, String scriptName)
        {
            OptionObject returnOptionObject = new OptionObject();

            switch (scriptName)
            {
                case "YourScriptHere":
                    break;
                default:
                    break;
            }

            return returnOptionObject;
        }

        
    }
}
