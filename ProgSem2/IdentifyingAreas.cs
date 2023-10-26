using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgSem2
{
    public partial class IdentifyingAreas : Form
    {
        // form controls
        public ListBox lbleft = new ListBox();
        public ListBox lbright = new ListBox();
        public Label lblScoreLabel = new Label();

        // dictionary for pairs and list to hold answers for comparison
        public static Dictionary<string, string> DeweyPairs = new Dictionary<string, string>();
        public List<string> strList = new List<string>();

        Random random = new Random();
        int counter, counter2;

        public IdentifyingAreas()
        {
            InitializeComponent();
            lbleft = listBox1;
            lbright = listBox2;
            lblScoreLabel = scorelbl;
            
        }
        public void PopulateDictionary()
        {
            DeweyPairs["000"] = "General Knowledge";
            DeweyPairs["100"] = "Philosophy and Psychology";
            DeweyPairs["200"] = "Religion";
            DeweyPairs["300"] = "Social Sciences";
            DeweyPairs["400"] = "Languages";
            DeweyPairs["500"] = "Science";
            DeweyPairs["600"] = "Technology";
            DeweyPairs["700"] = "Arts and Recreation";
            DeweyPairs["800"] = "Literature";
            DeweyPairs["900"] = "History and Geography";
        }
        public void PopulateListBox()
        {
            PopulateDictionary();
            counter = 0;
            counter2 = 0;

            for (int i = 0; i < 4; i++)
            {
                int randInt = random.Next(0, DeweyPairs.Count);
                lbleft.Items.Add(DeweyPairs.ElementAt(randInt).Key);
                strList.Add(DeweyPairs.ElementAt(randInt).Value);
            }

            for (int i = 0; i < 3; i++)
            {
                int randInt = random.Next(0, DeweyPairs.Count);
                strList.Add(DeweyPairs.ElementAt(randInt).Value);
            }

            strList.Sort();
            for (int i = 0; i < strList.Count; i++)
            {
                lbright.Items.Add(strList[i]);
            }
        }


        public void AddItems()
        {

            if (lbleft.SelectedItem == null)
            {
                MessageBox.Show("Please select a value from the list of call numbers on the left");
            }
            else

            if (DeweyPairs[lbleft.Text] != lbright.Text)
            {
                counter2 = counter2 + 1;
                lbleft.Items.Remove(lbleft.SelectedItem);
                lbright.Items.Remove(lbright.SelectedItem);
            }
            else
            {
                counter = counter + 1;
                counter2 = counter2 + 1;
                lbleft.Items.Remove(lbleft.SelectedItem);
                lbright.Items.Remove(lbright.SelectedItem);
            }

            if (counter2 == 4)
            {
                scorelbl.Visible = true;
                scorelbl.Text = "Your score is " + counter + "/4";

                DialogResult dr = MessageBox.Show("Would you like to play again?", "Game Over!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    // if yes reload form
                    IdentifyingAreas ida = new IdentifyingAreas();
                    ida.Show();
                    this.Dispose(false);
                }
                else if (dr == DialogResult.No)
                {
                    // if no take them back to home page
                    this.Visible = false;
                    Menu mn = new Menu();
                    mn.Show();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddItems();
        }

        private void IdentifyingAreas_Load(object sender, EventArgs e)
        {
            PopulateListBox();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu mn = new Menu();
            mn.Show();
        }

        private void IdentifyingAreas_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        public void ResetForm()
        {
         
            IdentifyingAreas ida = new IdentifyingAreas();
            ida.Show();
            this.Dispose(false);

        }
    }
}
