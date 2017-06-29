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
    public partial class Track : Form
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
        public Track(MySqlConnectionStringBuilder mysqlCSB)
        {
            this.mysqlCSB = mysqlCSB;
            InitializeComponent();
            RefreshDB();
            //dataGridView1.Columns[0].Visible = false;
            dataGridView1.ReadOnly = true;
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

        private void оновитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDB();
            оновитиToolStripMenuItem.Visible = true;
            додатиToolStripMenuItem.Visible = true;
            підтвердитиДодаванняToolStripMenuItem.Visible = false;
            редагуватиToolStripMenuItem.Visible = true;
            зберегтиToolStripMenuItem.Visible = false;
            видалитиToolStripMenuItem.Visible = true;
        }
        private void додатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDB();
            оновитиToolStripMenuItem.Visible = false;
            додатиToolStripMenuItem.Visible = false;
            підтвердитиДодаванняToolStripMenuItem.Visible = true;
            редагуватиToolStripMenuItem.Visible = false;
            зберегтиToolStripMenuItem.Visible = false;
            видалитиToolStripMenuItem.Visible = false;
        }
        private void підтвердитиДодаванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OkAddDB();
            оновитиToolStripMenuItem.Visible = true;
            додатиToolStripMenuItem.Visible = true;
            підтвердитиДодаванняToolStripMenuItem.Visible = false;
            редагуватиToolStripMenuItem.Visible = true;
            зберегтиToolStripMenuItem.Visible = false;
            видалитиToolStripMenuItem.Visible = true;
        }
        private void редагуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDB();
            оновитиToolStripMenuItem.Visible = false;
            додатиToolStripMenuItem.Visible = false;
            підтвердитиДодаванняToolStripMenuItem.Visible = false;
            редагуватиToolStripMenuItem.Visible = false;
            зберегтиToolStripMenuItem.Visible = true;
            видалитиToolStripMenuItem.Visible = false;
        }
        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveEdit();
            оновитиToolStripMenuItem.Visible = true;
            додатиToolStripMenuItem.Visible = true;
            підтвердитиДодаванняToolStripMenuItem.Visible = false;
            редагуватиToolStripMenuItem.Visible = true;
            зберегтиToolStripMenuItem.Visible = false;
            видалитиToolStripMenuItem.Visible = true;
        }
        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelateDB();
            додатиToolStripMenuItem.Visible = true;
            підтвердитиДодаванняToolStripMenuItem.Visible = false;
            редагуватиToolStripMenuItem.Visible = true;
            зберегтиToolStripMenuItem.Visible = false;
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
        public void RefreshDB()
        {
            //LoadCity();
            if (isFirst == false)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dr = null;
                dt = new DataTable();
            }
            queryString = " select name_Track, length" +
                          " from track;" ;
           

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

                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                dataGridView1.Columns.Add("", "");
                                dataGridView1.Columns[i].HeaderText = dt.Columns[i].Caption;

                            }
                            for (int i = 0; i < dt.Rows.Count; i++)
                                dataGridView1.Rows.Add();
                            
                          
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];

                                for (int n = 0; n < dt.Columns.Count; n++)
                                {
                                    DataColumn column = dt.Columns[n];
                                    dataGridView1[n, j].Value = row[column];
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження Бази даних");
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                isFirst = false;
            }
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            buttons.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            buttons.Visible = true;
            buttons.ToolTipText = "Редагувати трек";
            buttons.Text = "Деталі...";
            buttons.UseColumnTextForButtonValue = true;
            buttons.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(buttons);

            //DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
            //comboBoxColumn.Items.AddRange(CityList.ToArray());
            //dataGridView1.Columns.Add(comboBoxColumn);
            //for (int j = 0; j < dt.Rows.Count; j++)
            //{
            //    dataGridView1[4, j].Value = dataGridView1[0, j].Value;
            //}

            //dataGridView1.Columns[0].Visible = false;
        }
        public void AddDB()
        {

            dataGridView1.Rows.Add();
            dataGridView1.ReadOnly = false;
            LoadCity();
            int newElementPosition = dataGridView1.RowCount - 2;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, newElementPosition].Style.BackColor = Color.Green;
            }
            
            dataGridView1.FirstDisplayedScrollingRowIndex = newElementPosition;
            dataGridView1.CurrentCell = dataGridView1[1, newElementPosition];
            
        }
        public void OkAddDB()
        {
            dataGridView1.CurrentCell = dataGridView1[1, 1];
            dataGridView1.CurrentCell = dataGridView1[2, 2];
            dataGridView1.Refresh();
            dataGridView1.ReadOnly = true;
            string[] train = new string[dataGridView1.ColumnCount];
            
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                train[i] = dataGridView1[i, dataGridView1.RowCount - 2].Value?.ToString();
            }
            string queryString = "";
            if (!Test("select * from track where name_Track = '" + train[0] + "' and  length = '" + train[1] + "';"))
            {
                queryString += " INSERT INTO track (name_Track, length)"
                               + " VALUE( '" + train[0] + "', '" + train[1] + "');";
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
                            for (int i = 0; i < dt.Rows.Count; i++)
                                dataGridView1.Rows.Add();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];

                                for (int n = 0; n < dt.Columns.Count; n++)
                                {
                                    DataColumn column = dt.Columns[n];

                                    dataGridView1[n, j].Value = row[column];
                                }
                            }
                            dataGridView1.Refresh();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка додання запису. Перевірте правильність введених даних.");
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, dataGridView1.RowCount - 2].Style.BackColor = Color.White;
            }
        }
        public void EditDB()
        {
            LoadCity();
            int numberOfRows = dataGridView1.CurrentCell.RowIndex;
            editTrain = new string[dataGridView1.ColumnCount];
            dataGridView1.ReadOnly = false;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, numberOfRows].Style.BackColor = Color.Yellow;
                var dsfgd = dataGridView1[i, numberOfRows].Value;
                editTrain[i] = dataGridView1[i, numberOfRows].Value.ToString();
                //if (editTrain[i] == "") editTrain[i] = null;
            }
            numberOfEditRows = numberOfRows;
           
        }
        public void SaveEdit()
        {
            
            dataGridView1.CurrentCell = dataGridView1[1, 1];
            dataGridView1.CurrentCell = dataGridView1[2, 2];
            dataGridView1.Refresh();
            dataGridView1.ReadOnly = true;
            string[] train = new string[dataGridView1.ColumnCount];
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, numberOfEditRows].Style.BackColor = Color.White;
                var value = dataGridView1[i, numberOfEditRows].Value;
                train[i] = value == null ? "":value.ToString();

            }
            string queryString = "";
            if (!Test("select * from track where name_Track = '" + train[0] + "' and  length = '" + train[1] + "';"))
            {
                queryString += "UPDATE track set name_Track =  '" + train[0] + "', length =  '" + train[1] + "' where name_Track =  '" + editTrain[0] + "' and length =  '" + editTrain[1] + "';";
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

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];

                                for (int n = 0; n < dt.Columns.Count; n++)
                                {
                                    DataColumn column = dt.Columns[n];

                                    dataGridView1[n, j].Value = row[column];
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка редагування запису. Перевірте правильність введених даних.");
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void DelateDB()
        {
            int numberOfRows = dataGridView1.CurrentCell.RowIndex;
            string[] train = new string[dataGridView1.ColumnCount];

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {

                train[i] = dataGridView1[i, numberOfRows].Value?.ToString();
            }
            string queryString = "delete from track_city where FKTrack=(select id_Track from track where name_Track='" + train[0] + "'); DELETE FROM track WHERE length = '" + train[1] + "' and  name_Track = '" + train[0] + "';";
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
                            for (int i = 0; i < dt.Rows.Count; i++)
                                dataGridView1.Rows.Add();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];

                                for (int n = 0; n < dt.Columns.Count; n++)
                                {
                                    DataColumn column = dt.Columns[n];

                                    dataGridView1[n, j].Value = row[column];
                                }
                            }
                            dataGridView1.Refresh();

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
            RefreshDB();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EditCitysInTrack ecity = new EditCitysInTrack(dataGridView1[0, e.RowIndex].Value.ToString(), mysqlCSB);
            ecity.Show();
            //MessageBox.Show(dataGridView1[0, e.RowIndex].Value.ToString());
           
        }
    }
}
