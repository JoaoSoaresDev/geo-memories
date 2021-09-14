using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoMemories
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void DisplayTweets(Dictionary<string, string> Data)
        {
            

            foreach (KeyValuePair<string, string> kpv in Data)
            {
                if (kpv.Value == "tweet")
                {

                    var picture = new PictureBox
                    {
                        Parent = perthMap,
                        ImageLocation = "../../Assets/map-pointer.png",
                        Name = "pictureBox",
                        Size = new Size(50, 50),
                        Location = new Point(400, 400),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.Transparent,
                        
                    };

                    this.Controls.Add(picture);

                    picture.BringToFront();

                    Console.WriteLine("It should add");

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

        private void Form1_Shown(object sender, EventArgs e)
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

            }
        }
    }
}
