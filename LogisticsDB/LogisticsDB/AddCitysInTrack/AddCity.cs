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

namespace LogisticsDB.AddCitysInTrack
{
    public partial class AddCity : Form
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
        string nametrack;
        public AddCity(MySqlConnectionStringBuilder mysqlCSB, string nameTrack)
        {
            this.nametrack = nameTrack;
            this.mysqlCSB = mysqlCSB;
            InitializeComponent();
            LoadCity();
            comboBox1.Items.AddRange(CityList.ToArray());
            comboBox1.SelectedIndex = 0;
            comboBox1.Refresh();
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
            queryString = " select name_City" +
                          " from city;";


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
        private bool Test(string queryString_1)
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand com = new MySqlCommand(queryString_1, con);

                try
                {
                    con.Open();
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            con.Close();
                            return true;


                        }
                        else
                        {
                            con.Close();
                            return false;
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Введені дані не відповідають вимогам. Можливо такі дані вже є в таблиці, де запис має бути унікальним");
                    MessageBox.Show(ex.ToString());
                }
                con.Close();
                return false;
            }


        }



        private void button1_Click(object sender, EventArgs e)
        {
            string queryString = "";
            if (!Test("select * from track_city where FKCity = (select id_City from city where name_City ='" + comboBox1.SelectedValue + "') and FKTrack = (select id_Track from Track where name_Track = '"+nametrack+"');"))
            {
                queryString += " INSERT INTO track_city (FKCity, FKTrack)"
                               + " VALUE((select id_City from city where name_City ='" + comboBox1.SelectedItem.ToString() + "'), (select id_Track from Track where name_Track = '" + nametrack + "'));";
            }
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
                    MessageBox.Show("Помилка додання міста в маршрут. Перевірте правильність введених даних.");
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }

            }
            this.Close();
           
        }
        
    }
}
