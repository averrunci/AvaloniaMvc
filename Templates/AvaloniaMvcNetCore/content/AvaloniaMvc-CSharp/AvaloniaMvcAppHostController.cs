using Charites.Windows.Mvc;

namespace AvaloniaMvcApp
{
    [View(Key = nameof(AvaloniaMvcAppHost))]
    public class AvaloniaMvcAppHostController
    {
        private void SetDataContext(AvaloniaMvcAppHost host) => this.host = host;
        private AvaloniaMvcAppHost host;
    }
}
