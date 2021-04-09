namespace Consumption.ViewModel.Common
{
    public class ModulePageEmu
    {
        public static string GetModulePage(string pageName)
        {
            string pageUrl = "";
            switch (pageName)
            {
                case
                    "Escolha":
                    pageUrl = "UserControlEscolhaCenter";
                    break;
                case "Inicio":
                    pageUrl = "UserControlInicioCenter";
                    break;
            }
            return pageUrl;
        }
    }
}
