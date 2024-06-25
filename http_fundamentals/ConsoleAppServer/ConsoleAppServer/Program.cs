using System.Net;

HttpListener listener = new HttpListener();

listener.Prefixes.Add("http://localhost:5678/");

listener.Start();
Console.WriteLine("Listening...");


var context = listener.GetContext(); 
var text = context.Request.Url.LocalPath.Trim('/');
var response = "Answer: " + text;

byte[] buffer = System.Text.Encoding.UTF8.GetBytes(response);

var responseOutput = context.Response.OutputStream;
responseOutput.Write(buffer, 0, buffer.Length);

context.Response.Close(); 

listener.Stop();
