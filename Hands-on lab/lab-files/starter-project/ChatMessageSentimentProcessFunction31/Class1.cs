using System;
using System.Collections.Generic;
using System.Text;

namespace ChatMessageSentimentProcessFunction31
{
    class Class1
    {


        public class LuisResponse
        {
            public string query { get; set; }
            public Prediction prediction { get; set; }
        }

        public class Prediction
        {
            public string topIntent { get; set; }
            public Intents intents { get; set; }
            public Entities entities { get; set; }
        }

        public class Intents
        {
            public Orderin OrderIn { get; set; }
            public None None { get; set; }
        }

        public class Orderin
        {
            public float score { get; set; }
        }

        public class None
        {
            public float score { get; set; }
        }

        public class Entities
        {
            public string[] RoomService { get; set; }
            public Instance instance { get; set; }
        }

        public class Instance
        {
            public Roomservice[] RoomService { get; set; }
        }

        public class Roomservice
        {
            public string type { get; set; }
            public string text { get; set; }
            public int startIndex { get; set; }
            public int length { get; set; }
            public float score { get; set; }
            public int modelTypeId { get; set; }
            public string modelType { get; set; }
            public string[] recognitionSources { get; set; }
        }

    }
}
