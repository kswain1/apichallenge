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
	public class sendStringBack
	{

		public sendStringBack ()
		{
		
			//Send the validated string back to the webpost 
			Task.Run (async () => {

				//helper function is the post function for the webrequest
				var helper1 = new Helper ();
				Session.Instance.SetToken (JsonConvert.DeserializeObject<Response> (
					await helper1.Post ("http://challenge.code2040.org/api/validatestring", 
						JsonConvert.SerializeObject (new { token = "Y6C15DVN99",
							reversestring = alorgorithmForReversingString.reverser (Session.Instance.Token)
						}))).Result);
				Console.WriteLine ("This is the Validation post");
				Console.WriteLine (Session.Instance.Token);

			});
			System.Threading.Thread.Sleep (3000);
		}
	}
}

