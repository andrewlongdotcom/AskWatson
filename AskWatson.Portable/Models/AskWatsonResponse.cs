using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskWatson.Portable.Models.AskWatsonResponse
{
    public class Rootobject
    {
        public Question question { get; set; }
    }

        
    public class Question
    {
        public Qclasslist[] qclasslist { get; set; }
        public Focuslist[] focuslist { get; set; }
        public Latlist[] latlist { get; set; }
        public Evidencelist[] evidencelist { get; set; }
        public Synonymlist[] synonymList { get; set; }
        public string pipelineid { get; set; }
        public bool formattedAnswer { get; set; }
        public string category { get; set; }
        public int items { get; set; }
        public string status { get; set; }
        public string id { get; set; }
        public string questionText { get; set; }
        public Evidencerequest evidenceRequest { get; set; }
        public Answer[] answers { get; set; }
        public object[] errorNotifications { get; set; }
        public string passthru { get; set; }
    }

    public class Evidencerequest
    {
        public int items { get; set; }
        public string profile { get; set; }
    }

    public class Qclasslist
    {
        public string value { get; set; }
    }

    public class Focuslist
    {
        public string value { get; set; }
    }

    public class Latlist
    {
        public string value { get; set; }
    }

    public class Evidencelist
    {
        public string value { get; set; }
        public string text { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string document { get; set; }
        public string copyright { get; set; }
        public string termsOfUse { get; set; }
        public Metadatamap metadataMap { get; set; }
    }

    public class Metadatamap
    {
        public string _abstract { get; set; }
        public string originalfile { get; set; }
        public string title { get; set; }
        public string corpusName { get; set; }
        public string description { get; set; }
        public string deepqaid { get; set; }
        public string fileName { get; set; }
        public string DOCNO { get; set; }
        public string CorpusPlusDocno { get; set; }
    }

    public class Synonymlist
    {
        public string partOfSpeech { get; set; }
        public string value { get; set; }
        public string lemma { get; set; }
        public Synset[] synSet { get; set; }
    }

    public class Synset
    {
        public string name { get; set; }
        public Synonym[] synonym { get; set; }
    }

    public class Synonym
    {
        public bool isChosen { get; set; }
        public string value { get; set; }
        public int weight { get; set; }
    }

    public class Answer
    {
        public int id { get; set; }
        public string text { get; set; }
        public string pipeline { get; set; }
        public float confidence { get; set; }
    }

}
