using EXX_CP_FacturacionMasiva.Presentation.Forms.USRForms;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EXX_CP_FacturacionMasiva.Presentation
{
    class Menu
    {
        public void AddMenuItems()
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;

            oMenus = Application.SBO_Application.Menus;

            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));

            Application.SBO_Application.Forms.GetForm("169", 1)?.Freeze(true);
            oMenuItem = Application.SBO_Application.Menus.Item("43520"); // moudles'
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
            oCreationPackage.UniqueID = "EXX_MNU_FM";
            oCreationPackage.String = "Facturación Masiva";
            oCreationPackage.Enabled = true;
            oCreationPackage.Position = -1;

            oMenus = oMenuItem.SubMenus;

            try
            {
                //  If the manu already exists this code will fail
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception e)
            {

            }

            try
            {
                // Get the menu collection of the newly added pop-up item
                oMenuItem = Application.SBO_Application.Menus.Item("EXX_MNU_FM");
                oMenus = oMenuItem.SubMenus;

                // Create s sub menu
                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
                oCreationPackage.UniqueID = "EXX_MNU_FM_COMP";
                oCreationPackage.String = "Compras";
                oMenus.AddEx(oCreationPackage);


                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "EXX_MNU_FM_VENT";
                oCreationPackage.String = "Ventas";
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception er)
            { //  Menu already exists
              //  Application.SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }

            try
            {
                oMenuItem = Application.SBO_Application.Menus.Item("EXX_MNU_FM_COMP");
                oMenus = oMenuItem.SubMenus;

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "EXX_MNU_FM_COMP1";
                oCreationPackage.String = "Distribución";
                oMenus.AddEx(oCreationPackage);

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = "EXX_MNU_FM_COMP2";
                oCreationPackage.String = "Dulcería y servicios";
                oMenus.AddEx(oCreationPackage);
            }
            catch (Exception)
            {

            }
            finally
            {
                Application.SBO_Application.Forms.GetForm("169", 1)?.Freeze(false);
                Application.SBO_Application.Forms.GetForm("169", 1)?.Update();
                Application.SBO_Application.StatusBar.SetText("Menú facturación masiva cargado correctamente", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (pVal.BeforeAction)
                {
                    switch (pVal.MenuUID)
                    {
                        case "EXX_MNU_FM_COMP1":
                            new FormFacturacionMasivaCompras("D").Show();
                            break;
                        case "EXX_MNU_FM_COMP2":
                            new FormFacturacionMasivaCompras("S").Show();
                            break;
                        case "EXX_MNU_FM_VENT":
                            new FormFacturacionMasivaVentas().Show();
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }
    }
}
