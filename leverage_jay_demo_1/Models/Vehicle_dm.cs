using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;


using System.ComponentModel.DataAnnotations; //this is for data validation


namespace leverage_jay_demo_1.Models
{
    //here is the data model
    
    public class Vehicle_dm
    {

        //the usual ID stuff for the db. I will leave it here as it is
        public int ID { get; set; }

        //here are the items I will need

        //vehicle registration number - going with string
        //this is required
        //this has to be unique

        //the required attribute will ensure that the view will check if this entry is entered by user
        //this will also make the DB enable the NOT NULL attribute
        //ASP will take care of both the DB NOT NULL settings and also the user side validation
        [Required]
        public string Registration_Number { get; set; }

        //manufacturer - going with string 
        //this is required

        [Required]
        public string Manufacturer { get; set; }

        //model of the vehicle - going with string
        //this is required

        [Required]
        public string Vehicle_Model { get; set; }

        //covered - going with boolean
        //this is NOT mandatory
        
        public bool Covered { get; set; }

        //rcbook - this will be an image path - for now keeping it string
        //this is required

        [Required]
        public string Image_Path { get; set; }
        //insurance expiry date - going with date type 
        //this is required

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }

    //here is the data base context
    public class Vehicle_dmDBContext : DbContext
    {
        public DbSet<Vehicle_dm> Vehicle_dms { get; set; }
    }
}