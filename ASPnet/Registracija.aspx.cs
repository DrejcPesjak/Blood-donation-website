using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    private SqlConnection con;
    private SqlDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ButtonRegistriraj_Click(object sender, EventArgs e)
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";


        string ukaz = "INSERT INTO admini(uporabnisko_ime, kriptirano_geslo) VALUES('" + TextBoxUser.Text + "', '" + FormsAuthentication.HashPasswordForStoringInConfigFile(TextBoxPass.Text, "SHA1") + "')";

        String connectionString = "data source=" + imeRacunalnika + "\\" + imeRazlicice +
                                      "; database=" + imeBaze + "; integrated security=SSPI";

        con = new SqlConnection(connectionString);
        con.Open();

        try
        {
            SqlCommand cmd = new SqlCommand(ukaz, con);
            cmd.ExecuteNonQuery();
            TextBoxUser.Text = TextBoxPass.Text = "";
        }
        catch
        {
            
        }
        finally
        {
            con.Close();
        }
    }
}