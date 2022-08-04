using Charites.Windows.Mvc;

namespace $safeprojectname$;

[View(Key = nameof($safeprojectname$Host))]
public class $safeitemrootname$
{
    private void SetDataContext($safeprojectname$Host? host) => this.host = host;
    private $safeprojectname$Host? host;
}
