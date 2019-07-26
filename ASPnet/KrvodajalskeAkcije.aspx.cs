using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    private SqlConnection con;
    private SqlDataReader reader;
    private int DropDownSpremeni, DropDownIzbrisi;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Session["IsAlreadyLoad"] == null)        //se izvede le ob prvem nalaganju
        {
            CheckBoxDoDanes.Checked = RadioButtonID.Checked = true;            
            Session["IsAlreadyLoad"] = true;
        }        
        LabelWarning.Visible = false;

        //drop down list checked changed - kliče PageLoad, ta pa na novo napiše vse vrednosti in tako dobi DropDownListEx.SelectedValue neko privzeto vrednost (0)
        int num;
        if(int.TryParse(DropDownListIDSpremeni.SelectedValue, out num)) DropDownSpremeni = int.Parse(DropDownListIDSpremeni.SelectedValue);
        if (int.TryParse(DropDownListIDIzbrisi.SelectedValue, out num)) DropDownIzbrisi = int.Parse(DropDownListIDIzbrisi.SelectedValue);

        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";

        //razvrščanje tabele po stolpcih
        string ukaz = ""; string danes = ""; string vrstni_red = "";
        if (CheckBoxDoDanes.Checked)
        {
            danes = " WHERE akcija.datum_k > GETDATE()";
        }

        //order by - uredi po izbranem stoplcu
        if (sender == RadioButtonID || RadioButtonID.Checked)
        {
            vrstni_red = " ORDER BY akcija.sifra";
        }
        else if (sender == RadioButtonZDatum)
        {
            vrstni_red = " ORDER BY akcija.datum_z";
        }
        else if (sender == RadioButtonKDatum)
        {
            vrstni_red = " ORDER BY akcija.datum_k";
        }
        else if (sender == RadioButtonVrsta)
        {
            vrstni_red = " ORDER BY akcija.vrsta";
        }
        else if (sender == RadioButtonEnota)
        {
            vrstni_red = " ORDER BY enota.naziv";
        }
                
        ukaz = "SELECT akcija.sifra, akcija.datum_z, akcija.datum_k, akcija.vrsta, enota.naziv FROM enota INNER JOIN akcija ON enota.sifra = akcija.sifra_enote" + danes + vrstni_red;

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
                KrvodajalskeAkcije.InnerHtml = "";
                DropDownListIDSpremeni.Items.Clear();
                DropDownListIDIzbrisi.Items.Clear();

                while (reader.Read())
                {
                    int sifra = reader.GetInt32(0);
                    DropDownListIDSpremeni.Items.Add(new ListItem(sifra.ToString(), sifra.ToString()));
                    DropDownListIDIzbrisi.Items.Add(new ListItem(sifra.ToString(), sifra.ToString()));
                    DateTime datum_z = reader.GetDateTime(1);
                    DateTime datum_k = reader.GetDateTime(2);
                    string vrsta_akc = reader.GetString(3);
                    string naziv_enote = reader.GetString(4);

                    KrvodajalskeAkcije.InnerHtml += AkcijaVTabelo(sifra, datum_z, datum_k, vrsta_akc, naziv_enote);
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); }
    }

    private string AkcijaVTabelo(int sif, DateTime d_zacetek, DateTime d_konec, string vrsta, string naz_enota)
    {
        string HTML =
            "<tr>" +
                "<td>" + sif + "</td>" +
                "<td>" + d_zacetek.ToString("dd-MM-yyyy HH:mm") + "</td>" +
                "<td>" + d_konec.ToString("dd-MM-yyyy HH:mm") + "</td>" +
                "<td>" + vrsta + "</td>" +
                "<td>" + naz_enota + "</td>" +
            "</tr>";
        return HTML;
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)      //dodaj v bazo
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";
        string ukaz = "SELECT DISTINCT enota.sifra FROM enota INNER JOIN akcija ON enota.sifra=akcija.sifra_enote WHERE enota.naziv LIKE '" + TextBoxEnota.Text + "'";        //preveri če je vpisana enota veljavna

        String connectionString = "data source=" + imeRacunalnika + "\\" + imeRazlicice +
                                      "; database=" + imeBaze + "; integrated security=SSPI";

        con = new SqlConnection(connectionString);
        con.Open();

        reader = null; int sifra_enote = 0;
        try
        {
            SqlCommand cmd = new SqlCommand(ukaz, con);
            //reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

            using (SqlDataReader reader1 = cmd.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (reader1.Read())
                {
                    sifra_enote = reader1.GetInt32(0);                //prebere šifro enote
                }
                else
                {
                    LabelWarning.Visible = true;
                    LabelWarning.Text = "Opozorilo, vnešena enota ne obstaja!";
                }
            }            
        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); } 

        //-------------------------------------------------------------------------------------------------------------//
        
        if (!LabelWarning.Visible)
        { 
            ukaz = "SET DATEFORMAT dmy INSERT INTO akcija(datum_z,datum_k,vrsta,sifra_enote) VALUES('" + TextBoxZacetek.Text + "','" + TextBoxKonec.Text + "','" + TextBoxVrsta.Text + "','" + sifra_enote + "')";

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

        TextBoxZacetek.Text = TextBoxKonec.Text = TextBoxVrsta.Text = TextBoxEnota.Text = "";
    }
    protected void ButtonSpremeni_Click(object sender, EventArgs e)     //spremeni krvodajalsko akcijo
    {
        if (!(TextBoxSpremeniZD.Text == "" && TextBoxSpremeniKD.Text == ""))
        {
            string imeRacunalnika = "localhost";
            string imeRazlicice = "SQLEXPRESS";
            string imeBaze = "KRVODAJALSKA_AKCIJA";

            string ukaz = "SET DATEFORMAT dmy ";
            if (TextBoxSpremeniZD.Text != "" && TextBoxSpremeniKD.Text == "")       //spremeni samo začetni datum
            {
                ukaz += "UPDATE akcija SET akcija.datum_z = '" + TextBoxSpremeniZD.Text + "' WHERE akcija.sifra = " + DropDownSpremeni;
            }
            else if(TextBoxSpremeniZD.Text == "" && TextBoxSpremeniKD.Text != "")   //spremeni samo končni datum
            {
                ukaz += "UPDATE akcija SET akcija.datum_k = '" + TextBoxSpremeniKD.Text + "' WHERE akcija.sifra = " + DropDownSpremeni;
            }
            else                                                                    //spremeni oba datuma
            {
                ukaz += "UPDATE akcija SET akcija.datum_z = '" + TextBoxSpremeniZD.Text + "', akcija.datum_k = '" + TextBoxSpremeniKD.Text + "' WHERE akcija.sifra = " + DropDownSpremeni;
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

        TextBoxSpremeniZD.Text = TextBoxSpremeniKD.Text = "";
    }
    protected void ButtonIzbrisi_Click(object sender, EventArgs e)      //izbriši krvodajalsko akcijo
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";

        //preveri ali se kakšna dajatev sklicuje na izbrano akcijo
        string ukaz = "SELECT krvodajalec.ime, krvodajalec.priimek, oddaja_krvi.datum, oddaja_krvi.kolicina " +
                      "FROM akcija INNER JOIN oddaja_krvi ON akcija.sifra = oddaja_krvi.sifra_akcije LEFT JOIN krvodajalec ON krvodajalec.sifra = oddaja_krvi.sifra_krvodajalca " +
                      "WHERE akcija.sifra = " + DropDownIzbrisi;

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
                while (reader.Read())
                {
                    GridViewPotrebnoIzbrisati.Visible = true;

                    DataTable dt = new DataTable();     //nova podatkovna tabela
                    DataRow dr = null;
                    dt.Columns.Add(new DataColumn("Ime", typeof(string)));
                    dt.Columns.Add(new DataColumn("Priimek", typeof(string)));
                    dt.Columns.Add(new DataColumn("DatumOddaje", typeof(string)));
                    dt.Columns.Add(new DataColumn("Kolicina", typeof(string)));

                    dr = dt.NewRow();                   //nova vrstica
                    dr["Ime"] = reader.GetString(0);
                    dr["Priimek"] = reader.GetString(1);
                    dr["DatumOddaje"] = reader.GetDateTime(2).ToString("dd-MM-yyyy HH:mm");
                    dr["Kolicina"] = reader.GetInt32(3).ToString();
                    dt.Rows.Add(dr);
                    //dr = dt.NewRow();

                    //Store the DataTable in ViewState
                    ViewState["CurrentTable"] = dt;

                    GridViewPotrebnoIzbrisati.DataSource = dt;
                    GridViewPotrebnoIzbrisati.DataBind();
                }
            }
            else
            {
                reader.Close();
                ukaz = "DELETE FROM akcija WHERE akcija.sifra = " + DropDownIzbrisi;

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