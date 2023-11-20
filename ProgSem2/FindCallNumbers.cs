using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProgSem2.CallNumber;

namespace ProgSem2
{
    public partial class FormFindCallNumbers : Form
    {

        // constants
        public static int MAX_TOP_LEVELS = 7;
        public static int MAX_LEVELS = 3;
        public static int MAX_OPTIONS = 4;

        //the file is stored in the bin->debug folder
        private static string LIBRARY_FILE_PATH = "callNo.txt";

        // global variables
        public static int currentLevel;
        public static int numTopLevelNodes;
        public static int[] selectedIndices = new int[MAX_LEVELS];
        public static string[] selectedAnswers = new string[MAX_LEVELS];
        static LibraryNode libraryRoot;
        public static Random random = new Random();
        public static List<int> topLevelIndices = new List<int>();

        // scoring variables
        public static int INCORRECT_POINTS = -500;
        public static int CORRECT_POINTS = 250;
        public static int TIME_BONUS = 10000;
        public static int seconds;
        public static int highScore = 0;
        public static int sessionPoints = 0;

        // form controls
        private Button[] optionButtons;
        private Label toFind;

        public FormFindCallNumbers()
        {
            InitializeComponent();
            StartGame();
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        private void StartGame()
        {
            // resetting scoring
            seconds = 0;
            sessionPoints = 0;
            lblTime.Text = seconds.ToString();
            lblHighScore.Text = highScore.ToString();

            // form controls
            optionButtons = new Button[] { btnOption1, btnOption2, btnOption3, btnOption4 };
            toFind = lblToFind;

            SetRandomTopLevelIndices();
            BuildLibraryTree(optionButtons);
            SetToFindLabel(toFind);
            lblCurrentPoints.Text = sessionPoints.ToString();
            timer1.Start();
        }

        /// <summary>
        /// Reset game button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            //restart game
            StartGame();
        }

        /// <summary>
        /// Help button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("Select the call number and description that is a " +
                "\nhigher tier of said description" +
                "\ndisplayed in the centre of the screen", "Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
            timer1.Start();
        }
        //-----------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Timer, used to calculate points
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            lblTime.Text = seconds.ToString();
        }

        //-----------------------------------------------------------------------------------------------------------------------

        private void btnOption1_Click_1(object sender, EventArgs e)
        {
            if (OnButtonClicked(sender, optionButtons, lblCurrentPoints))
            {
                StartGame();
            };
        }

        private void btnOption2_Click_1(object sender, EventArgs e)
        {
            if (OnButtonClicked(sender, optionButtons, lblCurrentPoints))
            {
                StartGame();
            };
        }

        private void btnOption3_Click_1(object sender, EventArgs e)
        {
            if (OnButtonClicked(sender, optionButtons, lblCurrentPoints))
            {
                StartGame();
            };
        }

        private void btnOption4_Click_1(object sender, EventArgs e)
        {
            if (OnButtonClicked(sender, optionButtons, lblCurrentPoints))
            {
                StartGame();
            };
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu mn = new Menu();
            mn.Show();
        }
    }
}
