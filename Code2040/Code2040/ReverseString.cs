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


	//Algorithim for reversing the string
	static class alorgorithmForReversingString
	{
		public static string reverser (string tokenResult)
		{

			char[] arr = tokenResult.ToCharArray ();
			Array.Reverse (arr);
			return new string (arr);

		}

	}



	public class ReverseString
	{
		public ReverseString ()
		{

			Task.Run (async () => {

				//helper function is the post function for the webrequest
				var helper1 = new Helper ();
				Session.Instance.SetToken (JsonConvert.DeserializeObject<Response> (
					await helper1.Post ("http://challenge.code2040.org/api/getstring", 
						JsonConvert.SerializeObject (new { token = "Y6C15DVN99" 
							 }))).Result);
				Console.WriteLine ("This is the Second Token");
				Console.WriteLine (Session.Instance.Token);
				Console.WriteLine (alorgorithmForReversingString.reverser (Session.Instance.Token));
			});


			//Created a pause in the code to wait on the Second asynchorounous web post to load
			//its token value

			System.Threading.Thread.Sleep (3000);


			string results = Session.Instance.Token;
			Console.WriteLine (results);


			/*Results.sendstringback (Session.Instance.Token);*/
			Console.WriteLine ("Please be last");
			//Console.WriteLine (alorgorithmForReversingString.reverser (Session.Instance.Token));
		}
	}
}

