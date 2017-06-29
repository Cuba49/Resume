using LogisticsDB.AddCitysInTrack;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogisticsDB.EditWindows
{
    public partial class EditCitysInTrack : Form
    {
        string[] editTrain;
        int numberOfEditRows;

        private MySqlCommand com;
        private MySqlConnection con;
        private MySqlDataReader dr;
        MySqlConnectionStringBuilder mysqlCSB;
        private string queryString;
        DataTable dt = new DataTable();
        private bool isFirst = true;
        string nameTrack;
        public EditCitysInTrack(string nameTrack, MySqlConnectionStringBuilder mysqlCSB)
        {
            this.nameTrack = nameTrack;
            this.mysqlCSB = mysqlCSB;
            InitializeComponent();
            this.Text = nameTrack;
            LoadCity();
            listBox1.Items.AddRange(CityList.ToArray());
            listBox1.Refresh();

        }
        List<string> CityList;
        public void LoadCity()
        {
            CityList = new List<string>();
            if (isFirst == false)
            {
                dr = null;
                dt = new DataTable();
            }
            queryString = " select name_City from city, track_city where track_city.FKCity = city.id_City and FKTrack=any(select id_Track from track where name_Track = '" + nameTrack + "')order by id_Track_City;";


            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                com = new MySqlCommand(queryString, con);
                try
                {
                    con.Open();

                    using (dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {

                            dt.Load(dr);
                            var i = dt.Columns[0];

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];


                                DataColumn column = dt.Columns[0];

                                CityList.Add(row[0].ToString());


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка створення переліку міст");
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                isFirst = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCity addCity = new AddCity(mysqlCSB, nameTrack);
            addCity.ShowDialog();
            LoadCity();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(CityList.ToArray());
            listBox1.Refresh();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string queryString = "DELETE FROM track_city WHERE FKTrack = (select id_Track from track where name_Track = '" + nameTrack + "') and FKCity= (select id_City from city where name_City = '" + listBox1.SelectedItem.ToString() + "'); ";
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                com = new MySqlCommand(queryString, con);
                try
                {
                    con.Open();
                    using (dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {

                            dt.Load(dr);


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка видалення запису. Скоріше за все, на нього посилаються інші таблиці. Видаліть зв'язки і повторіть спробу");
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            LoadCity();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(CityList.ToArray());
            listBox1.Refresh();
        }
    }
}
