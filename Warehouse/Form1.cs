using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Warehouse
{
    public
    partial class Form1 : Form
    {
        SqlConnection sqlconnection;
        public
            Form1()
        {
            InitializeComponent();
        }

        private
            void label1_Click_1(object sender, EventArgs e)
        {
        }

        private
            async void Refresh_GridView_1()
        {
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Articles]", sqlconnection);
            List<string[]> data = new List<string[]>();
            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (sqlReader.Read())
                {
                    data.Add(new string[4]);
                    data[data.Count - 1][0] = sqlReader[0].ToString();
                    data[data.Count - 1][1] = sqlReader[1].ToString();
                    data[data.Count - 1][2] = sqlReader[2].ToString();
                    data[data.Count - 1][3] = sqlReader[3].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
            }
        }

        private
            bool check_index_1(string index)
        {
            SqlDataReader sqlReader = null;
            List<string> data = new List<string>();
            SqlCommand command = new SqlCommand("SELECT * FROM [Articles]", sqlconnection);
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                if (sqlReader[0].ToString() == index)
                {
                    sqlReader.Close();
                    return true;
                }
            }
            sqlReader.Close();
            return false;
        }

        private
            bool check_index_2(string index)
        {
            SqlDataReader sqlReader = null;
            List<string> data = new List<string>();
            SqlCommand command = new SqlCommand("SELECT * FROM [Staff]", sqlconnection);
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                if (sqlReader[0].ToString() == index)
                {
                    sqlReader.Close();
                    return true;
                }
            }
            sqlReader.Close();
            return false;
        }


        private
            async void Refresh_GridView_2()
        {
            SqlDataReader sqlReader_2 = null;
            SqlCommand command_2 = new SqlCommand("SELECT * FROM [Staff]", sqlconnection);
            List<string[]> data = new List<string[]>();
            try
            {
                sqlReader_2 = await command_2.ExecuteReaderAsync();

                while (sqlReader_2.Read())
                {
                    data.Add(new string[5]);
                    data[data.Count - 1][0] = sqlReader_2[0].ToString();
                    data[data.Count - 1][1] = sqlReader_2[1].ToString();
                    data[data.Count - 1][2] = sqlReader_2[2].ToString();
                    data[data.Count - 1][3] = sqlReader_2[3].ToString();
                    data[data.Count - 1][4] = sqlReader_2[4].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader_2 != null)
                    sqlReader_2.Close();
            }
            foreach (string[] s in data)
            {
                dataGridView2.Rows.Add(s);
            }
        }
        private
            async void Form1_Load(object sender, EventArgs e)
        {
            string connectionstring = @"Data " + @"Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" +
                @"|DataDirectory|\Database.mdf;" +
                @"Integrated Security = True; MultipleActiveResultSets = True";

            sqlconnection = new SqlConnection(connectionstring);
            await sqlconnection.OpenAsync();
            Refresh_GridView_1();
            Refresh_GridView_2();
        }

        private
            void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlconnection != null && sqlconnection.State != ConnectionState.Closed)
                sqlconnection.Close();
            this.Close();
        }

        private
            async void button1_Click(object sender, EventArgs e)
        {
            int output;
            bool flag = false;
            if (int.TryParse(textBox1.Text, out output) && output > 0)
                flag = true;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox9.Text)
                && !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrWhiteSpace(textBox9.Text) && flag && !check_index_1(textBox1.Text))
            {
                if (label9.Visible)
                    label9.Visible = false;
                SqlCommand command = new SqlCommand("INSERT INTO [Articles] (Id, Name, Manufacturer, " +

                        "Consumer)VALUES(@Id, @Name, @Manufacturer, @Consumer) ",
                    sqlconnection);
                command.Parameters.AddWithValue("id", textBox1.Text);
                command.Parameters.AddWithValue("Name", textBox2.Text);
                command.Parameters.AddWithValue("Manufacturer", textBox3.Text);
                command.Parameters.AddWithValue("Consumer", textBox9.Text);
                await command.ExecuteNonQueryAsync();
                textBox1.Clear();
                textBox2.Clear();
                ;
                textBox3.Clear();
                ;
                textBox9.Clear();
                ;
                dataGridView1.Rows.Clear();
                Refresh_GridView_1();
            }
            else if (check_index_1(textBox1.Text))
            {
                label9.Visible = true;
                label9.Text = "По данному идентификатору уже существует запись!";
            }
            else
            {
                label9.Visible = true;
                label9.Text = "Для вставки необходимо заполнить все поля!";
            }
        }

        private
            async void button2_Click(object sender, EventArgs e)
        {
            int output;
            bool flag = false;
            if (int.TryParse(textBox1.Text, out output) && output > 0)
                flag = true;
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox9.Text)
                && !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrWhiteSpace(textBox9.Text) && flag && check_index_1(textBox1.Text))
            {
                if (label9.Visible)
                    label9.Visible = false;
                SqlCommand command = new SqlCommand("UPDATE [Articles] SET [Name]=@Name, " +

                        "[Manufacturer]=@Manufacturer, " +

                        "[Consumer]=@Consumer WHERE [Id]=@Id",
                    sqlconnection);
                command.Parameters.AddWithValue("Id", textBox1.Text);
                command.Parameters.AddWithValue("Name", textBox2.Text);
                command.Parameters.AddWithValue("Manufacturer", textBox3.Text);
                command.Parameters.AddWithValue("Consumer", textBox9.Text);
                await command.ExecuteNonQueryAsync();
                dataGridView1.Rows.Clear();
                Refresh_GridView_1();
                textBox1.Clear();
                ;
                textBox2.Clear();
                ;
                textBox3.Clear();
                ;
                textBox9.Clear();
                ;
            }
            else if (!check_index_1(textBox1.Text))
            {
                label9.Visible = true;
                label9.Text = "Не существует записи по данному идентификатору!";
            }
            else
            {
                label9.Visible = true;
                label9.Text = "Для редактирования по идентификатору \n заполните все поля!";
            }
        }

        private
            async void button3_Click(object sender, EventArgs e)
        {
            int output;
            bool flag = false;
            if (int.TryParse(textBox1.Text, out output) && output > 0)
                flag = true;
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)
                && flag && check_index_1(textBox1.Text))
            {
                if (label9.Visible)
                    label9.Visible = false;
                SqlCommand command
                    = new SqlCommand("DELETE FROM [Articles] WHERE [Id]=@Id", sqlconnection);
                command.Parameters.AddWithValue("Id", textBox1.Text);
                await command.ExecuteNonQueryAsync();
                dataGridView1.Rows.Clear();
                Refresh_GridView_1();
                textBox1.Clear();
                ;
            }
            else if (!check_index_1(textBox1.Text))
            {
                label9.Visible = true;
                label9.Text = "Не существует записи по данному идентификатору!";
            }
            else
            {
                label9.Visible = true;
                label9.Text = "Для удаления введите идентификатор записи!";
            }
        }

        private
            async void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                dataGridView1.Rows.Clear();
                if (label9.Visible)
                    label9.Visible = false;
                SqlCommand command
                    = new SqlCommand("SELECT * FROM [Articles] WHERE [Name]=@Name", sqlconnection);
                command.Parameters.AddWithValue("Name", textBox2.Text);
                await command.ExecuteNonQueryAsync();
                SqlDataReader sqlReader = null;
                List<string[]> data = new List<string[]>();
                try
                {
                    sqlReader = await command.ExecuteReaderAsync();

                    while (sqlReader.Read())
                    {
                        data.Add(new string[4]);
                        data[data.Count - 1][0] = sqlReader[0].ToString();
                        data[data.Count - 1][1] = sqlReader[1].ToString();
                        data[data.Count - 1][2] = sqlReader[2].ToString();
                        data[data.Count - 1][3] = sqlReader[3].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                foreach (string[] s in data)
                {
                    dataGridView1.Rows.Add(s);
                }
            }
            else
            {
                label9.Visible = true;
                label9.Text = "Укажите для поиска \"Товар\"!";
            }
        }

        private
            void button13_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Refresh_GridView_1();
        }

        private
            async void button8_Click(object sender, EventArgs e)
        {
            int output;
            bool flag = false;
            if (int.TryParse(textBox4.Text, out output) && output > 0)
                flag = true;
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text)
                && !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrEmpty(textBox7.Text)
                && !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox4.Text)
                && !string.IsNullOrWhiteSpace(textBox5.Text)
                && !string.IsNullOrWhiteSpace(textBox6.Text)
                && !string.IsNullOrWhiteSpace(textBox7.Text)
                && !string.IsNullOrWhiteSpace(textBox8.Text) && flag && !check_index_2(textBox4.Text))
            {
                if (label14.Visible)
                    label14.Visible = false;
                SqlCommand command
                    = new SqlCommand("INSERT INTO [Staff] (Id, Surname, Name, Patronymic, " +

                            "Position)VALUES(@Id, @Surname, @Name, @Patronymic, @Position)",
                        sqlconnection);
                command.Parameters.AddWithValue("Id", textBox4.Text);
                command.Parameters.AddWithValue("Surname", textBox5.Text);
                command.Parameters.AddWithValue("Name", textBox6.Text);
                command.Parameters.AddWithValue("Patronymic", textBox7.Text);
                command.Parameters.AddWithValue("Position", textBox8.Text);
                await command.ExecuteNonQueryAsync();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                dataGridView2.Rows.Clear();
                Refresh_GridView_2();
            }
            else if (check_index_2(textBox4.Text))
            {
                label14.Visible = true;
                label14.Text = "По данному идентификатору уже существует запись!";
            }
            else
            {
                label14.Visible = true;
                label14.Text = "Для вставки необходимо заполнить все поля!";
            }
        }

        private
        async void button7_Click(object sender, EventArgs e)
        {
            int output;
            bool flag = false;
            if (int.TryParse(textBox4.Text, out output) && output > 0)
                flag = true;
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text)
                && !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrEmpty(textBox7.Text)
                && !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox4.Text)
                && !string.IsNullOrWhiteSpace(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox6.Text)
                && !string.IsNullOrWhiteSpace(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox8.Text)
                && flag && check_index_2(textBox4.Text))
            {
                if (label14.Visible)
                    label14.Visible = false;
                SqlCommand command
                    = new SqlCommand("UPDATE [Staff] SET [Surname]=@Surname, [Name]=@Name, " +

                            "[Patronymic]=@Patronymic, [Position]=@Position WHERE [Id]=@Id",
                        sqlconnection);
                command.Parameters.AddWithValue("Id", textBox4.Text);
                command.Parameters.AddWithValue("Surname", textBox5.Text);
                command.Parameters.AddWithValue("Name", textBox6.Text);
                command.Parameters.AddWithValue("Patronymic", textBox7.Text);
                command.Parameters.AddWithValue("Position", textBox8.Text);
                await command.ExecuteNonQueryAsync();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                dataGridView2.Rows.Clear();
                Refresh_GridView_2();
            }
            else if (!check_index_2(textBox4.Text))
            {
                label14.Visible = true;
                label14.Text = "Не существует записи по данному идентификатору!";
            }
            else
            {
                label14.Visible = true;
                label14.Text = "Для редактирования по идентификатору \n заполните все поля!";
            }
        }

        private
        async void button6_Click(object sender, EventArgs e)
        {
            int output;
            bool flag = false;
            if (int.TryParse(textBox4.Text, out output) && output > 0)
                flag = true;
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) && flag
                && check_index_2(textBox4.Text))
            {
                if (label14.Visible)
                    label14.Visible = false;
                SqlCommand command = new SqlCommand("DELETE FROM [Staff] WHERE [Id]=@Id", sqlconnection);
                command.Parameters.AddWithValue("Id", textBox4.Text);
                await command.ExecuteNonQueryAsync();
                dataGridView2.Rows.Clear();
                Refresh_GridView_2();
                textBox4.Clear();
            }
            else if (!check_index_2(textBox4.Text))
            {
                label14.Visible = true;
                label14.Text = "Не существует записи по данному идентификатору!";
            }
            else
            {
                label14.Visible = true;
                label14.Text = "Для удаления введите идентификатор записи!";
            }
        }

        private
        async void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                if (label14.Visible)
                    label14.Visible = false;
                SqlCommand command
                    = new SqlCommand("SELECT * FROM [Staff] WHERE [Position]=@Position", sqlconnection);
                command.Parameters.AddWithValue("Position", textBox8.Text);
                await command.ExecuteNonQueryAsync();
                SqlDataReader sqlReader = null;
                List<string[]> data = new List<string[]>();
                try
                {
                    sqlReader = await command.ExecuteReaderAsync();

                    while (sqlReader.Read())
                    {
                        data.Add(new string[5]);
                        data[data.Count - 1][0] = sqlReader[0].ToString();
                        data[data.Count - 1][1] = sqlReader[1].ToString();
                        data[data.Count - 1][2] = sqlReader[2].ToString();
                        data[data.Count - 1][3] = sqlReader[3].ToString();
                        data[data.Count - 1][4] = sqlReader[4].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                dataGridView2.Rows.Clear();
                foreach (string[] s in data)
                {
                    dataGridView2.Rows.Add(s);
                }
                textBox8.Clear();
            }
            else
            {
                label14.Visible = true;
                label14.Text = "Укажите для поиска \"Должность\"!";
            }
        }

        private
        void button14_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            Refresh_GridView_2();
        }
    }
}
