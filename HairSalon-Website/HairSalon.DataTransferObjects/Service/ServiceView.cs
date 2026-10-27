using HairSalon.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HairSalon.DataTransferObjects.Service
{
    public class ServiceView
    {
      

            public string Name { get; set; } = null!;





            public decimal Price { get; set; }


         
            public string ServicePhotoExampleUrl { get; set; } = null!;



            public string? Details { get; set; }



        }
    
}
