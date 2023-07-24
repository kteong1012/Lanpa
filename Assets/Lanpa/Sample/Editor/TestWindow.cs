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
    }
}