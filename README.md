Api Challenge!
============

Code2040's API challenge

Essentially it was four stage challenge 

Stage I: Reverse a string 

- POST a JSON dictionary with the key token and your previous token value to this 

endpoint:http://challenge.code2040.org/api/getstring 

the getstring endpoint will return a string that your code should then reverse.

Stage II: Needle in a haystack

- They will send a dictionary with two values and keys. The first value, needle, is a string. The second value, haystack, is an array of strings. You’re going to tell the API where the needle is in the array. POST a JSON dictionary

http://challenge.code2040.org/api/haystack

Stage III: Prefix

- In this challenge, the API is going to give you another dictionary. The first value, prefix, is a string. The second value, array, is an array of strings. Your job is to return an array containing only the strings that do not start with that prefix.


Stage IV: The dating game

- The API will again give you a dictionary. The value for datestamp is a string, formatted as an ISO 8601 datestamp. The value for interval is a number of seconds. You’re going to add the interval to the date, then return the resulting date to the API.
