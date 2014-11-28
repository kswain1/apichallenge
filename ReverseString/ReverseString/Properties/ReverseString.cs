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
			//Console.WriteLine ("Wassup");


			char[] arr = tokenResult.ToCharArray ();
			Array.Reverse (arr);
			return new string (arr);

		}

	}


	public class ReverseString
	{
		public ReverseString ()
		{

			/*Task.Run (async () => {

				//helper function is the post function for the webrequest
				var helper = new Helper ();
				Session.Instance.SetToken (JsonConvert.DeserializeObject<Response> (
					await helper.Post ("http://challenge.code2040.org/api/getstring", 
						JsonConvert.SerializeObject (new { token = "Y6C15DVN99" 
							 }))).Result);

			});*/
			Console.WriteLine (alorgorithmForReversingString.reverser (Session.Instance.Token));
		}
	}


}
