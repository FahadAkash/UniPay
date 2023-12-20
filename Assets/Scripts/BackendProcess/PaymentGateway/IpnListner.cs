using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class IpnListner : MonoBehaviour
{
    HttpListener listener;

    void Start()
    {
        // Create an HTTP listener
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/"); // Replace with your desired URL or use a different port
        listener.Start();

        // Start listening for IPN notifications
        
    }
    public void LisnerEndpoint()
    {
        StartCoroutine(ListenForIPN());
    }
    
  

    IEnumerator ListenForIPN()
    {
       
            yield return new WaitForSeconds(3f);
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;

            // Read the request body
            using (StreamReader reader = new StreamReader(request.InputStream))
            {
                string requestBody = reader.ReadToEnd();

                // Validate the received message using the Transaction Validation API of SSLCommerz
                // Make an API call to SSLCommerz to verify the transaction
                // You can use Unity's WWW or UnityWebRequest to make the API call

                // Example using UnityWebRequest
                string validationUrl = "https://sandbox.sslcommerz.com/validator/api/validationserverAPI.php"; // Replace with the appropriate API endpoint based on your environment (sandbox or production)
                UnityWebRequest validationRequest = UnityWebRequest.PostWwwForm(validationUrl, requestBody);
                yield return validationRequest.SendWebRequest();

                if (validationRequest.result == UnityWebRequest.Result.Success)
                {
                    string validationResponse = validationRequest.downloadHandler.text;

                    // Process the response from SSLCommerz
                    // You can check the response status and perform necessary actions based on the payment status

                    // Example response handling
                    if (validationResponse == "VALID")
                    {
                        // Payment is valid, update your database or perform other actions
                    }
                    else
                    {
                        // Payment is not valid, handle the error or log the response
                    }
                }
                else
                {
                    // Handle the error if the API call fails
                }
            }

            // Send a response back to SSLCommerz to acknowledge the IPN notification
            HttpListenerResponse response = context.Response;
            response.StatusCode = 200;
            byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes("OK");
            response.ContentLength64 = responseBytes.Length;
            response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            response.OutputStream.Close();
        
    }

    void OnDestroy()
    {
        // Stop the HTTP listener when the script is destroyed
        if (listener != null && listener.IsListening)
        {
            listener.Stop();
        }
    }
}
