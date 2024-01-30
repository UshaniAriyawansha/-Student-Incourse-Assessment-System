using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace StudentLoginSystem
{
    public partial class Login : Form
    {
        private Timer _timer;
        public Login()
        {
            InitializeComponent();

            //panelWelcome.BackColor = Color.Red;
            //panelLogin.BackColor = Color.Green;
            //panelRegister.BackColor = Color.Yellow;
            panelRegister.Hide();

            _timer = new Timer(TimerCallback, null, 0, 1000);

        }
        private void registermenu()
        {
            panelLogin.Visible = false;
            panelRegister.Visible = true;
            panelWelcome.Dock = DockStyle.Right;
            panelRegister.Dock = DockStyle.Fill;
        }

        private void loginmenu()
        {
            panelLogin.Visible = true;
            panelRegister.Visible = false;
            panelWelcome.Dock = DockStyle.Left;
            panelLogin.Dock = DockStyle.Fill;
        }

        private void linkLabelGoToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registermenu();
        }

        private void linkLabelGoToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginmenu();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            Createacc();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LoginToSys();
        }

        private void Createacc()
        {
            var client = new MongoClient("mongodb+srv://sndn:123LLLlll@cluster1.at34znm.mongodb.net/?retryWrites=true&w=majority");
            var database = client.GetDatabase("usrs");
            var collection = database.GetCollection<BsonDocument>("normalusrs");
            var document = new BsonDocument
            {
                    {"username", textBoxRegUserName.Text},
                    {"Email", textBoxRegEmail.Text },
                    {"password", textBoxRegPassword.Text}
                    
            };
            collection.InsertOne(document);
        }

        private void LoginToSys()
        {
            var client = new MongoClient("mongodb+srv://sndn:123LLLlll@cluster1.at34znm.mongodb.net/?retryWrites=true&w=majority");
            var database = client.GetDatabase("usrs");
            var collection = database.GetCollection<BsonDocument>("normalusrs");

            // Get the username and password from the login form
            var username = textBoxUserName.Text;
            var password = textBoxPassword.Text;

            // Create a filter
            var filter = Builders<BsonDocument>.Filter.Eq("username", username);

            // Use Find method
            var result = collection.Find(filter).ToList();

            // Check if the result is empty
            if (!result.Any())
            {
                // The username and password combination is incorrect
                MessageBox.Show("Username and password combination is Error");
                return;
            }

            // Get the password from the document
            var dbPassword = result[0]["password"].AsString;

            // Validate the password
            if (password != dbPassword)
            {
                // The username and password combination is incorrect
                MessageBox.Show("Username and password combination is Error");
                return;
            }

            // The login is successful
            //this.Owner.Enabled = true;
            this./*Close*/Hide();
            Form home = new Home();
            home.Show();
            MessageBox.Show("Login to system");

        }
        private void TimerCallback(object state)
        {
            bool isConnected = CheckConnection();
            if (isConnected)
            {

                pictureBoxStatusIndicatorInternet.BackColor = Color.Green;
            }
            else
            {

                pictureBoxStatusIndicatorInternet.BackColor = Color.Red;
            }
        }

        private bool CheckConnection()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("google.com");
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkVisible.Checked==true)
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }

        private void pictureBoxStatusIndicatorInternet_Click(object sender, EventArgs e)
        {

        }
    }

}
