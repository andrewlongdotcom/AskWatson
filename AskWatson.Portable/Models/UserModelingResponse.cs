using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskWatson.Portable.Models.UserModelingResponse
{
    
    public class Rootobject
    {
        public Tree tree { get; set; }
        public string id { get; set; }
        public string source { get; set; }
        public string word_count_message { get; set; }
        public int word_count { get; set; }
    }

    public class Tree
    {
        public string id { get; set; }
        public string name { get; set; }
        public Child[] children { get; set; }
    }

    public class Child
    {
        public string id { get; set; }
        public string name { get; set; }
        public Child1[] children { get; set; }
    }

    public class Child1
    {
        public string id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public float percentage { get; set; }
        public Child2[] children { get; set; }
    }

    public class Child2
    {
        public string id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public float percentage { get; set; }
        public Child3[] children { get; set; }
        public int size { get; set; }
    }

    public class Child3
    {
        public string id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public float percentage { get; set; }
        public int size { get; set; }
    }

}

