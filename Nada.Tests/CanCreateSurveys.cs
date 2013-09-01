using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nada.Model.Repositories;

namespace Nada.Tests
{
    [TestClass]
    public class CanCreateSurveys : BaseTest
    {
        [TestMethod]
        public void CanFetchLfMfPrevalence()
        {
            SurveyRepository repo = new SurveyRepository();
            var survey = repo.GetLfMfPrevalenceSurvey(15);
            Assert.IsNotNull(survey);
        }
    }
}
