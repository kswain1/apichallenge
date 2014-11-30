using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChallengeResponse
{
	class Program
	{

		public class RequestState
		{

			public HttpWebRequest _request = null;
			public HttpWebResponse _response = null;
			public Byte[] _requestDataArray = new Byte[1024];
			public String _responseText = null;

		}

		public class HayStackResponse
		{

			public Dictionary<String, Object> result { get; set; }
		}

		public class PrefixResponse
		{
			public Dictionary<String, Object> result { get; set; }
		}

		static void Main (string[] args)
		{

			Dictionary<String, String> registerInfo = new Dictionary<string, string> ();
			registerInfo ["email"] = @"kehlinswain@yahoo.com";
			registerInfo ["github"] = @"https://github.com/kswain1/apichallenge";

			String jsonContentString = JsonConvert.SerializeObject (registerInfo, Formatting.Indented);
			RequestState state = new RequestState ();
			String responseString = postResponseForChallenge (@"http://challenge.code2040.org/api/register",
				                        jsonContentString, state);

			Dictionary<String, String> responseRef = JsonConvert.DeserializeObject<Dictionary<String, String>> (responseString);            
			Dictionary<String, String> tokenRef = new Dictionary<string,string> ();
			tokenRef ["token"] = responseRef ["result"];

			String validateString = performStage1Validation (tokenRef, state);
			if (validateString.Equals (String.Empty))
				return;


			//validation for trial one checker
			Console.WriteLine (validateString);
			//Console.ReadLine ();


			//validation for trial two checker
			validateString = performStage2Validation (tokenRef, state);
			if (validateString.Equals (String.Empty))
				return;
			Console.WriteLine (validateString);
			//Console.ReadLine ();


			//validation for trial three checker
			validateString = performStage3Validation (tokenRef, state);
			if (validateString.Equals (string.Empty))
				return; 

			Console.WriteLine (validateString);
			//Console.ReadLine ();


			//validation for trial four checker
			validateString = performStage4Validation (tokenRef, state);
			if (validateString.Equals (string.Empty))
				Console.WriteLine ("To Fast!"); 
			Console.ReadLine ();
			Console.ReadKey ();

		}
		//Stage 1

		private static String performStage1Validation (Dictionary<String, String> tokenRef, RequestState state)
		{

			//converts the token value into json
			String jsonContentString = JsonConvert.SerializeObject (tokenRef, Formatting.Indented);

			//post json converted values
			String stage1String = postResponseForChallenge (@"http://challenge.code2040.org/api/getstring",
				                      jsonContentString, state);

			//function for reversing the string 
			stage1String = JsonConvert.DeserializeObject<Dictionary<String, String>> (stage1String) ["result"];
			char[] reverseCharsArray = stage1String.ToCharArray ();
			Array.Reverse (reverseCharsArray);
			String reverseString = new String (reverseCharsArray);

			// converts the dicitonary values of valuref into json
			Dictionary<String, String> validateRef = new Dictionary<string, string> ();
			validateRef ["token"] = tokenRef ["token"];
			validateRef ["string"] = reverseString;

			//converts the validateRef Dictionary values into json
			jsonContentString = JsonConvert.SerializeObject (validateRef, Formatting.Indented);

			//Post the dictionary of validate reference to the web api
			String validateString = postResponseForChallenge (@"http://challenge.code2040.org/api/validatestring",
				                        jsonContentString, state);
				
			return validateString;

		}

		//Stage 2
	
		private static String performStage2Validation (Dictionary<String, String> tokenRef, RequestState state)
		{

			String jsonContentString = JsonConvert.SerializeObject (tokenRef, Formatting.Indented);
			String stage2String = postResponseForChallenge (@"http://challenge.code2040.org/api/haystack",
				                      jsonContentString, state);

			// converts json response to the Hay Stack response structure, so that it can be extracted
			HayStackResponse hayStackResponseRef = JsonConvert.DeserializeObject<HayStackResponse> (stage2String);

			String needleString = (String)(hayStackResponseRef.result ["needle"]);
			JArray hayStackArray = (JArray)(hayStackResponseRef.result ["haystack"]);
			JToken[] stacksArray = hayStackArray.ToArray ();
			JToken tk = JToken.FromObject (needleString);

			//index of finds where the value tk is and inside of the stacks Array and returns the index value
			int indexOf = Array.IndexOf<JToken> (stacksArray, tk);
			Dictionary<String, String> validateRef = new Dictionary<string, string> ();
			validateRef ["token"] = tokenRef ["token"];
			validateRef ["needle"] = indexOf.ToString ();

			// converts the dicitonary values of valuref into json
			jsonContentString = JsonConvert.SerializeObject (validateRef, Formatting.Indented);

			//Post the dictionary of validate reference to the web api
			String validateString = postResponseForChallenge (@"http://challenge.code2040.org/api/validateneedle",
				                        jsonContentString, state);

			return validateString;

		}

		//Stage 3

		private static String performStage3Validation (Dictionary<String, String> tokenRef, RequestState state)
		{
			String jsonContentString = JsonConvert.SerializeObject (tokenRef, Formatting.Indented);
			String stage3String = postResponseForChallenge (@"http://challenge.code2040.org/api/prefix", jsonContentString, state);

			// converts json response to the Prefix Stack response structure, so that it can be extracted
			PrefixResponse prefixResponseRef = JsonConvert.DeserializeObject<PrefixResponse> (stage3String);


			String prefixString = (String)(prefixResponseRef.result ["prefix"]);
			JArray arrayOfString = (JArray)(prefixResponseRef.result ["array"]);
			JToken[] stacksArray = arrayOfString.ToArray (); 
			String[] stringArray = new string[stacksArray.Length];
			List<String> list1 = new List<String> (); 



			//string fu checks to see if the prefix and the array string value are the same 
			for (int arrayindex = 0; arrayindex <= stacksArray.Length - 1; arrayindex++) {
				int checker = 0; 

				//Comparision of the prefix string character and the array string characters
				for (int i = 0; i <= prefixString.Length - 1; i++) {
					String JtokenString = stacksArray [arrayindex].ToString ();
					stringArray [arrayindex] = stacksArray [arrayindex].ToString ();
					char[] JtokenChar = JtokenString.ToCharArray ();

					char[] prefixchar = prefixString.ToCharArray ();



					if (prefixchar [i] != JtokenChar [i]) {
						checker = -1; 
						break; 
					} 

				}
					
				if (checker == -1) {

					list1.Add (stringArray [arrayindex]);
				}
			}


			//Making a dictionary that has a string and array object to post json into web api
			Dictionary<String, Object> validateRef = new Dictionary<string, object> ();
			validateRef ["token"] = tokenRef ["token"];
			validateRef ["array"] = list1;



			//Serliazed the validateref dictionary into json
			jsonContentString = JsonConvert.SerializeObject (validateRef, Formatting.Indented);

			String validateString = postResponseForChallenge (@"http://challenge.code2040.org/api/validateprefix",
				                        jsonContentString, state);

			return validateString;



		}
	
		//Stage four

		private static String performStage4Validation (Dictionary<String, String> tokenRef, RequestState state)
		{
			String jsonContentString = JsonConvert.SerializeObject (tokenRef, Formatting.Indented);
			String stage4String = postResponseForChallenge (@"http://challenge.code2040.org/api/time",
				                      jsonContentString, state);

			// converts json response to the Hay Stack response structure, so that it can be extracted
			HayStackResponse datetimeInfo = JsonConvert.DeserializeObject<HayStackResponse> (stage4String);


			//gets the datestamp value 
			DateTime dateStamp = (DateTime)(datetimeInfo.result ["datestamp"]);

			//interval value 
			long interval = (long)(datetimeInfo.result ["interval"]);


			//adds the interval of the datetime challenge
			DateTime newDate = dateStamp.AddSeconds (interval);

			Dictionary<String, Object> validateRef = new Dictionary<String, Object> (); 
			validateRef ["token"] = tokenRef ["token"]; // token value
			validateRef ["datestamp"] = newDate.ToUniversalTime (); //converts time to iso

			//converts the Dictionary validateRef into json content 
			jsonContentString = JsonConvert.SerializeObject (validateRef);

			//Receive the response success or failure
			String validateString = postResponseForChallenge (@"http://challenge.code2040.org/api/validatetime",
				                        jsonContentString, state);
         
			Console.WriteLine (validateString);
			return validateString;


		}
			
	
		//web post and response function
		private static String postResponseForChallenge (String urlstring, String jsonContentString,
		                                                RequestState state)
		{
			// creates and UTF8Encoding object
			UTF8Encoding enc = new UTF8Encoding ();

			//converts the json into bytes
			Byte[] bytesArray = enc.GetBytes (jsonContentString);

			//Formats what type of data we are sending to the website(post)
			HttpWebRequest req = (HttpWebRequest)(WebRequest.Create (urlstring));
			req.Method = @"POST";
			req.ContentType = @"application/json";
			req.ContentLength = bytesArray.Length;

			//sends the data to the website
			using (Stream sr = req.GetRequestStream ()) {

				sr.Write (bytesArray, 0, bytesArray.Length);
			}


			state._request = req;

			//create an object that recives the response from the website 
			state._response = (HttpWebResponse)(req.GetResponse ());

			//Retrieves the response fromt the website
			using (StreamReader reader = new StreamReader (state._response.GetResponseStream (), Encoding.UTF8)) {
				String responseText = reader.ReadToEnd ();
				state._responseText = responseText;
			}

			state._response.Close ();


			return state._responseText;

		}
			
	}

}
