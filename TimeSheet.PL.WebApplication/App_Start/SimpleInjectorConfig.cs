﻿using SimpleInjector;
using MVCSimpleInjectorDemo.Interfaces;
using MVCSimpleInjectorDemo.Repository;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;

namespace MVCSimpleInjectorDemo.App_Start
{
    public class SimpleInjectorConfig
    {
        public static void RegisterComponents()
        {
            var container = new Container();
            // register all your components with the container here 
            // it is NOT necessary to register your controllers 

        }
    }
}