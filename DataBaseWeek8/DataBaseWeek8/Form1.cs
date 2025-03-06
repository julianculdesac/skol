using System.Data;
using System.Data.OleDb;

namespace DataBaseWeek8
{
    public partial class Form1 : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Julian\OneDrive\Desktop\WEEK8_DB.accdb;Persist Security Info=True");

        public Form1()
        {

            InitializeComponent();
        }


        //REFRESH 
        void Dataview()
        {
            try
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM csave ORDER BY StudentID ASC"; // Added ORDER BY ID

                DataTable dt = new DataTable();
                OleDbDataAdapter dp = new OleDbDataAdapter(cmd);
                dp.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Dataview();
        }



        //ADD DATA
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || //ENTRU CHECKER
                    string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) ||
                    string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text))
                {
                    MessageBox.Show("Please fill in all text boxes.");
                    return;
                }

                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    // EXISTING ID CHECKER
                    OleDbCommand checkCmd = conn.CreateCommand();
                    checkCmd.CommandType = CommandType.Text;
                    checkCmd.CommandText = "SELECT COUNT(*) FROM csave WHERE StudentID = ?";
                    checkCmd.Parameters.AddWithValue("?", textBox2.Text);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("StudentID already exists.");
                        conn.Close();
                        return;
                    }

                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO csave (StudentID, FirstName, LastName, Address, PostCode, Telephone) VALUES (?, ?, ?, ?, ?, ?)";

                    cmd.Parameters.AddWithValue("?", textBox2.Text);
                    cmd.Parameters.AddWithValue("?", textBox3.Text);
                    cmd.Parameters.AddWithValue("?", textBox4.Text);
                    cmd.Parameters.AddWithValue("?", textBox5.Text);
                    cmd.Parameters.AddWithValue("?", textBox6.Text);
                    cmd.Parameters.AddWithValue("?", textBox7.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Dataview();
                    conn.Close();
                    MessageBox.Show("Data inserted successfully!");
                    conn.Close();
                }

                else
                {
                    MessageBox.Show("Connection Failed to Open.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        //VIEW
        private void button8_Click(object sender, EventArgs e)
        {
            Dataview();
        }
        //EXIT
        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult iExit;
            iExit = MessageBox.Show("Do you Want to Exit?", "Access Connect", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (iExit == DialogResult.Yes)
            {
                Application.Exit();

            }
        }





        //UPDATE
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE csave SET StudentID = ?, FirstName = ?, LastName = ?, Address = ?, PostCode = ?, Telephone = ? WHERE StudentID = ?";


                cmd.Parameters.AddWithValue("?", textBox2.Text); // New StudentID
                cmd.Parameters.AddWithValue("?", textBox3.Text); // FirstName
                cmd.Parameters.AddWithValue("?", textBox4.Text); // LastName
                cmd.Parameters.AddWithValue("?", textBox5.Text); // Address
                cmd.Parameters.AddWithValue("?", textBox6.Text); // PostCode
                cmd.Parameters.AddWithValue("?", textBox7.Text); // Telephone
                cmd.Parameters.AddWithValue("?", dataGridView1.SelectedRows[0].Cells[0].Value?.ToString() ?? "");

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("UPDATED SUCCESSFULLY");
                Dataview();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }



        //CLICKING OF DATA GRID
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataGridView1.SelectedRows.Count > 0) //CHEKK
                {
                    textBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value?.ToString() ?? "";
                    textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString() ?? "";
                    textBox4.Text = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString() ?? "";
                    textBox5.Text = dataGridView1.SelectedRows[0].Cells[3].Value?.ToString() ?? "";
                    textBox6.Text = dataGridView1.SelectedRows[0].Cells[4].Value?.ToString() ?? "";
                    textBox7.Text = dataGridView1.SelectedRows[0].Cells[5].Value?.ToString() ?? "";

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }



        }


        //DELETE
        private void button2_Click(object sender, EventArgs e)
        {


            try
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM csave WHERE StudentID = ?";

                if (string.IsNullOrEmpty(textBox2.Text)) // Only StudentID is needed for deletion
                {
                    MessageBox.Show("Please enter the StudentID to delete.");
                    return;
                }

                cmd.Parameters.AddWithValue("?", textBox2.Text);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record Deleted Successfully", "Access Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dataview();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.ToString());
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        //RESETTT
        private void button5_Click(object sender, EventArgs e)
        {
            Dataview();

            textBox2.Text = " ";
            textBox3.Text = " ";
            textBox4.Text = " ";
            textBox5.Text = " ";
            textBox6.Text = " ";
            textBox7.Text = " ";

            Dataview();
        }
        //SEARCH
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;

               
                cmd.CommandText = "SELECT * FROM csave WHERE StudentID = ? OR FirstName = ? OR LastName = ?";

                cmd.Parameters.AddWithValue("?", textBox1.Text);
                cmd.Parameters.AddWithValue("?", textBox1.Text);
                cmd.Parameters.AddWithValue("?", textBox1.Text);

                DataTable dt = new DataTable();
                OleDbDataAdapter dp = new OleDbDataAdapter(cmd);
                dp.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


        }
    }
}