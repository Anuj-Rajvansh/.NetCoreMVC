using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSV_75WAY.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSV_75WAY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVfile : ControllerBase
    {

        private readonly DbContexts _context;

        public CSVfile(DbContexts context)
        {
            _context = context;
        }
        // GET: api/<CSVfile>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CSVfile>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CSVfile>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CSVfile>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CSVfile>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("~/api/uploadcsv")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(fileName);
                if (fileExtension !=".csv" && fileExtension !=".CSV")
                {
                    return BadRequest(new { count = 1, error = "It seems that file is not in format of CSV" });
                }
                else
                {
                    var filePath = Path.GetTempFileName();

                    if (file.Length > 0)
                        using (var stream = new FileStream(filePath, FileMode.Create))
                            await file.CopyToAsync(stream);


                    var data = ReadCSVFile(filePath);
                    int counts = 0;
                    int inserted = 0;
                    int notinserted = 0;
                    foreach (var d in data)
                    {

                        if (d.address == "" || d.building_type == "" || d.city == "" || d.epl_identifier == "" || d.province == "" || d.site_name == "")
                        {
                            notinserted++;
                            continue;
                        }
                        else
                        {
                            
                            _context.Buildings.Add(d);
                            _context.SaveChanges();
                            inserted++;
                        }
                        counts++;
                        if (data.Count == counts)
                        {

                            var myUniqueFileName = "Processed_";

                            string newname = myUniqueFileName + file.FileName;
                            var newFileName = newname;
                            var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Processed", "Processed folder")).Root + $@"\{newFileName}";

                            using (FileStream fs = System.IO.File.Create(filepath))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }

                    }

                    return Ok(new { Executed = counts, Inserted = inserted, NotInserted = notinserted, path = filePath });
                }
                
   
            }
            catch(Exception e)
            {
                return BadRequest(new { count = 1, error = e.InnerException });
            }
            
        }
        public List<Building> ReadCSVFile(string location)
        {
            try
            {
                var building = new Building();
                var reader = new StreamReader(location);
              
                    var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                
                    csv.Configuration.RegisterClassMap<CsvUserDetailsMapping>();
                   // csv.Configuration.MissingFieldFound = null;
                //csv.Configuration.HeaderValidated = null;
                var records = csv.GetRecords<Building>();
                    //building = (Building)records;
                
                return records.ToList();
           
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public IActionResult CsvfileUpload(IFormFile files)
        //{
        //    try
        //    {
        //        //We would always copy the attachments to the folder specified above but for now dump it wherver....
        //        TextReader reader = new StreamReader(files.Name);
        //        var csvReader = new CsvReader(reader);
        //        var records = csvReader.GetRecords<Automobile>();

        //        return Ok();

        //    }
        //    catch(Exception e)
        //    {
        //        return BadRequest();
        //    }

        //}
    }
}
