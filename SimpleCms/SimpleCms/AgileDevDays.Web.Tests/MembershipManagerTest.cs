using AgileDevDays.Core.Membership;
using NUnit.Framework;


namespace AgileDevDays.Web.Tests
{
    [TestFixture]
    public class MembershipManagerTest
    {
        [Test]
        public void MembershipManager()
        {
            MembershipManager manager = new MembershipManager();
            var result = manager.GetApplicationId();
            Assert.IsNotNull(result);
        }
    }
}
