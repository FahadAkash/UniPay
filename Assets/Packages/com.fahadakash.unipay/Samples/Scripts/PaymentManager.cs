using System;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;
using SSLCommerz.Web.PaymentGateway;
using UnityEngine;
using TMPro;

namespace SSLCommerz.Web.Controllers
{
    /// <summary>
    /// আমি Third Party Server use করেছি  , চাইলে আপ্নারা আপনাদের Server ব্যাবহার করতে পারেন :-) 
    /// আমি Ipn ছাড়া শুধু Transaction_Id দিয়ে চেক করেছি , কেও যদি IPN System ব্যাবহার করে থাকেন তাহলে Feel free to contact me 
    /// </summary>
    public class PaymentManager : MonoBehaviour
    {
        [Header("Please Add Payment Id and payment Password")]
        [Multiline()]
        public string Notification;
        CartControllernew cartController;

        public TMP_Text _Text;
        public Transform _Transform;
        private void Start()
        {
           cartController = new CartControllernew();
            //Just For Testing  :-) 
            InvokeRepeating(nameof(Checkout), 3, 2f);
           

        }
       

        public void Checkout_System()
        {

            cartController.Checkout();

        }

        public void Checkout()
        {
          
           StartCoroutine(cartController.sslcz.CheckValidated());
            _Text.text = $"{cartController.sslcz.newroot.status} {cartController.sslcz.newroot.store_amount} {cartController.sslcz.newroot.card_type}"

                ;  

        }

      

        void OnGUI()
        {
            if (GUILayout.Button("Click Here"))
            {
                Checkout();
            }
        }
     
    }
    public class CartControllernew :   Controller
    {
        public SSLCommerzGatewayProcessor sslcz;
        //we can get from email notificaton
        public string _storeID = string.Empty;
        public string _storPass = string.Empty;
        public IActionResult ProductList()
        {
            return View();
        }

     
        public void Checkout()
        {
           
            var productName = "Chocolate Product";
            var price = 85000;

            var baseUrl = string.Empty;

            // CREATING LIST OF POST DATA
            NameValueCollection PostData = new NameValueCollection();

            PostData.Add("total_amount", $"{price}");
            PostData.Add("tran_id", "TESTASPNET1234");
          //  PostData.Add("success_url", baseUrl + "/Cart/CheckoutFail");
            PostData.Add("success_url", "http://127.0.0.1:13579/fahadakash");
            PostData.Add("fail_url",    "http://127.0.0.1:13579/failed");
            PostData.Add("cancel_url", "http://127.0.0.1:13579/Cancle");

            PostData.Add("version", "3.00");
            PostData.Add("cus_name", "ABC XY");
            PostData.Add("cus_email", "abc.xyz@mail.co");
            PostData.Add("cus_add1", "Address Line On");
            PostData.Add("cus_add2", "Address Line Tw");
            PostData.Add("cus_city", "City Nam");
            PostData.Add("cus_state", "State Nam");
            PostData.Add("cus_postcode", "Post Cod");
            PostData.Add("cus_country", "Countr");
            PostData.Add("cus_phone", "0111111111");
            PostData.Add("cus_fax", "0171111111");
            PostData.Add("ship_name", "ABC XY");
            PostData.Add("ship_add1", "Address Line On");
            PostData.Add("ship_add2", "Address Line Tw");
            PostData.Add("ship_city", "City Nam");
            PostData.Add("ship_state", "State Nam");
            PostData.Add("ship_postcode", "Post Cod");
            PostData.Add("ship_country", "Countr");
            PostData.Add("value_a", "ref00");
            PostData.Add("value_b", "ref00");
            PostData.Add("value_c", "ref00");
            PostData.Add("value_d", "ref00");
            PostData.Add("shipping_method", "NO");
            PostData.Add("num_of_item", "1");
            PostData.Add("product_name", $"{productName}");
            PostData.Add("product_profile", "general");
            PostData.Add("product_category", "Demo");

           
           
            var isSandboxMood = true;

            sslcz = new SSLCommerzGatewayProcessor(_storeID, _storPass, isSandboxMood);

            string response = sslcz.InitiateTransaction(PostData);
            System.Diagnostics.Process.Start(response); 
            Debug.Log(response);
         
        }
        
        // যদি কেও পারেন এই কোড টি Ipn System করে ফেলেন Unity এর জন্য 
        #region For Validation with validation id and other stuffs
        public void CheckoutConfirmation()
        {
            if (!(!String.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID"))
            {
                Debug.Log("There some error while processing your payment. Please try again.");
                return;
            }

            string TrxID = Request.Form["tran_id"];
            // AMOUNT and Currency FROM DB FOR THIS TRANSACTION
            string amount = "85000";
            string currency = "BDT";

            var storeId = _storeID;
            var storePassword =  _storPass;

            SSLCommerzGatewayProcessor sslcz = new SSLCommerzGatewayProcessor(storeId, storePassword, true);
            var resonse = sslcz.OrderValidate(TrxID, amount, currency, Request);
            var successInfo = $"Validation Response: {resonse}";
           
            Debug.Log(successInfo);

            
        }
        #endregion

    }
}
