using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Consumption.ViewModel
{
    /// <summary>
    /// 消息提示
    /// </summary>
    public class ModuleGameInfo : ObservableObject
    {
        private string code;

        private string gameCountry;

        private int? gameHot;
        private string gameName;

        private string gamePackage;

        private string gamePic;

        private string gamePicPath;

        private string gamePlatform;

        private string gameWebsite;

        private bool hot;
        private int vitibilityRun;
        private int vitibilityStop = 3;
        private int id;
        private string isTimeFree;




        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged(); }
        }



        public string GameCountry
        {
            get { return gameCountry; }
            set { gameCountry = value; OnPropertyChanged(); }
        }



        public int? GameHot
        {
            get { return gameHot; }
            set { gameHot = value; OnPropertyChanged(); }
        }

        public string GameName
        {
            get { return gameName; }
            set { gameName = value; OnPropertyChanged(); }
        }

        public string GamePackage
        {
            get { return gamePackage; }
            set { gamePackage = value; OnPropertyChanged(); }
        }

        public string GamePic
        {
            get { return gamePic; }
            set { gamePic = value; OnPropertyChanged(); }
        }

        public string GamePicPath
        {
            get { return gamePicPath; }
            set { gamePicPath = value; OnPropertyChanged(); }
        }

        public string GamePlatform
        {
            get { return gamePlatform; }
            set { gamePlatform = value; OnPropertyChanged(); }
        }

        public string GameWebsite
        {
            get { return gameWebsite; }
            set { gameWebsite = value; OnPropertyChanged(); }
        }

        public bool Hot
        {
            get { return hot; }
            set { hot = value; OnPropertyChanged(); }
        }

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        public int VitibilityRun
        {
            get { return vitibilityRun; }
            set { vitibilityRun = value; OnPropertyChanged(); }
        }

        public int VitibilityStop
        {
            get { return vitibilityStop; }
            set { vitibilityStop = value; OnPropertyChanged(); }
        }


        public string IsTimeFree
        {
            get { return isTimeFree; }
            set { isTimeFree = value; OnPropertyChanged(); }
        }



    }
}
