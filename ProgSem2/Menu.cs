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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnReplaceBooks_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReplaceBooks replaceBooksForm = new ReplaceBooks();
            replaceBooksForm.Show();
        }

        private void btnIdentifyAreas_Click(object sender, EventArgs e)
        {
            this.Hide();
            IdentifyingAreas identifyAreasForm = new IdentifyingAreas();
            identifyAreasForm.Show();
        }

        private void btnFindCallNumber_Click(object sender, EventArgs e)
        {
            /* this.Hide();
            FindCallNumbers findCallNumbersForm = new FindCallNumbers();
            findCallNumbersForm.Show();*/

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
