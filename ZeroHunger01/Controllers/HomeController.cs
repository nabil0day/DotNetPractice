using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ZeroHunger01.Models;

namespace ZeroHunger01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRestaurant(Restaurant restaurant)
        {
            var db = new ZHContext();
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RestaurantList()
        {
            var db = new ZHContext();
            var list = db.Restaurants.ToList();
            return View(list);
        }





        //Product Cart then can confirm and goes to AddtoReqTable
        public ActionResult Request(int id)
        {
            var db = new ZHContext();
            List<Restaurant> reslist = null;
            var restitem = db.Restaurants.Find(id);
            if (Session["request"] == null)
            {
                reslist = new List<Restaurant>();
            }
            else
            {
                reslist = (List<Restaurant>)Session["request"];
            }
            reslist.Add(restitem);
            Session["request"] = reslist;
            return RedirectToAction("RestaurantList");
        }

        public ActionResult Requests()
        {

            var reslist = (List<Restaurant>)Session["request"];
            return View(reslist);
        }

        public ActionResult AddtoReqTable()
        {

            var cart = (List<Restaurant>)Session["request"];
            Restaurant restaurant = new Restaurant();
            restaurant.MaxTime = DateTime.Now;
            //restaurant.OrderStatus = "Available";

            ZHContext db = new ZHContext();

            db.SaveChanges();
            foreach (var p in cart)
            {
                var od = new Request();

                od.rid = p.Id;
                od.Location = p.Location;
                od.MaxTime = p.MaxTime;
                od.Foodname = p.Foodname;
                od.Quantity = p.Quantity;
                od.OrderStatus = "Available";

                db.Requests.Add(od);
            }

            db.SaveChanges();
            Session["request"] = null;
            TempData["Msg"] = "Order Placed Successfully";
            return RedirectToAction("ShowRequestList");
        }

        //Employee

        public ActionResult EmployeeList()
        {
            var db = new ZHContext();
            var list = db.Employees.ToList();
            return View(list);
        }
        public ActionResult ShowEmployeeList(int id)
        {
            Session["Reqid"] = id;

            var db = new ZHContext();
            var list = db.Employees.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            var db = new ZHContext();
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

        //RequestListShow can be visit by ADMIN
        public ActionResult ShowRequestList()
        {
            var db = new ZHContext();
            var list = db.Requests.ToList();
            //TempData["id"] = id;
            return View(list);
        }

        //RequestListShow can be visit by Employee to set the Deliver Status
        public ActionResult ProcessEmployee()
        {
            var db = new ZHContext();
            var emp = (Employee)Session["emp"];
            var c = (from item in db.Processes
                     where item.Eid == emp.Id
                     select item).ToList();




            return View(c);
        }



        //Delivered For Employee
        public ActionResult Delivered(int id)
        {
            var db = new ZHContext();
            var process = db.Processes.Find(id);

            process.EmpStatus = "";
            process.OrderStatus = "Delivered";

            TempData["Message"] = "Successfully Delivered";
            db.SaveChanges();
            return RedirectToAction("ProcessEmployee");
        }


        //Admin Login
        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(AdminLogin adminlogin)
        {
            var db = new ZHContext();
            var c = (from item in db.Admins
                     where item.username == adminlogin.username
                     && item.password == adminlogin.password
                     select item).SingleOrDefault();

            if (c == null)
            {
                TempData["Message"] = "Wrong username and password";
                return View();
            }
            else
            {
                TempData["Message"] = "Welcome Admin";
                return RedirectToAction("ShowRequestList");
            }
        }


        //EmployeeLogin


        [HttpGet]
        public ActionResult EmployeeLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeLogin(EmployeeLogin employeeLogin)
        {
            var db = new ZHContext();
            var c = (from item in db.Employees
                     where item.Username == employeeLogin.Username
                     && item.Password == employeeLogin.Password
                     select item).SingleOrDefault();

            if (c == null)
            {
                TempData["Message"] = "Wrong username and password";
                return View();
            }
            else
            {
                TempData["Message"] = "Welcome Employee!";
                Session["emp"] = c;
                return RedirectToAction("ProcessEmployee");
            }
        }

        //Admin Assign Employeee
        public ActionResult GetEmployee(int id)
        {

            var db = new ZHContext();
            var eid = (from item in db.Employees
                       where item.Id == id
                       select item).SingleOrDefault();
            eid.EmpStatus = "Assigned";


            var process = new Process();
            process.Rid = (int)Session["ReqId"];
            var req = (from item in db.Requests
                       where item.Id == process.Rid
                       select item).SingleOrDefault();
            process.Eid = id;
            process.Foodname = req.Foodname;
            process.Quantity = req.Quantity;
            process.EmpStatus = eid.EmpStatus;
            process.OrderStatus = req.OrderStatus;
            db.Processes.Add(process);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        //Restaurant Login
        public ActionResult RestaurantLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RestaurantLogin(RestaurantLogin restaurantLogin)
        {
            var db = new ZHContext();
            var c = (from item in db.Restaurants
                     where item.Username == restaurantLogin.Username
                     && item.Password == restaurantLogin.Password
                     select item).SingleOrDefault();
            if (c == null)
            {
                TempData["Message"] = "Wrong username and password";
                return View();
            }
            else
            {
                TempData["Message"] = "Welcome to ZeroHunger!";
                Session["res"] = c;
                Session["ResId"] = (int)c.Id;
                return RedirectToAction("RestaurantDetails");
            }
        }

        public ActionResult RestaurantDetails()
        {
            var db = new ZHContext();
            int reid = (int)Session["ResId"];
            Restaurant restaurant = db.Restaurants.Find(reid);
            List<Restaurant> restitem = new List<Restaurant> { restaurant };

            return View(restitem);

        }

    }
}