using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO; //Needed for Directory.GetFiles
using System.Windows.Forms; // Needed for MessageBox

namespace SlotMachineMegaChallenge
{
    public partial class Default : System.Web.UI.Page
    {
        
        int userWinnings = 0;
        int userBet = 0;
        int winningsMultiplier = 0;
        
        string[] displayedImages = new string[3];
        //Load all png files into an array for random pulling later
        string[] imageNames = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Images"), "*.png");

        //New random seed for random pulling of images. Called once outside of all methods to ensure
        //time dependent seeds are not made at the same time .Next is called.
        Random random = new Random();

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                ViewState["userAmount"] = 100;
                int userAmount = Convert.ToInt32(ViewState["userAmount"]);
                moneyLabel.Text = String.Format("Player's Money: {0:C}", userAmount);
                spin();
            }
            
        }

        public void spin()
        {
            //Load three random images.
            displayedImages[0] = randomImageURL();
            displayedImages[1] = randomImageURL();
            displayedImages[2] = randomImageURL();
            

            //My hack to pull the URL of the images that are in an IMAGES folder which has 6 characters
            //and therefore I subtract 6 from the index of the last backslash.
            Image1.ImageUrl = displayedImages[0].Substring(displayedImages[0].LastIndexOf(("\\")) - 6);
            Image2.ImageUrl = displayedImages[1].Substring(displayedImages[1].LastIndexOf(("\\")) - 6);
            Image3.ImageUrl = displayedImages[2].Substring(displayedImages[2].LastIndexOf(("\\")) - 6);
        }

        protected void leverButton_Click(object sender, EventArgs e)
        {
            int userAmount = Convert.ToInt32(ViewState["userAmount"]);
            
            if (!int.TryParse(betBox.Text.Trim(), out userBet)) Response.Redirect(Request.RawUrl);
            if (userAmount < userBet)
            {
                MessageBox.Show("You can't bet more than you have.", "Bet exceeds holdings.", MessageBoxButtons.OK);
                betBox.Text = userAmount.ToString();
            }
            else
            {
                spin();
                userAmount = userAmount - userBet;
                ViewState["userAmount"] = userAmount;
                moneyLabel.Text = String.Format("Player's Money: {0:C}", userAmount);
                calculateResults();

                if (userAmount <= 0)
                {
                    MessageBox.Show("You're out of money. Here, let us stake you. ", "Bust!", MessageBoxButtons.OK);
                    userAmount = 100;
                    ViewState["userAmount"] = userAmount;
                    moneyLabel.Text = String.Format("Player's Money: {0:C}", userAmount);
                }


            }
            



            
        }

        private void calculateResults()
        {
            int userAmount = Convert.ToInt32(ViewState["userAmount"]);
            if(!barExists())
            {
                //execute cherries boost
                if(jackpot())
                {
                    userWinnings = (userBet * winningsMultiplier);
                    userAmount = userAmount + userWinnings;
                    ViewState["userAmount"] = userAmount;
                    userWins();
                }
                else if(cherryBoost())
                {
                    userWinnings = (userBet * winningsMultiplier);
                    userAmount = userAmount + userWinnings;
                    ViewState["userAmount"] = userAmount;
                    userWins();
                    
                }
                
                else
                {
                    resultLabel.Text = String.Format("You bet {0:C}. Sorry, better luck next time!", userBet);
                    
                }
                
            }
            else
            {
                resultLabel.Text = String.Format("You bet {0:C}. Sorry, better luck next time!", userBet);
                
            }
            
        }

        
        protected string randomImageURL()
        {
            //return a string with the URL of a randomly selected image.

            int randomIndex = (random.Next(0, imageNames.Count()));
            string imageURL = imageNames[randomIndex];

            return (imageURL);
        }
            //Use random number generator to select an index
            //pull image name at that index
            //assemble string with selected name to form URL
            //apply url to image
        //Check for number of cherries
        //Check for presence of a bar
        //Check for 3 7s
        //Calculate winnings
        //Print Adjusted User amount
        //Print result sentence

        protected string getFileName(string relImagePath)
        {
            string filename = relImagePath.Substring(relImagePath.LastIndexOf(("\\"))+1);
            return filename;
        }

        protected bool cherryBoost()
        {
            int cherryCount = 0;
            for (int i = 0; i <= 2; i++)
            {
                if(getFileName(displayedImages[i]) == "Cherry.png")
                {
                    cherryCount = cherryCount + 1;
                }
            }
            if (cherryCount == 0)
            {
                return false;
            }

            else
            {
                switch (cherryCount)
                {
                    case 1:
                        winningsMultiplier = 2;
                        break;
                    case 2:
                        winningsMultiplier = 3;
                        break;
                    case 3:
                        winningsMultiplier = 4;
                        break;

                }
                return true;
            }
             
        }
        protected bool jackpot()
        {
            int sevenCount = 0;
            for (int i = 0; i <= 2; i++)
            {
                if (getFileName(displayedImages[i]) == "Seven.png")
                {
                    sevenCount = sevenCount + 1;
                }
            }
            if (sevenCount == 3)
            {
                winningsMultiplier = 100;
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool barExists()
        {
            for (int i = 0; i <= 2; i++)
            {
                if (getFileName(displayedImages[i]) == "Bar.png")
                {
                    return true;
                }
            }
            return false;
            
        }

        protected void userWins()
        {
            int userAmount = Convert.ToInt32(ViewState["userAmount"]);
            

            resultLabel.Text = String.Format("You bet {0:C}. You won {1:C}!", userBet, userWinnings);
            moneyLabel.Text = String.Format("Player's Money: {0:C}", userAmount);

        }
        
    }
}