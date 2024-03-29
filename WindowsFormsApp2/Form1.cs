﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net.Http;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.zameen.com");
            ZameenStart();
        }


        private static async Task ZameenStart()
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["conn"].ToString());
            SqlCommand cmd;
            int tot = 0;



            for (int i = 1; i < 11; i++)
            {




                var url = "https://www.zameen.com/Rentals/Lahore-1-" + i + ".html?price_min=30000&price_max=40000";
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(html);

                List<Houses> houses = new List<Houses>();


                var divs = htmlDocument.DocumentNode.Descendants("li").Where(node => node.GetAttributeValue("class", "").Equals("ef447dde")).ToList();

                string Title = "";
                string Price = "";
                string Location = "";

                string hrefValue = "";

                foreach (var ad in divs)
                {

                    var home = new Houses();
                    //Title = ad.Descendants("dive").FirstOrDefault().InnerText;
                    Title = ad.Descendants("h2").FirstOrDefault().InnerText;
                    Price = ad.Descendants("div").Where(a => a.GetAttributeValue("class", "").Equals("_7ac32433")).FirstOrDefault().InnerText;
                    Location = ad.Descendants("div").Where(a => a.GetAttributeValue("class", "").Equals("_162e6469")).FirstOrDefault().InnerText;
                    //link = ad.Descendants("a").Where(a => a.GetAttributeValue("class", "").Equals("_7ac32433")).FirstOrDefault().;

                    var link = ad.Descendants("a").First(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "_7ac32433");

                    hrefValue = link.Attributes["href"].Value;


                    //var vlLocation = ad.Descendants("div").Where(a => a.GetAttributeValue("class", "").Equals("place")).ToList();




                    string sql = "insert into data (Title, Price, Location, link) values ('" + Title + "', '" + Price + "', '" + Location + "', '" + "https://www.zameen.com" + hrefValue + "')";
                    cmd = new SqlCommand(sql, connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    //string val = ad.Descendants("script").FirstOrDefault().InnerText; 
                }


                tot = i;


            }


            MessageBox.Show("total pages parsed " + tot);


        }

        private static async Task Dubbizlestart()
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["conn"].ToString());
            SqlCommand cmd;
            int tot = 0;



            for (int i = 1; i < 11; i++)
            {



                var url = "https://dubai.dubizzle.com/en/property-for-rent/residential/apartmentflat/?filters=(bedrooms%3E=1%20AND%20bedrooms%3C=1)%20AND%20(size%3E=900)&sort=lowest&page=" + i;
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(html);

                List<Houses> houses = new List<Houses>();


                var divs = htmlDocument.DocumentNode.Descendants("a").Where(node => node.GetAttributeValue("class", "").Equals("listing-link")).ToList();

                string Title = "";
                string Price = "";
                string Location = "";

                foreach (var ad in divs)
                {

                    var home = new Houses();
                    Title = ad.Descendants("h2").FirstOrDefault().InnerText;
                    Price = ad.Descendants("div").Where(a => a.GetAttributeValue("class", "").Equals("price")).FirstOrDefault().InnerText;
                    Location = ad.Descendants("div").Where(a => a.GetAttributeValue("class", "").Equals("place")).FirstOrDefault().InnerText;

                    //var vlLocation = ad.Descendants("div").Where(a => a.GetAttributeValue("class", "").Equals("place")).ToList();




                    string sql = "insert into data (Title, Price, Location) values ('" + Title + "', '" + Price + "', '" + Location + "')";
                    cmd = new SqlCommand(sql, connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    //string val = ad.Descendants("script").FirstOrDefault().InnerText; 
                }


                tot = i;


            }


            MessageBox.Show("total pages parsed " + tot);


        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
