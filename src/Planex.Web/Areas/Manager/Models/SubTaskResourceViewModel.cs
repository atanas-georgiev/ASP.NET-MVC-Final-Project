using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Planex.Data.Models;
using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.Manager.Models
{
    public class SubTaskResourceViewModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string color { get; set; }

        public double value { get; set; }

        public string formatedValue { get; set; }
    }
}