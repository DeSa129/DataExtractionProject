using System;
using System.Globalization;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace DataExtractionProject
{
    class Program
    {
        public static List<FareFlight> fareFlightList = new List<FareFlight>();
        static void Main(string[] args)
        {
            // nustatymai reikalingi sugeruoti url, pavyko padaryti projekta dinamiska, gali skristi ne tik vienas ir daugiau zmoniu duomenis isgaus vistiek teisingai
            var departureDayFromToday = 10;
            var returnDaysFromDeparture = 7;
            var numberOfAdults = 1;

            var webHtml = getWebHtml(GeneratedUrl(departureDayFromToday, returnDaysFromDeparture, numberOfAdults));
            HtmlNodeCollection outboundFlights = webHtml.DocumentNode.SelectNodes("/html/body/div[1]/div/section/div[3]/form/div[1]/div[2]/div");
            HtmlNodeCollection inbounddFlights = webHtml.DocumentNode.SelectNodes("/html/body/div[1]/div/section/div[3]/form/div[2]/div[2]/div");
            var outboundFlightsList = GetFlights(outboundFlights, numberOfAdults);
            var inboundFlightsList = GetFlights(inbounddFlights, numberOfAdults);
            var fareFlightsList = GetFareFlight(outboundFlightsList, inboundFlightsList);

            departureDayFromToday = 20;
            webHtml = getWebHtml(GeneratedUrl(departureDayFromToday, returnDaysFromDeparture, numberOfAdults));
            outboundFlights = webHtml.DocumentNode.SelectNodes("/html/body/div[1]/div/section/div[3]/form/div[1]/div[2]/div");
            inbounddFlights = webHtml.DocumentNode.SelectNodes("/html/body/div[1]/div/section/div[3]/form/div[2]/div[2]/div");
            outboundFlightsList = GetFlights(outboundFlights, numberOfAdults);
            inboundFlightsList = GetFlights(inbounddFlights, numberOfAdults);
            fareFlightsList = GetFareFlight(outboundFlightsList, inboundFlightsList);

            Export(fareFlightsList);
        }
        static string GeneratedUrl(int departureDaysFromToday, int ReturnDaysFromDeparture, int numberOFAdults)
        {
            var departureDateString = DateTime.Today.AddDays(departureDaysFromToday).ToString("ddd'%'2C+dd+MMM+yyyy", CultureInfo.GetCultureInfo("en-US")); ;
            var returnDateString = DateTime.Today.AddDays(ReturnDaysFromDeparture + departureDaysFromToday).ToString("ddd'%'2C+dd+MMM+yyyy", CultureInfo.GetCultureInfo("en-US"));
            string url = "https://www.fly540.com/flights/nairobi-to-mombasa?isoneway=0&currency=KES&depairportcode=NBO&arrvairportcode=MBA&date_from=Mon%2C+" + departureDateString + "&date_to=" + returnDateString + "&adult_no=" + numberOFAdults + "&children_no=0&infant_no=0&searchFlight=&change_flight=";
            return url;
        }
        static List<FareFlight> GetFareFlight(List<Flight> outboundFlights, List<Flight> inboundFlights)
        {


            foreach (var outboundFlight in outboundFlights)
            {
                foreach (var inboundFlight in inboundFlights)
                {
                    var s = ";";
                    var f = new FareFlight
                    {
                        outbound_departure = outboundFlight.DepartureGeoCode + s + outboundFlight.ArrivalGeoCode + s +
                        outboundFlight.DepartureTime,
                        outbound_arrival = outboundFlight.DepartureGeoCode + s + outboundFlight.ArrivalGeoCode + s +
                        outboundFlight.ArrivalTime,
                        inbound_departure = inboundFlight.DepartureGeoCode + s + inboundFlight.ArrivalGeoCode + s +
                        inboundFlight.DepartureTime,
                        inbound_arrival = inboundFlight.DepartureGeoCode + s + inboundFlight.ArrivalGeoCode + s +
                        inboundFlight.ArrivalTime,
                        total_price = float.Parse(outboundFlight.Price) + float.Parse(inboundFlight.Price) + ";" + outboundFlight.Currency,
                        total_taxes = outboundFlight.Taxes + inboundFlight.Taxes
                    };
                    fareFlightList.Add(f);
                }
            }
            return fareFlightList;
        }
        static List<Flight> GetFlights(HtmlNodeCollection htmlNodes, int numberOfAdults)
        {
            var taxesPerPerson = 600;
            var FlightsList = new List<Flight>();
            string departureAirportGeocodeXpath = ".//td[@class='fdetails'][1]/span[@class='flfrom'][1]/text()[2]";
            string arrivalAirportGeocodeXpath = ".//td[@class='fdetails'][2]/span[@class='flfrom'][1]/text()[2]";
            string departureTimeXpath = ".//td[@class='fdetails'][1]/span[@class='fltime ftop'][1]/text()[1]";
            string arrivalTimeXpath = ".//td[@class='fdetails'][2]/span[@class='fltime ftop'][1]/text()[1]";
            string departureDateXpath = ".//td[@class='fdetails'][1]/span[@class='fldate'][1]/text()[1]";
            string arrivalDateXpath = ".//td[@class='fdetails'][2]/span[@class='fldate'][1]/text()[1]";
            string priceXpath = ".//span[@class='flprice'][1]/text()[1]";
            string curencyXpath = ".//span[@class='flcur'][1]/text()[1]";


            foreach (HtmlNode flight in htmlNodes)
            {
                var f = new Flight
                {

                    DepartureGeoCode = flight.SelectSingleNode(departureAirportGeocodeXpath).InnerText.Trim(new Char[] { ' ', '(', ')', '\n' }),
                    ArrivalGeoCode = flight.SelectSingleNode(arrivalAirportGeocodeXpath).InnerText.Trim(new Char[] { ' ', '(', ')' }),
                    DepartureTime = flight.SelectSingleNode(departureDateXpath).InnerText + flight.SelectSingleNode(departureTimeXpath).InnerText,
                    ArrivalTime = flight.SelectSingleNode(arrivalDateXpath).InnerText + flight.SelectSingleNode(arrivalTimeXpath).InnerText,
                    Price = flight.SelectSingleNode(priceXpath).InnerText.Replace(",", string.Empty),
                    Currency = flight.SelectSingleNode(curencyXpath).InnerText,
                    Taxes = taxesPerPerson * numberOfAdults
                };
                FlightsList.Add(f);
            }

            return FlightsList;
        }
        static HtmlDocument getWebHtml(string url)
        {
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }
        private static void Export(List<FareFlight> fareFlights)
        {

            using (var writer = new StreamWriter("../../../.flights.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(fareFlights);
            }
            Console.WriteLine("File created ! ");
        }
    }
}
