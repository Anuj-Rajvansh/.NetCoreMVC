using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace CSV_75WAY.Models
{
    public class Building
    {
        [Key]
        [Name("epl_identifier")]
        public string epl_identifier { get; set; }
        [Name("site_name")]
        public string site_name { get; set; }
        [Name("city")]
        public string city { get; set; }
        [Name("province")]
        public string province { get; set; }
        [Name("building_type")]
        public string building_type { get; set; }
        [Name("address")]
        public string address { get; set; }

    }
    public class CsvUserDetailsMapping : ClassMap<Building>
    {
        public CsvUserDetailsMapping()
        {
            Map(m => m.epl_identifier).Name("epl_identifier");
            Map(m => m.site_name).Name("site_name");
            Map(m => m.city).Name("city");
            Map(m => m.province).Name("province");
            Map(m => m.building_type).Name("building_type");
            Map(m => m.address).Name("address");
        }
    }
    //public class CsvUserDetailsMapping : CsvMapping<Building>
    //{
    //    public CsvUserDetailsMapping()
    //        : base()
    //    {
    //        MapProperty(0, x = &gt; x.ID);
    //        MapProperty(1, x = &gt; x.Name);
    //        MapProperty(2, x = &gt; x.City);
    //        MapProperty(3, x = &gt; x.Country);
    //    }
    //}

}
