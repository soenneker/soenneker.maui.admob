using Soenneker.Tests.HostedUnit;

namespace Soenneker.Maui.Admob.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class AdmobUtilTests : HostedUnitTest
{

    public AdmobUtilTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
