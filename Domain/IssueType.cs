using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ExpertalSystem.Domain
{
    public enum IssueType
    {
        ScreenIssue = 0,
        HardwareIssue = 1,
        IOIssue = 2
    }
}
