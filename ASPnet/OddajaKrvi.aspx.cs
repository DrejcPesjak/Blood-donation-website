using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Default2 : System.Web.UI.Page
{
    private SqlConnection con;
    private SqlDataReader reader;
    private string DropDownIzbrisi, DropDownDodKrvDaj, DropDownDodAkc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Session["IsAlreadyLoad"] == null)        //se izvede le ob prvem nalaganju
        {
            RadioButtonIDKrvDaj.Checked = true;
            Session["IsAlreadyLoad"] = true;
        }
        LabelWarning.Visible = false;
        
        //drop down list checked changed - kliče PageLoad, ta pa na novo napiše vse vrednosti in tako dobi DropDownListEx.SelectedValue neko privzeto vrednost (0)
        int num;
        if (int.TryParse(DropDownListIDIzbrisi.SelectedValue, out num)) DropDownIzbrisi = DropDownListIDIzbrisi.SelectedValue;
        if (int.TryParse(DropDownListKrvDaj.SelectedValue, out num)) DropDownDodKrvDaj = DropDownListKrvDaj.SelectedValue;
        if (int.TryParse(DropDownListAkc.SelectedValue, out num)) DropDownDodAkc = DropDownListAkc.SelectedValue;

        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";

        //razvrščanje tabele po stolpcih
        string ukaz = ""; string vrstni_red = "";
       
        //order by - uredi po izbranem stoplcu
        if (sender == RadioButtonIDKrvDaj || RadioButtonIDKrvDaj.Checked) { vrstni_red = " ORDER BY sifra"; }
        else if (sender == RadioButtonIme) { vrstni_red = " ORDER BY ime"; }
        else if (sender == RadioButtonPriimek) { vrstni_red = " ORDER BY priimek"; }
        else if (sender == RadioButtonDatum) { vrstni_red = " ORDER BY EMSO"; }
        else if (sender == RadioButtonKol) { vrstni_red = " ORDER BY kolicina"; }
        else if (sender == RadioButtonIDAkc) { vrstni_red = " ORDER BY sifra_akcije"; }

        ukaz = "SELECT sifra_krvodajalca, ime, priimek, datum, kolicina, sifra_akcije FROM oddaja_krvi INNER JOIN krvodajalec ON oddaja_krvi.sifra_krvodajalca = krvodajalec.sifra" + vrstni_red;

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
                OddajaKrvi.InnerHtml = "";
                DropDownListIDIzbrisi.Items.Clear();
                DropDownListKrvDaj.Items.Clear();
                DropDownListAkc.Items.Clear();

                while (reader.Read())
                {
                    int sifraKrvodajalca = reader.GetInt32(0);                    
                    string ime = reader.GetString(1);
                    string priimek = reader.GetString(2);
                    DateTime datum = reader.GetDateTime(3);
                    int kolicina = reader.GetInt32(4);
                    int sifraAkcije = reader.GetInt32(5);

                    DropDownListIDIzbrisi.Items.Add(new ListItem(sifraKrvodajalca.ToString("00") + " | " + datum.ToString("dd-MM-yyyy HH:mm"), sifraKrvodajalca.ToString() + " AND datum LIKE '" + datum.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    DropDownListKrvDaj.Items.Add(new ListItem(sifraKrvodajalca.ToString(), sifraKrvodajalca.ToString()));
                    DropDownListAkc.Items.Add(new ListItem(sifraAkcije.ToString(), sifraAkcije.ToString()));

                    OddajaKrvi.InnerHtml += AkcijaVTabelo(sifraKrvodajalca, ime, priimek, datum, kolicina, sifraAkcije);
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); }
    }

    private string AkcijaVTabelo(int sifKrvDaj, string ime, string priimek, DateTime datum, int kolicina, int sifAkc)
    {
        string HTML =
            "<tr>" +
                "<td>" + sifKrvDaj + "</td>" +
                "<td>" + ime + "</td>" +
                "<td>" + priimek + "</td>" +
                "<td>" + datum + "</td>" +
                "<td>" + kolicina + "</td>" +
                "<td>" + sifAkc + "</td>" +
            "</tr>";
        return HTML;
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)      //dodaj v bazo
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";
        string ukaz = "SET DATEFORMAT dmy INSERT INTO [dbo].[ODDAJA_KRVI]([datum],[sifra_krvodajalca],[kolicina],[sifra_akcije]) VALUES('" + TextBoxDatum.Text + "', " + DropDownDodKrvDaj + ", " + TextBoxKol.Text + ", " + DropDownDodAkc + ")";        

        String connectionString = "data source=" + imeRacunalnika + "\\" + imeRazlicice +
                                      "; database=" + imeBaze + "; integrated security=SSPI";

        con = new SqlConnection(connectionString);
        con.Open();

        reader = null; 
        try
        {
            SqlCommand cmd = new SqlCommand(ukaz, con);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); }

        TextBoxDatum.Text = TextBoxKol.Text = "";
    }
   
    protected void ButtonIzbrisi_Click(object sender, EventArgs e)      //izbriši krvodajalsko akcijo
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";

        //preveri ali se kakšna dajatev sklicuje na izbrano akcijo
        string ukaz = "SET DATEFORMAT dmy ";
        ukaz += "DELETE FROM oddaja_krvi WHERE sifra_krvodajalca = " + DropDownIzbrisi + "'";     //FORMAT DropDownIzbrisi:  "12 AND datum LIKE '24-12-2017 09:00"

        String connectionString = "data source=" + imeRacunalnika + "\\" + imeRazlicice +
                                      "; database=" + imeBaze + "; integrated security=SSPI";

        con = new SqlConnection(connectionString);
        con.Open();

        reader = null;
        try
        {
            SqlCommand cmd = new SqlCommand(ukaz, con);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); }
    }
    protected void CheckedChangedAll(object sender, EventArgs e)
    {
        Page_Load(sender, e);
    }
}