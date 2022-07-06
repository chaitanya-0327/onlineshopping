using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using  System.Linq;
using Foodcore.Models;

namespace Foodcore.Controllers
{
   
    public class HomeController : Controller
    {
        lifestyleContext dc = new lifestyleContext();



        [HttpGet]
        public ViewResult Login()
        {


            return View();

        }

        [HttpPost]
        public ActionResult Login(string email, string password, string role)
        {
            bool isValidUser = false;

            if (role == "admin")
            {
                isValidUser = dc.Admintables.Where(c => c.Adminemail == email && c.Adminpwd == password).Count() > 0;


            }
            else if (role == "customer")
            {
                isValidUser = dc.Registers.Where(c => c.Email == email && c.Pwd == password).Count() > 0;
            }
            var a = isValidUser;
            if (a)
            {
                HttpContext.Session.SetString("login_eamil", email);
                HttpContext.Session.SetString("login_role", role);
                return RedirectToAction("home");
            }
            else
            {
                ViewData["Message"] = "Invaild user";
            }

            return View();
        }

        public ViewResult electronics()
        {
            //var result = dc.Products.ToList();
            var result = dc.Products.ToList().Where(c => c.Category == "electronics");


            return View(result);
        }
        public ViewResult kitchen()
        {
            var result = dc.Products.ToList().Where(c => c.Category == "kitchen" || c.Category == "home");

            return View(result);
        }
        public ViewResult fashion()
        {
            var result = dc.Products.ToList().Where(c => c.Category == "fashion");

            return View(result);
        }



        public ViewResult contactus()
        {

            return View();
        }









        [HttpGet]
        public ViewResult RegisterPage()
        {

            return View();

        }

        [HttpPost]
        public ViewResult RegisterPage(Register r)
        {


            if (ModelState.IsValid)
            {
                dc.Registers.Add(r);
                int i = dc.SaveChanges();

                if (i > 0)
                {
                    ViewData["a"] = "New User created successfully";
                }
                else
                {
                    ViewData["a"] = "Error occured try after some time";
                }

                return View();
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ViewResult myorders(string myitemid)
        {
            // is it 1 or many

            var result = dc.Products.ToList().Find(c => c.Productid == myitemid);

            if (result != null)
            {
                TempData["pri"] = result.Price;
                TempData["id"] = result.Productid;

                TempData.Keep();
            }

            return View(result);


        }


        [HttpPost]
        public ActionResult myorders(IFormCollection r)
        {

            // insert new value to myorders table

            if (HttpContext.Session.GetString("login_eamil") == null)
            {
                return RedirectToAction("Login");
            }

            else
            {
                Myorder ob = new Myorder();
                ob.Email = HttpContext.Session.GetString("login_eamil");
                ob.Itemid = TempData["id"].ToString();
                ob.Price = Convert.ToInt32(TempData["pri"]);
                ob.Qty = Convert.ToInt32(r["t1"]);

                dc.Myorders.Add(ob);
                int i = dc.SaveChanges();

                if (i > 0)
                {
                    ViewData["a"] = "Your order placed successfully";
                }
                else
                {
                    ViewData["a"] = "Error occured try after some time";
                }

                return View();
            }
        }


        [HttpGet]
        public ActionResult Addtocart(string myitemid)
        {
            //  var result = dc.Menus.ToList().Find(c => c.Itemid == myitemid);

            //  li.Add(result);

            Cart m = new Cart();
            m.Email = HttpContext.Session.GetString("login_eamil");
            m.Itemid = myitemid;
            dc.Carts.Add(m);

            int i = dc.SaveChanges();
            if (i > 0)
            {
                ViewData["a"] = myitemid + "  Item Add successfully to cart";
            }
            else
            {
                ViewData["a"] = myitemid + "failed to add try again";
            }

            //TempData.Add(myitemid, myitemid);

            var res = dc.Products.ToList().Join(dc.Carts.ToList(), c => c.Productid, w => w.Itemid, (c, w) => new { c.Productid, c.Productname, c.Price, c.Images, c.Productdesc });

            int? sum = 0;

            foreach (var item in res)
            {
                sum = sum + item.Price;


            }

            ViewData["total"] = sum;


            return View(res);


        }
        [HttpGet]
        public ActionResult addproducts()
        {

            return View();

        }

        //public ActionResult addproducts(Product images)
        //{


        //    string path =System.Web.HttpContext.Current.Request.Path;

        //    return View();

        //}


        public ActionResult logout()
        {

            HttpContext.Session.Remove("login_eamil");
            HttpContext.Session.Remove("login_role");




            return RedirectToAction("login");

        }

        [HttpGet]
        public ViewResult deleteproduct(string myitemid)
        {

            Cart c = new Cart();
            var result = dc.Products.ToList().Find(c => c.Productid == myitemid);

            if (result != null)
            {

                TempData["id"] = result.Productid;

                TempData.Keep();
            }

            return View(result);


        }

        [HttpPost]
        public ActionResult deleteproduct(IFormCollection c)
        {



            if (HttpContext.Session.GetString("login_eamil") == "admin")
            {

                Product ob = new Product();
                ob.Productid = TempData["id"].ToString();
                dc.Products.Remove(ob);
                int i = dc.SaveChanges();


                if (i > 0)
                {
                    ViewData["a"] = "deltedsucessfully";
                }
                else
                {
                    ViewData["a"] = "Error occured try after some time";
                }

                return View();

            }
            return View();
        }

        public ViewResult admindisplay()
        {
            var result = dc.Products.ToList();
            return View(result);




        }



        [HttpGet]
        public ActionResult addfeedback()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addfeedback(string myitemid)
        {
            Feedback f = new Feedback();
            f.Email = HttpContext.Session.GetString("login_eamil");
            f.Productid = myitemid;
            dc.Feedbacks.Add(f);

            int k = dc.SaveChanges();
            if (k > 0)
            {
                ViewData["f"] = myitemid + "feedback successfully";
            }
            else
            {
                ViewData["f"] = myitemid + "failed to add try again";
            }

            return View();
        }
        public ViewResult cartlist()
        {
            var result = dc.Carts.ToList();
            return View(result);
        }

        [HttpGet]
        public ViewResult home()
        {
            return View();

        }
        [HttpPost]
        public ViewResult home(string search)
        {
            var res = dc.Products.ToList();
            
            var result = dc.Products.Where(s => s.Productname.Contains(search)).ToList();

            if (result != null)
            {
                return View(result);
            }
            else
            {
                ViewBag.a = "not found";
            }

            return View(res);

        }


    }

}



