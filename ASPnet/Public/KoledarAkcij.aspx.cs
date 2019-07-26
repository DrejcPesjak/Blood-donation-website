using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private SqlConnection con;
    private SqlDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";
        string ukaz = "SELECT akcija.datum_z, akcija.datum_k, posta.kraj, enota.naziv FROM posta INNER JOIN enota ON posta.postna_st = enota.posta INNER JOIN akcija ON enota.sifra = akcija.sifra_enote ";

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
                    DateTime datum_z = reader.GetDateTime(0);//DateTime.Parse(reader.GetString(0));
                    DateTime datum_k = reader.GetDateTime(1);//DateTime.Parse(reader.GetString(1));
                    string kraj_enote = reader.GetString(2);
                    string naziv_enote = reader.GetString(3);

                    koledarAkcijTabela.InnerHtml += AkcijaVTabelo(datum_z, datum_k, kraj_enote, naziv_enote);
                }
            }
        }
        catch (Exception ex)
        {
            
        }
        finally { con.Close(); }
    }

    private string AkcijaVTabelo(DateTime d_zacetek, DateTime d_konec, string kraj, string naziv)
    {
        string HTML =
            "<tr>" +
                "<td>" + d_zacetek.ToString("dd-MM-yyyy HH:mm") + "</td>" +
                "<td>" + d_konec.ToString("dd-MM-yyyy HH:mm") + "</td>" +
                "<td>" + kraj + "</td>" +
                "<td>" + naziv + "</td>" +
            "</tr>";

        return HTML;
    }
}