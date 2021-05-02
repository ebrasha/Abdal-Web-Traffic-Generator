using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Abdal_Web_Traffic_Generator;


namespace Abdal_Security_Group_App
{
    public partial class Abdal_Web_Traffic_Generator : Telerik.WinControls.UI.RadForm
    {
      
        private bool stop_spider = false;
        

        public Abdal_Web_Traffic_Generator()
        {
            InitializeComponent();
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Text = "Abdal Web Traffic Generator" + " " + version.Major + "." + version.Minor; //change form title
            bgWorker_traffic_gen.WorkerReportsProgress = true;
            bgWorker_traffic_gen.WorkerSupportsCancellation = true;

            bgWorker_spider.WorkerReportsProgress = true;
            bgWorker_spider.WorkerSupportsCancellation = true;

        }

        private void EncryptToggleSwitch_ValueChanged(object sender, EventArgs e)
        {
            
        }

 
        private void Abdal_2Key_Triple_DES_Builder_Load(object sender, EventArgs e)
        {
            // Call Global Chilkat Unlock
            Abdal_Security_Group_App.GlobalUnlockChilkat GlobalUnlock = new Abdal_Security_Group_App.GlobalUnlockChilkat();
            GlobalUnlock.unlock();


            radRadialGauge1.Value = 0;


            
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        

        private void randButton_Click(object sender, EventArgs e)
        {

           

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
           

            string[] DangerNameArray = { "abdal",
                "ebrasha",
                "hackers.zone",
                "mambanux",
                "nahaanbin",
                "blackwin"};

            // Check Target Url
            foreach (var DangerName in DangerNameArray)
            {

                new Thread(() =>
                {
                    Regex regex = new Regex(@"" + DangerName + ".*");
                    
                        if (regex.Match(targetUrlTextBox.Text.ToLower()).Success)
                        {

                           AbdalControler.unauthorized_process = true;
                            
                            
                        }

                    

                }).Start();


            }


            // Check Spoof Url
            foreach (var DangerName in DangerNameArray)
            {

                new Thread(() =>
                {
                    Regex regex = new Regex(@"" + DangerName + ".*");

                    if (regex.Match(spoofUrlTextBox.Text.ToLower()).Success)
                    {


                        AbdalControler.unauthorized_process = true;


                    }



                }).Start();


            }



            if (textBox_number_of_views.Value <= 1)
            {
                MessageBox.Show("Views Value must be greater than 1 ! ");
            }else if (AbdalControler.unauthorized_process == true)
            {
                MessageBox.Show("This domain is unauthorized !");
                
            }
            else
            {
                if (bgWorker_traffic_gen.IsBusy != true)
                {
                    ResultTextEditor.Text = "";
                    radProgressBar1.Value1 = 0;
                    radProgressBar1.Value2 = 0;
                    // Start the asynchronous operation.
                    bgWorker_traffic_gen.RunWorkerAsync();
                }
            }

       




        }

        private void cancelPenTest_Click(object sender, EventArgs e)
        {
            if (bgWorker_traffic_gen.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                bgWorker_traffic_gen.CancelAsync();
            }


        }

       

        private void bgWorker_req_maker_DoWork(object sender, DoWorkEventArgs e)
        {

            if (AbdalControler.unauthorized_process == true)
            {
                MessageBox.Show("This domain is unauthorized !");
                Application.Exit();


            }
            else
            {

                try
                {

                    if (ExtractRichTextBox.Lines.Length <= 1)
                    {
                        MessageBox.Show("Crawled links must be greater than one ! ");
                    }
                    else
                    {


                        BackgroundWorker worker = sender as BackgroundWorker;

                        Chilkat.Http http = new Chilkat.Http();
                        http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.9 Safari/537.36";

                        int number_of_attack_req = Convert.ToInt32(textBox_number_of_views.Value) * (Convert.ToInt32(ExtractRichTextBox.Lines.Length) - 1);
                        radProgressBar1.Maximum = number_of_attack_req;
                        radProgressBar1.Minimum = 0;

                        radialGaugeArc2.RangeStart = 0;
                        radialGaugeArc2.RangeEnd = number_of_attack_req;
                        radialGaugeArc1.RangeStart = 0;
                        radialGaugeArc1.RangeEnd = number_of_attack_req;
                        radRadialGauge1.RangeEnd = number_of_attack_req;

                        //Sound Alert For Start Attack
                        using (var soundPlayer = new SoundPlayer(@"start.wav"))
                        {
                            soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                        }




                        int trafficSizebyte = 0;
                        int traffic_counter = 0;
                        for (int i = 1; i <= textBox_number_of_views.Value; i++)
                        {

                            if (worker.CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }
                            else
                            {


                                string[] RichTextBoxLines = ExtractRichTextBox.Lines;
                                foreach (string line_url_addr in RichTextBoxLines)
                                {
                                    if (line_url_addr != "")
                                    {

                                        traffic_counter++;
                                        radRadialGauge1.Value = traffic_counter;
                                        radProgressBar1.Value2 = traffic_counter;

                                        //randString = RandomString(rnd.Next(15, 30));
                                        // Send the HTTP GET and return the content in a string.
                                        http.ReadTimeout = 20;
                                        http.ConnectTimeout = 20;
                                        http.FollowRedirects = true;
                                        if (spoofUrlTextBox.Text != "")
                                        {
                                            http.Referer = spoofUrlTextBox.Text;
                                        }



                                        string reponse_http = http.QuickGetStr(line_url_addr);

                                        trafficSizebyte += System.Text.ASCIIEncoding.Unicode.GetByteCount(reponse_http);
                                        trafficSizebyte_Label.Text = (trafficSizebyte / 1024).ToString() + " KB";

                                        //Add AttackLog in Result Box
                                        ResultTextEditor.AppendText(line_url_addr + Environment.NewLine);
                                        ResultTextEditor.SelectionStart = ResultTextEditor.Text.Length;
                                        ResultTextEditor.ScrollToCaret();

                                        // Perform a time consuming operation and report progress.
                                        if (FastTrafficGenToggleSwitch.Value == false)
                                        {
                                            System.Threading.Thread.Sleep(500);
                                        }

                                        worker.ReportProgress(i);


                                    }

                                }




                            }
                        }

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            } // End else


           

           
           




        }

        private void bgWorker_req_maker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //radProgressBar1.Value2 = e.ProgressPercentage;
             
//            radRadialGauge1.Value = e.ProgressPercentage;
        }

        private void bgWorker_req_maker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            


            if (e.Cancelled == true)
                    {
                this.radDesktopAlert1.CaptionText = "Abdal Web Traffic Generator";
                this.radDesktopAlert1.ContentText = "Canceled Process By User!";
                this.radDesktopAlert1.Show();
                using (var soundPlayer = new SoundPlayer(@"cancel.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }
            }
            else if (e.Error != null)
                    {
                this.radDesktopAlert1.CaptionText = "Abdal Web Traffic Generator";
                this.radDesktopAlert1.ContentText = e.Error.Message;
                this.radDesktopAlert1.Show();

                using (var soundPlayer = new SoundPlayer(@"error.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }


            }
            else
                    {
                this.radDesktopAlert1.CaptionText = "Abdal Web Traffic Generator";
                this.radDesktopAlert1.ContentText = "Done!";
                this.radDesktopAlert1.Show();
                using (var soundPlayer = new SoundPlayer(@"done.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }

            }

        }

        private void radRadialGauge1_Click(object sender, EventArgs e)
        {

        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void radButton1_Click_2(object sender, EventArgs eSpider)
        {
            
            string[] DangerNameArray = { "abdal",
                "ebrasha",
                "hackers.zone",
                "mambanux",
                "nahaanbin",
                "blackwin"};

            // Check Target Url
            foreach (var DangerName in DangerNameArray)
            {

                new Thread(() =>
                {
                    Regex regex = new Regex(@"" + DangerName + ".*");

                    if (regex.Match(targetUrlTextBox.Text.ToLower()).Success)
                    {


                        AbdalControler.unauthorized_process = true;


                    }



                }).Start();


            }


                // Check Spoof Url
            foreach (var DangerName in DangerNameArray)
            {

                new Thread(() =>
                {
                    Regex regex = new Regex(@"" + DangerName + ".*");

                    if (regex.Match(spoofUrlTextBox.Text.ToLower()).Success)
                    {


                        AbdalControler.unauthorized_process = true;


                    }



                }).Start();


            }

             





            if (targetUrlTextBox.Text == "") {
                MessageBox.Show("Please Set the Target Domain! ");
            }else if(CrawlerLimitationValue.Value <= 1)
            {
                MessageBox.Show("Crawled links must be greater than 1 ! ");
            }
            else if (AbdalControler.unauthorized_process == true )
            {
                MessageBox.Show("This domain is unauthorized !");
                
                
                
            }
            else
            {

                if (bgWorker_spider.IsBusy != true)
                {
                    radProgressBar1.Value1 = 0;
                    radProgressBar1.Value2 = 0;

                    ExtractRichTextBox.Text = "";
                    // Start the asynchronous operation.
                    bgWorker_spider.RunWorkerAsync();
                    stop_spider = false;
                }

            }

            
        }

        private void radButton2_Click(object sender, EventArgs eSpider)
        {
            if (bgWorker_spider.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                bgWorker_spider.CancelAsync();
                stop_spider = true;
            }
        }

        private void bgWorker_spider_DoWork(object sender, DoWorkEventArgs eSpidere)
        {

            if (AbdalControler.unauthorized_process == true)
            {
                MessageBox.Show("This domain is unauthorized !");
                Application.Exit();


            }
            else
            {

                Chilkat.Spider spider = new Chilkat.Spider();
                spider.Initialize(targetUrlTextBox.Text);
                //  Add the 1st URL:
                spider.AddUnspidered(targetUrlTextBox.Text + "/");

                //  Begin crawling the site by calling CrawlNext repeatedly.
                int i;
                int maxSpiderLink = Convert.ToInt32(CrawlerLimitationValue.Value);
                spiderProgressBar.Maximum = maxSpiderLink;

                for (i = 1; i <= maxSpiderLink; i++)
                {



                    if (stop_spider)
                    {

                        break;
                    }
                    bool success;
                    success = spider.CrawlNext();
                    if (success == true)
                    {
                        Chilkat.StringBuilder url_encode = new Chilkat.StringBuilder();
                        string crawl_Last_url = spider.LastUrl;

                        //decode url
                        bool success_url_encode = url_encode.Append(crawl_Last_url);

                        url_encode.Decode("url", "utf-8");
                        string crawl_Last_url_decoded = url_encode.GetAsString();

                        //  The HTML is available in the LastHtml property
                        //Add AttackLog in Result Box
                        ExtractRichTextBox.AppendText(crawl_Last_url_decoded + Environment.NewLine);
                        ExtractRichTextBox.SelectionStart = ExtractRichTextBox.Text.Length;
                        ExtractRichTextBox.ScrollToCaret();

                        // add progress value
                        spiderProgressBar.Value2 = i;

                        crawled_links_text.Text = Convert.ToString(i);

                    }
                    else
                    {
                        //  Did we get an error or are there no more URLs to crawl?
                        if (spider.NumUnspidered == 0)
                        {
                            this.radDesktopAlert1.CaptionText = "Abdal Web Traffic Generator";
                            this.radDesktopAlert1.ContentText = "No more URLs to spider";
                            this.radDesktopAlert1.Show();

                        }
                        else
                        {
                            this.radDesktopAlert1.CaptionText = "Abdal Web Traffic Generator";
                            this.radDesktopAlert1.ContentText = spider.LastErrorText;
                            this.radDesktopAlert1.Show();

                        }

                    }

                    //  Sleep 1 second before spidering the next URL.
                    spider.SleepMs(1000);
                }

            }

           

        }

        private void bgWorker_spider_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs eSpider)
        {

            if (eSpider.Cancelled == true)
            {
                this.radDesktopAlert1.CaptionText = "Abdal Web Traffic Generator";
                this.radDesktopAlert1.ContentText = "Canceled By User!";
                this.radDesktopAlert1.Show();
                using (var soundPlayer = new SoundPlayer(@"cancel.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }
            }
            else if (eSpider.Error != null)
            {
                this.radDesktopAlert1.CaptionText = "Abdal Web Traffic Generator";
                this.radDesktopAlert1.ContentText = eSpider.Error.Message;
                this.radDesktopAlert1.Show();

                using (var soundPlayer = new SoundPlayer(@"error.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }


            }
            else if (stop_spider)
            {
                this.radDesktopAlert1.CaptionText = "Abdal Web Traffic Generator";
                this.radDesktopAlert1.ContentText = "Canceled By User!";
                this.radDesktopAlert1.Show();
                using (var soundPlayer = new SoundPlayer(@"cancel.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }


            }
            else
            {
                this.radDesktopAlert1.CaptionText = "Abdal Web Traffic Generator";
                this.radDesktopAlert1.ContentText = "Done!";
                this.radDesktopAlert1.Show();
                using (var soundPlayer = new SoundPlayer(@"done.wav"))
                {
                    soundPlayer.PlaySync(); // can also use soundPlayer.Play()
                }

            }


        }

        private void bgWorker_spider_ProgressChanged(object sender, ProgressChangedEventArgs eSpider)
        {
           spiderProgressBar.Value2 = eSpider.ProgressPercentage;
        }

         
    }
}
