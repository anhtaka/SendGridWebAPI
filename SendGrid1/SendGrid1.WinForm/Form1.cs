﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;


namespace SendGrid1.WinForm
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var sg = new SendGridmail();
			this.textBox1.Text = sg.sendmail();
		}
	}
	public class SendGridmail
	{
		public SendGridmail() { }

		public string sendmail(){
			string api_user = "your_sendgrid_username";
			string api_key = "your_sendgrid_key";


			string toAddress = "takayanagi.tsuyoshi@sojitz-sys.com";
			string toName = "To Name";
			string subject = "A message from SendGrid";
			string text = "Delivered by your friends at SendGrid.";
			string fromAddress = "takayanagi.tsuyoshi@sojitz-sys.com";

			string url = "https://api.sendgrid.com/api/mail.send.json";
			string result = string.Empty;

			// Create a form encoded string for the request body
			string parameters = "api_user=" + api_user + "&api_key=" + api_key + "&to=" + toAddress + 
								"&toname=" + toName + "&subject=" + subject + "&text=" + text + 
								"&from=" + fromAddress;
			try
			{
			  //Create Request
			  HttpWebRequest myHttpWebRequest = (HttpWebRequest) HttpWebRequest.Create(url);
			  myHttpWebRequest.Method = "POST";
			  myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";

			  // Create a new write stream for the POST body
			  StreamWriter streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream());

			  // Write the parameters to the stream
			  streamWriter.Write(parameters);
			  streamWriter.Flush();
			  streamWriter.Close();

			  // Get the response
			  HttpWebResponse httpResponse = (HttpWebResponse) myHttpWebRequest.GetResponse();

			  // Create a new read stream for the response body and read it
			  StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
			  result = streamReader.ReadToEnd();

			  // Write the results to the console
			  //Console.WriteLine(result);
			}
			catch(WebException ex)
			{
			  // Catch any execptions and gather the response
			  HttpWebResponse response = (HttpWebResponse) ex.Response;

			  // Create a new read stream for the exception body and read it
			  StreamReader streamReader = new StreamReader(response.GetResponseStream());
			  result = streamReader.ReadToEnd();

			  // Write the results to the console
			  //Console.WriteLine(result);
			}
			return result;
	
		}
		
	}

}
