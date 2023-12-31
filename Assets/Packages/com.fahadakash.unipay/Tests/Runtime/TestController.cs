﻿using SSLCommerz.Web.Controllers;
using SSLCommerz.Web.PaymentGateway;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TestController : MonoBehaviour
{
    public PaymentManager controller;
    public UnityEvent Onevent;
    public void SimpleMethod()
    {
        Debug.Log("Cool, fire via http connect");
        Onevent.Invoke();   



    }
    public void testingmethos()
    {
        Debug.Log("Successs");
    }
    public string[] SimpleStringMethod()
    {
        return new string[]{
            "result","result2"
        };
    }
    public int[] SimpleIntMethod()
    {
        return new int[]{
            1,2
        };
    }

    public ReturnResult CustomObjectReturnMethod()
    {
        ReturnResult result = new ReturnResult
        {
            code = 1,
            msg = "testing"
        };
        return result;
    }
    public ReturnResult CustomObjectReturnMethodWithQuery(int code, string msg)
    {
        ReturnResult result = new ReturnResult
        {
            code = code,
            msg = msg
        };
        return result;
    }

    //Mark as Serializable to make Unity's JsonUtility works.
    [System.Serializable]
    public class ReturnResult
    {
        public string msg;
        public int code;
    }



}
