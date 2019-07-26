using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Default3 : System.Web.UI.Page
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
        LabelWarning.Visible = false;

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
        if (sender == RadioButtonID || RadioButtonID.Checked) { vrstni_red = " ORDER BY sifra"; }
        else if (sender == RadioButtonIme) { vrstni_red = " ORDER BY ime"; }
        else if (sender == RadioButtonPriimek) { vrstni_red = " ORDER BY priimek"; }
        else if (sender == RadioButtonEMSO) { vrstni_red = " ORDER BY EMSO"; }
        else if (sender == RadioButtonSpol) { vrstni_red = " ORDER BY spol"; }
        else if (sender == RadioButtonKrvnSk) { vrstni_red = " ORDER BY krvna_skupina"; }
        else if (sender == RadioButtonNasl) { vrstni_red = " ORDER BY naslov"; }
        else if (sender == RadioButtonPostna) { vrstni_red = " ORDER BY posta"; }
        else if (sender == RadioButtonKraj) { vrstni_red = " ORDER BY kraj"; }

        ukaz = "SELECT sifra, ime, priimek, EMSO, spol, krvna_skupina, naslov, posta, kraj FROM krvodajalec INNER JOIN POSTA ON krvodajalec.posta = POSTA.postna_st" + vrstni_red;

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
                Krvodajalci.InnerHtml = "";
                DropDownListIDSpremeni.Items.Clear();
                DropDownListIDIzbrisi.Items.Clear();

                while (reader.Read())
                {
                    int sifra = reader.GetInt32(0);
                    string ime = reader.GetString(1);
                    string priimek = reader.GetString(2);
                    string EMSO = reader.GetString(3);
                    string spol = reader.GetString(4);
                    string krvnSk = reader.GetString(5);
                    string naslov = reader.GetString(6);
                    string posta = reader.GetString(7);
                    string kraj = reader.GetString(8);

                    DropDownListIDSpremeni.Items.Add(new ListItem(sifra.ToString(), sifra.ToString()));
                    DropDownListIDIzbrisi.Items.Add(new ListItem(sifra.ToString(), sifra.ToString()));

                    Krvodajalci.InnerHtml += AkcijaVTabelo(sifra, ime, priimek, EMSO, spol, krvnSk, naslov, posta, kraj);
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); }
    }

    private string AkcijaVTabelo(int sif, string ime, string priimek, string emso, string spol, string krvSk, string naslov, string posta, string kraj)
    {
        string HTML =
            "<tr>" +
                "<td>" + sif + "</td>" +
                "<td>" + ime + "</td>" +
                "<td>" + priimek + "</td>" +
                "<td>" + emso + "</td>" +
                "<td>" + spol + "</td>" +
                "<td>" + krvSk + "</td>" +
                "<td>" + naslov + "</td>" +
                "<td>" + posta + "</td>" +
                "<td>" + kraj + "</td>" +
            "</tr>";
        return HTML;
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)      //dodaj v bazo
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";
        string ukaz = "SELECT postna_st FROM posta WHERE postna_st LIKE '" + TextBoxPostna.Text + "'";        //preveri če je vpisana posta veljavna

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
                ukaz = "INSERT INTO [dbo].[KRVODAJALEC]([ime],[priimek],[EMSO],[spol],[naslov],[krvna_skupina],[posta]) VALUES('" + 
                        TextBoxIme.Text + "', '" + TextBoxPriimek.Text + "', '" + TextBoxEMSO.Text + "', '" + TextBoxSpol.Text + "', '" + TextBoxNasl.Text + "', '" + TextBoxKrvSk.Text + "', '" + TextBoxPostna.Text + "')";

                cmd = new SqlCommand(ukaz, con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                LabelWarning.Visible = true;
                LabelWarning.Text = "Opozorilo, vnešena poštna številka ne obstaja!";
            }
        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); }

        TextBoxIme.Text = TextBoxPriimek.Text = TextBoxEMSO.Text = TextBoxSpol.Text = TextBoxNasl.Text = TextBoxKrvSk.Text = TextBoxPostna.Text = "";
    }
    protected void ButtonSpremeni_Click(object sender, EventArgs e)     //spremeni krvodajalsko akcijo
    {
        if (!(TextBoxSpremeniIme.Text == "" && TextBoxSpremeniPriimek.Text == "" && TextBoxSpremeniNasl.Text == "" && TextBoxSpremeniPostna.Text == ""))
        {
            string imeRacunalnika = "localhost";
            string imeRazlicice = "SQLEXPRESS";
            string imeBaze = "KRVODAJALSKA_AKCIJA";

            string ukaz = "UPDATE krvodajalec SET ";
            if (TextBoxSpremeniIme.Text != "") { ukaz += "ime = '" + TextBoxSpremeniIme.Text + "', "; } 
            if (TextBoxSpremeniPriimek.Text != "") { ukaz += "priimek = '" + TextBoxSpremeniPriimek.Text + "', "; }
            if (TextBoxSpremeniNasl.Text != "") { ukaz += "naslov = '" + TextBoxSpremeniNasl.Text + "', "; }

            if (TextBoxSpremeniPostna.Text != "") { ukaz += "posta = '" + TextBoxSpremeniPostna.Text + "'"; }
            else { ukaz = ukaz.Substring(ukaz.Length - 2); }                                              //odstrani zadnjo vejico

            ukaz += " WHERE sifra = " + DropDownListIDSpremeni;
        
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
                //posta je napačna
            }
            finally { con.Close(); }
        }

        TextBoxSpremeniIme.Text = TextBoxSpremeniPriimek.Text = TextBoxSpremeniNasl.Text = TextBoxSpremeniPostna.Text = "";
    }
    protected void ButtonIzbrisi_Click(object sender, EventArgs e)      //izbriši krvodajalsko akcijo
    {
        string imeRacunalnika = "localhost";
        string imeRazlicice = "SQLEXPRESS";
        string imeBaze = "KRVODAJALSKA_AKCIJA";

        //preveri ali se kakšna dajatev sklicuje na izbranega krvodajalca
        string ukaz = "SELECT krvodajalec.ime, krvodajalec.priimek, oddaja_krvi.datum, oddaja_krvi.kolicina " +
                      "FROM oddaja_krvi INNER JOIN krvodajalec ON krvodajalec.sifra = oddaja_krvi.sifra_krvodajalca " +
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
            {                                                 //najdena je bila datajatev, ki se navezuje na izbranega krvodajalca
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
                ukaz = "DELETE FROM krvodajalec WHERE sifra = " + DropDownIzbrisi;

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