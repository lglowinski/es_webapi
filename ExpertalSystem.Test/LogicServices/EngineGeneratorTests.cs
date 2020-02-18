using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpertalSystem.Domain;
using ExpertalSystem.LogicServices;
using ExpertalSystem.Repositories;
using Moq;
using NUnit.Framework;

namespace ExpertalSystem.Test.LogicServices
{
    public class EngineGeneratorTests
    {
        private readonly List<Problem> _mockedProblems = new List<Problem>()
        {
            new Problem()
            {
                Id = Guid.NewGuid(),
                ProblemName = "problem1",
                Questions = new List<QuestionBasic>()
                {
                    new QuestionBasic()
                    {
                        QuestionName="q1",
                        Answer="a1",
                        Id = Guid.NewGuid()
                    }
                },
                IssueType = IssueType.HardwareIssue,
                Solution = "s1"
            },
            new Problem()
            {
                Id = Guid.NewGuid(),
                ProblemName = "problem2",
                Questions = new List<QuestionBasic>()
                {
                    new QuestionBasic()
                    {
                        QuestionName="q2",
                        Answer="a2",
                        Id = Guid.NewGuid()
                    },
                    new QuestionBasic()
                    {
                        QuestionName="q3",
                        Answer="a3",
                        Id = Guid.NewGuid()
                    }
                },
                IssueType = IssueType.HardwareIssue,
                Solution = "s2"
            }
        };

        [Test]
        public async Task CreateEngine_WithIssueType_ReturnNewRuleInferenceEngineExtensionWith2Rules()
        {
            var problemsRepository = new Mock<IProblemRepository>();
            problemsRepository.Setup(p => p.FindAsync(It.IsAny<IssueType>())).ReturnsAsync(_mockedProblems);

            var generator = new EngineGenerator(problemsRepository.Object);
            var result = await generator.CreateEngine(IssueType.HardwareIssue);

            Assert.AreEqual(result.GetType(), typeof(RuleInferenceEngineExtension));
            

        }
    }
}
