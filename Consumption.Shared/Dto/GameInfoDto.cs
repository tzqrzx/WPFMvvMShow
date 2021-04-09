namespace Consumption.Shared.Dto
{
    public class GameInfoDto : BaseDto
    {

        private string code;

        private string gameCountry;

        private int gameHot;
        private string gameName;

        private string gamePackage;

        private string gamePic;

        private string gamePicPath;

        private string gamePlatform;

        private string gameWebsite;

        private bool hot;


        private string isTimeFree;


        public string Code
        {
            get { return code; }
            set { code = value; RaisePropertyChanged(); }
        }



        public string GameCountry
        {
            get { return gameCountry; }
            set { gameCountry = value; RaisePropertyChanged(); }
        }



        public int GameHot
        {
            get { return gameHot; }
            set { gameHot = value; RaisePropertyChanged(); }
        }

        public string GameName
        {
            get { return gameName; }
            set { gameName = value; RaisePropertyChanged(); }
        }

        public string GamePackage
        {
            get { return gamePackage; }
            set { gamePackage = value; RaisePropertyChanged(); }
        }

        public string GamePic
        {
            get { return gamePic; }
            set { gamePic = value; RaisePropertyChanged(); }
        }

        public string GamePicPath
        {
            get { return gamePicPath; }
            set { gamePicPath = value; RaisePropertyChanged(); }
        }

        public string GamePlatform
        {
            get { return gamePlatform; }
            set { gamePlatform = value; RaisePropertyChanged(); }
        }

        public string GameWebsite
        {
            get { return gameWebsite; }
            set { gameWebsite = value; RaisePropertyChanged(); }
        }

        public bool Hot
        {
            get { return hot; }
            set { hot = value; RaisePropertyChanged(); }
        }


        public string IsTimeFree
        {
            get { return isTimeFree; }
            set { isTimeFree = value; RaisePropertyChanged(); }
        }


    }
}
