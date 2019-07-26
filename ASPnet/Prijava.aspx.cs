using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{    
    private SqlConnection con;
    private SqlDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelWarning.Visible = false;
    }    
    protected void Button1_Click(object sender, EventArgs e)        //prijava
    {
        
        /*if(FormsAuthentication.Authenticate(TextBoxUser.Text, TextBoxPass.Text))
        {
            FormsAuthentication.RedirectFromLoginPage(TextBoxUser.Text, false);
        }
        else
        {
            LabelWarning.Visible = true;
        }*
    }*/

        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";


        string ukaz = "SELECT * " +
                      "FROM admini " +
                      "WHERE uporabnisko_ime LIKE '" + TextBoxUser.Text + "' AND kriptirano_geslo LIKE '" + FormsAuthentication.HashPasswordForStoringInConfigFile(TextBoxPass.Text, "SHA1") + "'";

        String connectionString = "data source=" + imeRacunalnika + "\\" + imeRazlicice +
                                      "; database=" + imeBaze + "; integrated security=SSPI";

        con = new SqlConnection(connectionString);
        con.Open();

        try
        {
            SqlCommand cmd = new SqlCommand(ukaz, con);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                FormsAuthentication.RedirectFromLoginPage(TextBoxUser.Text, false);
            }                        
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "Neveljavna registracija! Poskusite znova.", true);
        }
        finally
        {
            con.Close();
        }
    }
}