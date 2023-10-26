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
    public partial class ReplaceBooks : Form
    {

        Random random = new Random();


        Label firstClick, secondClick;
        public ReplaceBooks()
        {
            InitializeComponent();
            AssignDeweyDecimalsToSquare();
        }

        private string GenerateRandomDeweyDecimal()
        {
            int wholePart = random.Next(0, 1000); // Generate a random number between 0 and 999
            int decimalPart = random.Next(0, 10);  // Generate a random digit for the decimal part

            return $"{wholePart:D3}.{decimalPart}";
        }


        private void AssignDeweyDecimalsToSquare()
        {
            List<Label> labels = new List<Label>();

            // Find all labels and store them in a list
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Label label)
                {
                    labels.Add(label);
                }
            }

            // Shuffle the labels to randomize their order
            Shuffle(labels);

            // Assign Dewey Decimal numbers to pairs of labels
            for (int i = 0; i < labels.Count; i += 2)
            {
                string deweyDecimal = GenerateRandomDeweyDecimal();
                labels[i].Text = deweyDecimal;
                labels[i + 1].Text = deweyDecimal;
            }
        }
        //Find all the labels and store them in a list then randomise it
        //Assign pairs to make to each pair of labels have the same call number
        //Used chatgpt to help me with this bit as i struggled endlessly to figure it out
        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


        private void label_Click(object sender, EventArgs e)
        {
            if (firstClick != null && secondClick != null)
                return;

            Label clickLabel = sender as Label;

            if (clickLabel == null)
                return;

            if (clickLabel.ForeColor == Color.Black)
                return;

            if (firstClick == null)
            {
                firstClick = clickLabel;
                firstClick.ForeColor = Color.Black;
                return;
            }

            secondClick = clickLabel;
            secondClick.ForeColor = Color.Black;

            CheckForWinner();

            if (firstClick.Text == secondClick.Text)
            {
                firstClick = null;
                secondClick = null;
            }
            else
                timer1.Start();

        }

        private void CheckForWinner()
        {
            Label label;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                {
                    return;
                }
            }

            MessageBox.Show("You did it! Score: 10");
            this.Hide();
            Menu mn = new Menu();
            mn.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClick.ForeColor = firstClick.BackColor;
            secondClick.ForeColor = secondClick.BackColor;

            firstClick = null;
            secondClick = null;
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {

        }
    }
}

