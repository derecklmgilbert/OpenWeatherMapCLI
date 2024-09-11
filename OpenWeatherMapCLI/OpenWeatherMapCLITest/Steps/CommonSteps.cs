using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.UnitTestProvider;
using TechTalk.SpecFlow;
using System.Diagnostics;
using NUnit.Framework;
using FluentAssertions;

namespace OpenWeatherMapCLITest.Steps
{
    [Binding]
    internal class CommonSteps
    {
        private string programResponse { get; set; }
        [Given(@"I am a user")]
        public void GivenIAmAUser()
        {
        }
        [When(@"I send zip code (.*)")]
        public void WhenISendZipCode(string zipCode)
        {
            programResponse = RunProcess(new List<string>() { zipCode });
        }
        [When(@"I send city and state (.*)")]
        public void WhenISendCityAndState(string cityState)
        {
            programResponse = RunProcess(new List<string>() { cityState });
        }
        [Then(@"I should get coordinates (.*) and (.*)")]
        public void ThenIShouldGetCoordinates(string latitude, string longitude)
        {
            if (!programResponse.Contains("Latitude"))
            {
                Assert.Fail(programResponse); return;
            }
            var coordinates = ParseCoordinates(programResponse);
            Assert.That(coordinates.Latitude.StartsWith(latitude), $"Expected Latitude {latitude}, but received {coordinates.Latitude}");
            Assert.That(coordinates.Longitude.StartsWith(longitude), $"Expected Longitude {longitude}, but received {coordinates.Longitude}");
        }
        [Then(@"I should get an error message (.*)")]
        public void ThenIShouldGetCoordinates(string error)
        {
            Assert.That(programResponse.Equals(error), $"Expected message {error}, but received {programResponse}");
        }
        private static string RunProcess(List<string> args)
        {
            StringBuilder outputBuilder;
            ProcessStartInfo processStartInfo;
            Process process;

            outputBuilder = new StringBuilder();

            processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Arguments = string.Join(" ", args);
            processStartInfo.FileName = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName + "\\OpenWeatherMapCLI\\bin\\Debug\\net8.0\\OpenWeatherMapCLI.exe";

            process = new Process();
            process.StartInfo = processStartInfo;
            // enable raising events because Process does not raise events by default
            process.EnableRaisingEvents = true;
            // attach the event handler for OutputDataReceived before starting the process
            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate (object sender, DataReceivedEventArgs e)
                {
                    // append the new data to the data already read-in
                    outputBuilder.Append(e.Data);
                }
            );
            // start the process
            // then begin asynchronously reading the output
            // then wait for the process to exit
            // then cancel asynchronously reading the output
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();

            // use the output
            string output = outputBuilder.ToString();
            return output;
        }
        private static Coordinates ParseCoordinates(string responseBody)
        {
            var lonLat = responseBody.Split(",");
            var lon = lonLat[0].Split(":")[1];
            var lat = lonLat[1].Split(":")[1];
            return new Coordinates()
            {
                Latitude = Math.Round(Convert.ToDecimal(lat.Trim()), 2).ToString(),
                Longitude = Math.Round(Convert.ToDecimal(lon.Trim()), 2).ToString()
            };
        }
    }
    public class Coordinates
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}