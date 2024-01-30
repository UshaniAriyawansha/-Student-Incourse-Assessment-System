using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentLoginSystem
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            
        }

        private void Home_Load(object sender, EventArgs e)
        {
         
            //this.Hide();
            //Login loginForm = new Login();
            //this.Enabled = false;
            //loginForm.Show(this);
        }



        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            var client = new MongoClient("mongodb+srv://sndn:123LLLlll@cluster1.at34znm.mongodb.net/?retryWrites=true&w=majority");
            var database = client.GetDatabase("usrs");
            var collection = database.GetCollection<BsonDocument>("UserData");
            var document = new BsonDocument
            {
                    {"Name", comboBoxTitle.SelectedValue.ToString() /*+ textBoxFirstName +textBoxMiddleName + textBoxLastName*/},
                    {"Email", textBoxEmail.Text },
                    // {"password", textBoxRegPassword.Text}

            };
            collection.InsertOne(document);
        }

        private void comboBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            //comboBoxTitle.Text("Mr");
        }
    }
}
