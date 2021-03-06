﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using EUni_Client.Services;
using Newtonsoft.Json;

namespace EUni_Client.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public async Task<ActionResult> Index()
        {
            var ApiService = Session[ServiceNames.ApiService] as ApiService;
            var UserId = Session.GetApiService().UserId;
            var Courses = await ApiService.GetAsync<IList<dynamic>>("/Course/GetAllCourses");
            var Students = await ApiService.GetAsync<IList<dynamic>>("/User/AllUsers");
            var StudentCourses = await ApiService.GetAsync<IList<dynamic>, int>("/Course/GetAllCoursesByStudId", "studId", UserId);
            var UnenrolledCourses = new List<object>();
            UnenrolledCourses = Courses.Where(c => StudentCourses.All(sc => sc.CourseId != c.CourseId)).ToList();
            //foreach (var studcourse in StudentCourses)
            //{
            //    if (course.CourseID != studcourse.CourseId)
            //    {
            //        UnenrolledCourses.Add(course);
            //    }
            //}   
            ViewBag.UnenrolledCourses = UnenrolledCourses;
            ViewBag.UserId = UserId;
            ViewBag.Students = Students;
            ViewBag.StudentCourses = StudentCourses;
            ViewBag.Courses = Courses;
            return View();
        }
        [HttpPost]
        public async Task<RedirectToRouteResult> CreateCourse(CreateCourseViewModel course)
        {
            var apiService = Session.GetApiService();
            var result = await apiService.PostAsyncWithReturn<string, CreateCourseViewModel>("/Course/Add", course);
            //var data = new RouteValueDictionary();
            //data.Add("Module", course.Course);
            return RedirectToAction("Index", "Courses");
        }
        public async Task<ActionResult> Create()
        {
            //var ApiService = Session[ServiceNames.ApiService] as ApiService;
            //var Courses = await ApiService.GetAsync<IEnumerable<dynamic>>("/Course/GetAllCourses");
            //ViewBag.Courses = Courses;
            return View();
        }

        public async Task<RedirectToRouteResult> EnrollToCourse(int courseId)
        {
            var apiService = Session.GetApiService();
            var userId = apiService.UserId;
            await apiService.GetAsync<object, object>("/User/EnrollUserToCourse", new Dictionary<string, object> { { "courseId", courseId }, { "userId", userId } });
            return RedirectToAction("Index", "Courses");
        }

        public async Task<ActionResult> Course(string c)
        {
            var course = JsonConvert.DeserializeObject(c);
            var ApiService = Session[ServiceNames.ApiService] as ApiService;
            var Modules = await ApiService.GetAsync<IEnumerable<dynamic>, int>("/Module/GetByCourse", "courseId", (int)(((dynamic)course).CourseId));

            if (c != null)
                ViewBag.Course = JsonConvert.DeserializeObject(c);

            ViewBag.Modules = Modules;
            return View();
        }
        public async Task<ActionResult> AssignTeacher()
        {
            var ApiService = Session[ServiceNames.ApiService] as ApiService;
            var Teachers = await ApiService.GetAsync<IEnumerable<dynamic>>("/User/AllTeachers");
            var Courses = await ApiService.GetAsync<IEnumerable<dynamic>>("/Course/GetAllCourses");
            ViewBag.Courses = Courses;
            ViewBag.Teachers = Teachers;
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> PostAssignTeacher(string fullname, string courseCode)
        {
            var split = fullname.Split(' ');
            var apiService = Session.GetApiService();
            var result = await apiService.GetAsync<object, object>("/Course/AssignTeacher", new Dictionary<string, object>
            { {"lastName", split[1]}, {"firstName", split[0]}, {"courseCode", courseCode}});

            return RedirectToAction("Index", "Courses");
        }
    }

    public class CreateCourseViewModel
    {
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}