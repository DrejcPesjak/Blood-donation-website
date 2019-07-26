using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Default4 : System.Web.UI.Page
{
    private SqlConnection con;
    private SqlDataReader reader;
    private int DropDownSpremeni, DropDownIzbrisi;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Session["IsAlreadyLoad"] == null)        //se izvede le ob prvem nalaganju
        {
            RadioButtonID.Checked = true;
            Session["IsAlreadyLoad"] = true;
        }
        LabelWarning.Visible = LabelWarningDelete.Visible = false;

        //drop down list checked changed - kliče PageLoad, ta pa na novo napiše vse vrednosti in tako dobi DropDownListEx.SelectedValue neko privzeto vrednost (0)
        int num;
        if (int.TryParse(DropDownListIDSpremeni.SelectedValue, out num)) DropDownSpremeni = int.Parse(DropDownListIDSpremeni.SelectedValue);
        if (int.TryParse(DropDownListIDIzbrisi.SelectedValue, out num)) DropDownIzbrisi = int.Parse(DropDownListIDIzbrisi.SelectedValue);

        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";

        //razvrščanje tabele po stolpcih
        string ukaz = ""; string vrstni_red = "";
        
        //order by - uredi po izbranem stoplcu
        if (sender == RadioButtonID || RadioButtonID.Checked)
        {
            vrstni_red = " ORDER BY sifra";
        }
        else if (sender == RadioButtonNaziv)
        {
            vrstni_red = " ORDER BY naziv";
        }
        else if (sender == RadioButtonNaslov)
        {
            vrstni_red = " ORDER BY naslov";
        }
        else if (sender == RadioButtonPosta)
        {
            vrstni_red = " ORDER BY posta";
        }
        else if (sender == RadioButtonKraj)
        {
            vrstni_red = " ORDER BY kraj";
        }
        else if (sender == RadioButtonTel)
        {
            vrstni_red = " ORDER BY telefonska_st";
        }

        ukaz = "SELECT sifra, naziv, naslov, posta, kraj, telefonska_st FROM ENOTA INNER JOIN POSTA ON ENOTA.posta = POSTA.postna_st" + vrstni_red;

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
                KrvodajalskeEnote.InnerHtml = "";
                DropDownListIDSpremeni.Items.Clear();
                DropDownListIDIzbrisi.Items.Clear();

                while (reader.Read())
                {
                    int sifra = reader.GetInt32(0);
                    DropDownListIDSpremeni.Items.Add(new ListItem(sifra.ToString(), sifra.ToString()));
                    DropDownListIDIzbrisi.Items.Add(new ListItem(sifra.ToString(), sifra.ToString()));
                    string naziv = reader.GetString(1);
                    string naslov = reader.GetString(2);
                    string posta = reader.GetString(3);
                    string kraj = reader.GetString(4);
                    string telefonska = reader.GetString(5);

                    KrvodajalskeEnote.InnerHtml += AkcijaVTabelo(sifra, naziv, naslov, posta, kraj, telefonska);
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); }
    }

    private string AkcijaVTabelo(int sif, string naziv, string naslov, string posta, string kraj, string tel)
    {
        string HTML =
            "<tr>" +
                "<td>" + sif + "</td>" +
                "<td>" + naziv + "</td>" +
                "<td>" + naslov + "</td>" +
                "<td>" + posta + "</td>" +
                "<td>" + kraj + "</td>" +                
                "<td>" + tel + "</td>" +
            "</tr>";
        return HTML;
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)      //dodaj v bazo
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";
        string ukaz = "SELECT postna_st FROM posta WHERE postna_st LIKE '" + TextBoxPosta.Text + "'";        //preveri če je vpisana pošta veljavna

        String connectionString = "data source=" + imeRacunalnika + "\\" + imeRazlicice +
                                      "; database=" + imeBaze + "; integrated security=SSPI";

        con = new SqlConnection(connectionString);
        con.Open();

        reader = null; 
        try
        {
            SqlCommand cmd = new SqlCommand(ukaz, con);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)         //true če je vpisana pošta veljavna
            {
                ukaz = "INSERT INTO [dbo].[ENOTA]([naziv],[naslov],[telefonska_st],[posta]) VALUES ('" + TextBoxNaziv.Text + "', '" + TextBoxNaslov.Text + "', '" + TextBoxTel.Text + "', '" + TextBoxPosta.Text + "')";

                cmd = new SqlCommand(ukaz, con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                LabelWarning.Visible = true;
                LabelWarning.Text = "Opozorilo, vnešena poštna številka ne obstaja na našem seznamu!";
            }
            
        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); }

        TextBoxNaziv.Text = TextBoxNaslov.Text = TextBoxTel.Text = TextBoxPosta.Text = "";
    }
    protected void ButtonSpremeni_Click(object sender, EventArgs e)     //spremeni krvodajalsko akcijo
    {
        if (!(TextBoxSpremeniNaziv.Text == "" && TextBoxSpremeniTele.Text == "")) 
        {
            string imeRacunalnika = "localhost";
            string imeRazlicice = "SQLEXPRESS";
            string imeBaze = "KRVODAJALSKA_AKCIJA";

            string ukaz;
            if (TextBoxSpremeniNaziv.Text != "" && TextBoxSpremeniTele.Text == "")       //spremeni samo naziv enote
            {
                ukaz = "UPDATE enota SET naziv = '" + TextBoxSpremeniNaziv.Text + "' WHERE enota.sifra = " + DropDownSpremeni;
            }
            else if (TextBoxSpremeniNaziv.Text == "" && TextBoxSpremeniTele.Text != "")   //spremeni samo telefonsko
            {
                ukaz = "UPDATE enota SET telefonska_st = '" + TextBoxSpremeniTele.Text + "' WHERE enota.sifra = " + DropDownSpremeni;
            }
            else                                                                    //spremeni oba atributa
            {
                ukaz = "UPDATE enota SET naziv = '" + TextBoxSpremeniNaziv.Text + "', telefonska_st = '" + TextBoxSpremeniTele.Text + "' WHERE enota.sifra = " + DropDownSpremeni;
            }

            String connectionString = "data source=" + imeRacunalnika + "\\" + imeRazlicice +
                                          "; database=" + imeBaze + "; integrated security=SSPI";

            con = new SqlConnection(connectionString);
            con.Open();

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

        TextBoxSpremeniNaziv.Text = TextBoxSpremeniTele.Text = "";
    }
    protected void ButtonIzbrisi_Click(object sender, EventArgs e)      //izbriši krvodajalsko akcijo
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";

        //preveri ali se kakšna akcija sklicuje na izbrano enoto
        string ukaz = "SELECT akcija.sifra " +
                      "FROM akcija INNER JOIN enota ON akcija.sifra_enote = enota.sifra" +
                      "WHERE enota.sifra = " + DropDownIzbrisi;

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
            {                                                 //najdena je bila datajatev, ki se navezuje na izbrano akcijo
                //najdene dajatve se izpišejo in je potrebno predhodno izbrisati

                LabelWarningDelete.Visible = true;
                LabelWarningDelete.Text = "Pred izbrisom izbrane enote je potrebno izbrisati naslednje akcije: ";
                while (reader.Read())
                {
                    LabelWarningDelete.Text += reader.GetString(0);
                }
            }
            else
            {
                reader.Close();
                ukaz = "DELETE FROM enota WHERE sifra = " + DropDownIzbrisi;

                cmd.CommandText = ukaz;
                cmd.ExecuteNonQuery();
            }
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