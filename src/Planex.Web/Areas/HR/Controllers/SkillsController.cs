﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
﻿using AutoMapper;
﻿using AutoMapper.QueryableExtensions;
﻿using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
﻿using Planex.Data;
﻿using Planex.Data.Models;
﻿using Planex.Services.Skills;
﻿using Planex.Web.App_Start;
﻿using Planex.Web.Areas.HR.Models;
﻿using Planex.Web.Infrastructure.Mappings;

namespace Planex.Web.Areas.HR.Controllers
{
    public class SkillsController : Controller
    {
        ISkillService skillService = new SkillService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Skills_Read([DataSourceRequest]DataSourceRequest request)
        {
            var skills = skillService.GetAll().To<SkillViewModel>();            
            return Json(skills.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Skills_Create([DataSourceRequest]DataSourceRequest request, SkillViewModel skill)
        {
            if (ModelState.IsValid)
            {
                var entity = new Skill
                {
                    Name = skill.Name
                };

                skillService.Add(entity);
                skill.Id = entity.Id;
            }

            return Json(new[] { skill }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Skills_Update([DataSourceRequest]DataSourceRequest request, SkillViewModel skill)
        {
            if (ModelState.IsValid)
            {
                skillService.UpdateName(skill.Id, skill.Name);                
            }

            return Json(new[] { skill }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Skills_Destroy([DataSourceRequest]DataSourceRequest request, SkillViewModel skill)
        {
            if (ModelState.IsValid)
            {
                skillService.Delete(skill.Id);                
            }

            return Json(new[] { skill }.ToDataSourceResult(request, ModelState));
        }
    }
}
