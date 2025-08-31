using Microsoft.AspNetCore.Mvc;

namespace DemoDay4.Controllers
{
    public class BindController : Controller
    {
        //Bind/testDataTypePermi?name=ali&age=22

        //public IActionResult testDataTypePermi(string name,int age,string address,string color) 
        //{
        //    return Content($"name: {name}  age :{age} address :{address} color:{color}" );
        //}
        #region Array Bind
        // test data array
        //public IActionResult testDataTypePermi(string name, int age, string address, string[] color)
        //{
        //    return Content($"name: {name}  age :{age} address :{address} color:{color[1]}");
        //    //color 0 red 1
        //    //    color blue 0
        //    //    color[1]=red
        //    //    color[0]=yellow
        //}
        #endregion
        #region Dic Bind
        // test data Dic
        //public IActionResult testDataTypePermi(Dictionary<string,string> phones,string name)
        //{
        //    return Content($"name: {name}  Phones{phones}");
        //}
        #endregion

        #region Complex == (Coplex)
        // test object
        public IActionResult testDataTypeObject(Department dept)
        {
            return Content($"name done");
        }
        #endregion
    }
}
