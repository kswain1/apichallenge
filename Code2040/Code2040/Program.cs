using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Code2040
{


	class Program
	{
		//Sends the request to gether the token and value from the website
		static void Main (string[] args)
		{

			Task.Run (async () => {
				var helper = new Helper ();
				Session.Instance.SetToken (JsonConvert.DeserializeObject<Response> (
					await helper.Post ("http://challenge.code2040.org/api/register", 
						JsonConvert.SerializeObject (new { email = "kehlinswain@yahoo.com", 
									github = "https://github.com/kswain1/apichallenge" }))).Result);

				//Reads out the line to verify if the token was printed out	
				Console.WriteLine ("my token Value");
				Console.WriteLine (Session.Instance.Token);   
				Console.ReadLine ();
			});

			Console.ReadKey (true);
			ReverseString reverseString = new ReverseString ();
			sendStringBack SendStringBack = new sendStringBack ();
			string Data1 = alorgorithmForReversingString.reverser (Session.Instance.Token);
	
			//sendStringBack SendStringBack = new sendStringBack (); 
		

			//Console.WriteLine (reverseString);
			//Console.WriteLine (alorgorithmForReversingString.reverser (Session.Instance.Token));
			//sendStringBack SendStringBack = new sendStringBack ();
			//Console.WriteLine (SendStringBack);
		}
	}


	// Response model
	public class Response
	{
		public string Result { get; private set; }

		public Response (string result)
		{
			Result = result;
		}
	}

	//singleton to store the token and anything else that has to be shared in the application
	public class Session
	{
		private readonly static Lazy<Session> _instance = new Lazy<Session> (() => new Session ());

		public static Session Instance { get { return _instance.Value; } }

		private Session ()
		{
		}

		public string Token { get; private set; }

		public void SetToken (string token)
		{
			Token = token;
		}
	}

	public class Helper
	{
		/// <summary>
		/// post to web site 
		/// </summary>
		/// <param name="uri">url to post the data</param>
		/// <param name="data">json to post</param>
		/// <returns>Result sent from the web site</returns>
		public async Task<string> Post (string uri, string data)
		{
			var request = (HttpWebRequest)WebRequest.Create (uri);
			request.Method = "POST";
			request.ContentType = "application/json";

			var buffer = Encoding.UTF8.GetBytes (data);
			request.ContentLength = buffer.Length;

			var stream = await request.GetRequestStreamAsync ();
			await stream.WriteAsync (buffer, 0, buffer.Length);
			stream.Close ();

			var response = await request.GetResponseAsync ();
			stream = response.GetResponseStream ();
			if (stream == null)
				throw new ArgumentNullException ();

			var reader = new StreamReader (stream);
			var result = await reader.ReadToEndAsync ();

			reader.Close ();
			stream.Close ();
			response.Close ();
			//Console.WriteLine (result);
			return result;

		}
	}
}
	

