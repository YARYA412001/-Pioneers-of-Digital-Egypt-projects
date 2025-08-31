namespace DemoDay4.tEST
{
    public class FAKEdATA
    {
        string _viewData;
        public object ViewData
        { //WAY 1

            get
            {
                return _viewData;
            }
            set
            {
                _viewData = (string)value;
            }
        }
        public dynamic ViewBag
        {
            //WAY2
            get
            {
                return _viewData;
            }
            set
            {
                _viewData = value;
            }
        }
    }
}
