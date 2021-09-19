using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WMPLib;
using AxWMPLib;
using System.Reflection;
using System.IO;

namespace GeoMemories
{
    public partial class Form1 : Form
    {

        // Code to allow window to move even without borders
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public Form1()
        {
            InitializeComponent();
        }

        // Function to move window without borders
        private void pictureBox1_MouseDown(object sender,
        System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // Check if event is a tweet, create picturebox with tweet icon and
        // diplay into canvas.
        // Also add some funcionalities for picturebox (hover, click, etc.)
        private void DisplayTweets(Dictionary<string, string> Data)
        {
            // Flag to allow map pointers to resize depending on mouse click 
            // and event correspondent.
            bool dontRunHandler = false;

            foreach (KeyValuePair<string, string> kpv in Data)
            {
                string xPos = "", yPos = "";
                string tweetText = "", dateTime = ""; 

                if (kpv.Value == "tweet")
                {
                    Data.ToList().ForEach(x => Console.WriteLine(x.Key + " " + x.Value));

                    foreach (KeyValuePair<string, string> pv in Data)
                    {
                        if (pv.Key == "long")
                            xPos = pv.Value;
                        else if (pv.Key == "lat")
                            yPos = pv.Value;
                        else if (pv.Key == "text")
                            tweetText = pv.Value;
                        else if (pv.Key == "datetimestamp")
                            dateTime = pv.Value;

                    }

                    var picture = new PictureBox
                    {
                        ImageLocation = "../../Assets/map-pointer-twitter.png",
                        Name = "pictureBox",
                        Size = new Size(50, 50),
                        Location = new Point(Int32.Parse(xPos), Int32.Parse(yPos)),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.Transparent,
                    };

                    this.Controls.Add(picture);

                    picture.Parent = perthMap;

                    var textbox = new TextBox
                    {
                        Parent = perthMap,
                        Multiline = true,
                        Size = new Size(150, 100),
                        ReadOnly = true,
                        TabStop = false,
                        Location = new Point(Int32.Parse(xPos) + 35, Int32.Parse(yPos) + 65),
                        Visible = false,
                        Text = ("Tweet: '" + tweetText + "' \r\n\r\nDate/Time: " + dateTime),
                    };

                    textbox.BringToFront();

                    picture.MouseHover += new System.EventHandler(picture_MouseHover);
                    picture.MouseLeave += new System.EventHandler(picture_MouseLeave);
                    picture.Click += new System.EventHandler(picture_Click);

                    void picture_MouseHover(object sender, EventArgs e)
                    {
                        picture.Size = new Size(70, 70);
                        this.Cursor = Cursors.Hand;
                    }

                    void picture_MouseLeave(object sender, EventArgs e)
                    {
                        if(!dontRunHandler)
                        {
                            picture.Size = new Size(50, 50);
                            this.Cursor = Cursors.Default;
                        }
                    }

                    void picture_Click(object sender, EventArgs e)
                    {
                        if (!textbox.Visible)
                        {
                            textbox.Visible = true;
                            dontRunHandler = true;
                        }
                        else
                        {
                            textbox.Visible = false;
                            dontRunHandler = false;
                        }
                        
                        picture.Size = new Size(70, 70);
                        this.Cursor = Cursors.Default;
                    }
                }

            }
        }

        private void DisplayFacebook(Dictionary<string, string> Data)
        {
            bool dontRunHandler = false;

            foreach (KeyValuePair<string, string> kpv in Data)
            {
                string xPos = "", yPos = "";
                string statusText = "", dateTime = "";

                if (kpv.Value == "facebook-status-update")
                {
                    Data.ToList().ForEach(x => Console.WriteLine(x.Key + " " + x.Value));

                    foreach (KeyValuePair<string, string> pv in Data)
                    {
                        if (pv.Key == "long")
                            xPos = pv.Value;
                        else if (pv.Key == "lat")
                            yPos = pv.Value;
                        else if (pv.Key == "text")
                            statusText = pv.Value;
                        else if (pv.Key == "datetimestamp")
                            dateTime = pv.Value;

                    }

                    var picture = new PictureBox
                    {
                        ImageLocation = "../../Assets/map-pointer-facebook.png",
                        Name = "pictureBox",
                        Size = new Size(50, 50),
                        Location = new Point(Int32.Parse(xPos), Int32.Parse(yPos)),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.Transparent,
                    };

                    this.Controls.Add(picture);

                    picture.Parent = perthMap;

                    var textbox = new TextBox
                    {
                        Parent = perthMap,
                        Multiline = true,
                        Size = new Size(150, 100),
                        ReadOnly = true,
                        TabStop = false,
                        Location = new Point(Int32.Parse(xPos) + 35, Int32.Parse(yPos) + 65),
                        Visible = false,
                        Text = ("Status: '" + statusText + "' \r\n\r\nDate/Time: " + dateTime),
                    };

                    textbox.BringToFront();

                    picture.MouseHover += new System.EventHandler(picture_MouseHover);
                    picture.MouseLeave += new System.EventHandler(picture_MouseLeave);
                    picture.Click += new System.EventHandler(picture_Click);

                    void picture_MouseHover(object sender, EventArgs e)
                    {
                        picture.Size = new Size(70, 70);
                        this.Cursor = Cursors.Hand;
                    }

                    void picture_MouseLeave(object sender, EventArgs e)
                    {
                        if (!dontRunHandler)
                        {
                            picture.Size = new Size(50, 50);
                            this.Cursor = Cursors.Default;
                        }
                    }

                    void picture_Click(object sender, EventArgs e)
                    {
                        if (!textbox.Visible)
                        {
                            textbox.Visible = true;
                            dontRunHandler = true;
                        }
                        else
                        {
                            textbox.Visible = false;
                            dontRunHandler = false;
                        }

                        picture.Size = new Size(70, 70);
                        this.Cursor = Cursors.Default;
                    }
                }

            }
        }

        private void DisplayPhotos(Dictionary<string, string> Data)
        {
            // Flag to allow map pointers to resize depending on mouse click 
            // and event correspondent.
            bool dontRunHandler = false;

            foreach (KeyValuePair<string, string> kpv in Data)
            {
                string xPos = "", yPos = "";
                string filePath = "";

                if (kpv.Value == "photo")
                {
                    Data.ToList().ForEach(x => Console.WriteLine(x.Key + " " + x.Value));

                    foreach (KeyValuePair<string, string> pv in Data)
                    {
                        if (pv.Key == "long")
                            xPos = pv.Value;
                        else if (pv.Key == "lat")
                            yPos = pv.Value;
                        else if (pv.Key == "filepath")
                            filePath = pv.Value;
                    }

                    var picture = new PictureBox
                    {
                        ImageLocation = "../../Assets/map-pointer-photo.png",
                        Name = "pictureBox",
                        Size = new Size(50, 50),
                        Location = new Point(Int32.Parse(xPos), Int32.Parse(yPos)),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.Transparent,
                    };

                    this.Controls.Add(picture);

                    picture.Parent = perthMap;

                    var photo = new PictureBox
                    {
                        Parent = perthMap,
                        ImageLocation = "../.." + filePath,
                        Name = "photo",
                        Size = new Size(150, 150),
                        Location = new Point(Int32.Parse(xPos) + 35, Int32.Parse(yPos) + 70),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.Transparent,
                        Visible = false,
                    };

                    photo.BringToFront();


                    picture.MouseHover += new System.EventHandler(picture_MouseHover);
                    picture.MouseLeave += new System.EventHandler(picture_MouseLeave);
                    picture.Click += new System.EventHandler(picture_Click);

                    void picture_MouseHover(object sender, EventArgs e)
                    {
                        picture.Size = new Size(70, 70);
                        this.Cursor = Cursors.Hand;
                    }

                    void picture_MouseLeave(object sender, EventArgs e)
                    {
                        if (!dontRunHandler)
                        {
                            picture.Size = new Size(50, 50);
                            this.Cursor = Cursors.Default;
                        }
                    }

                    void picture_Click(object sender, EventArgs e)
                    {
                        if (!photo.Visible)
                        {
                            photo.Visible = true;
                            dontRunHandler = true;
                        }
                        else
                        {
                            photo.Visible = false;
                            dontRunHandler = false;
                        }

                        picture.Size = new Size(70, 70);
                        this.Cursor = Cursors.Default;
                    }
                }

            }
        }

        private void DisplayVideos(Dictionary<string, string> Data)
        {
            // Flag to allow map pointers to resize depending on mouse click 
            // and event correspondent.
            bool dontRunHandler = false;

            foreach (KeyValuePair<string, string> kpv in Data)
            {
                string xPos = "", yPos = "";
                string filePath = "";

                if (kpv.Value == "video")
                {
                    Data.ToList().ForEach(x => Console.WriteLine(x.Key + " " + x.Value));

                    foreach (KeyValuePair<string, string> pv in Data)
                    {
                        if (pv.Key == "long")
                            xPos = pv.Value;
                        else if (pv.Key == "lat")
                            yPos = pv.Value;
                        else if (pv.Key == "filepath")
                            filePath = pv.Value;

                    }

                    string fullPath = Path.GetFullPath(@"../../");

                    string megaFullPath = fullPath + filePath;

                    Console.WriteLine("This is full path: " + fullPath);


                    var picture = new PictureBox
                    {
                        ImageLocation = "../../Assets/map-pointer-video.png",
                        Name = "pictureBox",
                        Size = new Size(50, 50),
                        Location = new Point(Int32.Parse(xPos), Int32.Parse(yPos)),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.Transparent,
                    };

                    this.Controls.Add(picture);

                    picture.Parent = perthMap;

                    var mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
                    
                    this.Controls.Add(mediaPlayer);

                    mediaPlayer.Parent = perthMap;
                    mediaPlayer.uiMode = "none";
                    mediaPlayer.windowlessVideo = true;
                    mediaPlayer.enableContextMenu = true;
                    mediaPlayer.Ctlenabled = true;
                    mediaPlayer.Visible = false;
                    mediaPlayer.stretchToFit = true;

                    mediaPlayer.Name = "VideoPlayer";
                    mediaPlayer.Location = new Point(Int32.Parse(xPos) + 35, Int32.Parse(yPos) + 70);
                    mediaPlayer.Size = new Size(400, 300);
                    
                    picture.MouseHover += new System.EventHandler(picture_MouseHover);
                    picture.MouseLeave += new System.EventHandler(picture_MouseLeave);
                    picture.Click += new System.EventHandler(picture_Click);

                    void picture_MouseHover(object sender, EventArgs e)
                    {
                        picture.Size = new Size(70, 70);
                        this.Cursor = Cursors.Hand;
                    }

                    void picture_MouseLeave(object sender, EventArgs e)
                    {
                        if (!dontRunHandler)
                        {
                            picture.Size = new Size(50, 50);
                            this.Cursor = Cursors.Default;
                        }
                    }

                    void picture_Click(object sender, EventArgs e)
                    {
                        if (!mediaPlayer.Visible)
                        {
                            mediaPlayer.Visible = true;
                            dontRunHandler = true;
                            mediaPlayer.URL = megaFullPath;
                            mediaPlayer.Ctlcontrols.play();
                        }
                        else
                        {
                            mediaPlayer.Visible = false;
                            dontRunHandler = false;
                            mediaPlayer.Ctlcontrols.pause();
                        }

                        picture.Size = new Size(70, 70);
                        this.Cursor = Cursors.Default;
                    }
                }

            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            // Button click closes the application
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("HELLO!");

            lifelog lifelogs = new lifelog();

            lifelogs.loadLogs();

            foreach (KeyValuePair<string, Dictionary<string, string>> obj in lifelogs.Event)
            {
                Dictionary<string, string> Data = new Dictionary<string, string>();
                Data = obj.Value;

                DisplayTweets(Data);
                DisplayFacebook(Data);
                DisplayPhotos(Data);
                DisplayVideos(Data);
            }
        }
    }
}
