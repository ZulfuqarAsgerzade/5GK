using EFCore.BulkExtensions;
using Excel.Improter.Extensions;
using Excel.Improter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excel.Improter.Controllers
{
    [Authorize]
    public class ExcelImportController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly DataContext dataContext;

        public ExcelImportController(IWebHostEnvironment webHostEnvironment, DataContext dataContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.dataContext = dataContext;
        }
        public IActionResult Index(string searchText = "", int searchType = 1)
        {
            if (searchType != 1)
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    TempData["ErrorMessage"] = "Zəhmət olmasa axtarış bölməsin doldurun";
                    return RedirectToAction("Index");
                }
            }
            var predicate = GetExpression(searchType, searchText);
            var data = dataContext.ExcelDatas.Where(predicate).ToList();

            return View(data);
        }

        public IActionResult ImportExcel(IFormFile file)
        {
            int failedCount = 0;
            var rootPath = webHostEnvironment.WebRootPath;
            var folder = Path.Combine(rootPath, @"Excels");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var fileName = Guid.NewGuid().ToString().Replace("-", "").ToUpper() + file.FileName;
            if (!fileName.EndsWith(".xlsx") && !fileName.EndsWith(".xls"))
            {
                ViewBag.ErrorMessage = "UnCorrect Excel File";
                return View("Index");
            }
            var fullPath = Path.Combine(folder, fileName);

            using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                file.CopyTo(fs);
            }
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            List<DatabaseModel> list = new List<DatabaseModel>();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(fullPath)))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                for (int i = 8; i <= worksheet.Dimension.Rows; i++)
                {
                    try
                    {
                        var yataginYerleshdiyiErazi = worksheet.Cells[i, 2].Value;
                        var yataginKodu = worksheet.Cells[i, 3].Value;
                        var yataginAdi = worksheet.Cells[i, 4].Value;
                        var saheninKodu = worksheet.Cells[i, 5].Value;
                        var saheninAdi = worksheet.Cells[i, 6].Value;
                        var dmaaNomresi = worksheet.Cells[i, 7].Value;
                        var dmaaQeydiyyatTarixi = worksheet.Cells[i, 8].Value;
                        var dmaaBitmeTarixi = worksheet.Cells[i, 9].Value;
                        var senayeMenimsenilmesi = worksheet.Cells[i, 10].Value;
                        var faydaliQazintiNovu = worksheet.Cells[i, 11].Value;
                        var ehtiyyatinKategoriyasi = worksheet.Cells[i, 12].Value;
                        var balansEhtiyyatlari2019 = worksheet.Cells[i, 13].Value;
                        var hasilat = worksheet.Cells[i, 14].Value;
                        var hasilatItkileri = worksheet.Cells[i, 15].Value;
                        var kesfiyyat = worksheet.Cells[i, 16].Value;
                        var yenidenQiymetlendirme = worksheet.Cells[i, 17].Value;
                        var moteberliyiTesdiqlenmeyenEhtiyyat = worksheet.Cells[i, 18].Value;
                        var serhedlerinDeyishmesi = worksheet.Cells[i, 19].Value;
                        var qaliqEhtiyyatlari2020 = worksheet.Cells[i, 20].Value;
                        var ayrilanSahe = worksheet.Cells[i, 21].Value;
                        var voen = worksheet.Cells[i, 22].Value;
                        var tesdiqEdilmishBalansEhtiyyatCemi = worksheet.Cells[i, 23].Value;
                        var koordinat = worksheet.Cells[i, 24].Value;
                        var mineralXammalBazasininBerpasi = worksheet.Cells[i, 25].Value;
                        var tesdiqOlunmasiBaredeMelumat = worksheet.Cells[i, 26].Value;
                        var serh = worksheet.Cells[i,27].Value;


                        DatabaseModel databaseModel = new DatabaseModel
                        {
                            YataginInzibatiErazisi = yataginYerleshdiyiErazi is null ? "" : yataginYerleshdiyiErazi.ToString(),
                            YataginAdi = Convert.ToString(yataginAdi),
                            YataginKodu = Convert.ToString(yataginKodu),
                            SaheninKodu = Convert.ToString(saheninKodu),
                            SaheninAdi = Convert.ToString(saheninAdi),
                            FaydaliQazintiNovu = faydaliQazintiNovu is null ? "" : faydaliQazintiNovu.ToString(),
                            DMAANomresi = Convert.ToString(dmaaNomresi),
                            DMAABitmeTarix = dmaaBitmeTarixi is null ? new DateTime() : Convert.ToDateTime(dmaaBitmeTarixi),
                            DMAAQeydiyyatTarixi = dmaaQeydiyyatTarixi is null ? new DateTime() : Convert.ToDateTime(dmaaQeydiyyatTarixi),
                            EhtiyyatinKategoryasi = Convert.ToString(ehtiyyatinKategoriyasi),
                            SenayeMenimsenilmesiSeviyyesi = Convert.ToString(senayeMenimsenilmesi),
                            BalansEhtiyyatlari2019 = Convert.ToString(balansEhtiyyatlari2019),
                            AyrilanSahe = Convert.ToString(ayrilanSahe),
                            VOEN = Convert.ToString(voen),
                            Hasilat = Convert.ToString(hasilat),
                            Koordinat = Convert.ToString(koordinat),
                            HasilatZamaniItkiler = Convert.ToString(hasilatItkileri),
                            Kesfiyyat = kesfiyyat is null ? "" : kesfiyyat.ToString(),
                            YenidenQiymetlendirme = yenidenQiymetlendirme is null ? "" : yenidenQiymetlendirme.ToString(),
                            MoteberliyiTesdiqlenmeyen = moteberliyiTesdiqlenmeyenEhtiyyat?.ToString(),
                            SerhedlerinDeyishmesiVeDiger = Convert.ToString(serhedlerinDeyishmesi),
                            QaliqEhtiyyatlari2020 = Convert.ToString(qaliqEhtiyyatlari2020),
                            TesdiqEdilmishBalansCemi = Convert.ToString(tesdiqEdilmishBalansEhtiyyatCemi),
                            MineralXammalBazasiBerpasi = Convert.ToString(mineralXammalBazasininBerpasi),
                            TesdiqOlunmaseBarede = Convert.ToString(tesdiqOlunmasiBaredeMelumat),
                            Serh = Convert.ToString(serh)
                        };
                        list.Add(databaseModel);
                    }
                    catch (Exception)
                    {
                        failedCount++;
                        continue;
                    }
                }
                dataContext.ExcelDatas.BatchDelete();
                dataContext.BulkInsert(list);
            }
            return RedirectToAction("Index");
        }
        private Expression<Func<DatabaseModel, bool>> GetExpression(int key, string searchFilter)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (key == 6 || key == 7 || key == 8)
            {
                startDate = Convert.ToDateTime(searchFilter.Split('-')[0]);
                endDate = Convert.ToDateTime(searchFilter.Split('-')[1]);
            }
            Dictionary<int, Expression<Func<DatabaseModel, bool>>> keyValuePairs = new Dictionary<int, Expression<Func<DatabaseModel, bool>>>()
            {
                { 1 , i=>1==1 },
                { 2 , i=>i.VOEN.StartsWith(searchFilter)},
                { 3 , i=>i.YataginAdi.StartsWith(searchFilter)},
                { 4 , i=>i.SaheninAdi.StartsWith(searchFilter)},
                { 5 , i=>i.YataginInzibatiErazisi.StartsWith(searchFilter)},
                { 6 , i=>startDate<i.DMAAQeydiyyatTarixi && i.DMAAQeydiyyatTarixi<=endDate},
                { 7 , i=>startDate<= i.DMAABitmeTarix && i.DMAABitmeTarix<=endDate }
            };
            return keyValuePairs[key];
        }


        public IActionResult FilterData(FilterDatasInput input)
        {
            return Ok();
        }

        public IActionResult Edit(int id)
        {
            var data = dataContext.ExcelDatas.FirstOrDefault(i => i.Id == id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DatabaseModel model,IFormFile file)
        {
            if (file != null)
            {
                var fileName = Guid.NewGuid().ToString() + file.FileName;
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);
                using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    await file.CopyToAsync(fs);
                }
                model.Koordinat = fileName;
            }
            dataContext.ExcelDatas.Update(model);
            await dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View(new DatabaseModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(DatabaseModel model, IFormFile file)
        {
            if (file != null)
            {
                var fileName = Guid.NewGuid().ToString() + file.FileName;
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);
                using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    await file.CopyToAsync(fs);
                }
                model.Koordinat = fileName;
            }
            
            dataContext.ExcelDatas.Add(model);
            await dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
