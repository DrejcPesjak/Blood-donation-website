using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {            
            asideLoggedIn.Attributes.Add("style", "display:none;");
        }
        
        /* testSpace.Attributes.Add("style", "text-align: center;");
         * testSpace.Attributes.Add("class", "centerIt");
         * testSpace.Attributes["style"] = "text-align: center;";
         * testSpace.Attributes["class"] = "centerIt";
         * testSpace.Style.Add("display", "none");
         * testSpace.Style["background-image"] = "url(images/foo.png)";
         * testSpace.Style.Item("display") = "none"*/
    }
    protected void ButtonOdjava_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
}
