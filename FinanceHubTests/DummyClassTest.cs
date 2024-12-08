


namespace FinanceHub.Tests
{
    public class DummyClass_testData_returns3 //smoke test for testing framework. Can be deleted after several more tests are developed. 
    {
        [Fact]
        public void Test1()
        {
            int x = FinanceHub.Controllers.DummyClass.testData;
            Assert.Equal(3,x);
        }
    }
}
