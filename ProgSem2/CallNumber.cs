using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static ProgSem2.FormFindCallNumbers;

namespace ProgSem2
{
    internal class CallNumber
    {
        // Constants
        public static int MAX_TOP_LEVELS = 7;
        public static int MAX_LEVELS = 3;
        public static int MAX_OPTIONS = 4;

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

        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Populate library tree
        /// </summary>
        /// <param name="buttons"></param>
        public static void BuildLibraryTree(Button[] buttons)
        {
            // root node of the library tree
            libraryRoot = CreateLibraryNode("Library Classification", "General");

            // reset current level to 0 when populating the library tree
            currentLevel = 0;

            // Read data from the library file to get tree data
            string[] lines = File.ReadAllLines(LIBRARY_FILE_PATH);

            int level1Index = 0;
            int level2Index = 0;

            foreach (string line in lines)
            {
                string[] words = line.Split('+');

                // switch based on the number associated with the call number level (level 1, 2, 3)
                switch (words[0])
                {
                    case "1":
                        libraryRoot.children.Add(CreateLibraryNode(words[1], words[2]));
                        level2Index = 0;
                        level1Index++;
                        break;
                    case "2":
                        libraryRoot.children[level1Index - 1].children.Add(CreateLibraryNode(words[1], words[2]));
                        level2Index++;
                        break;
                    case "3":
                        libraryRoot.children[level1Index - 1].children[level2Index - 1].children.Add(CreateLibraryNode(words[1], words[2]));
                        break;
                }
            }
            numTopLevelNodes = level1Index;
            SetLibraryButtons(buttons);
        }

        /// <summary>
        /// Represents a node in the library tree
        /// </summary>
        public class LibraryNode
        {
            public string key;
            public string value;
            public List<LibraryNode> children = new List<LibraryNode>();
        };

        /// <summary>
        /// Utility function to create a new library tree node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static LibraryNode CreateLibraryNode(string key, string value)
        {
            LibraryNode temp = new LibraryNode();
            temp.key = key;
            temp.value = value;
            return temp;
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------

        #region
        /// <summary>
        /// Populate buttons with call numbers and descriptions from the library tree
        /// </summary>
        /// <param name="buttons"></param>
        public static void SetLibraryButtons(Button[] buttons)
        {
            string[] callNumbers = new string[MAX_OPTIONS];

            // select which level of call numbers to populate the callNumbers array with
            switch (currentLevel)
            {
                case 0:
                    for (int i = 0; i < MAX_OPTIONS; i++)
                    {
                        callNumbers[i] = libraryRoot.children[topLevelIndices[i]].key + "\n" + libraryRoot.children[topLevelIndices[i]].value;
                    }
                    break;
                case 1:
                    for (int i = 0; i < MAX_OPTIONS; i++)
                    {
                        callNumbers[i] = libraryRoot.children[selectedIndices[0]].children[i].key + "\n" + libraryRoot.children[selectedIndices[0]].children[i].value;
                    }
                    break;
                case 2:
                    for (int i = 0; i < MAX_OPTIONS; i++)
                    {
                        callNumbers[i] = libraryRoot.children[selectedIndices[0]].children[selectedIndices[1]].children[i].key + "\n" + libraryRoot.children[selectedIndices[0]].children[selectedIndices[1]].children[i].value;
                    }
                    break;
            }

            // populate buttons with callNumbers array values
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text = callNumbers[i];
            }
        }

        /// <summary>
        /// Populate topLevelIndices list with unique random numbers
        /// </summary>
        public static void SetRandomTopLevelIndices()
        {
            topLevelIndices.Clear();
            while (topLevelIndices.Count != 4)
            {
                int randomIndex = random.Next(0, MAX_TOP_LEVELS);
                if (!topLevelIndices.Contains(randomIndex))
                {
                    topLevelIndices.Add(randomIndex);
                }
            }
            topLevelIndices.Sort();
        }

        /// <summary>
        /// Set array selectedAnswers to contain answers for comparisons
        /// </summary>
        public static void SetAnswers()
        {
            selectedAnswers[0] = libraryRoot.children[selectedIndices[0]].key + "\n" + libraryRoot.children[selectedIndices[0]].value;
            selectedAnswers[1] = libraryRoot.children[selectedIndices[0]].children[selectedIndices[1]].key + "\n" + libraryRoot.children[selectedIndices[0]].children[selectedIndices[1]].value;
            selectedAnswers[2] = libraryRoot.children[selectedIndices[0]].children[selectedIndices[1]].children[selectedIndices[2]].key + "\n" + libraryRoot.children[selectedIndices[0]].children[selectedIndices[1]].children[selectedIndices[2]].value;
        }

        /// <summary>
        /// Set label to top-level library classification description
        /// </summary>
        /// <param name="label"></param>
        public static void SetToFindLabel(Label label)
        {
            // randomly choose which top-level category from the list of call numbers being used in [topLevelIndices]
            while (true)
            {
                int randomIndex = random.Next(0, MAX_TOP_LEVELS);
                if (topLevelIndices.Contains(randomIndex))
                {
                    selectedIndices[0] = randomIndex;
                    break;
                }
            }

            // randomly selected description
            for (int i = 1; i < MAX_LEVELS; i++)
            {
                selectedIndices[i] = random.Next(0, MAX_LEVELS);
            }

            SetAnswers();
            label.Text = "Where would this be:\n" + libraryRoot.children[selectedIndices[0]].children[selectedIndices[1]].children[selectedIndices[2]].value + "?";
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------

        #region
        /// <summary>
        /// Method for when an option is selected
        /// </summary>
        /// <param name="btnSender"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public static bool OnButtonClicked(object btnSender, Button[] buttons, Label label)
        {
            Button clickedButton = (Button)btnSender;

            if (IsAnswerCorrect(clickedButton.Text))
            {
                currentLevel++;
                AddPoints(label);
                if (currentLevel == 3)
                {
                    MessageBox.Show("Yippie!" +
                        "\nYou scored: " +
                        sessionPoints, "Millionare!");
                    return true;
                }
                SetLibraryButtons(buttons);
            }
            else
            {
                currentLevel++;
                DeductPoints(label);
                MessageBox.Show("Wrong!" +
                    "\nCorrect call number was:\n\n" +
                    selectedAnswers[currentLevel - 1], "Wrong!");
                if (currentLevel == 3)
                {
                    return true;
                }
                SetLibraryButtons(buttons);
            }
            return false;
        }

        /// <summary>
        /// Checks if the correct answer was selected
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public static bool IsAnswerCorrect(string answer)
        {
            if (!answer.Equals(selectedAnswers[currentLevel]))
            {
                return false;
            }
            return true;

        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Deduct points from current points
        /// </summary>
        public static void DeductPoints(Label label)
        {
            // deduct points
            sessionPoints += INCORRECT_POINTS;
            label.Text = sessionPoints.ToString();
        }

        /// <summary>
        /// Add points and update high score
        /// </summary>
        public static void AddPoints(Label label)
        {
            if (seconds == 0)
            {
                seconds = 1;
            }
            // calculate points
            sessionPoints += CORRECT_POINTS + (TIME_BONUS / seconds);

            // check if it is a new high score
            UpdateHighScore(sessionPoints);

            // add points
            label.Text = sessionPoints.ToString();
        }

        /// <summary>
        /// Update high score
        /// </summary>
        /// <returns></returns>
        public static void UpdateHighScore(int score)
        {
            // check if it is a new high score
            if (score > highScore)
            {
                highScore = score;
            }
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------

    }
}
