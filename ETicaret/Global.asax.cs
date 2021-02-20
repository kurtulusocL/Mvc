﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ETicaret
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            if (Application["AktifKullanici"]==null)
            {
                int sayac = 1;
                Application["AktifKullanici"] = sayac;
            }
            else
            {
                int sayac = (int)Application["AktifKullanici"];
                sayac++;
                Application["AktifKullanici"] = sayac;
            }

            if (Application["ToplamZiyaretci"]==null)
            {
                int sayac = 1;
                Application["ToplamZiyaretci"] = sayac;
            }
            else
            {
                int sayac = (int)Application["ToplamZiyaretci"];
                sayac++;
                Application["ToplamZiyaretci"] = sayac;
            }
        }

        protected void Session_End()
        {
            int sayac = (int)Application["AktifKullanici"];
            sayac--;
            Application["AktifKullanici"] = sayac;
        }
    }
}
