using UnityEditor;

namespace Lanpa
{
    public class TestWindow2 : LWindowCreator<ReferenceCollector>
    {
        [MenuItem("Test/OpenTestWindow2")]
        public static void Open()
        {
            var window = GetWindow<TestWindow2>();
            window.Show();
        }

        public void DoSomething()
        {
            var rc = _target;
        }
    }
}