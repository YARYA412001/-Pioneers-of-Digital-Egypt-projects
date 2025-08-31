using System.ComponentModel.DataAnnotations;

namespace DemoDay4.Models
{
    public class User
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
