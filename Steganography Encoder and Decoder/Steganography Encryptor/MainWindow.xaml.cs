using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using System;

namespace Steganography_Encryptor {
    /// <summary>
    /// Users can type his/her own message into the application and it will be encrypted from char to bytes and hidden into the image of chose.
    /// There is a decryption application that can decode the user's hidden message 

    public partial class MainWindow : Window {
        //Global variables       
        string filePath = "";
        PortablePixelMap ppmImg;
        string filePath2 = "";
        PortablePixelMap ppmImg2;

        public MainWindow() {
            InitializeComponent();
        }//end event

        #region MENU BAR 
        /*
        private void openTab_Click(object sender, RoutedEventArgs e) {
            //create instance of openfile dialog class
            OpenFileDialog imgSelection = new OpenFileDialog();

            //Filter for desired file types 
            imgSelection.Filter = "Portable PixelMap image (*ppm)|*ppm";

            //Displays dialog box and checks if actual is opened 
            bool isSelected = imgSelection.ShowDialog() == true;

            //Keep record of file path for later use 
            filePath = imgSelection.FileName;

            //Valid input: if user selected a file then open the image and send to imagebox
            if (isSelected) { //Read PPM files
                //Declare object
                PortablePixelMap ppm = new PortablePixelMap(filePath);

                //Display ppm thru xaml 
                imgDisplay.Source = ppm.MakeBitmap();

                //Save portable pixel map image to global variable
                ppmImg = ppm;

            }//end if
        }//end event 
        */

        //Saves image from encryption screen (as is)
        private void saveTab_Click(object sender, RoutedEventArgs e) {
            //Initiates save event if filepath isn't empty
            if (filePath != String.Empty) {
                //Create instance 
                SaveFileDialog saveFile = new SaveFileDialog();

                //Set default file extension 
                saveFile.FileName = "ppm";
                saveFile.DefaultExt = "ppm";

                //Filters for desired file type 
                saveFile.Filter =
                "Portable PixelMap P3 image (*.ppm)|*.ppm" +
                "|Portable PixelMap P6 image (*.ppm)|*.ppm";

                //Validates if filepath is established
                if (saveFile.ShowDialog() == true) {
                    //Specifies which ext type was choise
                    int filterIndex = saveFile.FilterIndex;

                    //Initiate Save
                    ppmImg.SaveImage(saveFile.FileName, filterIndex);

                    //Confirm encryption 
                    MessageBox.Show("Saved");
                } else {
                    MessageBox.Show("Not Saved");
                }//end else 
            }//end if 
        }//end event 

        private void exitTab_Click(object sender, RoutedEventArgs e) {
            Close();
        }//end event 

        private void aboutTab_Click(object sender, RoutedEventArgs e) {
            //Declare Process object - enables start or stop task processing 
            Process webDoc = new Process();

            //Specifies os shell should be used to start process
            webDoc.StartInfo.UseShellExecute = true;

            //Reference file path - direct users to your help me guide 
            webDoc.StartInfo.FileName = "https://github.com/mrkennytran/C-Steganography-2.0/blob/main/README.md";

            //Run task 
            webDoc.Start();
        }//end event 
        #endregion

        #region PROGRAM EVENTS
        //Encrypt
        private void hideBtn_Click(object sender, RoutedEventArgs e) {
           //(y == (int)(ppmImg.Height - 1) / 2)
            if (secretMsgBox.Text == string.Empty || imgDisplay.Source == null) { // Valid input: empty text box or empty image
                MessageBox.Show("Upload a ppm image and enter a message.");
            } else {
                if (secretMsgBox.Text.Length > ppmImg.Height - 2 || secretMsgBox.Text.Length > ppmImg.Width - 2) { //Valid input: Message too large for image 
                    MessageBox.Show("Message is too long to encrypt in image.");

                } else {
                    int counter = 0; //count for pixel data 

                    //Setting up stopping point for decoding
                    string editMsg = secretMsgBox.Text + "/";// here 
                    string reverseMsg = Reverse(editMsg);
                    reverseMsg += "=";

                    //Split string to char array 
                    char[] msgArray = reverseMsg.ToCharArray();

                    for (int y = 0; y <= ppmImg.Height - 1; y++) {
                        for (int x = 0; x <= ppmImg.Width - 1; x++) {
                            //Get current pixel color 
                            Color pixelClr = ppmImg.GetPixelColor(x, y);

                            if (x > 0 && y < ppmImg.Height - 1) {//Paramter to where pixels can be set 
                                if (counter <= msgArray.Length - 1) {//Set stopping point for character encryption 
                                    //Char index to char 
                                    char character = msgArray[counter];

                                    //Get dec char value from user input 
                                    int decVal = character - 0;

                                    //Encrypt current char into pixel 
                                    pixelClr = Encode(decVal, pixelClr);

                                    //Set encrypted pixel into ppm
                                    ppmImg.SetPixel(x, y, pixelClr.R, pixelClr.G, pixelClr.B);

                                    //Keep track of index of secret message 
                                    counter++;
                                }//end if 
                            }//end if 

                        }//end for 
                    }//end for

                    //Save encrypted image
                    SaveEncode();
                }//end else
            } //end else if  

            //Remove stop trigger from display 
            string finalMsg = secretMsgBox.Text.Substring(0, secretMsgBox.Text.Length);
            secretMsgBox.Text = finalMsg;
        }//end event  

        //Counter
        private void secretMsgBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) { //DISPLAYS TEXT COUNT 
            if (secretMsgBox.Text.Length <= 256) {
                string userInput = secretMsgBox.Text;

                //Get user input count 
                int charCount = userInput.Length;

                //Display count
                textCount.Text = charCount.ToString();
            }//end if
        }//end event

        //Decrypt
        private void decodeBtn_Click(object sender, RoutedEventArgs e) {
            if (ppmImg2 != null) {
                string message = string.Empty;
                int counter = 0;
                int maxCharCount = ppmImg2.Width * 2;
                string letter = string.Empty;
                string stop = string.Empty;

                //Iterate thru each pixel and decrypt the hidden message
                for (int y = 0; y <= ppmImg2.Height - 1; y++) {
                    for (int x = 0; x <= ppmImg2.Width - 1; x++) {
                        Color pixelClr = ppmImg2.GetPixelColor(x, y);

                        //(y == (int)(ppmImg.Height - 1) / 2)
                        if (x > 0 && y < ppmImg2.Height - 1) { //Location of message
                            if (counter < maxCharCount) { //Stopping point
                                                          //Decode letter from pixel
                                letter = Decode(pixelClr);

                                //Stops decryption if "\" key symbol doesn't exist
                                if (counter == 0 && letter != "/") {
                                    MessageBox.Show("No hidden message exists");
                                    return;
                                }//end if 

                                //Add character to the message 
                                message += letter;

                                //Limit output of additional text on wpf textbox
                                counter++;
                            }//end if 
                        }//end if 
                    }//end for 
                }//end for 

                //Trim message and make it readable
                string cleanMsg = CleanMsg(message);

                //Display message into wpf 
                revealBox.Text = cleanMsg.Trim();
            } else {
                MessageBox.Show("Upload image to extract.");
            }
            
        }//end event

        //Encrypt Browser
        private void encodeBrowser_Click(object sender, RoutedEventArgs e) {
            //create instance of openfile dialog class
            OpenFileDialog imgSelection = new OpenFileDialog();

            //Filter for desired file types 
            imgSelection.Filter = "Portable PixelMap image (*ppm)|*ppm";

            //Displays dialog box and checks if actual is opened 
            bool isSelected = imgSelection.ShowDialog() == true;

            //Keep record of file path for later use 
            filePath = imgSelection.FileName;

            //Valid input: if user selected a file then open the image and send to imagebox
            if (isSelected) { //Read PPM files
                //Declare object
                PortablePixelMap ppm = new PortablePixelMap(filePath);

                //Display ppm thru xaml 
                imgDisplay.Source = ppm.MakeBitmap();

                //Save portable pixel map image to global variable
                ppmImg = ppm;

            }//end if
        }//end event

        //Decrypt Browser
        private void decodeBrowser_Click(object sender, RoutedEventArgs e) {
            //create instance of openfile dialog class
            OpenFileDialog imgSelection = new OpenFileDialog();

            //Filter for desired file types 
            imgSelection.Filter = "Portable PixelMap image (*ppm)|*ppm";

            //Displays dialog box and checks if actual is opened 
            bool isSelected = imgSelection.ShowDialog() == true;

            //Keep record of file path for later use 
            filePath2 = imgSelection.FileName;

            //Valid input: if user selected a file then open the image and send to imagebox
            if (isSelected) { //Read PPM files
                //Declare object
                PortablePixelMap ppm = new PortablePixelMap(filePath2);

                //Display ppm thru xaml 
                imgDisplay2.Source = ppm.MakeBitmap();

                //Save portable pixel map image to global variable
                ppmImg2 = ppm;

            }//end if
        }//end event
        #endregion

        #region METHODS
        private Color Encode(int decVal, Color pixelClr) { //ENCRYPT CURRENT CHARACTER INTO PIXEL - LEASE SIGNFICANT BIT (LSB)
            //Convert current char and RGBs' int values to binary
            string current = BinaryConversion(decVal);
            string R = BinaryConversion(pixelClr.R);
            string G = BinaryConversion(pixelClr.G);
            string B = BinaryConversion(pixelClr.B);

            //Split string to char[]
            char[] currentCharArray = current.ToCharArray();
            char[] redArray = R.ToCharArray();
            char[] greenArray = G.ToCharArray();
            char[] blueArray = B.ToCharArray();

            //Plug current char binary into 8 color components
            redArray[6] = currentCharArray[0];
            redArray[7] = currentCharArray[1];
            greenArray[5] = currentCharArray[2];
            greenArray[6] = currentCharArray[3];
            greenArray[7] = currentCharArray[4];
            blueArray[5] = currentCharArray[5];
            blueArray[6] = currentCharArray[6];
            blueArray[7] = currentCharArray[7];

            //Convert char arrays back to string
            R = new string(redArray);
            G = new string(greenArray);
            B = new string(blueArray);

            //Convert string to int
            int red = Convert.ToInt32(R, 2);
            int green = Convert.ToInt32(G, 2);
            int blue = Convert.ToInt32(B, 2);

            //Convert binary to int to byte 
            pixelClr.R = Convert.ToByte(red);
            pixelClr.G = Convert.ToByte(green);
            pixelClr.B = Convert.ToByte(blue);

            return pixelClr;
        }//end method 

        private void SaveEncode() { //SAVE NEW ENCRYPTED IMAGE 
            //Initiates save event if filepath isn't empty
            if (filePath != String.Empty) {
                //Create instance 
                SaveFileDialog saveFile = new SaveFileDialog();

                //Set default file extension 
                saveFile.FileName = "ppm";
                saveFile.DefaultExt = "ppm";

                //Filters for desired file type 
                saveFile.Filter =
                "Portable PixelMap P3 image (*.ppm)|*.ppm" +
                "|Portable PixelMap P6 image (*.ppm)|*.ppm";

                //Validates if filepath is established
                if (saveFile.ShowDialog() == true) {
                    //Specifies which ext type was choise
                    int filterIndex = saveFile.FilterIndex;

                    //Initiate Save
                    ppmImg.SaveImage(saveFile.FileName, filterIndex);

                    //Confirm encryption 
                    MessageBox.Show("Encryption was successful.");
                } else {
                    MessageBox.Show("Encryption wasn't successful.");
                }//end else 
            }//end if
        }//end method 

        private string BinaryConversion(int input) { //CONVERT CHAR'S ASCII VALUE TO BINARY FORM 
            //Format char's dec value to binary
            string binary = Convert.ToString(input, 2);

            //Reformat strings to have 8 digit placements
            binary = binary.PadLeft(8, '0');

            return binary;
        }//end method 

        public static string Reverse(string message) { //Reverse message for harder detection
            //Used to reverse message to better hide message
            char[] charArray = message.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }//end method

        private string Decode(Color pixel) {
            char[] decodeBin = new char[8];

            //Convert RGB components to binary format
            string R = BinaryConversion(pixel.R);
            string G = BinaryConversion(pixel.G);
            string B = BinaryConversion(pixel.B);

            //Retrieve binary values and saving to string array 
            decodeBin[0] = R[6];
            decodeBin[1] = R[7];
            decodeBin[2] = G[5];
            decodeBin[3] = G[6];
            decodeBin[4] = G[7];
            decodeBin[5] = B[5];
            decodeBin[6] = B[6];
            decodeBin[7] = B[7];

            //Convert char array to string
            string binary = new string(decodeBin);

            //Convert binary value to 
            int decVal = Convert.ToInt32(binary, 2);

            //Convert int value to string
            string hiddenLetter = Convert.ToChar(decVal).ToString();

            return hiddenLetter;
        }//end method 

        private string CleanMsg(string message) {
            string cleanMsg = string.Empty;

            foreach (char index in message) {
                bool isUppercase = index >= 65 && index <= 90;
                bool isLowercase = index >= 97 && index <= 122;
                bool isNumber = index >= 48 && index <= 57;
                bool isSpace = index == 32;
                bool magicKey = index == 61;

                if (isUppercase || isLowercase || isNumber || isSpace) {
                    cleanMsg += index;
                } else if (magicKey) { //Stopping point 
                    //Reverse backwards message 
                    cleanMsg = Reverse(cleanMsg);
                    return cleanMsg;
                } else {
                    cleanMsg += " ";
                }//end else 
            }//end foreach

            return cleanMsg;
        }//end method 
        #endregion

    }//end class
}//end namespace
