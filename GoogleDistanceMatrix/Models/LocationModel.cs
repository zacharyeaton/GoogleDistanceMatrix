using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GoogleDistanceMatrix.Models
{    
    public class LocationModel
    {
        private string origin;
        private string destination;
        private string mode;
        private List<LocationModel> history;
        private string duration;
        private string distance;

        [Required]
        public string Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        [Required]
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        [Required]
        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public List<LocationModel> History
        {
            get { return history; }
            set { history = value; }
        }

        public string Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public string Distance
        {
            get { return distance; }
            set { distance = value; }
        }        

        public List<SelectListItem> Modes;              

        public LocationModel()
        {
            Modes = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Driving", Value = "driving" },
                new SelectListItem() { Text = "Walking", Value = "walking" },
                new SelectListItem() { Text = "Cycling", Value = "cycling" }
            };      
            
        }       
    }
}