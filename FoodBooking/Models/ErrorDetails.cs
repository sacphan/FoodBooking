﻿using Newtonsoft.Json;

namespace FoodBooking.Models
{
    /// <summary>
    /// Response for production env in case of error
    /// </summary>
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = "";

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
