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
			Console.WriteLine ("hello");
			Task.Run (async () => {

				//helper function is the post function for the webrequest
				var helper = new Helper ();
				Session.Instance.SetToken (JsonConvert.DeserializeObject<Response> (
					await helper.Post ("http://challenge.code2040.org/api/getstring", 
						JsonConvert.SerializeObject (new { token = "Y6C15DVN99" 
						}))).Result);

			});
			Console.WriteLine (Session.Instance.Token);
			ReverseString reverseString = new ReverseString ();
			Console.WriteLine (reverseString);
			Console.WriteLine (alorgorithmForReversingString.reverser (Session.Instance.Token));
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
			return result;
		}
	}
}
	

