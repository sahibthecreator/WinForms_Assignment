using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CarRent
{
    public partial class MainForm : Form
    {

        private Company company;
        private List<Car> cars;

        private List<Label> carNames;

        string MySQLConnectionString;
        MySqlConnection conn;
        public MainForm()
        {
            InitializeComponent();
            company = new Company("Rent Cars");
            cars = new List<Car>();
            carNames = new List<Label>();
            carNames.Add(label3);
            carNames.Add(label4);
            carNames.Add(label5);
            carNames.Add(label6);

            MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=rentcar";
            conn = new MySqlConnection(MySQLConnectionString);

            this.ContextMenuStrip = StripMenu;
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("Select * from cars", conn);
            cmd.CommandTimeout = 60;

            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        Car car = new Car(reader.GetString(1), reader.GetString(2));
                        cars.Add(car);
                        carNames[i].Text = reader.GetString(1) + ", " + reader.GetString(2);
                        carNames[i].Name = reader.GetString(1);
                        //MessageBox.Show("i = : " + i + " - " + reader.GetString(1) + ", " + reader.GetString(2));
                        i++;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowDialog(label3.Name);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form f in System.Windows.Forms.Application.OpenForms)
                f.Close();
        }

        public string ShowDialog(string name)
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 250,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Rent dates",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label startTxt = new Label() { Left = 30, Top = 55, Text = "Start Date: " };
            DateTimePicker dateTimePicker = new DateTimePicker() { Left = 100, Top = 50 };

            Label endTxt = new Label() { Left = 30, Top = 85, Text = "End Date: " };
            DateTimePicker dateTimePicker1 = new DateTimePicker() { Left = 100, Top = 80 };

            Button confirmation = new Button() { Text = "Ok", Left = 200, Width = 100, Top = 180, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { InsertDB(); prompt.Close(); };
            prompt.Controls.Add(dateTimePicker);
            prompt.Controls.Add(startTxt);
            prompt.Controls.Add(dateTimePicker1);
            prompt.Controls.Add(endTxt);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            void InsertDB()
            {
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO `rentedcars`(`carName`, `startDate`, `endDate`) VALUES ('{name}', '{dateTimePicker.Value.ToString()}', '{dateTimePicker1.Value.ToString()}')", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
                foreach (Car car in cars)
                {
                    if (car.Name.Equals(name))
                    {
                        RentCar rentCar = new RentCar(car, dateTimePicker.Value, dateTimePicker1.Value);
                        company.AddRentCars(rentCar);
                    }
                }

                label9.Text = "Car: " + company.RentCars[company.RentCars.Count - 1].Car.Name;
                label10.Text = "Rent start date: " + company.RentCars[company.RentCars.Count - 1].StartDate;
                label11.Text = "Rent end date: " + company.RentCars[company.RentCars.Count - 1].EndDate;
                label12.Text = "Total days: " + (company.RentCars[company.RentCars.Count - 1].EndDate - company.RentCars[company.RentCars.Count - 1].StartDate).Days;
                company.RentCars[company.RentCars.Count - 1].Period = (company.RentCars[company.RentCars.Count - 1].EndDate - company.RentCars[company.RentCars.Count - 1].StartDate).Days;
            }
            return prompt.ShowDialog() == DialogResult.OK ? dateTimePicker.Text : "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowDialog(label4.Name);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowDialog(label5.Name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowDialog(label6.Name);
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            label13.Text = "Total rent fee: " + company.RentCars[company.RentCars.Count - 1].GetRentFee(Int32.Parse(textBox1.Text)) + "$";
            company.RentCars[company.RentCars.Count - 1].KmDriven = Int32.Parse(textBox1.Text);

            label15.Text = "Total Income: " + company.getTotalIncome() + "$";
            label16.Text = "Average distance: " + company.getAvgDistance() + "Km";
            label17.Text = "Longest period: " + company.getLongestPeriod() + " days";
        }

        private void viewCarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void calculateRentFeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void companyInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}
