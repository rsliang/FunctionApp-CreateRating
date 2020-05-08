using System;

namespace FunctionApp_HttpExample
{
    public class Rating
    {
        string id;
        string userId = "";
        string productId = "";
        //System.DateTime timestamp;
        string  timestamp;
        string locationName = "";
        //int rating = 0;
        string rating = "";
        string userNotes = "";

        public Rating()
        {
            id = "";
            userId = "";
            productId = "";
            //timestamp = System.DateTime.Today;
            timestamp = "";
            locationName = "";
            //rating = 0;
            rating = "";
            userNotes = "";

        }

        //public Rating(string id, string uid, string pid, System.DateTime ts, string name, int rating, string notes)
        public Rating(string id, string uid, string pid, string ts, string name, string rating, string notes)
        {
            SetId(id);
            SetUserId(uid);
            SetProducId(pid);
            SetTimestamp(ts);
            SetLocationName(name);
            SetRating(rating);
            SetUserNotes(notes);
        }

        public string GetId()
        {
            return this.id;
        }

        public string GetUserId()
        {
            return this.userId;
        }

        public string GetProductId()
        {
            return this.productId;
        }

        //public System.DateTime GetTimestamp(System.DateTime ts)
        public string GetTimestamp()
        {
            return this.timestamp;
        }

        public string GetLocationName()
        {
            return this.locationName;
        }

        public string GetRating()
        {
            return this.rating;
        }

        public string GetUserNotes()
        {
            return this.userNotes;
        }

        public string SetId(string id)
        {
            return this.id = id;
        }

        public string SetUserId(string uid)
        {
            return this.userId = uid;
        }

        public string SetProducId(string pid)
        {
            return this.productId = pid;
        }

        public string SetTimestamp(string ts)
        {
            return this.timestamp = ts;
        }

        public string SetLocationName(string name)
        {
            return this.locationName = name;
        }

        public string SetRating(string rating)
        {
            return this.rating = rating;
        }

        public string SetUserNotes(string notes)
        {
            return this.userNotes = notes;
        }


    }
}
