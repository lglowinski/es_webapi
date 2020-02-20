using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ExpertalSystem.Domain
{
    public class Problem : BaseEntity
    {
        [JsonProperty(propertyName:"problemName")]
        public string ProblemName { get; set; }

        [JsonProperty(propertyName: "questions")]
        public List<QuestionBasic> Questions { get; set; } = new List<QuestionBasic>();

        [JsonProperty(propertyName: "issueType")]
        [BsonRepresentation(BsonType.String)]
        public IssueType IssueType { get; set; }

        [JsonProperty(propertyName: "solution")]
        public string Solution { get; set; }
    }
    public class QuestionBasic : BaseEntity
    {
        [JsonProperty(propertyName: "answer")]
        public string Answer { get; set; }
        [JsonProperty(propertyName: "questionName")]
        public string QuestionName { get; set; }
    }
}
