using M_08_Hotel.Models;
using M_08_Hotel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace M_08_Hotel.Controllers
{
    public class HotelsController : Controller
    {
            private readonly HotelDbContext db;
            private readonly IWebHostEnvironment env;
            public HotelsController(HotelDbContext db, IWebHostEnvironment env) { this.db = db; this.env = env; }
            public IActionResult Index()
            {
                //Eager loading
                var data = db.Hotels.Include(x => x.Rooms).ToList();

                return View(data);
            }

            [HttpPost]
            public IActionResult Create()
            {
                var model = new HotelInputModel();
                model.Rooms.Add(new Room { });
                return View(model);
            }
            [HttpPost]
            public IActionResult Create(HotelInputModel model, string operation = "")
            {
                if (operation == "add")
                {
                    model.Rooms.Add(new Room { });
                    foreach (var item in ModelState.Values)
                    {
                        item.Errors.Clear();
                        item.RawValue = null;
                    }
                }
                if (operation.StartsWith("remove"))
                {
                    //int i = operation.IndexOf("_");
                    //int index = int.Parse(operation.Substring(i+1));
                    int index = int.Parse(operation.Substring(operation.IndexOf("_") + 1));
                    model.Rooms.RemoveAt(index);
                    foreach (var item in ModelState.Values)
                    {
                        item.Errors.Clear();
                        item.RawValue = null;
                    }
                }
                if (operation == "insert")
                {
                    if (ModelState.IsValid)
                    {
                        var w = new Hotel
                        {
                            HotelName = model.HotelName,
                            Location = model.Location,
                            Rating = model.Rating,
                            ContactInfo = model.ContactInfo,
                            HotelRoomAvailable = model.HotelRoomAvailable,
                     

                        };
                        string ext = Path.GetExtension(model.Picture.FileName);
                        string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string filePath = Path.Combine(env.WebRootPath, "Pictures", f);
                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        model.Picture.CopyTo(fs);
                        fs.Close();
                        w.Picture = f;

                        db.Hotels.Add(w);
                        db.SaveChanges();
                        foreach (var wl in model.Rooms)
                        {
                            w.Rooms.Add(wl);
                        }
                        return RedirectToAction("Index");
                    }
                }
                return View(model);
            }
            
        }
    }
