using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using AlexaAltTran.Data.APIModels;

namespace AlexaAltTran.Test
{
    /// <summary>
    /// Summary description for AlexaAltTranTest
    /// </summary>
    [TestClass]
    public class AlexaAltTranTest
    {
        static HttpClient client = new HttpClient();
        private static string allFuelStations = @"/api/alt-fuel-stations/v1.json?api_key={0}&limit={1}&zip={2}&fuel_type={3}";
        private static string allFuelStationsCount = @"/api/alt-fuel-stations/v1.json?api_key={0}&fuel_type={1}";
        private static string client_id = "*****";
        private static string amazon_id = "amzn1.ask.skill.1139ca56-de78-4375-8b8a-903816f6db7e";
        private static string host = "https://developer.nrel.gov";
        private static string locationBasedPolicies = @"/api/transportation-incentives-laws/v1.json?api_key={0}&limit={1}&jurisdiction={2}&incentive_type={3}";
        private static string idBasedPolicy = @"/api/transportation-incentives-laws/v1/{0}.json?api_key={1}";
        private static string categoryBasedPolicy = @"/api/transportation-incentives-laws/v1/category-list.json?api_key={0}";
        private static string contactBasedPolicy = @"api/transportation-incentives-laws/v1/pocs.json?api_key={0}&jurisdiction={1}";
        private static string allNearbyFuelStationsByState = @"/api/alt-fuel-stations/v1.json?api_key={0}&limit={1}&state={2}&fuel_type={3}";

        #region NearbyStationIntent
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
        static int GetAllStationsCountByFuelType(string type)
        {

            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = string.Format(allFuelStationsCount, client_id, type);


            RootObject allStations = null;
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                allStations = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result);
            }

            return allStations.fuel_stations.Count;


        }

        static CategoryRootObject GetAllCategories()
        {

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


        public PolicyRootObject GetAllPoliciesByLocation(string state, string category)
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


        static ContactRootObject GetContactBasedPolicy(string state)
        {

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



        static RootObject GetAllStationsAsync(string zip, string type)
        {

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
        public AlexaAltTranTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void TestGetAllPoliciesByLocation()
        {
            var policyObject = GetAllPoliciesByLocation("VA", "Rbate");
        }

        [TestMethod]
        public void TestGetAllPolicyById()
        {
            var policyById = GetAllPolicyById("9835");

        }

        [TestMethod]
        public void TestCategory()
        {
            var category = GetAllCategories();
        }

        [TestMethod]

        public void TestContactBased()
        {
            var contact = GetContactBasedPolicy("VA");
        }
            

        [TestMethod]
        public void TestMethod1()
        {
            var rootObject = GetAllStationsAsync("20850", "ELEC");
            var totalCount = GetAllStationsCountByFuelType("ELEC");
        }

        [TestMethod]

        public void GetAddress()
        {
            var address = GetAddress("", "");

        }

        [TestMethod]
        public void TestNearbyStationIntent()
        {
            var test = GetNearbyStationbyState("VA", "ELEC");

        }



        private static string amazonHost = @"https://api.amazonalexa.com";
        private static string amazonAddress = @"/v1/devices/{0}/settings/address/countryAndPostalCode";

        static string GetAddress(string deviceId, string auth)
        {
            string address = "";
            try
            {
                string path = string.Format(amazonAddress, deviceId);
                client.BaseAddress = new Uri(amazonHost);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
                HttpResponseMessage response = client.GetAsync(path).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    address = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(result);
                }

            }
            catch
            {

            }
            return address;
        }
    }
}
