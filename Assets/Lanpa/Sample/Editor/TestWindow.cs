using Lanpa;
using UnityEditor;

namespace Lanpa
{
    public class TestWindow : LWindowCreator<TestDesigner>
    {
        [MenuItem("Test/OpenTestWindow")]
        public static void Open()
        {
            var window = GetWindow<TestWindow>();
            window.Show();
        }

        protected override TestDesigner LoadTarget()
        {
            return new TestDesigner();
        }
    }
}