using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    private SqlConnection con;
    private SqlDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";
        string ukaz = "SELECT enota.naziv, akcija.datum_z, akcija.datum_k, akcija.sifra FROM akcija INNER JOIN enota ON akcija.sifra_enote = enota.sifra ";

        String connectionString = "data source=" + imeRacunalnika + "\\" + imeRazlicice +
                                      "; database=" + imeBaze + "; integrated security=SSPI";

        con = new SqlConnection(connectionString);
        con.Open();

        reader = null;
        try
        {
            SqlCommand cmd = new SqlCommand(ukaz, con);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                DropDownListAkcije.Items.Clear();
                while (reader.Read())
                {
                    DropDownListAkcije.Items.Add(new ListItem(reader.GetString(0) + "; " + reader.GetDateTime(1).ToString("dd-MM-yyyy HH:mm") + " - " + reader.GetDateTime(2).ToString("dd-MM-yyyy HH:mm"), reader.GetInt32(3).ToString()));
                }
            }
        }
        catch
        {

        }
        finally { con.Close(); }
    }
    protected void DropDownListAkcije_SelectedIndexChanged(object sender, EventArgs e)
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";
        string ukaz = "SELECT COUNT(sifra_akcije), SUM(kolicina) FROM AKCIJA INNER JOIN ODDAJA_KRVI ON AKCIJA.sifra = ODDAJA_KRVI.sifra_akcije WHERE AKCIJA.sifra = " + DropDownListAkcije.SelectedValue;

        String connectionString = "data source=" + imeRacunalnika + "\\" + imeRazlicice +
                                      "; database=" + imeBaze + "; integrated security=SSPI";

        con = new SqlConnection(connectionString);
        con.Open();

        reader = null;
        try
        {
            SqlCommand cmd = new SqlCommand(ukaz, con);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StatAkcija.InnerHtml = "<p> Število krvodajalcev na izbrani akciji: " + reader.GetInt32(0) + "<br/> Količina zbrane krvi na tej akciji: " + reader.GetInt32(1) + "ml</p>";
                }
            }
        }
        catch
        {

        }
        finally { con.Close(); }
    }
}