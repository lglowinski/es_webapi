using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpertalSystem.Domain
{
    public class Problem : BaseEntity
    {
        public string ProblemName { get; set; }

        public List<QuestionBasic> Questions { get; set; } = new List<QuestionBasic>();

        [BsonRepresentation(BsonType.String)]
        public IssueType IssueType { get; set; }

        public string Solution { get; set; }
    }
    public class QuestionBasic
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public string QuestionName { get; set; }
    }
}
