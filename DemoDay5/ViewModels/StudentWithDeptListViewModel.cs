namespace DemoDay4.ViewModels
{
    public class StudentWithDeptListViewModel
    {
        public int StId { get; set; }

        public string StFname { get; set; }

        public string StLname { get; set; }

        public string StAddress { get; set; }
        public int? DeptId { get; set; }
        public List<Department> deptList { get; set; }
    }
}
