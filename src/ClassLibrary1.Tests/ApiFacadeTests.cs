using System.Collections.Generic;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace ClassLibrary1.Tests
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>One of these two test methods will fail and produce a warning about stale fakes. I'm lost on how to either a) resolve this error or b) test this in a way that's acceptable for Typemock.</remarks>
    [TestFixture]
    public class ApiFacadeTests
    {
        [Test]
        public void Test01()
        {
            // arrange
            var cm = Isolate.Fake.AllInstances<CacheManager>();
            Isolate.WhenCalled(() => cm.KeyExists("teams_data"))
                .WillReturn(false);

            // act
            var api = new ApiFacade();
            List<Team> result = api.GetTeams();

            // assert

            Assert.AreEqual(3, result.Count);

            int times = Isolate.Verify.GetTimesCalled(() => cm.KeyExists("teams_data"));
            Assert.AreEqual(1, times);

            int timesGetData = Isolate.Verify.GetTimesCalled(() => cm.GetData("teams_data"));
            Assert.AreEqual(0, timesGetData);
        }

        [Test]
        public void Test02()
        {
            // arrange
            var cm = Isolate.Fake.AllInstances<CacheManager>();
            Isolate.WhenCalled(() => cm.KeyExists("teams_data"))
                .WillReturn(true);

            Isolate.WhenCalled(() => cm.GetData("teams_data"))
                .WillReturn(new List<Team> { new Team(999, "tigers") });

            // act
            var api = new ApiFacade();
            List<Team> result = api.GetTeams();

            // assert
            Assert.AreEqual(1, result.Count);

            int times = Isolate.Verify.GetTimesCalled(() => cm.KeyExists("teams_data"));
            Assert.AreEqual(1, times);

            int timesGetData = Isolate.Verify.GetTimesCalled(() => cm.GetData("teams_data"));
            Assert.AreEqual(1, timesGetData);
        }
    }
}
