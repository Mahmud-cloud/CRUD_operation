using crud_operation._SysDbContext;
using crud_operation.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace crud_operation.Controllers
{
    public class HomeController : Controller
    {

        //private readonly ILogger<HomeController> _logger;

        private readonly _sysdbcontext _dbstuinfocontext;
        private readonly IWebHostEnvironment _stuinfowebhostenv;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(_sysdbcontext context, IWebHostEnvironment hostEnvironment)
        {
            _dbstuinfocontext = context;
            _stuinfowebhostenv = hostEnvironment;
        }

        public IActionResult Index()
        {
            var obj = _dbstuinfocontext.TblRmpiStuInfo.ToList();
            return View(obj);
        }

        //get-create
        public IActionResult Create()
        {
            RmpiStuInfoModel modelobj = new RmpiStuInfoModel();
            

            return View(modelobj);
        }

        // POST: imageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RmpiStuInfoModel StuModels)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _stuinfowebhostenv.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(StuModels.StuPhotoFile.FileName);
                string extension = Path.GetExtension(StuModels.StuPhotoFile.FileName);
                StuModels.StuPhoto = fileName + extension;
                string path = Path.Combine(wwwRootPath, "studentimg", fileName + extension);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await StuModels.StuPhotoFile.CopyToAsync(fileStream);
                }

                _dbstuinfocontext.Add(StuModels);
                await _dbstuinfocontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(StuModels);
        }

        // GET: imageController/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            RmpiStuInfoModel MembMod = await _dbstuinfocontext.TblRmpiStuInfo.FirstOrDefaultAsync(m => m.StuID == id);
            if (MembMod == null)
            {
                return NotFound();
            }

            return View(MembMod);
        }

        // POST: imageController/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RmpiStuInfoModel StuModels, int id, IFormFile StuPhotoFile)
        {
            var objStuInfoModel = await _dbstuinfocontext.TblRmpiStuInfo.FindAsync(id);

            if (StuPhotoFile != null)
            {

                var memfilePath = Path.Combine(_stuinfowebhostenv.WebRootPath, "studentimg", objStuInfoModel.StuPhoto);
                if (System.IO.File.Exists(memfilePath))
                    System.IO.File.Delete(memfilePath);

                string fileName = Path.GetFileNameWithoutExtension(StuModels.StuPhotoFile.FileName);
                string extension = Path.GetExtension(StuModels.StuPhotoFile.FileName);
                string path = Path.Combine(_stuinfowebhostenv.WebRootPath, "studentimg", fileName + extension);
                StuModels.StuPhoto = fileName + extension;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await StuModels.StuPhotoFile.CopyToAsync(fileStream);
                }
                objStuInfoModel.StuPhoto = StuModels.StuPhoto;
                objStuInfoModel.StuPhotoFile = StuModels.StuPhotoFile;
            }
            objStuInfoModel.StuName = StuModels.StuName;
            objStuInfoModel.StuRoll = StuModels.StuRoll;
            objStuInfoModel.StuReg = StuModels.StuReg;
            objStuInfoModel.StuDept = StuModels.StuDept;
            objStuInfoModel.StuEmail = StuModels.StuEmail;
    
            _dbstuinfocontext.Entry(objStuInfoModel).State = EntityState.Modified;
            _dbstuinfocontext.Update(objStuInfoModel);
            await _dbstuinfocontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return View(tchrmgmntModels);
        }

        // GET: imageController/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var StuInfoMod = await _dbstuinfocontext.TblRmpiStuInfo.FirstOrDefaultAsync(m => m.StuID == id);

            if (StuInfoMod == null)
            {
                return NotFound();
            }
            return View(StuInfoMod);
        }


        // POST: imageController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var MembMod = await _dbstuinfocontext.TblRmpiStuInfo.FindAsync(id);

            var MembfilePath = Path.Combine(_stuinfowebhostenv.WebRootPath, "studentimg", MembMod.StuPhoto);
            if (System.IO.File.Exists(MembfilePath))
                System.IO.File.Delete(MembfilePath);

            _dbstuinfocontext.TblRmpiStuInfo.Remove(MembMod);
            await _dbstuinfocontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
