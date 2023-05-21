using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;
namespace THA_W12_Benito
{
    public partial class Form1 : Form
    {
        string connection;
        MySqlConnection sqlconnection;
        MySqlCommand sqlcommand;
        MySqlDataAdapter sqldataadapter;
        MySqlDataReader sqldatareader;

        DataTable dtTeam = new DataTable();
        DataTable dtNationality = new DataTable();
        DataTable dtPlayer = new DataTable();
        DataTable dtManager = new DataTable();
        DataTable dtBen = new DataTable();
        DataTable dtbaru = new DataTable();
        DataTable musolo = new DataTable();
        DataTable jitt = new DataTable();
        DataTable bimsalabim = new DataTable(); 

        string query = "";



        public Form1()
        {
            try
            {
                string connection = "server=localhost;uid=root;pwd=benitopriyasha2004;database=premier_league;";
                sqlconnection = new MySqlConnection(connection);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            InitializeComponent();
            DGV1.DataSource = dtTeam;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateDGV1();

            query = "select team_id as 'ID', team_name as 'Team Name' from team;";
            sqlcommand = new MySqlCommand(query, sqlconnection); ;
            sqldataadapter = new MySqlDataAdapter(sqlcommand);
            sqldataadapter.Fill(dtBen);
            comboBox3.DataSource = dtBen;
            comboBox3.ValueMember = "ID";
            comboBox3.DisplayMember = "Team Name";
            query = "select nationality_id as 'ID', nation as 'Nation' from nationality;";
            sqlcommand = new MySqlCommand(query, sqlconnection);
            sqldataadapter = new MySqlDataAdapter(sqlcommand);
            sqldataadapter.Fill(dtNationality);
            comboBox1.DataSource = dtNationality;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Nation";
            query = "select team_id as 'ID', team_name as 'Team Name' from team;";
            sqlcommand = new MySqlCommand(query, sqlconnection); ;
            sqldataadapter = new MySqlDataAdapter(sqlcommand);
            sqldataadapter.Fill(dtbaru);
            comboBox4.DataSource = dtbaru;
            comboBox4.ValueMember = "ID";
            comboBox4.DisplayMember = "Team Name";
            comboBox5.DataSource = dtbaru;
            comboBox5.ValueMember = "ID";
            comboBox5.DisplayMember = "Team Name";



        }

        private void updateDGV1()
        {
            dtTeam.Clear();
            try
            {
                string command = "select * from player";
                sqlcommand = new MySqlCommand(command, sqlconnection);
                sqldataadapter = new MySqlDataAdapter(sqlcommand);
                sqldataadapter.Fill(dtTeam);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void insertbutton_Click(object sender, EventArgs e)
        {
            string playerid = textBox1.Text.ToString();
            string name = textBox2.Text.ToString();
            string number = textBox3.Text.ToString();
            string height = textBox4.Text.ToString();
            string weight = textBox5.Text.ToString();
            string nationality = comboBox1.SelectedValue.ToString();
            string position = comboBox2.Text;
            string teamname = comboBox3.SelectedValue.ToString();
            string birthdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            query = $"insert into player values ('{playerid}','{number}','{name}','{nationality}','{position}','{height}','{weight}','{birthdate}','{teamname}',1,0);";

            try
            {
                sqlconnection.Open();
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqldatareader = sqlcommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
                updateDGV1();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void editbutton_Click(object sender, EventArgs e)
        {
            query = $"UPDATE manager set working = 0 WHERE manager_name = '{DGV2.SelectedCells[0].Value.ToString()}'";   
            try
            {
                sqlconnection.Open();
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqldatareader = sqlcommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            musolo.Clear();
            query = $"SELECT m.manager_name,t.team_name,m.birthdate,n.nation FROM manager m ,nationality n,team t WHERE m.nationality_id = n.nationality_id and t.manager_id = m.manager_id and t.team_id = '{comboBox4.SelectedValue.ToString()}'AND working=1;";
            sqlcommand = new MySqlCommand(query, sqlconnection);
            sqldataadapter = new MySqlDataAdapter(sqlcommand);
            sqldataadapter.Fill(musolo);
            DGV2.DataSource= musolo;

            query = $"SELECT m.manager_name, n.nation, m.birthdate FROM manager m , nationality n WHERE m.nationality_id = n.nationality_id and working = 0;";
            sqlcommand = new MySqlCommand(query, sqlconnection);
            sqldataadapter = new MySqlDataAdapter(sqlcommand);
            sqldataadapter.Fill(jitt);
            DGV3.DataSource = jitt;




        }

        private void DGV2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bimsalabim.Clear();

            query = $"SELECT p.player_name, n.nation , p.playing_pos, p.team_number, p.height, p.weight, p.birthdate FROM player p , nationality n WHERE p.nationality_id = n.nationality_id and p.team_id = '{comboBox5.SelectedValue.ToString()}'and status=1";
            sqlcommand = new MySqlCommand(query, sqlconnection);
            sqldataadapter = new MySqlDataAdapter(sqlcommand);
            sqldataadapter.Fill(bimsalabim);
            DGV4.DataSource = bimsalabim;
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            query = $"UPDATE player set status = 0 WHERE player_name = '{DGV4.SelectedCells[0].Value.ToString()}';";

            try
            {
                sqlconnection.Open();
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqldatareader = sqlcommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlconnection.Close();
            }
        }
    }
}
