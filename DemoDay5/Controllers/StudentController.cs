
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DemoDay4.Controllers
{
    public class StudentController : Controller
    {
        ITIContext context;
        public StudentController()
        {
            context=new ITIContext();
        }
        #region ViewData and ViewBag
        public IActionResult Index()
        {
            // Get All Student Data
            string msg = "hello";
            ViewData["msg"] = msg;
            int temp = 40;
            ViewData["temp"] = temp;
            if (temp >= 30) 
            {
                ViewData["color"] = "blue";
                ViewBag.Color = 20;
                ViewBag.msg = "Welcome to ITI";
            }
            else
            {
                ViewData["color"] = "red";
            }

                List<Student> stsData = context.Students.ToList();


            return View(stsData);
        }
        #endregion
        #region ViewModel
        public IActionResult getStsData()
        {
            List<Student> stsData = context.Students.Include(s=>s.Dept).Include(s=>s.StSuperNavigation).ToList();
            
            // Create ViewModel
            List<StudentDataWithMsgWithTempViewModel> stsVM = new();
            foreach (var st in stsData)
            {
                StudentDataWithMsgWithTempViewModel stsUserVM = new()
                {
                    id=st.StId,
                    stdName = $"{st.StFname} {st.StLname}",
                    stdAddress = st.StAddress,
                    deptName = st.Dept?.DeptName,
                    MangerName=st.StSuperNavigation?.StFname,
                    deptId = st?.DeptId,
                };
              stsVM.Add(stsUserVM);
            }
            return View(stsVM);
        }
        public IActionResult Details(int id)
        {
            Student sts=context.Students.Include(c=>c.StudCourses).FirstOrDefault(s => s.StId == id);
            StsDetailsViiewModel stsVM = new()
            {
                stdName = $"{sts.StFname} {sts.StLname}",
                stdAddress = sts.StAddress,
                CourseDegree= sts.StudCourses
                     .Select(sc => sc.Grade)
                     .ToList()
            };
            return View(stsVM);
        }
        #endregion
        #region day 5
        public IActionResult NewStudent() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveNew(Student stsFromRequest) 
        {
            //check
            if (stsFromRequest != null) 
            {
                //add
                context.Students.Add(stsFromRequest);
                //save
                context.SaveChanges();
                //return list 
                return RedirectToAction("getStsData","Student");
            }
            return View("NewStudent",stsFromRequest);
        }
        #endregion
        #region test One Case of Overload
        public IActionResult method1() 
        {
            return Content("This is method1 without id");
        }
        // get name ==> Query string
        // name post ==> form
        // route value ==> 
        // default
        [HttpPost]
        public IActionResult method1(string name)
        {
            return Content($"This is method1 with  name {name}");
        }
        #endregion
        #region Edit
        //public IActionResult editStd(Student sts,int id) 
        //{
            
        //}
        public IActionResult editSts(int id) 
        {
            // get Data from dATA BASe depend on id
            // getData from database
            Student stsModel = context.Students.FirstOrDefault(s => s.StId == id);
            StudentWithDeptListViewModel stsVM = new()
            {
                StId = stsModel.StId,
                StFname = stsModel.StFname,
                StLname = stsModel.StLname,
                StAddress = stsModel.StAddress,
                DeptId = stsModel.DeptId,
                deptList = context.Departments.ToList()
            };
            return View(stsVM);
        }
        public IActionResult SaveEdit(StudentWithDeptListViewModel stsFromRequest) 
        {
            if(stsFromRequest.StFname!=null && stsFromRequest.StLname != null) 
            {
                // get old Ref (data قديمه)
                Student stsModel = context.Students.FirstOrDefault(s => s.StId == stsFromRequest.StId);
                stsModel.StFname = stsFromRequest.StFname;
                stsModel.StLname = stsFromRequest.StLname;
                stsModel.StAddress = stsFromRequest.StAddress;
                stsModel.DeptId = stsFromRequest.DeptId;
                context.SaveChanges();
                return RedirectToAction("getStsData");
            }
            
            return View("editSts",stsFromRequest);
        }
        #endregion
        #region Delete
        public IActionResult Remove(int id) 
        {
            // get Row Data
            var sts = context.Students.FirstOrDefault(s => s.StId == id);
            if(sts != null) 
            {
                context.Students.Remove(sts);
                context.SaveChanges();
            }
            return RedirectToAction("getStsData");
        }
        #endregion
    }
}
