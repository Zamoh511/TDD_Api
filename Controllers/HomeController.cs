﻿using FoursqureService.ServiceLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
//using System.Web.Http;
//using System.Web.Http.Cors;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Cors;
using Foursquare.Model;
using TDD_Api.DAL;
using Foursquare.Response;

namespace TDD_Api.Controllers
{
    [EnableCors()]
    public class HomeController : ApiController
    {
        LandmarkService _landmarkService;
        DAL_Logic _Dal = new DAL_Logic();
        public ActionResult Index()
        {
            //ViewBag.Title = "Home Page";

            return null;
        }
        
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetImageAsync(string location)
        {
           

            var responceContent = _landmarkService.SearchImageAsync(location);
            var jObject = JsonConvert.SerializeObject(responceContent);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetLoationDetailsAsync(FoursquareLocation fLocation)
        {         

            var responceContent = _landmarkService.SearchLandmarksAsync(fLocation);
            var jObject = JsonConvert.SerializeObject(responceContent);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetLocationsListAsync(FoursquareLocation fLocation)
        {

            var responceContent = _landmarkService.SearchLocationsAsync(fLocation);
            var jObject = JsonConvert.SerializeObject(responceContent);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

        public void SaveImageDetails(FoursquareLocation foursquareLocation)
        {
            if(getExistingImage(foursquareLocation) == null)
            {
                _Dal.ExecuteStoredProcedure("SaveImageData", foursquareLocation);
            }
           
        }
        public async Task<FoursquareResponse<VenueSearch>> getExistingImage(FoursquareLocation foursquareLocation)
        {
         
            return _Dal.getProcedureData("RetrieveData");
        }
    }
}
