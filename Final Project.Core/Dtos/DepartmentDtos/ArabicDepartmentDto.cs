﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Core.Dtos.DepartmentDtos
{
    public class ArabicDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string ArabicTitle { get; set; }
        public string ArabicDescription { get; set; }
        public int HeadOfDepartmentId { get; set; }
        public string HeadOfDepartmentName { get; set; }


    }
}
