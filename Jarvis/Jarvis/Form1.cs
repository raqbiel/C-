using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using WatiN.Core;

namespace Jarvis
{
    public partial class Form1 : System.Windows.Forms.Form
    {

       

        string urlText;

        string imie = "Paweł";
        string sciezka = @"C:\Users\Paweł\Documents\Visual Studio 2015\Projects\Jarvis\Jarvis\bin\Debug\wyrazy_PL.txt";
        string odp = @"C:\Users\Paweł\Documents\Visual Studio 2015\Projects\Jarvis\Jarvis\bin\Debug\wyrazy_jarvis.txt";

        SerialPort port = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);

        SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        SpeechSynthesizer JARVIS = new SpeechSynthesizer();
        string QEvent;
        string ProcWindow;
        double timer = 10;
        int count = 1;
        bool wake;

        public Form1()
        {
            InitializeComponent();
            JARVIS.SelectVoiceByHints(VoiceGender.Male);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.LoadGrammar(new DictationGrammar());
            GrammarBuilder gb = new GrammarBuilder(new Choices(File.ReadAllLines(Application.StartupPath + "\\wyrazy_jarvis.txt")));
            gb.Culture = new System.Globalization.CultureInfo("en-GB");
            _recognizer.LoadGrammar(new Grammar(gb));
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void Mow(string h)
        {
            JARVIS.Speak(h);
            richTextBox2.AppendText(h + "\n");
        }

        public void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            
            textBox1.Text = urlText;
            

            string word;
            String[] colors = new String[3];

            DateTimeFormatInfo fmt = (new CultureInfo("pl-PL")).DateTimeFormat;

            string speech = e.Result.Text;
            richTextBox1.Text = e.Result.Text;

            if (speech == "wake") wake = true;
            if (speech == "sleep") wake = false;

            /*  Random random = new Random();
              int value = random.Next(1, 10);

                  colors[0] = "Green";
                  colors[1] = "Blue";
                  colors[2] = "Red";

             if (speech == "jarvis your color?" && value <=3)
                  {
                   Mow("My favorite color is:" + colors[0]);
              }else if (speech == "jarvis your color?" && value == 5)
              {
                  Mow("My favorite color is:" + colors[1]);
              }else if (speech == "jarvis your color?" && value >= 7)
              {
                  Mow("My favorite color is:" + colors[2]);
              }
              */
            String[] linie = new String[13];

            for (int i = 0; i < 1; i++)
            {

                using (StreamReader reader = new StreamReader(Application.StartupPath + "\\wyrazy_jarvis.txt"))
                {
                    linie[0] = reader.ReadLine();
                    linie[1] = reader.ReadLine();
                    linie[2] = reader.ReadLine();
                    linie[3] = reader.ReadLine();
                    linie[4] = reader.ReadLine();
                    linie[5] = reader.ReadLine();
                    linie[6] = reader.ReadLine();
                    linie[7] = reader.ReadLine();
                    linie[8] = reader.ReadLine();
                    linie[9] = reader.ReadLine();
                    linie[10] = reader.ReadLine();
                    linie[11] = reader.ReadLine();
                    linie[12] = reader.ReadLine();
                }

            

           

                if (speech == (linie[0]) || speech == (linie[9]) || speech == (linie[10]))
                {
                    Mow("Witam" + imie);
                }

                if (speech == linie[1])
                {
                    Mow("Jarvis uruchamia Facebook");
                    System.Diagnostics.Process.Start("http://www.facebook.com");
                }

                if (speech == linie[2])
                {
                    Mow("Dziękuję dobrze, miło że pytasz Sir");
                }
                if (speech == linie[3])
                {
                    Mow("Jarvis uruchamia kantor");
                    System.Diagnostics.Process.Start("https://internetowykantor.pl/kurs-euro/");
                    Mow("Kantor prezentuje kurs walut wraz z wykresami");
                }

                if (speech == linie[4])
                {
                    Mow("Wyszukuję Informacje");
                    if (linie[4].Contains("zginelo"))
                    {
                        word = "zginęło";
                        string s = "https://pl.wikipedia.org/wiki/RMS_Titanic";
                   
                    string pageContent = null;
                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(s);
                    HttpWebResponse myres = (HttpWebResponse)myReq.GetResponse();
                  
                    
                    using (StreamReader sr = new StreamReader(myres.GetResponseStream()))
                    {
                        pageContent = sr.ReadToEnd();
                    }

                    if (pageContent.Contains(word))
                    {
                        Mow(getBetween(pageContent, word, "."));
                      
                        }
                        else
                        {
                            Mow("brak Informacji");
                        }
                    }
                }
                    if (speech == linie[5])
                    {
                
                        Mow(DateTime.Now.ToString("t", fmt));
                    }
                    if (speech == linie[6])
                    {
                   
                             Mow(DateTime.Now.ToString("d", fmt));
                    }
                    if (speech == linie[7])
                    {
                        Mow("Przyjemność po mojej stronie Sir");
                    }

                    string[] linki = File.ReadAllLines(sciezka);
                    Random rsong = new Random();
                    Console.WriteLine(linki[rsong.Next(linki.Length)]);

                    if (speech == linie[8])
                    {


                        Mow("Jarvis wybiera piosenkę");
                        System.Diagnostics.Process.Start(linki[rsong.Next(linki.Length)]);

                    }
                if (speech == linie[11])
                {
                    string urlAdress = textBox1.Text;
                    

                    Mow("Jarvis wyszukuje wybrane słowo");
                    WebBrowser browser = new WebBrowser();
                    
                    browser.Navigate("https://www.google.pl/search?q=" + speech);



                }
            }

                if (speech == "light on")
                {
                    port.Open();
                    port.WriteLine("A");
                    port.Close();
                }
                if (speech == "light off")
                {
                    port.Open();
                    port.WriteLine("B");
                    port.Close();
                }
            

        }
   public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
   static string GetPageTitle(string link)
        {
          

            try
            {
                WebClient wc = new WebClient();
                string html = wc.DownloadString(link);

                Regex x = new Regex("(.*)");
                MatchCollection m = x.Matches(html);

                if (m.Count > 0)
                {
                    return m[0].Value.Replace("<title>", "").Replace("</title>", "");

                } else
                    return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nie mozna polaczyc. Bład: " + ex.Message);

                return "";
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /*
       public static void Poczta(string[] args)
       {
           using (var client = new ImapClient())
           {
               // For demo-purposes, accept all SSL certificates
               client.ServerCertificateValidationCallback = (s, c, h, e) => true;

               client.Connect("imap.gmail.com", 993, true);
 
               client.AuthenticationMechanisms.Remove("XOAUTH2");

               client.Authenticate("login", "password");

              
               var inbox = client.Inbox;
               inbox.Open(FolderAccess.ReadOnly);

               Console.WriteLine("Total messages: {0}", inbox.Count);
               Console.WriteLine("Recent messages: {0}", inbox.Recent);

               for (int i = 0; i < inbox.Count; i++)
               {
                   var message = inbox.GetMessage(i);
                   Console.WriteLine("Subject: {0}", message.Subject);
               }

               client.Disconnect(true);
           }
       }
   }*/
    }
}