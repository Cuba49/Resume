using LogisticsDB.EditWindows;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogisticsDB
{
    public partial class Menu : Form
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
        public Menu()
        {
            InitializeComponent();
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = "127.0.0.1";
            mysqlCSB.Database = "customproducts";
            mysqlCSB.UserID = "root";
            mysqlCSB.Password = "kubovych49";
            RefreshDB();

        }
        #region Виклик інших форм
        private void містаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Citys city = new Citys(mysqlCSB);
            city.Show();
        }
        private void вантажиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cargo cargo = new Cargo(mysqlCSB);
            cargo.Show();
        }
        private void клієнтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients clients = new Clients(mysqlCSB);
            clients.Show();
        }
        private void адресиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Adress adress = new Adress(mysqlCSB);
            adress.Show();
        }
        private void представництаваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Representationn rep = new Representationn(mysqlCSB);
            rep.Show();
        }
        private void транспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transports trans = new Transports(mysqlCSB);
            trans.Show();
        }
        private void маршрутиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Track track = new Track(mysqlCSB);
            track.Show();
        }
        private void складиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stores store = new Stores(mysqlCSB);
            store.Show();
        }
        #endregion


        #region Методи управління головною формою
        private void оновитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDB();
            оновитиToolStripMenuItem.Visible = true;
            додатиToolStripMenuItem.Visible = true;
            підтвердженняДодаванняToolStripMenuItem.Visible = false;
            редагуванняToolStripMenuItem.Visible = true;
            зберегтиToolStripMenuItem.Visible = false;
            видалитиToolStripMenuItem.Visible = true;
        }
        private void додатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDB();
            оновитиToolStripMenuItem.Visible = false;
            додатиToolStripMenuItem.Visible = false;
            підтвердженняДодаванняToolStripMenuItem.Visible = true;
            редагуванняToolStripMenuItem.Visible = false;
            зберегтиToolStripMenuItem.Visible = false;
            видалитиToolStripMenuItem.Visible = false;
        }
        private void підтвердженняДодаванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OkAddDB();
            оновитиToolStripMenuItem.Visible = true;
            додатиToolStripMenuItem.Visible = true;
            підтвердженняДодаванняToolStripMenuItem.Visible = false;
            редагуванняToolStripMenuItem.Visible = true;
            зберегтиToolStripMenuItem.Visible = false;
            видалитиToolStripMenuItem.Visible = true;
        }
        private void редагуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDB();
            оновитиToolStripMenuItem.Visible = false;
            додатиToolStripMenuItem.Visible = false;
            підтвердженняДодаванняToolStripMenuItem.Visible = false;
            редагуванняToolStripMenuItem.Visible = false;
            зберегтиToolStripMenuItem.Visible = true;
            видалитиToolStripMenuItem.Visible = false;
        }
        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveEdit();
            оновитиToolStripMenuItem.Visible = true;
            додатиToolStripMenuItem.Visible = true;
            підтвердженняДодаванняToolStripMenuItem.Visible = false;
            редагуванняToolStripMenuItem.Visible = true;
            зберегтиToolStripMenuItem.Visible = false;
            видалитиToolStripMenuItem.Visible = true;
        }
        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelateDB();
            додатиToolStripMenuItem.Visible = true;
            підтвердженняДодаванняToolStripMenuItem.Visible = false;
            редагуванняToolStripMenuItem.Visible = true;
            зберегтиToolStripMenuItem.Visible = false;
        }
        #endregion

        #region Управління базою даних
        List<string> CityListAll;
        public void LoadCityAll()
        {
            CityListAll = new List<string>();
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

                                CityListAll.Add(row[0].ToString());


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка створення переліку всіх міст");
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                isFirst = false;
            }
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
                          " from city, track where name_Track = '" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' and track.FKCity = city.id_City;";


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
        List<string> ClientList;
        public void LoadClient()
        {
            ClientList = new List<string>();
            if (isFirst == false)
            {
                dr = null;
                dt = new DataTable();
            }
            queryString = " select concat(Second_Name,' ',First_Name,' ',Midle_Name)" +
                          " from adress;";


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

                                ClientList.Add(row[0].ToString());


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
        List<string> CargoList;
        public void LoadCargo()
        {
            CargoList = new List<string>();
            if (isFirst == false)
            {
                dr = null;
                dt = new DataTable();
            }
            queryString = " select name_Cargo" +
                          " from cargo;";


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

                                CargoList.Add(row[0].ToString());


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
        List<string> TrackList;
        public void LoadTrack()
        {
            TrackList = new List<string>();
            if (isFirst == false)
            {
                dr = null;
                dt = new DataTable();
            }
            queryString = " select name_Track" +
                          " from track;";


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

                                TrackList.Add(row[0].ToString());


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
        List<string> TransportList;
        public void LoadTransport()
        {
            TransportList = new List<string>();
            if (isFirst == false)
            {
                dr = null;
                dt = new DataTable();
            }
            queryString = " select name_Transport" +
                          " from transports;";


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

                                TransportList.Add(row[0].ToString());


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
        List<string> ReprList;
        public void LoadRepr()
        {
            ReprList = new List<string>();
            if (isFirst == false)
            {
                dr = null;
                dt = new DataTable();
            }
            queryString = " select name_Representation" +
                          " from representation;";


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

                                ReprList.Add(row[0].ToString());


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
            LoadCityAll();
            LoadClient();
            LoadCargo();
            LoadTrack();
            LoadTransport();
            LoadRepr();
            if (isFirst == false)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dr = null;
                dt = new DataTable();
            }
            queryString = " select id_Custom_Products as 'Номер трека', name_City as 'Локація',  inTime as 'Час замовлення',concat(Second_Name,' ',First_Name,' ',Midle_Name) as 'ПІБ', name_Cargo as 'Товар', name_Track as 'Маршрут', name_Transport as 'Транспорт', name_Representation as 'Представництво', IsCompleted as 'Статус' from custom_products, adress, cargo, track, transports, representation, city "
                + " where custom_products.FKAdress = adress.idAdress and "
+ " custom_products.FKCargo = cargo.id_Cargo and"
+ " custom_products.FKTrack = track.id_Track and"
+ " custom_products.FKTransport = transports.id_Transport and"
+ " custom_products.FKRepresentation = representation.id_Representation and"
+ " custom_products.FKWhere = city.id_City; ";


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
                //dataGridView1.AutoResizeColumns();


                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.Items.AddRange(CityListAll.ToArray());
                dataGridView1.Columns.Add(comboBoxColumn);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dataGridView1[9, j].Value = dataGridView1[1, j].Value;
                }
                dataGridView1.Columns[1].Visible = false;

                comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.Items.AddRange(ClientList.ToArray());
                dataGridView1.Columns.Add(comboBoxColumn);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dataGridView1[10, j].Value = dataGridView1[3, j].Value;
                }
                dataGridView1.Columns[3].Visible = false;

                comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.Items.AddRange(CargoList.ToArray());
                dataGridView1.Columns.Add(comboBoxColumn);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dataGridView1[11, j].Value = dataGridView1[4, j].Value;
                }
                dataGridView1.Columns[4].Visible = false;

                comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.Items.AddRange(TrackList.ToArray());
                dataGridView1.Columns.Add(comboBoxColumn);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dataGridView1[12, j].Value = dataGridView1[5, j].Value;
                }
                dataGridView1.Columns[5].Visible = false;

                comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.Items.AddRange(TransportList.ToArray());
                dataGridView1.Columns.Add(comboBoxColumn);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dataGridView1[13, j].Value = dataGridView1[6, j].Value;
                }
                dataGridView1.Columns[6].Visible = false;

                comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.Items.AddRange(ReprList.ToArray());
                dataGridView1.Columns.Add(comboBoxColumn);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dataGridView1[14, j].Value = dataGridView1[7, j].Value;
                }
                dataGridView1.Columns[7].Visible = false;

                comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.Items.AddRange(new string [2] { "Доставлено", "Очікує доставки" });
                dataGridView1.Columns.Add(comboBoxColumn);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dataGridView1[15, j].Value = dataGridView1[8, j].Value.ToString() == "1" ? "Доставлено" : "Очікує доставки";
                }
                dataGridView1.Columns[8].Visible = false;
                //DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
                //buttons.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //buttons.Visible = true;
                //buttons.ToolTipText = "Редагувати трек";
                //buttons.Text = "Деталі...";
                //buttons.UseColumnTextForButtonValue = true;
                //buttons.FlatStyle = FlatStyle.Popup;
                //dataGridView1.Columns.Add(buttons);
            }
        }
        public void AddDB()
        {
            dataGridView1.Rows.Add();
            dataGridView1.ReadOnly = false;
            int newElementPosition = dataGridView1.RowCount - 2;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, newElementPosition].Style.BackColor = Color.Green;
            }
            dataGridView1.FirstDisplayedScrollingRowIndex = newElementPosition;
            dataGridView1.CurrentCell = dataGridView1[0, newElementPosition];

        }
        public void OkAddDB()
        {
            dataGridView1.CurrentCell = dataGridView1[9, 1];
            dataGridView1.CurrentCell = dataGridView1[10, 2];
            dataGridView1.Refresh();
            dataGridView1.ReadOnly = true;
            string[] train = new string[dataGridView1.ColumnCount];
            dataGridView1[1, dataGridView1.RowCount - 2].Value = dataGridView1[9, dataGridView1.RowCount - 2].Value;
            dataGridView1[2, dataGridView1.RowCount - 2].Value = DateTime.Now.ToString(new CultureInfo("ru-RU"));
            dataGridView1[3, dataGridView1.RowCount - 2].Value = dataGridView1[10, dataGridView1.RowCount - 2].Value;
            dataGridView1[4, dataGridView1.RowCount - 2].Value = dataGridView1[11, dataGridView1.RowCount - 2].Value;
            dataGridView1[5, dataGridView1.RowCount - 2].Value = dataGridView1[12, dataGridView1.RowCount - 2].Value;
            dataGridView1[6, dataGridView1.RowCount - 2].Value = dataGridView1[13, dataGridView1.RowCount - 2].Value;
            dataGridView1[7, dataGridView1.RowCount - 2].Value = dataGridView1[14, dataGridView1.RowCount - 2].Value;
            dataGridView1[8, dataGridView1.RowCount - 2].Value = dataGridView1[15, dataGridView1.RowCount - 2].Value.ToString() == "Очікує доставки" ? "0" : "1";
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                train[i] = dataGridView1[i, dataGridView1.RowCount - 2].Value?.ToString();
            }
            
            string queryString = "";
            if (!Test("select * from custom_products where FKWhere = (select id_City from city where name_City = '" + train[1] + "')  and FKAdress = (select idAdress from adress where concat(Second_Name,' ',First_Name,' ',Midle_Name) = '" + train[3] + "') and FKCargo = (select id_Cargo from cargo where name_Cargo = '" + train[4] + "') and FKTrack = (select id_Track from track where name_Track = '" + train[5] + "') and FKTransport = (select id_Transport from transports where name_Transport = '" + train[6] + "') and FKRepresentation = (select id_Representation from representation where name_Representation = '" + train[7] + "') and IsCompleted  = '" + train[8] + "'  ;"))
            {
                var  i = train[2].Length;
                queryString += " INSERT INTO custom_products (FKWhere, inTime, FKAdress, FKCargo, FKTrack, FKTransport, FKRepresentation, IsCompleted)"
                               + " VALUE((select id_City from city where name_City = '" + train[1] + "'), '" + train[2].Remove(0, 8).Remove(2, 9)+ train[2].Remove(0,2).Remove(3,14)+"."+ train[2].Remove(2,8)+ "', (select idAdress from adress where concat(Second_Name,' ',First_Name,' ',Midle_Name) = '" + train[3] + "'), (select id_Cargo from cargo where name_Cargo = '" + train[4] + "'), (select id_Track from track where name_Track = '" + train[5] + "'), (select id_Transport from transports where name_Transport = '" + train[6] + "'), (select id_Representation from representation where name_Representation = '" + train[7] + "'), '" + train[8] + "');";
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
            RefreshDB();
        }
        public void EditDB()
        {
            int numberOfRows = dataGridView1.CurrentCell.RowIndex;
            editTrain = new string[dataGridView1.ColumnCount];
            dataGridView1.ReadOnly = false;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, numberOfRows].Style.BackColor = Color.Yellow;
                editTrain[i] = dataGridView1[i, numberOfRows].Value.ToString();
                if (editTrain[i] == "") editTrain[i] = null;
            }
            numberOfEditRows = numberOfRows;

        }
        public void SaveEdit()
        {

            dataGridView1.CurrentCell = dataGridView1[0, 0];
            dataGridView1.CurrentCell = dataGridView1[0, 1];
            dataGridView1.Refresh();
            dataGridView1.ReadOnly = true;
            dataGridView1[1, numberOfEditRows].Value = dataGridView1[9, numberOfEditRows].Value;
            dataGridView1[2, numberOfEditRows].Value = DateTime.Now.ToString(new CultureInfo("ru-RU"));
            dataGridView1[3, numberOfEditRows].Value = dataGridView1[10, numberOfEditRows].Value;
            dataGridView1[4, numberOfEditRows].Value = dataGridView1[11, numberOfEditRows].Value;
            dataGridView1[5, numberOfEditRows].Value = dataGridView1[12, numberOfEditRows].Value;
            dataGridView1[6, numberOfEditRows].Value = dataGridView1[13, numberOfEditRows].Value;
            dataGridView1[7, numberOfEditRows].Value = dataGridView1[14, numberOfEditRows].Value;
            dataGridView1[8, numberOfEditRows].Value = dataGridView1[15, numberOfEditRows].Value.ToString() == "Очікує доставки" ? "0" : "1";
            
            string[] train = new string[dataGridView1.ColumnCount];
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, numberOfEditRows].Style.BackColor = Color.White;
                var value = dataGridView1[i, numberOfEditRows].Value;
                train[i] = value == null ? "" : value.ToString();

            }
            string queryString = "";
            if (!Test("select * from custom_products where FKWhere = (select id_City from city where name_City = '" + train[1] + "')  and FKAdress = (select idAdress from adress where concat(Second_Name,' ',First_Name,' ',Midle_Name) = '" + train[3] + "') and FKCargo = (select id_Cargo from cargo where name_Cargo = '" + train[4] + "') and FKTrack = (select id_Track from track where name_Track = '" + train[5] + "') and FKTransport = (select id_Transport from transports where name_Transport = '" + train[6] + "') and FKRepresentation = (select id_Representation from representation where name_Representation = '" + train[7] + "') and IsCompleted  = '" + train[8] + "'  ;"))
            {
                queryString += " UPDATE custom_products set FKWhere = (select id_City from city where name_City = '" + train[1] + "')  , FKAdress = (select idAdress from adress where concat(Second_Name,' ',First_Name,' ',Midle_Name) = '" + train[3] + "') , FKCargo = (select id_Cargo from cargo where name_Cargo = '" + train[4] + "') , FKTrack = (select id_Track from track where name_Track = '" + train[5] + "') , FKTransport = (select id_Transport from transports where name_Transport = '" + train[6] + "') , FKRepresentation = (select id_Representation from representation where name_Representation = '" + train[7] + "') , IsCompleted  = '" + train[8] + "' where FKWhere = (select id_City from city where name_City = '" + editTrain[1] + "')  and FKAdress = (select idAdress from adress where concat(Second_Name,' ',First_Name,' ',Midle_Name) = '" + editTrain[3] + "') and FKCargo = (select id_Cargo from cargo where name_Cargo = '" + editTrain[4] + "') and FKTrack = (select id_Track from track where name_Track = '" + editTrain[5] + "') and FKTransport = (select id_Transport from transports where name_Transport = '" + editTrain[6] + "') and FKRepresentation = (select id_Representation from representation where name_Representation = '" + editTrain[7] + "') and IsCompleted  = '" + editTrain[8] + "' ; ";
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
            RefreshDB();
        }
        public void DelateDB()
        {
            int numberOfRows = dataGridView1.CurrentCell.RowIndex;
            string[] train = new string[dataGridView1.ColumnCount];

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {

                train[i] = dataGridView1[i, numberOfRows].Value?.ToString();
            }
            string queryString = "DELETE FROM custom_products WHERE id_Custom_Products = '" + train[0] + "'; ";
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
        #endregion
        string link; 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (isFirst == false)
            {
                dr = null;
                dt = new DataTable();
            }

            string queryString = "Select Map_City FROM city WHERE name_City =  '" + dataGridView1[1, e.RowIndex].Value.ToString() + "'; ";
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


                                DataColumn column = dt.Columns[0];

                                link = row[column].ToString();

                            }
                           
                           



                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("У БД відсутнє значення посилання на карту.");
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            System.Diagnostics.Process.Start(link);
        }
    }
}
