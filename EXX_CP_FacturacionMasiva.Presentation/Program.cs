using EXX_CP_FacturacionMasiva.Common.Utiles;
using EXX_CP_FacturacionMasiva.Infrastructure.Data;
using EXX_Metadata.BL;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EXX_CP_FacturacionMasiva.Presentation
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application oApp = null;
                if (args.Length < 1)
                {
                    oApp = new Application();
                }
                else
                {
                    //If you want to use an add-on identifier for the development license, you can specify an add-on identifier string as the second parameter.
                    //oApp = new Application(args[0], "XXXXX");
                    oApp = new Application(args[0]);
                }
                Global.SBOCompany = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany();
                MDResources.Messages = mostrarMensajes;
                if (MDResources.loadMetaData(Assembly.GetExecutingAssembly().GetName().Version, Application.SBO_Application, "EXX", "FACMSV"))
                {
                    Menu MyMenu = new Menu();
                    MyMenu.AddMenuItems();
                    oApp.RegisterMenuEventHandler(MyMenu.SBO_Application_MenuEvent);
                    Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                    oApp.Run();
                }
                else
                {
                    System.Windows.Forms.Application.Exit();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                default:
                    break;
            }
        }

        static void mostrarMensajes(string m, MessageType t)
        {
            switch (t)
            {
                case MessageType.Info:
                    Application.SBO_Application.StatusBar.SetText(m, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
                    break;
                case MessageType.Success:
                    Application.SBO_Application.StatusBar.SetText(m, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    break;
                case MessageType.Error:
                    Application.SBO_Application.StatusBar.SetText(m, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    break;
                default:
                    break;
            }
        }
    }
}
