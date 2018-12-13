using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace StudyRoomScheduler
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// 


        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            string outputText = "";
            var requestType = input.GetRequestType();
            
            if (requestType == typeof(LaunchRequest))
            {
                
               outputText="Welcome to the University of Oklahoma Study Room Scheduler, please tell me what time you would like to book a study room in either Price College of Business, Adams Hall, or the Bizzell Memorial Library today";

                var intent = input.Request as IntentRequest;


                if (requestType == typeof(IntentRequest))
                {
                    outputText += "Request type is Intent";
                    var intentName = input.Request as IntentRequest;

                    if (intent.Intent.Name.Equals("reserveRoomIntent"))
                    {

                        var studyTime = intent.Intent.Slots["time"].Value;
                        var studyLocation = intent.Intent.Slots["location"].Value;

                        if (studyTime == null)
                        {
                            return BodyResponse("I could not hear what time you said, please repeat the time. Make sure you start with the hour, then minute. ", false);
                        }

                        else if (studyLocation == null)
                        {
                            return BodyResponse("I could not hear the location you said, please choose between Price College of Business, Adams Hall, or the Bizzell Memorial Library.", false);
                        }

                        else
                        {
                            //reserve room

                            Dictionary<string, string> studyPlace = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                            outputText = $"Your study room at {studyLocation} has been reserved for {studyTime}. Go get your bread, Yeet!";

                        }


                        return BodyResponse(outputText, false);

                    }

                    else if (intent.Intent.Name.Equals("AMAZON.StopIntent"))
                    {
                        return BodyResponse("You have now exited the University of Oklahoma Study Room Scheduler", false);
                    }

                    else
                    {
                        return BodyResponse("I did not understand your request, please try again", false);

                    }
                                        

                }

                return BodyResponse();

            }

            
            

            else
            {
                return BodyResponse("I did not understand your request, please try saying Alexa I want to schedule a study room.", false);
            }
            
        }

        private SkillResponse BodyResponse()
        {
            throw new NotImplementedException();
        }

        private SkillResponse BodyResponse(string outputSpeech, bool shouldEndSession, string repromptText = "Just say, I want to reserve a study room to get crackin, say exit to exit")
        {
            var response = new ResponseBody
            {
                ShouldEndSession = shouldEndSession,
                OutputSpeech = new PlainTextOutputSpeech { }
            };

            if (repromptText != null)
            {
                response.Reprompt = new Reprompt() { OutputSpeech = new PlainTextOutputSpeech() { Text = repromptText } };
            }

            var skillResponse = new SkillResponse
            {
                Response = response,
                Version = "1.0"
            };
            return skillResponse;
        }

        //public string GetStudyInfo(string studyTime, string studyAMPM, string studyLocation, string description)
        private static void Dictionary()
        {
            Dictionary<string, string> studyPlace = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)

                 //These are objects within the dictionary. This is known as a "collection initializer syntax". 1st value is the key, second is the actual value. 
            {
                                                //These are random values. Left are the key values you need.
                                                { "Price 2070","Basement; Hallways of floors 2-6 (with all hallway doors closed); Restrooms of floors 2-6 (with doors closed)"},
                                                { "Price 2071", "The S1 basement of David L. Boren Hall (CCD5). The S1 basement is located in the student resident entrance in the foyer of David L. Boren Hall (CCD5)"},
                                                { "Price 2072","Basement; Stairwell between basement and first floor; Foyer outside men's and women's restrooms on first floor; Freezers and walk-ins"},
                                                { "Price 2073","Central hallway between the walkin-in coolers and freezers"},

                                                  //This dictionary key-value pair is inputted if we don't recognize the building.
                                                { "unknown" , "I do not recognize the inputted building." }
            };
        }
    }
}


