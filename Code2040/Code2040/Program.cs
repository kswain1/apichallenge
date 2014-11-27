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
	class MainClass
	{
		public static void Main (string[] args)
		{
//		httpWebRequest = (HttpWebRequest)WebRequest.Create ("http://challenge.code2040.org/api/register");
//			httpWebRequest.ContentType = "application/json";
//			httpWebRequest.Method = "POST";
//
//			using (var streamWriter = new StreamWriter (httpWebRequest.GetRequestStream ())) {
//				string json = new JavaScriptSerializer ().Serialize (new
//					{
//						email = "kehlinswain@yahoo.com",
//						github = "https://github.com/kswain1/apichallenge"
//					});
//
//				streamWriter.Write (json);
//				streamWriter.Flush ();
//				streamWriter.Close ();
//
//				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse ();
//				using (var streamReader = new StreamReader (httpResponse.GetResponseStream ())) {
//					var result = streamReader.ReadToEnd ();
//					Console.WriteLine (result);
//				}
//
//				Console.WriteLine ("Hello World!");
			Task.Run (async () => {
				var request = (HttpWebRequest)WebRequest.Create ("http://challenge.code2040.org/api/register");
				request.Method = "POST";
				request.ContentType = "application/json";

				var buffer = Encoding.UTF8.GetBytes (JsonConvert.SerializeObject (new {
						email = "kehlinswain@yahoo.com",
					github = "https://github.com/kswain1/apichallenge"
					}));
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


				Console.WriteLine (result);
				Console.ReadLine ();

				reader.Close ();
				stream.Close ();
				response.Close ();    

			});
			Console.ReadKey (true);
		}

		// For each different session of the project it is necessary to the store the
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

		// Inorder to store to reuse the code
		public class Test
		{
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
}
