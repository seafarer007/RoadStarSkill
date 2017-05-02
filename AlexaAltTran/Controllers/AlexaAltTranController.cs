using AlexaAltTran.Data.APIModels;
using AlexaAltTran.Data.Models;
using AlexaAltTran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace AlexaAltTran.Controllers
{
    
    public class AlexaAltTranController : ApiController
    {
        static HttpClient client = new HttpClient();
        private static string host = "https://developer.nrel.gov";
        private string applicationId = "amzn1.ask.skill.d0b88736-a309-44ed-b003-3c7465481774";

        #region Fuel Stations
        private static string allFuelStations = @"/api/alt-fuel-stations/v1.json?api_key={0}&limit={1}&fuel_type={2}";
        private static string allNearbyFuelStations = @"/api/alt-fuel-stations/v1.json?api_key={0}&limit={1}&zip={2}&fuel_type={3}&city={4}";

        private static string allNearbyFuelStationsNoCity = @"/api/alt-fuel-stations/v1.json?api_key={0}&limit={1}&zip={2}&fuel_type={3}";

        private static string allNearbyFuelStationsByState = @"/api/alt-fuel-stations/v1.json?api_key={0}&limit={1}&state={2}&fuel_type={3}";

        private static string allFuelStationsCount = @"/api/alt-fuel-stations/v1.json?api_key={0}&fuel_type={1}";
        private static string allNearbyFuelStationsCount = @"/api/alt-fuel-stations/v1.json?api_key={0}&state={1}&fuel_type={2}";
        #endregion

        private static string client_id = "NJiRLfpeHsI5CPHVFq4W10WO3IZYGssuxTqeSw2V";

        #region Policy and Incentives

        private static string locationBasedPolicies = @"/api/transportation-incentives-laws/v1.json?api_key={0}&limit={1}&jurisdiction={2}&incentive_type={3}";
        private static string idBasedPolicy = @"/api/transportation-incentives-laws/v1/{0}.json?api_key={1}";
        private static string categoryBasedPolicy = @"/api/transportation-incentives-laws/v1/category-list.json?api_key={0}";
        private static string contactBasedPolicy = @"api/transportation-incentives-laws/v1/pocs.json?api_key={0}&jurisdiction={1}";

        private static string amazonHost = @"https://api.amazonalexa.com";
        private static string amazonAddress = @"/v1/devices/{0}/settings/address/countryAndPostalCode";
        private static string amazonFullAddress = @"/v1/devices/{0}/settings/address";
        #endregion

        #region GetAddress
        static PostalAddress GetAddress(string deviceId, string auth)
        {
            client = new HttpClient();
            PostalAddress address = new PostalAddress();
            try
            {
                string path = string.Format(amazonFullAddress, deviceId);
                client.BaseAddress = new Uri(amazonHost);
                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
                HttpResponseMessage response = client.GetAsync(path).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    address = Newtonsoft.Json.JsonConvert.DeserializeObject<PostalAddress>(result);
                }

            }
            catch
            {
                return null;
            }
            return address;
            }

        #endregion

        #region Policy and Incentive calls


        public static PolicyRootObject GetAllPoliciesByLocation(string state, string category)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            PolicyRootObject allPolicies = null;


            string path = string.Format(locationBasedPolicies, client_id, 1000, state, category);


            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allPolicies = Newtonsoft.Json.JsonConvert.DeserializeObject<PolicyRootObject>(result);
            }


            return allPolicies;
        }

        public static SinglePolicyRootObject GetAllPolicyById(string id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            SinglePolicyRootObject allPolicies = null;

            string path = string.Format(idBasedPolicy, id, client_id);


            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allPolicies = Newtonsoft.Json.JsonConvert.DeserializeObject<SinglePolicyRootObject>(result);
            }


            return allPolicies;
        }

        static CategoryRootObject GetAllCategories()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Random random = new Random();
            int randomNumber = random.Next(0, 100);


            CategoryRootObject allPolicies = null;

            string path = string.Format(categoryBasedPolicy, client_id);


            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allPolicies = Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryRootObject>(result);


            }


            return allPolicies;
        }


        static ContactRootObject GetContactBasedPolicy(string state)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

     
            ContactRootObject allContactPolicies = null;

            string path = string.Format(contactBasedPolicy, client_id, state);


            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allContactPolicies = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactRootObject>(result);
            }


            return allContactPolicies;
        }




        #endregion


        #region Alternative Fuel Locations

        static int GetAllNearbyStationsCountByFuelType(string state, string type, string city)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string path = string.Format(allNearbyFuelStationsCount, client_id, state, type, city);

            RootObject allNearbyStations = null;


            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allNearbyStations = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result);
            }

            return allNearbyStations.fuel_stations.Count;
        }



        static int GetAllStationsCountByFuelType(string type)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = string.Format(allFuelStationsCount, client_id,type);


            RootObject allStations = null;
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allStations = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result);
            }

           return allStations.fuel_stations.Count;


        }

        static RootObject GetAllStationsAsync(string zip, string type)
        {

            client = new HttpClient();
            //// New code:
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = string.Format(allFuelStations, client_id, 10, zip, type);


            RootObject allStations = null;
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allStations = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result);
            }


        
            return allStations;
        }


        static RootObject GetNearbyStationsAsync(string zip, string type, string city)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = string.Empty;

            if (string.IsNullOrEmpty(city))
            {
                 path = string.Format(allNearbyFuelStations, client_id, 10, zip, type, city);
            }
            else
            {
                path = string.Format(allNearbyFuelStationsNoCity, client_id, 10, zip, type);
            }


            RootObject allStations = null;
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allStations = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result);
            }



            return allStations;
        }


        #region GetNearbyStationbyState
        static RootObject GetNearbyStationbyState(string state, string type)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = string.Format(allNearbyFuelStationsByState, client_id, 10, state, type);


            RootObject allStations = null;
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allStations = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result);
            }



            return allStations;
        }

        #endregion

        #endregion

        #region Road Star Commands
        [HttpPost, Route("API/ALEXA/AlexaAltTran")]
        public dynamic AltTran(AlexaRequest alexaRequest)
        {
            //Validate application Id
            if (alexaRequest.Session.Application.ApplicationId != applicationId)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }

            var totalSeconds = (DateTime.Now - alexaRequest.Request.Timestamp).TotalSeconds;
            if(totalSeconds <=0 || totalSeconds > 150)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
               

            try
            {
                AlexaResponse response = null;
         
                var request = new Requests().Create(new AlexaAltTran.Data.Models.Request
                {
                    MemberId = (alexaRequest.Session.Attributes == null) ? 0 : alexaRequest.Session.Attributes.MemberId,
                    Timestamp = alexaRequest.Request.Timestamp,
                    Intent = (alexaRequest.Request.Intent == null) ? "" : alexaRequest.Request.Intent.Name,
                    AppId = alexaRequest.Session.Application.ApplicationId,
                    RequestId = alexaRequest.Request.RequestId,
                    SessionId = alexaRequest.Session.SessionId,
                    UserId = alexaRequest.Session.User.UserId,
                    IsNew = alexaRequest.Session.New,
                    Version = alexaRequest.Version,
                    Type = alexaRequest.Request.Type,
                    Reason = alexaRequest.Request.Reason,
                    consentToken = alexaRequest.Session.User != null ? alexaRequest.Session.User.Permission.consentToken : "0",
                    deviceId = alexaRequest.context != null ? alexaRequest.context.System.device.deviceId : "0" ,
                   SlotsList = alexaRequest.Request.Intent.GetSlots(),
                    DateCreated = DateTime.UtcNow
                });

                switch (request.Type)
                {
                    case "LaunchRequest":
                        response = LaunchRequestHandler(request);
                        break;
                    case "IntentRequest":
                        response = IntentRequestHandler(request);
                        break;
                    case "SessionEndedRequest":
                        response = SessionEndedRequestHandler(request);
                        break;
                }

                return response;
            }
            catch (Exception ex)
            {

                return new
                {
                    version = "1.0",
                    sessionAttributes = new { },
                    response = new

                    {

                        outputSpeech = new
                        {

                            type = "PlainText",
                            text = ex.Message
                        },
                        Card = new
                        {

                            type = "Simple",
                            title = "Road Star Error Message.",
                            content = "Road Star is currently having voice issues, please report this issue on campforcoders.com/feedback"
                        },
                        shouldEndSession = true
                    }

                };

            }

        }
        #endregion
        #region IntentRequestHandler

        private AlexaResponse IntentRequestHandler(Request request)
        {
            AlexaResponse response = null;

            switch (request.Intent)
            {
                case "HelloIntent":
                    response = HelloIntentHandler(request);
                    break;
                case "NearbyStationIntent":
                    response = NeareststationIntentHandler(request);
                    break;

                case "NearbyStationByZipIntent":
                    response = NearbyStationByZipIntentHandler(request);
                    break;

                case "NearbyStationNearMeIntent":
                    response = NearbyStationNearMeIntentHandler(request);
                    break;


                case "NearbyStationCountIntent":
                    response = NearestStationCountIntentHandler(request);
                    break;

                case "AllStationCountIntent":
                    response = AllStationCountIntentHandler(request);
                    break;

                case "CategoryBasedPolicyIntent":
                    response = CategoryBasedPolicyIntentHandler(request);
                    break;
                case "FuelBasedIntent":
                    response = FuelBasedIntentHandler(request);
                    break;
                case "AllPoliciesByLocationIntent":
                    response = AllPoliciesByLocationIntentHandler(request);
                    break;
                case "IDBasedPolicyIntent":
                    response = IDBasedPolicyIntentHandler(request);
                    break;

                case "ContactBasedPolicyIntent":
                    response = ContactBasedPolicyIntentHandler(request);
                    break;

                case "AMAZON.CancelIntent":
                    response = CancelOrStopIntentHandler(request);
                    break;
                case "AMAZON.StopIntent":
                    response = StopIntentHandler(request);
                    break;
                case "AMAZON.HelpIntent":
                    response = HelpIntent(request);
                    break;
              

            }

            return response;
        }

        #endregion
        #region Policy Handlers

        public AlexaResponse IDBasedPolicyIntentHandler(Request request)
        {

            string speech = "";
            string id = "";

            if (request.SlotsList.Any())
            {

                id = request.SlotsList.FirstOrDefault(s => s.Key == "id").Value;


            }

            try
            {
                string _policy = string.Empty;

                SinglePolicyRootObject policy = GetAllPolicyById(id);
                if (policy != null)
                {
                    
                        _policy = _policy + "<prosody rate=\"slow\">" + policy.result.text.Replace("<","").Replace(">","") + "</prosody>";

                           speech = _policy;

                }
                else
                {
                    speech = "Could not locate information, please try with a different ID number.";
                }
               
            }
            catch 
            {
                speech = "Road Star is currently experience voice issue, please try back shortly.";
            }

   
            var response = new AlexaResponse(speech, false, AddSSML(speech));

            speech = "You can find Alternative Fuel policy contacts in each state by saying, Get me All Contacts for Virginia";
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);

            return response;
        }

        private string ErrorMessage()
        {
            return "Road Star is having voice issue, please try back again shortly.";
        }

        public AlexaResponse AllPoliciesByLocationIntentHandler(Request request)
        {

            client = new HttpClient();

            string speech = "";
            string state_type = "";
            string categories = "";
            string state = "";
            string cat = "";


            if (request.SlotsList.Any())
            {

                state = request.SlotsList.FirstOrDefault(s => s.Key == "state_type").Value;
                cat = request.SlotsList.FirstOrDefault(s => s.Key == "categories").Value;


                state_type = request.SlotsList.FirstOrDefault(s => s.Key == "state_type").Value;
                categories = request.SlotsList.FirstOrDefault(s => s.Key == "categories").Value;           

                state_type = Helper.StateConversion.ConvertState(state_type);
               


            }

            try
            {
                string _contacts = string.Empty;
                int _total = 0;
                PolicyRootObject contacts = GetAllPoliciesByLocation(state_type, Helper.StateConversion.ConvertCategory(categories));
                if (contacts != null)
                {
                    foreach (var con in contacts.result)
                    {
                        _contacts = _contacts + " Policy Number <say-as interpret-as=\"digits\">" + con.id + "</say-as>" + " The Policy is " + con.title + " ";
                    }

                    _total = contacts.result.Count();
                    if (_total > 0)
                    {
                        speech = "<prosody rate=\"slow\">" + "There are a total of " + _total + " Policies. You can find detailed information on each policy by saying Give me details on policy number. Here are titles of each policy. " + _contacts + "</prosody>";
                    }
                    else
                    {
                        speech = "Unable to find any policies for " + cat + " in " + state + " Please try another Category";
                    }
                }
                else
                {
                    speech = "Unable to find any policies for " + cat + " in " + state + " Please try another Category";
                }
            }
            catch 
            {
                speech = ErrorMessage();
            }

      
            var response = new AlexaResponse(speech, false, AddSSML(speech));


            speech = "You can say Find all Categories to get a listing of all Categories";
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);
            return response;

        }

        public AlexaResponse ContactBasedPolicyIntentHandler(Request request)
        {
            string speech = "";
            string state_type = "";
            string state = "";


            if (request.SlotsList.Any())
            {
                state = request.SlotsList.FirstOrDefault(s => s.Key == "state_type").Value;
                state_type = request.SlotsList.FirstOrDefault(s => s.Key == "state_type").Value;

                state_type = Helper.StateConversion.ConvertState(state_type);
               
            }

            try
            {
                string _contacts = string.Empty;
                int _total = 0;
                ContactRootObject contacts = GetContactBasedPolicy(state_type);
                if (contacts != null)
                {
                    int counter = 1;

                    foreach (var con in contacts.result)
                    {
                        _contacts = _contacts +  " Contact " + counter  +  con.name + " Phone Number is " + con.telephone + "<break time=\"1s\"/> ";
                        counter = counter + 1;
                    }

                    _total = contacts.result.Count();
                }
                if (_total > 0)
                {


                    speech = "<prosody rate=\"slow\">There are a total of " + _total + " Contacts in " + state + "Here they are " + _contacts + "</prosody>";
                }
                else
                {
                    speech = "Unable to find any contacts, please try another state.";
                }
            }
            catch 
            {
                speech = ErrorMessage();
            }

      
         
            var response = new AlexaResponse(speech, false, AddSSML(speech));
            response.Response.Reprompt.OutputSpeech.Type = "SSML";  
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);
            return response;

        }

        #region FuelBasedIntentHandler

        public AlexaResponse FuelBasedIntentHandler(Request request)
        {
            string speech = "";
            try
            {
                string _categories = string.Empty;
               
                speech = "<prosody rate=\"slow\">The Fuel Types are Electric, Hybrid, Ethanol, Hydrogen,LPG,CNG and LNG</prosody><break time=\"1s\"/>";
            }
            catch
            {
                speech = speech = ErrorMessage();
            }

            var response = new AlexaResponse(speech, false, AddSSML(speech));

            speech = "You can find policies for each category and state by saying, Find all Tax policies in Virginia";
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);
            return response;
        }

        #endregion


        #endregion

        #region CategoryBasedPolicyIntentHandler
        public AlexaResponse CategoryBasedPolicyIntentHandler(Request request)
        {
            string speech = "";
            try
            {
                string _categories = string.Empty;

                speech = "<prosody rate=\"slow\">The Policy Categories are Tax, Grants, Rebate, Registration, Loans,Exemptions and Other</prosody><break time=\"1s\"/>";
            }
            catch
            {
                speech = speech = ErrorMessage();
            }

            var response = new AlexaResponse(speech, false, AddSSML(speech));

            speech = "You can say, Find all Fuel Types and find all Fuel Types";
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);
            return response;
        }
        #endregion

        #region NearbyStationByZipIntentHandler

        public AlexaResponse NearbyStationByZipIntentHandler(Request request)
        {

            string fuelType = string.Empty;
            string type = string.Empty;
            string zip = string.Empty;
          
            string _list = string.Empty;

            if (request.SlotsList.Any())
            {

                type = request.SlotsList.FirstOrDefault(s => s.Key == "fuel_type").Value;
                zip = request.SlotsList.FirstOrDefault(s => s.Key == "zipcode").Value;

                fuelType = Helper.StateConversion.ConvertFuelType(fuelType);
                
            }

            var allStations = GetNearbyStationsAsync(zip, fuelType,"");
            foreach (var station in allStations.fuel_stations)
            {
                _list = _list + "<say-as interpret-as=\"address\">" + station.street_address + "</say-as>" + " in " + station.city + " ";
            }
            if (allStations.fuel_stations.Count == 0)
            {
                _list = "Could not find any  " + type + " stations near " + zip;
            }
            var speech = "<prosody rate=\"slow\">" +  _list +"</prosody>";
            var response = new AlexaResponse(speech, false, AddSSML(speech));

            speech = "Would you like to see " + type + " stations near you? You can say, Find " + type + " station near me.";
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);

            return response;
        }
        #endregion


        #region NearestStationIntentHandler

        public AlexaResponse NeareststationIntentHandler(Request request)
        {

            string fuelType = string.Empty;
            string stateType = string.Empty;
            string state = string.Empty;
            string type = string.Empty;
            string _list = string.Empty;

            if (request.SlotsList.Any())
            {

                type = request.SlotsList.FirstOrDefault(s => s.Key == "fuel_type").Value;
                state = request.SlotsList.FirstOrDefault(s => s.Key == "state_type").Value;

                fuelType = Helper.StateConversion.ConvertFuelType(type);
                stateType = Helper.StateConversion.ConvertState(state);
            }

            var allStations = GetNearbyStationbyState(stateType, fuelType);

           
            foreach (var station in allStations.fuel_stations)
            {
                _list = _list + "<say-as interpret-as=\"address\">" + station.street_address + "</say-as>" + " in " + station.city + "<break time=\"1s\"/> ";
            }

            if(allStations.fuel_stations.Count == 0)
            {
                _list = "Could not find any top ten " + type + " stations in " + state;
            }

            var speech = "<prosody rate=\"slow\">Here is a sample of ten " + type + " stations in " + state + " " + _list + "</prosody>";

            var response = new AlexaResponse(speech, false, AddSSML(speech));

            //Reprompt speech
            speech = "Would you like to see " + type + " stations near you? You can say, Find " + type +" station near me.";
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);
            return response;
        }

        #endregion

        #region AllStationCountIntentHandler

        public AlexaResponse AllStationCountIntentHandler(Request request)
        {
            string fuelType = string.Empty;
            string Type = string.Empty;

            if (request.SlotsList.Any())
            {

                fuelType = request.SlotsList.FirstOrDefault(s => s.Key == "fuel_type").Value;

                Type = Helper.StateConversion.ConvertFuelType(fuelType);
            }



            var speech = "There is a total of <say-as interpret-as=\"cardinal\">" + GetAllStationsCountByFuelType(Type) + "</say-as>" + fuelType + " alternative fuel stations in the US";
            var response = new AlexaResponse(speech, false, AddSSML(speech));
            //Reprompt 
            speech = "Would you like to see ELectric stations near you? You can say, Find Electric station near me.";
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);
            return response;

        }

        #endregion



        #region NearestStationCountIntentHandler

        public AlexaResponse NearestStationCountIntentHandler(Request request)
        {
            string fuelType = string.Empty;
            string Type = string.Empty;
            string state_type = string.Empty;
            string state = string.Empty;
            string speech = string.Empty;

            if (request.SlotsList.Any())
            {

                fuelType = request.SlotsList.FirstOrDefault(s => s.Key == "fuel_type").Value;
                state = request.SlotsList.FirstOrDefault(s => s.Key == "state_type").Value;
                state_type = Helper.StateConversion.ConvertState(state);
                Type = Helper.StateConversion.ConvertFuelType(fuelType);
            }
            var totalCount = GetAllNearbyStationsCountByFuelType(state_type, Type, "");

            if(totalCount > 0)
            {
                speech = "There is a total of  <say-as interpret-as=\"cardinal\">" + totalCount + "</say-as>" + fuelType + " stations in " + state;
            }
            else
            {
                speech = "There are no " + fuelType + " stations in " + state;
            }

            var response = new AlexaResponse(speech, false, AddSSML(speech));

            

            speech = "Would you like to see how many " + fuelType + " stations are in the US? " + " You can say, How many " + fuelType + " stations are in the US";
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);


            response.Response.Reprompt.OutputSpeech.Text = speech;
            return response;

        }

        #endregion


        #region NearbyStationNearMeIntentHandler


        public AlexaResponse NearbyStationNearMeIntentHandler(Request request)
        {
            string zip = string.Empty;
            string type = string.Empty;
            string fuel_type = string.Empty;
            string address = string.Empty;
            PostalAddress location = new PostalAddress();
            string speech = string.Empty; 
             
            if (request.SlotsList.Any())
            {

                fuel_type = request.SlotsList.FirstOrDefault(s => s.Key == "fuel_type").Value;

                type = Helper.StateConversion.ConvertFuelType(fuel_type);
            }


            if (request.deviceId != "0" && request.consentToken != "0")
            {
                location = GetAddress(request.deviceId, request.consentToken);

                var stations = GetNearbyStationsAsync(location.postalCode, type, location.city);

                foreach (var station in stations.fuel_stations)
                {
                    address = station.street_address + " in " + station.city;
                    break;
                }

                if (stations.fuel_stations.Count > 0)
                {
                    speech = "Your current zipcode is " + location.postalCode + " We have found a " + fuel_type + " station at " + "<prosody rate=\"medium\">" + "<say-as interpret-as=\"address\">" + address + "</say-as>" + "</prosody>";
                }
                else
                {
                    speech = "We are unable to find any " + fuel_type + " Fuel Stations near You.";
                }

            }
            else
            {
                speech = "Please enable Your device address setting in your Road Star skill.";
            }



         
            var response = new AlexaResponse(speech, false, AddSSML(speech));
            speech = "Would you like to see " + fuel_type + " stations near your zipcode? You can say, Find " + fuel_type + " stations near <say-as interpret-as=\"digits\">20850.</say-as>";
            response.Response.Reprompt.OutputSpeech.Text = speech;
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);
            return response;
        }
        #endregion

        #region HelloIntent
        private AlexaResponse HelloIntentHandler(Request request)
        {


                var speech = "Hi and Welcome to Road Star, your guide to Alternative Transportation information and policy. Please issue your voice commands or say Help to get a list.";
                var response = new AlexaResponse(speech, false);
                response.Response.OutputSpeech.Ssml = AddSSML(speech);
                response.Response.Reprompt.OutputSpeech.Text = speech;
                return response;
            
        }

        #endregion

        #region SessionEndRequest
        private AlexaResponse SessionEndedRequestHandler(Request request)
        {
            return null;
        }

        #endregion


        #region Help or Cancel Intent

        private AlexaResponse HelpIntent(Request request)
        {
            var speech = "<prosody rate=\"slow\">You can say, Find All Categories or Find All Fuel Types or Find Nearest Electric stations in Virginia or Find Nearest Hybrid Stations or How many electric stations in Virginia</prosody>";
            var response = new AlexaResponse(speech, false, AddSSML(speech));
         
            response.Response.Reprompt.OutputSpeech.Text = "<prosody rate=\"slow\">You can say, Find All Categories or Find All Fuel Types or Find Nearest Electric stations in Virginia or Find Nearest Hybrid Stations or How many electric stations in Virginia</prosody>";
            response.Response.Reprompt.OutputSpeech.Ssml = AddSSML(speech);
            return response;
        }


        private AlexaResponse CancelOrStopIntentHandler(Request request)
        {
            var speech = "Operation Cancelled. You can say Help or continue to issue commands to Road Star.";
            var response = new AlexaResponse(speech, false, AddSSML(speech));
            return response;
        }

        private AlexaResponse StopIntentHandler(Request request)
        {
            var speech = "Thank You for visiting Road Star. Come back soon";
            var response = new AlexaResponse(speech, true, AddSSML(speech));          
            return response;
           

        }

        #endregion

        #region LaunchRequest

        private AlexaResponse LaunchRequestHandler(Request request)
        {
            
            string speech = string.Empty; 

            AlexaResponse response = new AlexaResponse();

            speech = "Hi and Welcome to Road Star, your guide to Alternative Transportation information and policy. Please issue your voice commands or say Help to get a list.";
         
            response = new AlexaResponse(speech);
            response.Response.OutputSpeech.Type = "SSML";
            response.Response.OutputSpeech.Ssml = AddSSML(speech);
            response.Response.OutputSpeech.Text = "";
            response.Session.MemberId = request.MemberId;
            response.Response.Card.Title = "Alternative Transportation";
            response.Response.Card.Content = speech;
            response.Response.Reprompt.OutputSpeech.Text = "Hi and Welcome to Road Star, your guide to Alternative Transportation information and policy. Please issue your voice commands or say Help to get a list.";
            response.Response.ShouldEndSession = false;

            return response;

        }

        private string AddSSML(string text)
        {

            string _text = string.Empty;

            _text = "<speak>" + text + "</speak>";

            return _text;

        }
        }

        #endregion

    }

